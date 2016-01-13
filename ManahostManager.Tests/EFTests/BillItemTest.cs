using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Repository;

namespace ManahostManager.Tests.EFTests
{
    /// <summary>
    /// Summary description for BillItemTest
    /// </summary>
    [TestClass]
    public class BillItemTest
    {
        private ManahostManagerDAL ctx;
        private BillItemRepository repo;

        [TestInitialize]
        public void Init()
        {
            ctx = EFContext.CreateContext();
            repo = new BillItemRepository(ctx);
        }

        [TestCleanup]
        public void Cleanup()
        {
            ctx.Dispose();
        }

        [TestMethod]
        public void ForbiddenResource()
        {
            Assert.IsNull(repo.GetBillItemById(1, -1));
        }
    }
}
