using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;
using ManahostManager.Domain.Repository;
using ManahostManager.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity.Validation;
using System.Linq;

namespace ManahostManager.Tests.EFTests
{
    [TestClass]
    public class DocumentCategoryTest
    {
        private ManahostManagerDAL ctx;
        private DocumentCategoryRepository repo;

        [TestInitialize]
        public void Init()
        {
            ctx = EFContext.CreateContext();
            repo = new DocumentCategoryRepository(ctx);
        }

        [TestCleanup]
        public void CleanUp()
        {
            ctx.Dispose();
        }

        [TestMethod]
        public void DeleteDocumentCategory()
        {
            DocumentCategory toDelete1 = ctx.DocumentCategorySet.FirstOrDefault(p => p.Title == "Cat1");

            repo.Delete(toDelete1);
            repo.Save();

            Assert.IsNull(ctx.DocumentCategorySet.FirstOrDefault(p => p.Title == "Cat1"));
            Assert.IsNotNull(ctx.DocumentSet.FirstOrDefault(p => p.Title == "Doc1"));
        }
    }
}