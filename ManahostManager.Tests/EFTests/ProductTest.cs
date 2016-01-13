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
    public class ProductTest
    {
        private ManahostManagerDAL ctx;
        private ProductRepository repo;
        private Product entity;

        [TestInitialize]
        public void Init()
        {
            ctx = EFContext.CreateContext();
            repo = new ProductRepository(ctx);
            entity = new Product()
            {
                Home = ctx.HomeSet.FirstOrDefault(p => p.Title == "LaCorderie"),
                Title = "EFTest",
                Supplier = null,
                PriceHT = 300M,
                ProductCategory = null,
                Stock = 10,
                Tax = null,
                Threshold = 3,
                RefHide = false,
                Hide = false,
                IsUnderThreshold = false
            };
        }

        [TestCleanup]
        public void Cleanup()
        {
            ctx.Dispose();
        }

        [TestMethod]
        public void ForbiddenResource()
        {
            repo.Add(entity);

            repo.Save();

            using (UserManager<Client, int> manager = new ClientUserManager(new CustomUserStore(ctx)))
            {
                Assert.IsNull(repo.GetProductById(entity.Id, manager.FindByEmail("contact4@manahost.fr").Id));
            }
        }

        [TestMethod]
        public void DeleteProduct()
        {
            Product toDelete1 = repo.GetUniq(p => p.Title == "<span style='background-color:red'>Massage</span>");
            Product toDelete2 = repo.GetUniq(p => p.Title == "Vin");

            Assert.IsNotNull(ctx.ProductBookingSet.Include("Product").FirstOrDefault(p => p.Product.Id == toDelete1.Id));
            Assert.IsNotNull(ctx.ProductBookingSet.Include("Product").FirstOrDefault(p => p.Product.Id == toDelete2.Id));

            repo.Delete(toDelete1);
            repo.Save();
            repo.Delete(toDelete2);
            repo.Save();

            Product toAdd;

            repo.Add(toAdd = new Product()
            {
                Home = ctx.HomeSet.FirstOrDefault(p => p.Title == "LaCorderie"),
                Title = "EFTest",
                Supplier = null,
                PriceHT = 300M,
                ProductCategory = null,
                Stock = 10,
                Tax = null,
                Threshold = 3,
                RefHide = false,
                Hide = false,
                BillItemCategory = ctx.BillItemCategorySet.FirstOrDefault(),
                IsUnderThreshold = false
            });

            repo.Save();
            repo.Delete(toAdd);
            repo.Save();

            Assert.IsNull(ctx.ProductBookingSet.Include("Product").FirstOrDefault(p => p.Product.Id == toDelete1.Id));
            Assert.IsNull(ctx.ProductBookingSet.Include("Product").FirstOrDefault(p => p.Product.Id == toDelete2.Id));
            Assert.IsNull(ctx.ProductSet.FirstOrDefault(p => p.Id == toDelete1.Id));
            Assert.IsNull(ctx.ProductSet.FirstOrDefault(p => p.Id == toDelete2.Id));
        }
    }
}