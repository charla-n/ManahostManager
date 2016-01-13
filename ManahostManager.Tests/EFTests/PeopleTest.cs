using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;
using ManahostManager.Domain.Repository;
using ManahostManager.Domain.Tools;
using ManahostManager.Utils;
using Microsoft.AspNet.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.Entity.Validation;
using System.Linq;

namespace ManahostManager.Tests.EFTests
{
    [TestClass]
    public class PeopleTest
    {
        private ManahostManagerDAL ctx;
        private PeopleRepository repo;

        [TestInitialize]
        public void Init()
        {
            ctx = EFContext.CreateContext();
            repo = new PeopleRepository(ctx);
        }

        [TestCleanup]
        public void CleanUp()
        {
            ctx.Dispose();
        }

        [TestMethod]
        public void GetPeopleWithBadHomeId()
        {
            using (UserManager<Client, int> manager = new ClientUserManager(new CustomUserStore(ctx)))
            {
                Assert.IsNull(repo.GetPeopleById(1, manager.FindByEmail("contact4@manahost.fr").Id));
            }
        }

        [TestMethod]
        public void ShouldExist()
        {
            People delete = ctx.PeopleSet.FirstOrDefault(p => p.Email == "contact@manahost.fr");

            Assert.IsNotNull(ctx.BookingSet.Include("People").FirstOrDefault(p => p.People.Id == delete.Id));
            Assert.IsNotNull(ctx.PeopleFieldSet.Include("People").FirstOrDefault(p => p.People.Id == delete.Id));
            Assert.IsNotNull(ctx.SatisfactionClientSet.Include("PeopleDest").FirstOrDefault(p => p.PeopleDest.Id == delete.Id));
        }

        [TestMethod]
        public void DeletePeople()
        {
            People delete = ctx.PeopleSet.FirstOrDefault(p => p.Email == "contact@manahost.fr");

            repo.Delete(delete);
            repo.Save();

            Assert.IsNull(ctx.BookingSet.Include("People").FirstOrDefault(p => p.People.Id == delete.Id));
            Assert.IsNull(ctx.PeopleFieldSet.Include("People").FirstOrDefault(p => p.People.Id == delete.Id));
            Assert.IsNull(ctx.SatisfactionClientSet.Include("PeopleDest").FirstOrDefault(p => p.PeopleDest.Id == delete.Id));
        }
    }
}