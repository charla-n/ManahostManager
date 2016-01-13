//using ManahostManager.Domain.Entity;
//using ManahostManager.Tests.ControllerTests.Utils;
//using ManahostManager.Tests.Repository;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System.Net;
//using System.Net.Http;

//namespace ManahostManager.Tests.ControllerTests
//{
//    [TestClass]
//    public class DinnerBookingControllerTest : ControllerTest<DinnerBooking>
//    {
//        private const string path = "/api/Booking/Dinner";

//        [TestMethod]
//        public void PostPutDeleteCompute()
//        {
//            reqCreator.CreateRequest(st, path, HttpMethod.Post, new DinnerBookingRepositoryTest().GetDinnerBookingById(1, 0), HttpStatusCode.OK, true, false);
//            reqCreator.CreateRequest(st, path + "/Compute/" + reqCreator.deserializedEntity.Id.ToString(), HttpMethod.Put, null, HttpStatusCode.OK, false, true);
//            reqCreator.CreateRequest(st, path, HttpMethod.Put, reqCreator.deserializedEntity, HttpStatusCode.OK, false, false);
//            reqCreator.CreateRequest(st, path + "/" + reqCreator.deserializedEntity.Id.ToString(), HttpMethod.Delete, null, HttpStatusCode.OK, false, true);
//        }
//    }
//}