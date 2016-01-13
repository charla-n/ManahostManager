using ManahostManager.App_Start;
using ManahostManager.Domain.DAL;
using ManahostManager.Model.DTO.Account;
using ManahostManager.Model.Factory;
using ManahostManager.Utils;
using Microsoft.Owin.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace ManahostManager.Tests.ControllerTests
{
    [TestClass]
    public class AccountControllerTest
    {
        [TestMethod]
        public void CreateAccount()
        {
            Database.SetInitializer(new ManahostManagerInitializer());
            using (ManahostManagerDAL prectx = new ManahostManagerDAL())
            {
                prectx.Database.Delete();
            }
            var AccountModel = new CreateAccountModel()
            {
                Civility = "Mr",
                Country = "France",
                Email = "Fabrice.Didierjean@gmail.com",
                FirstName = "Fabrice",
                LastName = "Didierjean",
                Password = "TOTOTITi88$$",
                PasswordConfirmation = "TOTOTITi88$$"
            };
            HttpResponseMessage result;
            using (var server = TestServer.Create<WebApiApplication>())
            {
                result = server.CreateRequest("/api/Account").And(x =>
                {
                    x.Content = new ObjectContent(typeof(CreateAccountModel), AccountModel, new JilFormatter());
                    x.Content.Headers.ContentType = new MediaTypeHeaderValue(GenericNames.APP_JSON);
                }).PostAsync().Result;
                string msg = result.Content.ReadAsStringAsync().Result;
                Assert.AreEqual(HttpStatusCode.OK, result.StatusCode, "Status Create Account" + msg);
            }
        }

        [TestMethod]
        public void ChangePassword()
        {
            Database.SetInitializer(new ManahostManagerInitializer());
            using (ManahostManagerDAL prectx = new ManahostManagerDAL())
            {
                prectx.Database.Delete();
            }
            var passwordModel = new ChangePasswordAccountModel()
            {
                CurrentPassword = ControllerUtils.password,
                Password = "JeSuisUnNooB88$$",
                PasswordConfirmation = "JeSuisUnNooB88$$"
            };
            using (var server = TestServer.Create<WebApiApplication>())
            {
                HttpResponseMessage response = server.CreateRequest("/token").And((x) => x.Content = new StringContent(string.Format("grant_type=password&username={0}&password={1}&client_id=UNITTEST&client_secret=BLAHBLAHCAR", ControllerUtils.username, ControllerUtils.password), Encoding.UTF8, "application/x-www-form-urlencoded")).PostAsync().Result;
                TokenAuth token = response.Content.ReadAsAsync<TokenAuth>().Result;
                Assert.AreEqual(response.StatusCode, HttpStatusCode.OK, "Status Authentication");

                var result = server.CreateRequest("/api/Account/Password").And(x =>
               {
                   x.Content = new ObjectContent(typeof(ChangePasswordAccountModel), passwordModel, new JilFormatter());
                   x.Content.Headers.ContentType = new MediaTypeHeaderValue(GenericNames.APP_JSON);
               }).AddHeader("Authorization", new AuthenticationHeaderValue("Bearer", token.access_token).ToString()).PostAsync().Result;
                Assert.AreEqual(HttpStatusCode.OK, result.StatusCode, "Status Change Password");

                response = server.CreateRequest("/token").And((x) => x.Content = new StringContent(string.Format("grant_type=password&username={0}&password={1}&client_id=UNITTEST&client_secret=BLAHBLAHCAR", ControllerUtils.username, "JeSuisUnNooB88$$"), Encoding.UTF8, "application/x-www-form-urlencoded")).PostAsync().Result;
                Assert.AreEqual(response.StatusCode, HttpStatusCode.OK, "Status Authentication with new password");
                token = response.Content.ReadAsAsync<TokenAuth>().Result;
            }
        }

        [TestMethod]
        public void AddPrincipalPhone()
        {
            Database.SetInitializer(new ManahostManagerInitializer());
            using (ManahostManagerDAL prectx = new ManahostManagerDAL())
            {
                prectx.Database.Delete();
            }
            var phoneModel = new PhoneModel()
            {
                IsPrimary = true,
                Phone = "0874543215",
                Type = Domain.Entity.PhoneType.MOBILE
            };
            using (var server = TestServer.Create<WebApiApplication>())
            {
                HttpResponseMessage response = server.CreateRequest("/token").And((x) => x.Content = new StringContent(string.Format("grant_type=password&username={0}&password={1}&client_id=UNITTEST&client_secret=BLAHBLAHCAR", ControllerUtils.username, ControllerUtils.password), Encoding.UTF8, "application/x-www-form-urlencoded")).PostAsync().Result;
                TokenAuth token = response.Content.ReadAsAsync<TokenAuth>().Result;
                Assert.AreEqual(response.StatusCode, HttpStatusCode.OK, "Status Authentication");

                var result = server.CreateRequest("/api/Account").AddHeader("Authorization", new AuthenticationHeaderValue("Bearer", token.access_token).ToString()).GetAsync().Result;
                Assert.AreEqual(HttpStatusCode.OK, result.StatusCode, "Status Get Account For delete");
                var msg = result.Content.ReadAsAsync<ExposeAccountModel>().Result;

                result = server.CreateRequest(string.Format("/api/Account/Phone/{0}", msg.PrincipalPhone.Id)).AddHeader("Authorization", new AuthenticationHeaderValue("Bearer", token.access_token).ToString()).SendAsync("DELETE").Result;
                Assert.AreEqual(HttpStatusCode.OK, result.StatusCode, "Status DELETE PHONE NUMBER");

                result = server.CreateRequest("/api/Account/Phone").And(x =>
               {
                   x.Content = new ObjectContent(typeof(PhoneModel), phoneModel, new JilFormatter());
                   x.Content.Headers.ContentType = new MediaTypeHeaderValue(GenericNames.APP_JSON);
               }).AddHeader("Authorization", new AuthenticationHeaderValue("Bearer", token.access_token).ToString()).PostAsync().Result;
                Assert.AreEqual(HttpStatusCode.OK, result.StatusCode, "Status Post New Principal Phone");

                result = server.CreateRequest("/api/Account").AddHeader("Authorization", new AuthenticationHeaderValue("Bearer", token.access_token).ToString()).GetAsync().Result;
                Assert.AreEqual(HttpStatusCode.OK, result.StatusCode, "Status Get Account CHECK");
                msg = result.Content.ReadAsAsync<ExposeAccountModel>().Result;
                Assert.AreEqual(phoneModel.Phone, msg.PrincipalPhone.Phone, "Check Principal Phone is Same PhoneNumber");
                Assert.AreEqual(phoneModel.Type, msg.PrincipalPhone.PhoneType, "Check Principal Phone is Same Type");

                var phonePutModel = Factory.Create(msg.PrincipalPhone);
                phonePutModel.IsPrimary = true;
                phonePutModel.Phone = "4242424242";
                result = server.CreateRequest(String.Format("/Api/Account/Phone/{0}", msg.PrincipalPhone.Id)).And(x =>
                {
                    x.Content = new ObjectContent(typeof(PhoneModel), phonePutModel, new JilFormatter());
                    x.Content.Headers.ContentType = new MediaTypeHeaderValue(GenericNames.APP_JSON);
                }).AddHeader("Authorization", new AuthenticationHeaderValue("Bearer", token.access_token).ToString()).SendAsync("PUT").Result;
                Assert.AreEqual(HttpStatusCode.OK, result.StatusCode, "Status Put New Principal Phone");

                result = server.CreateRequest("/api/Account").AddHeader("Authorization", new AuthenticationHeaderValue("Bearer", token.access_token).ToString()).GetAsync().Result;
                Assert.AreEqual(HttpStatusCode.OK, result.StatusCode, "Status Get Account for Verif Principal Phone");
                msg = result.Content.ReadAsAsync<ExposeAccountModel>().Result;
                Assert.AreEqual("4242424242", msg.PrincipalPhone.Phone, "Check Principal Phone is same Phone");
            }
        }

        [TestMethod]
        public void AddSecondaryPhone()
        {
            Database.SetInitializer(new ManahostManagerInitializer());
            using (ManahostManagerDAL prectx = new ManahostManagerDAL())
            {
                prectx.Database.Delete();
            }
            var phoneModel = new PhoneModel()
            {
                IsPrimary = false,
                Phone = "0874543215",
                Type = Domain.Entity.PhoneType.WORK
            };
            using (var server = TestServer.Create<WebApiApplication>())
            {
                HttpResponseMessage response = server.CreateRequest("/token").And((x) => x.Content = new StringContent(string.Format("grant_type=password&username={0}&password={1}&client_id=UNITTEST&client_secret=BLAHBLAHCAR", ControllerUtils.username, ControllerUtils.password), Encoding.UTF8, "application/x-www-form-urlencoded")).PostAsync().Result;
                TokenAuth token = response.Content.ReadAsAsync<TokenAuth>().Result;
                Assert.AreEqual(response.StatusCode, HttpStatusCode.OK, "Status Authentification");

                var result = server.CreateRequest("/api/Account/Phone").And(x =>
                {
                    x.Content = new ObjectContent(typeof(PhoneModel), phoneModel, new JilFormatter());
                    x.Content.Headers.ContentType = new MediaTypeHeaderValue(GenericNames.APP_JSON);
                }).AddHeader("Authorization", new AuthenticationHeaderValue("Bearer", token.access_token).ToString()).PostAsync().Result;
                Assert.AreEqual(HttpStatusCode.OK, result.StatusCode, "Add Secondary Phone");

                result = server.CreateRequest("/api/Account").AddHeader("Authorization", new AuthenticationHeaderValue("Bearer", token.access_token).ToString()).GetAsync().Result;
                Assert.AreEqual(HttpStatusCode.OK, result.StatusCode, "Status Get Account");
                var msg = result.Content.ReadAsAsync<ExposeAccountModel>().Result;
                var SearchPhone = from phone in msg.SecondaryPhone where phone.Phone == phoneModel.Phone select phone;
                Assert.AreEqual(1, SearchPhone.Count(), "Count Secondary Phone");

                var phoneModelPut = Factory.Create(SearchPhone.FirstOrDefault());
                phoneModelPut.IsPrimary = false;
                phoneModelPut.Phone = "4545454545";
                result = server.CreateRequest(String.Format("/Api/Account/Phone/{0}", SearchPhone.FirstOrDefault().Id)).And(x =>
                {
                    x.Content = new ObjectContent(typeof(PhoneModel), phoneModelPut, new JilFormatter());
                    x.Content.Headers.ContentType = new MediaTypeHeaderValue(GenericNames.APP_JSON);
                }).AddHeader("Authorization", new AuthenticationHeaderValue("Bearer", token.access_token).ToString()).SendAsync("PUT").Result;
                Assert.AreEqual(HttpStatusCode.OK, result.StatusCode, "Status Put Secondary Phone");

                result = server.CreateRequest("/api/Account").AddHeader("Authorization", new AuthenticationHeaderValue("Bearer", token.access_token).ToString()).GetAsync().Result;
                Assert.AreEqual(HttpStatusCode.OK, result.StatusCode, "Status Get Account");
                msg = result.Content.ReadAsAsync<ExposeAccountModel>().Result;
                SearchPhone = from phone in msg.SecondaryPhone where phone.Phone == "4545454545" select phone;
                Assert.AreEqual(1, SearchPhone.Count(), "Count Secondary Phone Check");

                result = server.CreateRequest(string.Format("/api/Account/Phone/{0}", SearchPhone.FirstOrDefault().Id)).AddHeader("Authorization", new AuthenticationHeaderValue("Bearer", token.access_token).ToString()).SendAsync("DELETE").Result;
                Assert.AreEqual(HttpStatusCode.OK, result.StatusCode, "Status DELETE PHONE NUMBER");

                result = server.CreateRequest("/api/Account").AddHeader("Authorization", new AuthenticationHeaderValue("Bearer", token.access_token).ToString()).GetAsync().Result;
                Assert.AreEqual(HttpStatusCode.OK, result.StatusCode, "Status Get Account");
                msg = result.Content.ReadAsAsync<ExposeAccountModel>().Result;
                Assert.AreEqual(0, msg.SecondaryPhone.Count());
            }
        }

        [TestMethod]
        public void PutAccount()
        {
            Database.SetInitializer(new ManahostManagerInitializer());
            using (ManahostManagerDAL prectx = new ManahostManagerDAL())
            {
                prectx.Database.Delete();
            }
            var PutClientModel = new PutAccountModel()
            {
                Civility = "Mlle",
                Country = "UnderCity",
                FirstName = "Sylvanas",
                LastName = "Coursevent"
            };
            using (var server = TestServer.Create<WebApiApplication>())
            {
                HttpResponseMessage response = server.CreateRequest("/token").And((x) => x.Content = new StringContent(string.Format("grant_type=password&username={0}&password={1}&client_id=UNITTEST&client_secret=BLAHBLAHCAR", ControllerUtils.username, ControllerUtils.password), Encoding.UTF8, "application/x-www-form-urlencoded")).PostAsync().Result;
                TokenAuth token = response.Content.ReadAsAsync<TokenAuth>().Result;
                Assert.AreEqual(response.StatusCode, HttpStatusCode.OK, "Status Authentification");

                response = server.CreateRequest("/Api/Account").And(x =>
                {
                    x.Content = new ObjectContent(typeof(PutAccountModel), PutClientModel, new JilFormatter());
                    x.Content.Headers.ContentType = new MediaTypeHeaderValue(GenericNames.APP_JSON);
                }).AddHeader("Authorization", new AuthenticationHeaderValue("Bearer", token.access_token).ToString()).SendAsync("PUT").Result;
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "Status Code PUT Account");

                response = server.CreateRequest("/api/Account").AddHeader("Authorization", new AuthenticationHeaderValue("Bearer", token.access_token).ToString()).GetAsync().Result;
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                var ExposeModel = response.Content.ReadAsAsync<ExposeAccountModel>().Result;
                Assert.AreEqual(PutClientModel.Civility, ExposeModel.Civility, "Check Civility");
                Assert.AreEqual(PutClientModel.Country, ExposeModel.Country, "Check Country");
                Assert.AreEqual(PutClientModel.FirstName, ExposeModel.FirstName, "Check FirstName");
                Assert.AreEqual(PutClientModel.LastName, ExposeModel.LastName, "Check LastName");
            }
        }

        [TestMethod]
        public void TestRefreshToken()
        {
            Database.SetInitializer(new ManahostManagerInitializer());
            using (ManahostManagerDAL prectx = new ManahostManagerDAL())
            {
                prectx.Database.Delete();
            }
            using (var server = TestServer.Create<WebApiApplication>())
            {
                HttpResponseMessage response = server.CreateRequest("/token").And((x) => x.Content = new StringContent(string.Format("grant_type=password&username={0}&password={1}&client_id=UNITTEST&client_secret=BLAHBLAHCAR", ControllerUtils.username, ControllerUtils.password), Encoding.UTF8, "application/x-www-form-urlencoded")).PostAsync().Result;
                TokenAuth token = response.Content.ReadAsAsync<TokenAuth>().Result;
                Assert.AreEqual(response.StatusCode, HttpStatusCode.OK, "Status Authentification");

                response = server.CreateRequest("/token").And((x) => x.Content = new StringContent(string.Format("grant_type=refresh_token&client_id=UNITTEST&client_secret=BLAHBLAHCAR&refresh_token={0}", token.refresh_token), Encoding.UTF8, "application/x-www-form-urlencoded")).PostAsync().Result;
                Assert.AreEqual(response.StatusCode, HttpStatusCode.OK, "Status RefreshToken");
                token = response.Content.ReadAsAsync<TokenAuth>().Result;

                response = server.CreateRequest("/api/Account").AddHeader("Authorization", new AuthenticationHeaderValue("Bearer", token.access_token).ToString()).GetAsync().Result;
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                var ExposeModel = response.Content.ReadAsAsync<ExposeAccountModel>().Result;
            }
        }
    }
}