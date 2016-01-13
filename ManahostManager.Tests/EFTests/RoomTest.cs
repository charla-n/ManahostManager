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
    public class RoomTest
    {
        private ManahostManagerDAL ctx;
        private RoomRepository repo;

        [TestInitialize]
        public void Init()
        {
            ctx = EFContext.CreateContext();
            repo = new RoomRepository(ctx);
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
                Assert.IsNull(repo.GetRoomById(ctx.RoomSet.FirstOrDefault(x => x.Title == "Melanie").Id, (int)manager.FindByEmail("contact4@manahost.fr").DefaultHomeId));
            }
        }

        [TestMethod]
        public void ValidateDelete()
        {
            Room toDelete = ctx.RoomSet.FirstOrDefault(x => x.Title == "Melanie");

            IQueryable<Bed> beds = ctx.BedSet.Include("Room").Where(x => x.Room.Id == toDelete.Id);

            Assert.AreEqual(beds.Count(), 2);

            IQueryable<PricePerPerson> ppps = ctx.PricePerPersonSet.Include("Room").Where(x => x.Room.Id == toDelete.Id);

            Assert.AreEqual(ppps.Count(), 6);

            IQueryable<RoomBooking> rbooks = ctx.RoomBookingSet.Include("Room").Where(x => x.Room.Id == toDelete.Id);

            Assert.AreEqual(rbooks.Count(), 2);
        }

        [TestMethod]
        public void DeleteRoom()
        {
            Room toDelete = ctx.RoomSet.FirstOrDefault(x => x.Title == "Melanie");

            repo.Delete(toDelete);
            repo.Save();

            Assert.IsNotNull(ctx.RoomCategorySet.FirstOrDefault(x => x.Title == "Cat1"));
            Assert.IsNotNull(ctx.RoomSet.FirstOrDefault(x => x.Title == "Georges"));

            IQueryable<Bed> beds = ctx.BedSet.Include("Room").Where(x => x.Room.Id == toDelete.Id);

            Assert.AreEqual(beds.Count(), 0);

            IQueryable<PricePerPerson> ppps = ctx.PricePerPersonSet.Include("Room").Where(x => x.Room.Id == toDelete.Id);

            Assert.AreEqual(ppps.Count(), 0);

            IQueryable<PricePerPerson> ppps2 = ctx.PricePerPersonSet.Include("Room").Where(x => x.Room == null);

            Assert.AreEqual(ppps2.Count(), 0);

            IQueryable<RoomBooking> rbooks = ctx.RoomBookingSet.Include("Room").Where(x => x.Room.Id == toDelete.Id);

            Assert.AreEqual(rbooks.Count(), 0);
        }
    }
}