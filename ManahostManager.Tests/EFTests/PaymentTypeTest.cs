﻿using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;
using ManahostManager.Domain.Repository;
using ManahostManager.Domain.Tools;
using ManahostManager.Utils;
using Microsoft.AspNet.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity.Validation;
using System.Linq;

namespace ManahostManager.Tests.EFTests
{
    [TestClass]
    public class PaymentTypeTest
    {
        private ManahostManagerDAL ctx;
        private PaymentTypeRepository repo;

        [TestInitialize]
        public void Init()
        {
            ctx = EFContext.CreateContext();
            repo = new PaymentTypeRepository(ctx);
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
                Assert.IsNull(repo.GetPaymentTypeById(1, manager.FindByEmail("contact4@manahost.fr").Id));
            }
        }

        [TestMethod]
        public void DeletePaymentType()
        {
            Assert.IsNotNull(repo.GetUniq<PaymentMethod>(p => p.Price == 400M));

            PaymentType toDelete = repo.GetUniq(p => p.Title == "CASH");

            repo.Delete(toDelete);
            repo.Save();

            Assert.IsNull(repo.GetUniq<PaymentMethod>(p => p.Price == 400M));
        }
    }
}