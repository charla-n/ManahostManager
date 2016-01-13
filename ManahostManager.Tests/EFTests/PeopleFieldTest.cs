using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;
using ManahostManager.Domain.Repository;
using ManahostManager.Domain.Tools;
using ManahostManager.Tests.ControllerTests.Utils;
using ManahostManager.Utils;
using Microsoft.AspNet.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity.Validation;
using System.Linq;

namespace ManahostManager.Tests.EFTests
{
    [TestClass]
    public class PeopleFieldTest : ControllerTest<PeopleField>
    {
        private ManahostManagerDAL ctx;
        private PeopleFieldRepository repo;

        [TestInitialize]
        public void Init()
        {
            ctx = EFContext.CreateContext();
            repo = new PeopleFieldRepository(ctx);
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
                Assert.IsNull(repo.GetPeopleFieldById(1, manager.FindByEmail("contact4@manahost.fr").Id));
            }
        }

        [TestMethod]
        public void DeletePeopleField()
        {
            PeopleField toDelete = repo.GetUniq(p => p.Key == "MyPersonnalField1");

            Assert.IsNotNull(repo.GetUniq(p => p.Key == "MyPersonnalField1"));

            repo.Delete(toDelete);
            repo.Save();

            Assert.IsNull(repo.GetUniq(p => p.Key == "MyPersonnalField1"));
        }
    }
}