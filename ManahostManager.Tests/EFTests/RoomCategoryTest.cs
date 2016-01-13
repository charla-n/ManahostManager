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
    public class RoomCategoryTest
    {
        private ManahostManagerDAL ctx;
        private RoomCategoryRepository repo;

        [TestInitialize]
        public void Init()
        {
            ctx = EFContext.CreateContext();
            repo = new RoomCategoryRepository(ctx);
        }

        [TestCleanup]
        public void CleanUp()
        {
            ctx.Dispose();
        }

        [TestMethod]
        public void GetRoomCategoryWithBadHomeId()
        {
            using (UserManager<Client, int> manager = new ClientUserManager(new CustomUserStore(ctx)))
            {
                Assert.IsNull(repo.GetRoomCategoryById(1, manager.FindByEmail("contact4@manahost.fr").Id));
            }
        }

        [TestMethod]
        public void ShouldFailBecauseOfNotPermitted()
        {
            using (UserManager<Client, int> manager = new ClientUserManager(new CustomUserStore(ctx)))
            {
                RoomCategory get = repo.GetRoomCategoryById(ctx.RoomCategorySet.FirstOrDefault(p => p.Title == "Cat1").Id, (int)manager.FindByEmail("contact4@manahost.fr").DefaultHomeId);

                Assert.IsNull(get);
            }
        }
    }
}