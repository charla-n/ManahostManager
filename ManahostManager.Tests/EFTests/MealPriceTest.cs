using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;
using ManahostManager.Domain.Repository;
using ManahostManager.Domain.Tools;
using ManahostManager.Utils;
using Microsoft.AspNet.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;

namespace ManahostManager.Tests.EFTests
{
    [TestClass]
    public class MealPriceTest
    {
        private ManahostManagerDAL ctx;
        private MealPriceRepository repo;

        [TestInitialize]
        public void Init()
        {
            ctx = EFContext.CreateContext();
            repo = new MealPriceRepository(ctx);
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
                Assert.IsNull(repo.GetMealPriceById(1, manager.FindByEmail("contact4@manahost.fr").Id));
            }
        }

        [TestMethod]
        public void DeleteMeal()
        {
            MealPrice toDelete = ctx.MealPriceSet.FirstOrDefault(p => p.PriceHT == 15M);

            repo.Delete(toDelete);
            repo.Save();

            Assert.IsNull(ctx.MealPriceSet.FirstOrDefault(p => p.PriceHT == 15M));
            Assert.IsNotNull(ctx.PeopleCategorySet.FirstOrDefault(p => p.Label == "Adultes"));
            Assert.IsNotNull(ctx.MealSet.FirstOrDefault(p => p.Title == "Meal1"));
        }
    }
}