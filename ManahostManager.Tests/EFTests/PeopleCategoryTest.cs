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
    public class PeopleCategoryTest
    {
        private ManahostManagerDAL ctx;
        private PeopleCategoryRepository repo;

        [TestInitialize]
        public void Init()
        {
            ctx = EFContext.CreateContext();
            repo = new PeopleCategoryRepository(ctx);
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
                Assert.IsNull(repo.GetPeopleCategoryById(1, manager.FindByEmail("contact4@manahost.fr").Id));
            }
        }

        [TestMethod]
        public void DeletePeopleCategory()
        {
            PeopleCategory toDelete = repo.GetUniq(p => p.Label == "Adultes");

            Assert.IsNotNull(repo.GetUniq(p => p.Label == "Adultes"));

            repo.Delete(toDelete);
            repo.Save();

            Assert.IsNull(repo.GetUniq(p => p.Label == "Adultes"));
            Assert.IsNull(ctx.MealPriceSet.FirstOrDefault(p => p.PriceHT == 15M));
            Assert.IsNull(ctx.PeopleBookingSet.FirstOrDefault(p => p.NumberOfPeople == 2));
            Assert.IsNull(ctx.PricePerPersonSet.FirstOrDefault(p => p.PriceHT == 80M));
        }
    }
}