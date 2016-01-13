//using ManahostManager.Domain.Entity;
//using ManahostManager.Tests.ControllerTests.Utils;
//using ManahostManager.Tests.Repository;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System.Net;
//using System.Net.Http;

//namespace ManahostManager.Tests.ControllerTests
//{
//    [TestClass]
//    public class PricePerPersonControllerTest : ControllerTest<PricePerPerson>
//    {
//        private const string path = "/api/PricePerPerson";

//        [TestMethod]
//        public void PostPutDelete()
//        {
//            // TODO uncomment when MsgPack works with RequestCreator or remove msgPack test for RequestCreator only test with Json
//            //reqCreator.CreateRequest(st, path, HttpMethod.Post, new PricePerPersonRepositoryTest().GetPricePerPersonById(2, 1), HttpStatusCode.OK, true, false, true);
//            //reqCreator.CreateRequest(st, path + "/" + reqCreator.deserializedEntity.Id.ToString(), HttpMethod.Delete, null, HttpStatusCode.OK, false, true);
//            reqCreator.CreateRequest(st, path, HttpMethod.Post, new PricePerPersonRepositoryTest().GetPricePerPersonById(2, 1), HttpStatusCode.OK, true, false, false);
//            reqCreator.CreateRequest(st, path, HttpMethod.Put, reqCreator.deserializedEntity, HttpStatusCode.OK, false, false);
//            reqCreator.CreateRequest(st, path + "/" + reqCreator.deserializedEntity.Id.ToString(), HttpMethod.Delete, null, HttpStatusCode.OK, false, true);
//        }
//    }
//}