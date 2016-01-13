using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;
using ManahostManager.Domain.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ManahostManager.Tests.EFTests
{
    [TestClass]
    public class SatisfactionConfigQuestionTest
    {
        private ManahostManagerDAL ctx;
        private SatisfactionConfigQuestionRepository repo;

        [TestInitialize]
        public void Init()
        {
            ctx = EFContext.CreateContext();
            repo = new SatisfactionConfigQuestionRepository(ctx);
        }

        [TestCleanup]
        public void CleanUp()
        {
            ctx.Dispose();
        }

        [TestMethod]
        public void DeleteSatisfactionConfigQuestion()
        {
            SatisfactionConfigQuestion toDelete = repo.GetUniq(p => p.Question == "Le séjour vous a t-il plu ?");

            Assert.IsNotNull(repo.GetUniq(p => p.Question == "Le séjour vous a t-il plu ?"));

            repo.Delete(toDelete);
            repo.Save();

            Assert.IsNull(repo.GetUniq(p => p.Question == "Le séjour vous a t-il plu ?"));
        }
    }
}