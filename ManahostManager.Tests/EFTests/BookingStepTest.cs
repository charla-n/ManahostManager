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
    public class BookingStepTest
    {
        private ManahostManagerDAL ctx;
        private BookingStepRepository repo;

        [TestInitialize]
        public void Init()
        {
            ctx = EFContext.CreateContext();
            repo = new BookingStepRepository(ctx);
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
                Assert.IsNull(repo.GetBookingStepById(1, manager.FindByEmail("contact4@manahost.fr").Id));
            }
        }

        [TestMethod]
        public void ForbiddenResourceFirstBooking()
        {
            Assert.IsNull(repo.GetFirstBookingStep(1, 999));
        }

        [TestMethod]
        public void DeleteBookingStep()
        {
            BookingStep toDelete1 = ctx.BookingStepSet.FirstOrDefault(p => p.Title == "WAITING_CONFIRMATION");
            BookingStep toDelete2 = ctx.BookingStepSet.FirstOrDefault(p => p.Title == "WAITING_PAYMENT");

            repo.Delete(toDelete1);
            repo.Save();
            repo.Delete(toDelete2);
            repo.Save();

            Assert.IsNotNull(ctx.BookingStepSet.FirstOrDefault(p => p.Title == "VALIDATE"));
            Assert.IsNotNull(ctx.BookingStepConfigSet.FirstOrDefault(p => p.Title == "Room Steps"));
            Assert.IsNull(ctx.BookingStepBookingSet.Include("CurrentStep").FirstOrDefault(p => p.CurrentStep.Id == toDelete1.Id));
        }
    }
}