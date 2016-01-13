//using ManahostManager.App_Start;
//using ManahostManager.Domain.Entity;
//using ManahostManager.Tests.ControllerTests.Utils;
//using Microsoft.Owin.Testing;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Net.Http.Headers;
//using System.Text;
//using System.Threading.Tasks;

//namespace ManahostManager.Tests.ControllerTests
//{
//    [TestClass]
//    public class GetHomesControllerTest : ControllerTest<Home>
//    {
//        private const string path = "/api/Home";
//        [TestMethod]
//        public void GetAllHome()
//        {
//            reqCreator.CreateRequest(st, path, HttpMethod.Get, null, HttpStatusCode.OK, false);
//            /*using (var server = TestServer.Create<WebApiApplication>())
//            {
//                HttpResponseMessage response;
//                AuthenticationHeaderValue auth = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes(String.Format("{0}:{1}", ControllerUtils.username, ControllerUtils.password))));
//                response = await server.CreateRequest("/api/Authentication").AddHeader("Authorization", auth.ToString()).PostAsync();

//                Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
//                List<string> result = response.Headers.GetValues("Token").ToList();
//                AuthenticationHeaderValue headerValueAuthentication = new AuthenticationHeaderValue("Token", Convert.ToBase64String(Encoding.UTF8.GetBytes(String.Format("{0}:{1}", ControllerUtils.username, result[0]))));
//                response = await server.CreateRequest("/api/Home").AddHeader("Authorization", headerValueAuthentication.ToString()).GetAsync();
//                Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
//            }*/
//        }

//        [TestMethod]
//        public void GetOneHome()
//        {
//            /*using (var server = TestServer.Create<WebApiApplication>())
//            {
//                HttpResponseMessage response;
//                AuthenticationHeaderValue auth = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes(String.Format("{0}:{1}", ControllerUtils.username, ControllerUtils.password))));
//                response = await server.CreateRequest("/api/Authentication").AddHeader("Authorization", auth.ToString()).PostAsync();

//                Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
//                List<string> result = response.Headers.GetValues("Token").ToList();
//                AuthenticationHeaderValue headerValueAuthentication = new AuthenticationHeaderValue("Token", Convert.ToBase64String(Encoding.UTF8.GetBytes(String.Format("{0}:{1}", ControllerUtils.username, result[0]))));
//                response = await server.CreateRequest("/api/Home/1").AddHeader("Authorization", headerValueAuthentication.ToString()).GetAsync();
//                Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
//                Home home = (Home)await response.Content.ReadAsAsync(typeof(Home));
//                Assert.AreEqual(home.Title, "La Corderie");
//            }*/
//            reqCreator.CreateRequest(st, path + "/1", HttpMethod.Get, null, HttpStatusCode.OK);
//        }
//    }
//}