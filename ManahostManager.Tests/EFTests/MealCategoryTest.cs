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
    public class MealCategoryTest
    {
        private ManahostManagerDAL ctx;
        private MealCategoryRepository repo;

        [TestInitialize]
        public void Init()
        {
            ctx = EFContext.CreateContext();
            repo = new MealCategoryRepository(ctx);
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
                Assert.IsNull(repo.GetMealCategoryById(1, manager.FindByEmail("contact4@manahost.fr").Id));
            }

        }

        [TestMethod]
        public void DeleteMeal()
        {
            MealCategory toDelete = ctx.MealCategorySet.FirstOrDefault(p => p.Label == "meal category 1");

            repo.Delete(toDelete);
            repo.Save();

            Assert.IsNull(ctx.MealCategorySet.FirstOrDefault(p => p.Label == "meal category 1"));
            Assert.IsNotNull(ctx.MealSet.FirstOrDefault(p => p.Title == "Meal1"));
        }
    }
}