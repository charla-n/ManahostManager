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
    public class SatisfactionConfigTest
    {
        private ManahostManagerDAL ctx;
        private SatisfactionConfigRepository repo;

        [TestInitialize]
        public void Init()
        {
            ctx = EFContext.CreateContext();
            repo = new SatisfactionConfigRepository(ctx);
        }

        [TestCleanup]
        public void CleanUp()
        {
            ctx.Dispose();
        }

        [TestMethod]
        public void DeleteSatisfactionConfig()
        {
            Home home;

            using (UserManager<Client, int> manager = new ClientUserManager(new CustomUserStore(ctx)))
            {
                ctx.HomeSet.Add(home = new Home()
                {
                    Title = "LaCorderieTest",
                    EstablishmentType = EEstablishmentType.BB,
                    Client = manager.FindByEmail("contact@manahost.fr"),
                });
            }

            SatisfactionConfig toAdd;

            repo.Add(toAdd = new SatisfactionConfig()
            {
                Home = home,
                Title = "ttt"
            });

            repo.Save();

            repo.Delete(toAdd);
            repo.Save();

            Assert.IsNull(ctx.SatisfactionConfigSet.FirstOrDefault(p => p.Home.Id == home.Id));
            Assert.IsNull(ctx.SatisfactionConfigQuestionSet.Include("SatisfactionConfig").FirstOrDefault(p => p.SatisfactionConfig.Id == toAdd.Id));
            Assert.IsNotNull(ctx.HomeSet.FirstOrDefault(p => p.Title == home.Title));
        }
    }
}