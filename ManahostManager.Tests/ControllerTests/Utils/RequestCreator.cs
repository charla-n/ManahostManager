using Jil;
using ManahostManager.Controllers;
using ManahostManager.Utils;
using ManahostManager.Utils.Formatters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MsgPack.Serialization;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;

namespace ManahostManager.Tests.ControllerTests
{
    public class RequestCreator<T> where T : class
    {
        public T deserializedEntity;
        private T entity;
        private HttpMethod method;
        private readonly Options _jilOptions;
        private int current;
        public HttpResponseMessage[] resp;

        public RequestCreator()
        {
            _jilOptions = new Options(unspecifiedDateTimeKindBehavior: UnspecifiedDateTimeKindBehavior.IsUTC, dateFormat: DateTimeFormat.ISO8601);
            current = 0;
        }

        private static readonly KeyValuePair<String, MediaTypeFormatter> JSON_MEDIATYPEFORMATTER = new KeyValuePair<string, MediaTypeFormatter>(GenericNames.APP_JSON, new JilFormatter());
        private static readonly KeyValuePair<String, MediaTypeFormatter> MSGPACK_MEDIATYPEFORMATTER = new KeyValuePair<string, MediaTypeFormatter>(GenericNames.APP_MSGPACK, new MsgPackFormatter());

        public static readonly Dictionary<KeyValuePair<String, MediaTypeFormatter>, String> CONTENT_TYPE_ACCEPT = new Dictionary<KeyValuePair<String, MediaTypeFormatter>, string>()
        {
            // ContentType - Accept
            {JSON_MEDIATYPEFORMATTER, GenericNames.APP_JSON},
            //TODO fix msgpack when badrequest
            //{MSGPACK_MEDIATYPEFORMATTER, GenericNames.APP_MSGPACK}
        };

        public U Deserialize<U>(String contentType, HttpResponseMessage resp) where U : class
        {
            switch (contentType)
            {
                case GenericNames.APP_JSON:
                    using (var reader = new StreamReader(resp.Content.ReadAsStreamAsync().Result))
                    {
                        return JSON.Deserialize<U>(reader, _jilOptions);
                    }
                case GenericNames.APP_MSGPACK:
                    var serializer = MessagePackSerializer.Get<U>(MsgPackFormatter.sContext);
                    return serializer.Unpack(resp.Content.ReadAsStreamAsync().Result);
            };
            return null;
        }

        public void RequestAction(HttpRequestMessage msg)
        {
            msg.Content = new ObjectContent(typeof(T), entity, CONTENT_TYPE_ACCEPT.ElementAt(current).Key.Value);
            msg.Method = method;
            msg.Content.Headers.ContentType = new MediaTypeHeaderValue(CONTENT_TYPE_ACCEPT.ElementAt(current).Key.Key);
            msg.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(CONTENT_TYPE_ACCEPT.ElementAt(current).Value));
        }

        public void CreateRequest(ServToken st, string path, HttpMethod method, T entity, HttpStatusCode expectedStatusCode, bool deserialize = true, bool ignoreAction = false, bool breakExecution = false)
        {
            this.entity = entity;
            this.method = method;
            resp = new HttpResponseMessage[CONTENT_TYPE_ACCEPT.Count];
            if (method.Method != HttpMethod.Post.Method &&
                method.Method != HttpMethod.Put.Method &&
                GenericNames.PATCH_VERB.Method != method.Method)
            {
                SendRequest(ignoreAction, path, st, expectedStatusCode, deserialize);
                return;
            }
            for (; current < CONTENT_TYPE_ACCEPT.Count; current++)
            {
                SendRequest(ignoreAction, path, st, expectedStatusCode, deserialize);
                if (ignoreAction)
                    break;
                if (breakExecution)
                {
                    if (current >= CONTENT_TYPE_ACCEPT.Count)
                        current = 0;
                    current++;
                    return;
                }
            }
            current = 0;
        }

        private void SendRequest(bool ignoreAction, string path, ServToken st, HttpStatusCode expectedStatusCode, bool deserialize, bool throwOnFail = true)
        {
            if (ignoreAction == true)
            {
                resp[current] = st.server.CreateRequest(path).
                    AddHeader("Authorization", new AuthenticationHeaderValue("Bearer", st.token).ToString()).
                    SendAsync(method.Method).Result;
            }
            else
            {
                resp[current] = st.server.CreateRequest(path).
                    And(RequestAction).
                    AddHeader("Authorization", new AuthenticationHeaderValue("Bearer", st.token).ToString()).
                    SendAsync(method.Method).Result;
            }
            if (resp[current].StatusCode != expectedStatusCode && throwOnFail == true)
                throw new Exception(resp[current].Content.ReadAsStringAsync().Result);
            if (deserialize)
                deserializedEntity = Deserialize<T>(CONTENT_TYPE_ACCEPT.ElementAt(current).Value, resp[current]);
        }

        public dynamic CallSearch(ServToken st, string searchString, List<string> includes, Type type)
        {
            RequestCreator<AdvSearch> search = new RequestCreator<AdvSearch>();
            search.CreateRequest(st, SearchControllerTest.path, HttpMethod.Post, new AdvSearch()
            {
                Search = searchString,
                Include = includes
            }, HttpStatusCode.OK, false);
            return search.resp[0].Content.ReadAsAsync(type).Result;
        }

        public void AssertHttpError(int count, IEnumerable<KeyValuePair<string, List<string>>> expectedErrors)
        {
            // of type Newtonsoft.Json.Linq.JObject
            // {{ "BedDTO": ["5"], "Bed.Room.Capacity": ["5"] }}

            List<KeyValuePair<string, List<string>>> props = new List<KeyValuePair<string, List<string>>>();
            foreach (HttpResponseMessage cur in resp)
            {
                HttpError error = cur.Content.ReadAsAsync<HttpError>().Result;
                Assert.AreEqual(error.Count, count + 1);
                IEnumerable<JProperty> properties = ((JObject)error["ModelState"]).Properties();
                foreach (JProperty prop in properties)
                {
                    List<string> errs = prop.Value.ToObject<List<string>>();
                    props.Add(new KeyValuePair<string, List<string>>(prop.Name, errs));
                }
                foreach (KeyValuePair<string, List<string>> keyvaluepair in expectedErrors)
                {
                    if (props.Where(d => d.Key == keyvaluepair.Key && d.Value.SequenceEqual(keyvaluepair.Value)).Count() == 0)
                        Assert.Fail(String.Format("Does not contain keyvaluepair {0}-{1}", keyvaluepair.Key, String.Join(",", keyvaluepair.Value)));
                }
            }
        }
    }
}