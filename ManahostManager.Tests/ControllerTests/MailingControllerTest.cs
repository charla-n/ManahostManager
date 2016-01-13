//using ManahostManager.Model;
//using ManahostManager.Tests.ControllerTests.Utils;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System.Net;
//using System.Net.Http;

//namespace ManahostManager.Tests.ControllerTests
//{
//    [TestClass]
//    public class MailingControllerTest : ControllerTest<MailModel>
//    {
//        private const string path = "/api/Mail";

//        [TestMethod]
//        public void Post()
//        {
//            MailModel entity = new MailModel()
//            {
//                Body = "Body",
//                To = new System.Collections.Generic.List<int>() { 1 },
//                Subject = "ControllerTests",
//                Password = "MUFhYWFhYWE=",
//                MailConfigId = 1,
//                Attachments = new System.Collections.Generic.List<int>() { 1, 2 }
//            };

//            reqCreator.CreateRequest(st, path, HttpMethod.Post, entity, HttpStatusCode.OK, false);
//        }
//    }
//}