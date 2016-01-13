using ManahostManager.Domain.DAL;
using ManahostManager.Tests.ControllerTests.Utils;
using Microsoft.Owin.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace ManahostManager.Tests.ControllerTests
{
    public class ThrottlingTest
    {
        public void ThrottlingNotAuthenticate()
        {
            Database.SetInitializer(new ManahostManagerInitializer());
            using (ManahostManagerDAL prectx = new ManahostManagerDAL())
            {
                prectx.Database.Delete();
            }
            using (var server = TestServer.Create<WebApiApplicationThrottle>())
            {
                for (int i = 0; i < 600; i++)
                {
                    HttpResponseMessage _response = server.CreateRequest("/api/Home").PostAsync().Result;
                    Assert.AreEqual(_response.StatusCode, HttpStatusCode.Unauthorized);
                }
                HttpResponseMessage response = server.CreateRequest("/api/Home").PostAsync().Result;
                Assert.AreEqual((int)response.StatusCode, 429);
            }
        }

        public void TrottlingAuthenticate()
        {
            Database.SetInitializer(new ManahostManagerInitializer());
            using (ManahostManagerDAL prectx = new ManahostManagerDAL())
            {
                prectx.Database.Delete();
            }
            using (var server = TestServer.Create<WebApiApplicationThrottle>())
            {
                HttpResponseMessage response = null;
                response = server.CreateRequest("/token").And((x) => x.Content = new StringContent(string.Format("grant_type=password&username={0}&password={1}&client_id=UNITTEST&client_secret=BLAHBLAHCAR", ControllerUtils.username, ControllerUtils.password), Encoding.UTF8, "application/x-www-form-urlencoded")).PostAsync().Result;
                Assert.AreEqual(response.StatusCode, HttpStatusCode.OK, "STATUS AUTHENTIFICATIOn");

                TokenAuth token = response.Content.ReadAsAsync<TokenAuth>().Result;
                AuthenticationHeaderValue headerValueAuthentication = new AuthenticationHeaderValue("Bearer", token.access_token);

                for (int i = 0; i < 4000; i++)
                {
                    response = server.CreateRequest("/api/Account").AddHeader("Authorization", headerValueAuthentication.ToString()).GetAsync().Result;
                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK, string.Format("STATUS Account GET I : {0}", i));
                }
                response = server.CreateRequest("/api/Account").AddHeader("Authorization", headerValueAuthentication.ToString()).GetAsync().Result;
                Assert.AreEqual((int)response.StatusCode, 429, "STATUS 429");
            }
        }
    }
}