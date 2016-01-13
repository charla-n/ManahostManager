using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;
using ManahostManager.Domain.Repository;
using ManahostManager.Domain.Tools;
using ManahostManager.Utils;
using Microsoft.AspNet.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;

namespace ManahostManager.Tests.EFTests
{
    [TestClass]
    public class ProductBookingTest
    {
        private ManahostManagerDAL ctx;
        private ProductBookingRepository repo;

        [TestInitialize]
        public void Init()
        {
            ctx = EFContext.CreateContext();
            repo = new ProductBookingRepository(ctx);
        }

        [TestMethod]
        public void ForbiddenResource()
        {
            using (UserManager<Client, int> manager = new ClientUserManager(new CustomUserStore(ctx)))
            {
                Assert.IsNull(repo.GetProductBookingById(1, manager.FindByEmail("contact4@manahost.fr").Id));
            }
        }

        [TestMethod]
        public void DeleteProductBooking()
        {
            repo.includes.Add("Booking");
            IEnumerable<ProductBooking> listProductBooking = repo.GetList(p => p.HomeId == (int)ctx.HomeSet.FirstOrDefault(x => x.Title == "LaCorderie").Id);

            foreach (ProductBooking cur in listProductBooking)
            {
                int savedBookingId = (int)cur.Booking.Id;
                repo.Delete(cur);
                repo.Save();
                Assert.IsNotNull(repo.GetUniq<Booking>(p => p.Id == savedBookingId));
            }
        }
    }
}