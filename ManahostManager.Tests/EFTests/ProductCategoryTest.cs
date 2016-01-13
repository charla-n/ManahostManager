using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;
using ManahostManager.Domain.Repository;
using ManahostManager.Domain.Tools;
using Microsoft.AspNet.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace ManahostManager.Tests.EFTests
{
    [TestClass]
    public class ProductCategoryTest
    {
        private ManahostManagerDAL ctx;
        private ProductCategoryRepository repo;
        private ProductCategory entity;

        [TestInitialize]
        public void Init()
        {
            ctx = EFContext.CreateContext();
            repo = new ProductCategoryRepository(ctx);
            entity = new ProductCategory()
            {
                Home = ctx.HomeSet.FirstOrDefault(p => p.Title == "LaCorderie"),
                Title = "EFTest",
                RefHide = false
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
                Assert.IsNull(repo.GetProductCategoryById(entity.Id, manager.FindByEmail("contact4@manahost.fr").Id));
            }
        }
    }
}