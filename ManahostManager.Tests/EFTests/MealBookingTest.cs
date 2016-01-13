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
    public class MealBookingTest
    {
        private ManahostManagerDAL ctx;
        private MealBookingRepository repo;

        [TestInitialize]
        public void Init()
        {
            ctx = EFContext.CreateContext();
            repo = new MealBookingRepository(ctx);
        }

        [TestCleanup]
        public void CleanUp()
        {
            ctx.Dispose();
        }

        [TestMethod]
        public void ForbiddenResource()
        {
            Assert.IsNull(repo.GetMealBookingById(1, -1));
        }

        [TestMethod]
        public void DeleteMealBooking()
        {
            repo.includes.Add("DinnerBooking");
            IEnumerable<MealBooking> listMealBooking = repo.GetList(p => p.HomeId == (int)ctx.HomeSet.FirstOrDefault(x => x.Title == "LaCorderie").Id);

            foreach (MealBooking cur in listMealBooking)
            {
                int savedDinnerBookingId = (int)cur.DinnerBooking.Id;
                repo.Delete(cur);
                repo.Save();
                Assert.IsNotNull(repo.GetUniq<DinnerBooking>(p => p.Id == savedDinnerBookingId));
            }
        }
    }
}