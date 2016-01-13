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
    public class FieldGroupTest
    {
        private ManahostManagerDAL ctx;
        private FieldGroupRepository repo;

        [TestInitialize]
        public void Init()
        {
            ctx = EFContext.CreateContext();
            repo = new FieldGroupRepository(ctx);
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
                Assert.IsNull(repo.GetFieldGroupById(1, manager.FindByEmail("contact4@manahost.fr").Id));
            }
        }

        [TestMethod]
        public void DeleteFieldGroup()
        {
            FieldGroup toDelete = repo.GetUniq(p => p.Title == "MyFieldGroup");

            Assert.IsNotNull(repo.GetUniq(p => p.Title == "MyFieldGroup"));
            Assert.IsNotNull(ctx.PeopleFieldSet.FirstOrDefault(p => p.Key == "MyPersonnalField1"));

            repo.Delete(toDelete);
            repo.Save();

            Assert.IsNull(repo.GetUniq(p => p.Title == "MyFieldGroup"));
            Assert.IsNull(ctx.PeopleFieldSet.FirstOrDefault(p => p.Key == "MyPersonnalField1"));
        }
    }
}