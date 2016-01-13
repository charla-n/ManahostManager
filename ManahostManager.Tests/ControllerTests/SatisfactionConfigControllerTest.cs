//using ManahostManager.Domain.Entity;
//using ManahostManager.Tests.ControllerTests.Utils;
//using ManahostManager.Tests.Repository;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System.Net;
//using System.Net.Http;

//namespace ManahostManager.Tests.ControllerTests
//{
//    [TestClass]
//    public class SatisfactionConfigControllerTest : ControllerTest<SatisfactionConfig>
//    {
//        private const string path = "/api/Satisfaction/Config";

//        [TestMethod]
//        public void PostPutDelete()
//        {
//            reqCreator.CreateRequest(st, path + "/1", HttpMethod.Delete, null, HttpStatusCode.OK, false, true);
//            // TODO uncomment when msgpack works with the RequestCreator or remove msgPack test for RequestCreator only test with Json
//            //reqCreator.CreateRequest(st, path, HttpMethod.Post, new SatisfactionConfigRepositoryTest().GetSatisfactionConfigById(1, 1), HttpStatusCode.OK, true, false, true);
//            //reqCreator.CreateRequest(st, path + "/" + reqCreator.deserializedEntity.Id.ToString(), HttpMethod.Delete, null, HttpStatusCode.OK, false, true);
//            reqCreator.CreateRequest(st, path, HttpMethod.Post, new SatisfactionConfigRepositoryTest().GetSatisfactionConfigById(1, 1), HttpStatusCode.OK, true, false, false);
//            reqCreator.CreateRequest(st, path, HttpMethod.Put, reqCreator.deserializedEntity, HttpStatusCode.OK, false, false);
//            reqCreator.CreateRequest(st, path + "/" + reqCreator.deserializedEntity.Id.ToString(), HttpMethod.Delete, null, HttpStatusCode.OK, false, true);
//            reqCreator.CreateRequest(st, path, HttpMethod.Post, new SatisfactionConfigRepositoryTest().GetSatisfactionConfigById(1, 1), HttpStatusCode.OK, true, false, true);
//        }
//    }
//}