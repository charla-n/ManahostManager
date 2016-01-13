using ManahostManager.Domain.DAL;
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
    public class TaxTest
    {
        private ManahostManagerDAL ctx;
        private TaxRepository repo;

        [TestInitialize]
        public void Init()
        {
            ctx = EFContext.CreateContext();
            repo = new TaxRepository(ctx);
        }

        [TestCleanup]
        public void Cleanup()
        {
            ctx.Dispose();
        }

        [TestMethod]
        public void GetForbiddenResource()
        {
            using (UserManager<Client, int> manager = new ClientUserManager(new CustomUserStore(ctx)))
            {
                Tax get = repo.GetTaxById(1, manager.FindByEmail("contact4@manahost.fr").Id);
                Assert.IsNull(get);
            }
        }

        [TestMethod]
        public void ResourceShouldExists()
        {
            Tax toDelete = ctx.TaxSet.FirstOrDefault(p => p.Title == "Tax1");

            Assert.IsNotNull(toDelete);

            Assert.AreNotEqual(0, ctx.ProductSet.Where(p => p.Tax.Id == toDelete.Id).ToList().Count);

            Assert.AreNotEqual(0, ctx.RoomSupplementSet.Include("Tax").Where(p => p.Tax.Id == toDelete.Id).ToList().Count);

            Assert.AreNotEqual(0, ctx.MealPriceSet.Include("Tax").Where(p => p.Tax.Id == toDelete.Id).ToList().Count);

            Assert.AreNotEqual(0, ctx.PeopleCategorySet.Include("Tax").Where(p => p.Tax.Id == toDelete.Id).ToList().Count);

            Assert.AreNotEqual(0, ctx.PricePerPersonSet.Include("Tax").Where(p => p.Tax.Id == toDelete.Id).ToList().Count);
        }

        [TestMethod]
        public void DeleteTax()
        {
            Tax toDelete = ctx.TaxSet.FirstOrDefault(p => p.Title == "Tax1");

            repo.Delete(toDelete);
            repo.Save();

            Assert.IsNull(repo.GetTaxById(toDelete.Id, (int)ctx.HomeSet.FirstOrDefault(p => p.Title == "LaCorderie").ClientId));
            Assert.IsNotNull(ctx.TaxSet.FirstOrDefault(p => p.Title == "Tax2"));

            Assert.AreEqual(0, ctx.ProductSet.Where(p => p.Tax.Id == toDelete.Id).ToList().Count);

            Assert.AreEqual(0, ctx.RoomSupplementSet.Include("Tax").Where(p => p.Tax.Id == toDelete.Id).ToList().Count);

            Assert.AreEqual(0, ctx.MealPriceSet.Include("Tax").Where(p => p.Tax.Id == toDelete.Id).ToList().Count);

            Assert.AreEqual(0, ctx.PeopleCategorySet.Include("Tax").Where(p => p.Tax.Id == toDelete.Id).ToList().Count);

            Assert.AreEqual(0, ctx.PricePerPersonSet.Include("Tax").Where(p => p.Tax.Id == toDelete.Id).ToList().Count);
        }
    }
}