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
    public class SupplierTest
    {
        private ManahostManagerDAL ctx;
        private SupplierRepository repo;

        [TestInitialize]
        public void Init()
        {
            ctx = EFContext.CreateContext();
            repo = new SupplierRepository(ctx);
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
                Assert.IsNull(repo.GetSupplierById(1, manager.FindByEmail("contact4@manahost.fr").Id));
            }
        }

        [TestMethod]
        public void DeleteSupplier()
        {
            Supplier toDelete = repo.GetUniq(p => p.SocietyName == "Nongfu");

            Assert.IsNotNull(repo.GetUniq(p => p.SocietyName == "Nongfu"));

            repo.Delete(toDelete);
            repo.Save();

            Assert.IsNull(repo.GetUniq(p => p.SocietyName == "Nongfu"));
        }
    }
}