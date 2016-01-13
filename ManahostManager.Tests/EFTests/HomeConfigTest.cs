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
    public class HomeConfigTest
    {
        private HomeConfigRepository repo;
        private ManahostManagerDAL ctx;
        private HomeConfig entity;

        [TestInitialize]
        public void Init()
        {
            ctx = EFContext.CreateContext();
            repo = new HomeConfigRepository(ctx);
            using (UserManager<Client, int> manager = new ClientUserManager(new CustomUserStore(ctx)))
            {
                entity = new HomeConfig()
                {
                    Home = new Home()
                    {
                        Title = "LaCorderieTest",
                        EstablishmentType = EEstablishmentType.BB,
                        Client = manager.FindByEmail("contact@manahost.fr"),
                    },
                    AutoSendSatisfactionEmail = false,
                    DepositNotifEnabled = false,
                    Devise = "$",
                    EnableDinner = false,
                    EnableDisplayActivities = false,
                    EnableDisplayMeals = false,
                    EnableDisplayProducts = false,
                    EnableDisplayRooms = false,
                    EnableReferencing = false,
                    FollowStockEnable = false,
                    HourFormat24 = true
                };
            }
            repo.Add(entity);
            repo.Save();
        }

        [TestCleanup]
        public void Cleanup()
        {
            ctx.Dispose();
        }

        [TestMethod]
        public void DeleteHomeConfig()
        {
            repo.Delete(entity);
            repo.Save();

            Assert.IsNull(ctx.HomeConfigSet.FirstOrDefault(p => p.Home.Id == entity.Id));
            Assert.IsNotNull(ctx.HomeSet.FirstOrDefault(p => p.Id == entity.Id));

            repo.Delete(repo.GetHomeConfigById((int)ctx.HomeSet.FirstOrDefault(p => p.Title == "LaCorderie").Id, (int)ctx.HomeSet.FirstOrDefault(x => x.Title == "LaCorderie").ClientId));
            repo.Save();

            Assert.IsNull(ctx.HomeConfigSet.FirstOrDefault(p => p.Home == ctx.HomeSet.FirstOrDefault(x => x.Title == "LaCorderie")));
            Assert.IsNotNull(ctx.HomeSet.FirstOrDefault(p => p.Title == "LaCorderie"));
        }
    }
}