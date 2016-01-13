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
    public class RoomSupplementTest
    {
        private ManahostManagerDAL ctx;
        private RoomSupplementRepository repo;

        [TestInitialize]
        public void Init()
        {
            ctx = EFContext.CreateContext();
            repo = new RoomSupplementRepository(ctx);
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
                Assert.IsNull(repo.GetRoomSupplementById(1, manager.FindByEmail("contact4@manahost.fr").Id));
            }
        }

        [TestMethod]
        public void DeleteRoomSupplement()
        {
            RoomSupplement toDelete = ctx.RoomSupplementSet.FirstOrDefault(p => p.Title == "supp1");

            Assert.IsNotNull(ctx.TaxSet.FirstOrDefault(p => p.Title == "Tax1"));
            Assert.AreNotEqual(0, ctx.SupplementRoomBookingSet.Include("RoomSupplement").Where(p => p.RoomSupplement.Id == toDelete.Id).ToList().Count);

            repo.Delete(toDelete);
            repo.Save();

            Assert.AreEqual(0, ctx.SupplementRoomBookingSet.Include("RoomSupplement").Where(p => p.RoomSupplement.Id == toDelete.Id).ToList().Count);
        }
    }
}