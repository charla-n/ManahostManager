using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;
using ManahostManager.Domain.Repository;
using ManahostManager.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;

namespace ManahostManager.Tests.EFTests
{
    [TestClass]
    public class RoomBookingTest
    {
        private ManahostManagerDAL ctx;
        private RoomBookingRepository repo;

        [TestInitialize]
        public void Init()
        {
            ctx = EFContext.CreateContext();
            repo = new RoomBookingRepository(ctx);
        }

        [TestCleanup]
        public void Cleanup()
        {
            ctx.Dispose();
        }

        [TestMethod]
        public void ForbiddenResource()
        {
            Assert.IsNull(repo.GetRoomBookingById(1, -1));
        }

        [TestMethod]
        public void DeleteRoomBooking()
        {
            repo.includes.Add("Booking");
            IEnumerable<RoomBooking> listRoomBooking = repo.GetList(p => p.HomeId == (int)ctx.HomeSet.FirstOrDefault(x => x.Title == "LaCorderie").Id);

            foreach (RoomBooking cur in listRoomBooking)
            {
                int savedId = cur.Id;
                int savedBookingId = (int)cur.Booking.Id;
                repo.Delete(cur);
                repo.Save();
                Assert.IsNotNull(repo.GetUniq<Booking>(p => p.Id == savedBookingId));
                repo.includes.Add("RoomBooking");
                Assert.AreEqual(0, repo.GetList<SupplementRoomBooking>(p => p.RoomBooking.Id == savedId).ToList().Count);
                repo.includes.Add("RoomBooking");
                Assert.AreEqual(0, repo.GetList<PeopleBooking>(p => p.RoomBooking.Id == savedId).ToList().Count);
            }
        }
    }
}