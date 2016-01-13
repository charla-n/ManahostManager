using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;
using ManahostManager.Domain.Repository;
using ManahostManager.Domain.Tools;
using ManahostManager.Utils;
using Microsoft.AspNet.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.Entity.Validation;
using System.Linq;

namespace ManahostManager.Tests.EFTests
{
    [TestClass]
    public class PeriodTest
    {
        private ManahostManagerDAL ctx;
        private PeriodRepository repo;

        [TestInitialize]
        public void Init()
        {
            ctx = EFContext.CreateContext();
            repo = new PeriodRepository(ctx);
        }

        [TestCleanup]
        public void Cleanup()
        {
            ctx.Dispose();
        }

        [TestMethod]
        public void ForbiddenResource()
        {
            using (UserManager<Client, int> manager = new ClientUserManager(new CustomUserStore(ctx)))
            {
                Assert.IsNull(repo.GetPeriodById(1, manager.FindByEmail("contact4@manahost.fr").Id));
            }
        }

        [TestMethod]
        public void DeletePeriod()
        {
            Period toDelete = repo.GetUniq(p => p.Title == "Remaining");

            Assert.IsNotNull(repo.GetUniq(p => p.Title == "Remaining"));

            repo.Delete(toDelete);
            repo.Save();

            Assert.IsNull(repo.GetUniq(p => p.Title == "Remaining"));
            Assert.IsNull(ctx.PricePerPersonSet.FirstOrDefault(p => p.Title == "Prix adulte room1 reste de l'année"));
        }
    }
}