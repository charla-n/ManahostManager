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
    public class DocumentTest
    {
        private ManahostManagerDAL ctx;
        private DocumentRepository repo;

        [TestInitialize]
        public void Init()
        {
            ctx = EFContext.CreateContext();
            repo = new DocumentRepository(ctx);
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
                Assert.IsNull(repo.GetDocumentById(1, manager.FindByEmail("contact4@manahost.fr").Id));
            }
        }

        [TestMethod]
        public void DeleteDocument()
        {
            Document toDelete1 = ctx.DocumentSet.FirstOrDefault(p => p.Title == "Doc1");
            Document toDelete2 = ctx.DocumentSet.FirstOrDefault(p => p.Title == "Doc2");

            repo.Delete(toDelete1);
            repo.Save();
            repo.Delete(toDelete2);
            repo.Save();

            Assert.IsNull(ctx.DocumentSet.FirstOrDefault(p => p.Title == "Doc1"));
            Assert.IsNotNull(ctx.DocumentCategorySet.FirstOrDefault(p => p.Title == "Cat1"));
            Assert.IsNotNull(ctx.DocumentCategorySet.FirstOrDefault(p => p.Title == "Cat2"));
        }
    }
}