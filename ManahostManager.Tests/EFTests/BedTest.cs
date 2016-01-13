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
    public class BedTest
    {
        private ManahostManagerDAL ctx;
        private BedRepository repo;
        private Bed entity;

        [TestInitialize]
        public void Init()
        {
            ctx = EFContext.CreateContext();
            repo = new BedRepository(ctx);
            entity = new Bed()
            {
                HomeId = ctx.HomeSet.FirstOrDefault(p => p.Title == "LaCorderie").Id,
                NumberPeople = 2,
                // TODO
                //RoomId = ctx.RoomSet.FirstOrDefault(p => p.Title == "Melanie").Id
            };
        }

        [TestCleanup]
        public void CleanUp()
        {
            ctx.Dispose();
        }

        [TestMethod]
        public void GetBedWithBadHomeId()
        {
            repo.Add(entity);
            repo.Save();

            using (UserManager<Client, int> manager = new ClientUserManager(new CustomUserStore(ctx)))
            {
                Assert.IsNull(repo.GetBedById(entity.Id, manager.FindByEmail("contact4@manahost.fr").Id));
            }
        }

        [TestMethod]
        public void DeleteBed()
        {
            Bed toDelete = ctx.BedSet.FirstOrDefault(p => p.NumberPeople == 2);

            repo.Delete(toDelete);
            repo.Save();

            Assert.IsNull(ctx.BedSet.FirstOrDefault(p => p.NumberPeople == 2));
            Assert.IsNotNull(ctx.BedSet.FirstOrDefault(p => p.NumberPeople == 1));
            Assert.IsNotNull(ctx.RoomSet.FirstOrDefault(p => p.Title == "Melanie"));
            Assert.IsNotNull(ctx.RoomSet.FirstOrDefault(p => p.Title == "Georges"));
        }

        [TestMethod]
        public void ShouldNotGetBecauseOfNotPermitted()
        {
            //Bed get = repo.GetBedById(ctx.BedSet.FirstOrDefault(p => p.NumberPeople == 2).Id, (int)ctx.ClientSet.FirstOrDefault(p => p.Email == "contact4@manahost.fr").DefaultHomeId);
            using (ClientUserManager manager = new ClientUserManager(new CustomUserStore(ctx)))
            {
                Bed get = repo.GetBedById(ctx.BedSet.FirstOrDefault(p => p.NumberPeople == 2).Id, (int)manager.FindByEmail("contact4@manahost.fr").DefaultHomeId);
                Assert.IsNull(get);
            }
        }
    }
}