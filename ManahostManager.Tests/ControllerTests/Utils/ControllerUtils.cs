using ManahostManager.App_Start;
using Microsoft.Owin.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;
using System.Text;

namespace ManahostManager.Tests.ControllerTests
{
    public struct ServToken
    {
        public string token;
        public TestServer server;
    }

    public class TokenAuth
    {
        public string access_token { get; set; }
        public string refresh_token { get; set; }
    }

    public class ControllerUtils
    {
        public const string username = "contact%40manahost.fr";
        public const string password = "TOTOTITi88$$";

        public static ServToken CreateAndAuthenticate()
        {
            var server = TestServer.Create<WebApiApplication>();

            HttpResponseMessage response = server.CreateRequest("/token").And((x) => x.Content = new StringContent(string.Format("grant_type=password&username={0}&password={1}&client_id=UNITTEST&client_secret=BLAHBLAHCAR", username, password), Encoding.UTF8, "application/x-www-form-urlencoded")).PostAsync().Result;

            TokenAuth token = response.Content.ReadAsAsync<TokenAuth>().Result;

            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);

            return new ServToken()
            {
                server = server,
                token = token.access_token
            };
        }
    }
}