using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;
using ManahostManager.Domain.Repository;
using ManahostManager.Domain.Tools;
using ManahostManager.Utils;
using Microsoft.AspNet.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;

namespace ManahostManager.Tests.EFTests
{
    [TestClass]
    public class DepositTest
    {
        private ManahostManagerDAL ctx;
        private DepositRepository repo;

        [TestInitialize]
        public void Init()
        {
            ctx = EFContext.CreateContext();
            repo = new DepositRepository(ctx);
        }

        [TestCleanup]
        public void CleanUp()
        {
            ctx.Dispose();
        }

        [TestMethod]
        public void ForbiddenResource()
        {
            using (UserManager<Client, int> manager = new ClientUserManager(new CustomUserStore(ctx)))
            {
                Assert.IsNull(repo.GetDepositById(1, manager.FindByEmail("contact4@manahost.fr").Id));
                Assert.IsNull(repo.GetDepositByBookingId(1, 8590, (int)ctx.HomeSet.FirstOrDefault(x => x.Title == "LaCorderie").ClientId));
                Assert.IsNull(repo.GetDepositByBookingId(1, 1, manager.FindByEmail("contact4@manahost.fr").Id));
            }
        }

        [TestMethod]
        public void DeleteDeposit()
        {
            repo.includes.Add("Booking");
            IEnumerable<Deposit> listDeposits = repo.GetList(p => p.HomeId == (int)ctx.HomeSet.FirstOrDefault(x => x.Title == "LaCorderie").ClientId);

            foreach (var cur in listDeposits)
            {
                int savedBookingId = (int)cur.Booking.Id;
                int savedId = cur.Id;
                repo.Delete(cur);
                repo.Save();
                Assert.IsNotNull(repo.GetUniq<Booking>(p => p.Id == savedBookingId));
                Assert.IsNull(repo.GetUniq(p => p.Id == savedId));
            }
        }
    }
}