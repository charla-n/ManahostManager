using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;
using ManahostManager.Domain.Repository;
using ManahostManager.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.Entity.Validation;
using System.Linq;

namespace ManahostManager.Tests.EFTests
{
    [TestClass]
    public class KeyGeneratorTest
    {
        private ManahostManagerDAL ctx;
        private KeyGeneratorRepository repo;

        [TestInitialize]
        public void Init()
        {
            ctx = EFContext.CreateContext();
            repo = new KeyGeneratorRepository(ctx);
        }

        [TestCleanup]
        public void CleanUp()
        {
            ctx.Dispose();
        }

        [TestMethod]
        public void DeleteKeyGenerator()
        {
            KeyGenerator toDelete = ctx.KeyGeneratorSet.FirstOrDefault(p => p.KeyType == EKeyType.BETA);

            repo.Delete(toDelete);
            repo.Save();

            Assert.IsNull(ctx.KeyGeneratorSet.FirstOrDefault(p => p.KeyType == EKeyType.BETA));
        }
    }
}