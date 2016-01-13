using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;
using ManahostManager.Domain.Repository;
using ManahostManager.Domain.Tools;
using Microsoft.AspNet.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace ManahostManager.Tests.EFTests
{
    [TestClass]
    public class BookingStepConfigTest
    {
        private ManahostManagerDAL ctx;
        private BookingStepConfigRepository repo;
        private BookingStepConfig entity;

        [TestInitialize]
        public void Init()
        {
            ctx = EFContext.CreateContext();
            repo = new BookingStepConfigRepository(ctx);
            entity = new BookingStepConfig()
            {
                HomeId = ctx.HomeSet.FirstOrDefault(x => x.Title == "LaCorderie").Id,
                Title = "EFtest"
            };
        }

        [TestCleanup]
        public void CleanUp()
        {
            ctx.Dispose();
        }

        [TestMethod]
        public void ForbiddenResource()
        {
            repo.Add(entity);

            repo.Save();

            using (UserManager<Client, int> manager = new ClientUserManager(new CustomUserStore(ctx)))
            {
                Assert.IsNull(repo.GetBookingStepConfigById(entity.Id, manager.FindByEmail("contact4@manahost.fr").Id));
            }
        }

        [TestMethod]
        public void ResourcesShouldExist()
        {
            BookingStepConfig toDelete = ctx.BookingStepConfigSet.FirstOrDefault(p => p.Title == "Room Steps");

            Assert.AreEqual(ctx.BookingStepBookingSet.Include("BookingStepConfig").Where(p => p.BookingStepConfig.Id == toDelete.Id).ToList().Count, 1);
            Assert.AreEqual(ctx.BookingStepSet.Include("BookingStepConfig").Where(p => p.BookingStepConfig.Id == toDelete.Id).ToList().Count, 3);
            Assert.AreNotEqual(ctx.BookingSet.ToList().Count, 0);
        }

        [TestMethod]
        public void DeleteBookingStepConfig()
        {
            BookingStepConfig toDelete = ctx.BookingStepConfigSet.FirstOrDefault(p => p.Title == "Room Steps");

            Assert.IsNotNull(toDelete);

            repo.Delete(toDelete);
            repo.Save();

            Assert.AreEqual(ctx.BookingStepBookingSet.Include("BookingStepConfig").Where(p => p.BookingStepConfig.Id == toDelete.Id).ToList().Count, 0);
            Assert.AreEqual(ctx.BookingStepSet.Include("BookingStepConfig").Where(p => p.BookingStepConfig.Id == toDelete.Id).ToList().Count, 0);
            Assert.AreNotEqual(ctx.BookingSet.ToList().Count, 0);
        }
    }
}