//using ManahostManager.Domain.Entity;
//using ManahostManager.Tests.ControllerTests.Utils;
//using ManahostManager.Tests.Repository;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System.Net;
//using System.Net.Http;

//namespace ManahostManager.Tests.ControllerTests
//{
//    [TestClass]
//    public class MailConfigControllerTest : ControllerTest<MailConfig>
//    {
//        private const string path = "/api/Mail/Config";

//        [TestMethod]
//        public void PostPutDelete()
//        {
//            reqCreator.CreateRequest(st, path, HttpMethod.Post, new MailConfigRepositoryTest().GetMailConfigById(10, 0), HttpStatusCode.OK, true, false);
//            reqCreator.CreateRequest(st, path, HttpMethod.Put, reqCreator.deserializedEntity, HttpStatusCode.OK, false, false);
//            reqCreator.CreateRequest(st, path + "/" + reqCreator.deserializedEntity.Id.ToString(), HttpMethod.Delete, null, HttpStatusCode.OK, false, true);
//        }
//    }
//}