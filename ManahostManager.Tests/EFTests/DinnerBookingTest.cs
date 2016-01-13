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
    public class DinnerBookingTest
    {
        private ManahostManagerDAL ctx;
        private DinnerBookingRepository repo;

        [TestInitialize]
        public void Init()
        {
            ctx = EFContext.CreateContext();
            repo = new DinnerBookingRepository(ctx);
        }

        [TestCleanup]
        public void Cleanup()
        {
            ctx.Dispose();
        }

        [TestMethod]
        public void ForbiddenResource()
        {
            Assert.IsNull(repo.GetDinnerBookingById(1, -1));
        }

        [TestMethod]
        public void DeleteDinnerBooking()
        {
            repo.includes.Add("Booking");
            IEnumerable<DinnerBooking> listDinnerBooking = repo.GetList(p => p.HomeId == (int)ctx.HomeSet.FirstOrDefault(x => x.Title == "LaCorderie").Id);

            foreach (DinnerBooking cur in listDinnerBooking)
            {
                int savedId = cur.Id;
                int savedBookingId = (int)cur.Booking.Id;
                repo.Delete(cur);
                repo.Save();
                Assert.IsNotNull(repo.GetUniq<Booking>(p => p.Id == savedBookingId));
                repo.includes.Add("DinnerBooking");
                Assert.AreEqual(0, repo.GetList<MealBooking>(p => p.DinnerBooking.Id == savedId).ToList().Count);
            }
        }
    }
}