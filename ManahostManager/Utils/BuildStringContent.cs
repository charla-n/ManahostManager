using System.Net;
using System.Net.Http;

namespace ManahostManager.Utils
{
    public class BuildStringContent
    {
        public static HttpResponseMessage BuildFromRequestOK<T>(HttpRequestMessage Request, T obj) where T : class
        {
            return Request.CreateResponse<T>(System.Net.HttpStatusCode.OK, obj);
        }

        public static HttpResponseMessage BuildFromRequestOK(HttpRequestMessage Request)
        {
            return Request.CreateResponse(System.Net.HttpStatusCode.OK);
        }

        public static HttpResponseMessage BuildFromRequestWithStatusCode<T>(HttpStatusCode code, HttpRequestMessage Request, T obj) where T : class
        {
            return Request.CreateResponse(code, obj);
        }
    }
}