//using ManahostManager.Domain.Entity;
//using ManahostManager.Tests.ControllerTests.Utils;
//using ManahostManager.Tests.Repository;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System.Net;
//using System.Net.Http;

//namespace ManahostManager.Tests.ControllerTests
//{
//    [TestClass]
//    public class HomeConfigControllerTest : ControllerTest<HomeConfig>
//    {
//        private const string path = "/api/Home/Config";

//        [TestMethod]
//        public void PostPutDelete()
//        {
//            reqCreator.CreateRequest(st, path + "/1", HttpMethod.Delete, null, HttpStatusCode.OK, false, true);
//            HomeConfig tmp = new HomeConfigRepositoryTest().GetHomeConfigById(1, 0);
//            tmp.Id = 1;
//            // TODO Uncommence when MsgPack works with RequestCreator or remove msgPack test for RequestCreator only test with Json
//            //reqCreator.CreateRequest(st, path, HttpMethod.Post, tmp, HttpStatusCode.OK, true, false, true);
//            //reqCreator.CreateRequest(st, path + "/" + reqCreator.deserializedEntity.Id.ToString(), HttpMethod.Delete, null, HttpStatusCode.OK, false, true);
//            reqCreator.CreateRequest(st, path, HttpMethod.Post, tmp, HttpStatusCode.OK, true, false, true);
//            reqCreator.CreateRequest(st, path, HttpMethod.Put, reqCreator.deserializedEntity, HttpStatusCode.OK, false, false);
//            reqCreator.CreateRequest(st, path + "/" + reqCreator.deserializedEntity.Id.ToString(), HttpMethod.Delete, null, HttpStatusCode.OK, false, true);
//        }
//    }
//}