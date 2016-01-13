//using ManahostManager.Domain.Entity;
//using ManahostManager.Tests.ControllerTests.Utils;
//using ManahostManager.Tests.Repository;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System.Net;
//using System.Net.Http;

//namespace ManahostManager.Tests.ControllerTests
//{
//    [TestClass]
//    public class RoomBookingControllerTest : ControllerTest<RoomBooking>
//    {
//        private const string path = "/api/Booking/Room";

//        [TestMethod]
//        public void PostPutDeleteCompute()
//        {
//            // TODO uncomment when MsgPack works with RequestCreator or remove msgPack test for RequestCreator only test with Json
//            //reqCreator.CreateRequest(st, path, HttpMethod.Post, new RoomBookingRepositoryTest().GetRoomBookingById(6, 0), HttpStatusCode.OK, true, false, true);
//            //reqCreator.CreateRequest(st, path + "/" + reqCreator.deserializedEntity.Id.ToString(), HttpMethod.Delete, null, HttpStatusCode.OK, false, true);
//            reqCreator.CreateRequest(st, path, HttpMethod.Post, new RoomBookingRepositoryTest().GetRoomBookingById(6, 0), HttpStatusCode.OK, true, false);
//            reqCreator.CreateRequest(st, path + "/Compute/" + reqCreator.deserializedEntity.Id.ToString(), HttpMethod.Put, null, HttpStatusCode.OK, false, true);
//            reqCreator.CreateRequest(st, path, HttpMethod.Put, reqCreator.deserializedEntity, HttpStatusCode.OK, false, false);
//            reqCreator.CreateRequest(st, path + "/" + reqCreator.deserializedEntity.Id.ToString(), HttpMethod.Delete, null, HttpStatusCode.OK, false, true);
//        }
//    }
//}