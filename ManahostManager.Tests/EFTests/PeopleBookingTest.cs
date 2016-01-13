using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;
using ManahostManager.Domain.Repository;
using ManahostManager.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;

namespace ManahostManager.Tests.EFTests
{
    [TestClass]
    public class PeopleBookingTest
    {
        private ManahostManagerDAL ctx;
        private PeopleBookingRepository repo;

        [TestInitialize]
        public void Init()
        {
            ctx = EFContext.CreateContext();
            repo = new PeopleBookingRepository(ctx);
        }

        [TestCleanup]
        public void Cleanup()
        {
            ctx.Dispose();
        }

        [TestMethod]
        public void ForbiddenResource()
        {
            Assert.IsNull(repo.GetPeopleBookingById(1, -1));
        }

        [TestMethod]
        public void DeletePeopleBooking()
        {
            repo.includes.Add("RoomBooking");
            IEnumerable<PeopleBooking> listPeopleBooking = repo.GetList(p => p.HomeId == (int)ctx.HomeSet.FirstOrDefault(x => x.Title == "LaCorderie").Id);

            foreach (PeopleBooking cur in listPeopleBooking)
            {
                int savedRoomBookingId = (int)cur.RoomBooking.Id;
                repo.Delete(cur);
                repo.Save();
                Assert.IsNotNull(repo.GetUniq<RoomBooking>(p => p.Id == savedRoomBookingId));
            }
        }
    }
}