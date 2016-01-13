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
    public class SupplementRoomBookingTest
    {
        private ManahostManagerDAL ctx;
        private SupplementRoomBookingRepository repo;

        [TestInitialize]
        public void Init()
        {
            ctx = EFContext.CreateContext();
            repo = new SupplementRoomBookingRepository(ctx);
        }

        [TestCleanup]
        public void Cleanup()
        {
            ctx.Dispose();
        }

        [TestMethod]
        public void ForbiddenResource()
        {
            Assert.IsNull(repo.GetSupplementRoomBookingById(1, -1));
        }

        [TestMethod]
        public void DeleteSupplementRoomBooking()
        {
            repo.includes.Add("RoomBooking");
            IEnumerable<SupplementRoomBooking> listSupplementRoomBooking = repo.GetList(p => p.HomeId == (int)ctx.HomeSet.FirstOrDefault(x => x.Title == "LaCorderie").Id);

            foreach (SupplementRoomBooking cur in listSupplementRoomBooking)
            {
                int savedRoomBookingId = (int)cur.RoomBooking.Id;
                repo.Delete(cur);
                repo.Save();
                Assert.IsNotNull(repo.GetUniq<RoomBooking>(p => p.Id == savedRoomBookingId));
            }
        }
    }
}