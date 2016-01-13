using ManahostManager.Controllers;
using ManahostManager.Tests.ControllerTests.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;

namespace ManahostManager.Tests.ControllerTests
{
    [TestClass]
    public class SearchControllerTest : ControllerTest<AdvSearch>
    {
        public const string path = "/api/Search";

        [TestMethod]
        public void Post()
        {
            reqCreator.CreateRequest(st, path, HttpMethod.Post, new AdvSearch()
            {
                Search = "RoomBooking/where/id/eq/1",
                Include = new System.Collections.Generic.List<string>()
                {
                    "Room",
                    "Booking"
                }
            }, HttpStatusCode.OK, false);

            reqCreator.CreateRequest(st, path, HttpMethod.Post, new AdvSearch()
            {
                Search = "RoomBooking/where/DateModification/lt/2015-12-30"
            }, HttpStatusCode.OK, false);

            reqCreator.CreateRequest(st, path, HttpMethod.Post, new AdvSearch()
            {
                Search = "RoomBooking/Orderby/Id/desc"
            }, HttpStatusCode.OK, false);

            reqCreator.CreateRequest(st, path, HttpMethod.Post, new AdvSearch()
            {
                Search = "RoomBooking/where/Id/eq/1/count"
            }, HttpStatusCode.OK, false);
        }
    }
}