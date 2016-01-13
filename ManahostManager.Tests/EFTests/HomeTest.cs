using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;
using ManahostManager.Domain.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace ManahostManager.Tests.EFTests
{
    [TestClass]
    public class HomeTest
    {
        private ManahostManagerDAL ctx;
        private HomeRepository repo;
        private Home entity;

        [TestInitialize]
        public void Init()
        {
            ctx = EFContext.CreateContext();
            repo = new HomeRepository(ctx);
            repo.includes = new List<string>();
            entity = new Home()
            {
                ClientId = 1,
                EstablishmentType = EEstablishmentType.BB,
                Title = "La corderie TEST"
            };
        }

        [TestCleanup]
        public void Cleanup()
        {
            ctx.Dispose();
        }

        [TestMethod]
        public void ShouldNotGetBecauseOfForbiddenResource()
        {
            repo.Add(entity);
            repo.Save();

            Home get = repo.GetHomeById(entity.Id, 2);

            Assert.IsNull(get);

            entity.ClientId = 2;
            repo.Add(entity);
            repo.Save();

            Home get2 = repo.GetHomeById(entity.Id, 2);

            Assert.IsNotNull(get2);
        }

        [TestMethod]
        public void EntitiesShouldBeFilled()
        {
            Home g = ctx.HomeSet.FirstOrDefault(p => p.Title == "LaCorderie");

            Assert.IsNotNull(ctx.AdditionnalBookingSet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNotNull(ctx.BedSet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNotNull(ctx.BillItemCategorySet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNotNull(ctx.BillItemSet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNotNull(ctx.BillSet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNotNull(ctx.BookingDocumentSet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNotNull(ctx.BookingSet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNotNull(ctx.BookingStepConfigSet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNotNull(ctx.BookingStepSet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNotNull(ctx.DepositSet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNotNull(ctx.DinnerBookingSet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNotNull(ctx.DocumentCategorySet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNotNull(ctx.DocumentSet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNotNull(ctx.GroupBillItemSet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNotNull(ctx.FieldGroupSet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNotNull(ctx.HomeConfigSet.FirstOrDefault(p => p.Id == g.Id));
            Assert.IsNotNull(ctx.KeyGeneratorSet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNotNull(ctx.MailConfigSet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNotNull(ctx.MealBookingSet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNotNull(ctx.MealCategorySet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNotNull(ctx.MealPriceSet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNotNull(ctx.MealSet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNotNull(ctx.PaymentMethodSet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNotNull(ctx.PaymentTypeSet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNotNull(ctx.PeopleBookingSet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNotNull(ctx.PeopleCategorySet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNotNull(ctx.PeopleFieldSet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNotNull(ctx.PeopleSet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNotNull(ctx.PeriodSet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNotNull(ctx.PricePerPersonSet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNotNull(ctx.ProductBookingSet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNotNull(ctx.ProductCategorySet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNotNull(ctx.ProductSet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNotNull(ctx.RoomBookingSet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNotNull(ctx.RoomCategorySet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNotNull(ctx.RoomSet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNotNull(ctx.RoomSupplementSet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNotNull(ctx.SatisfactionClientAnsweredSet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNotNull(ctx.SatisfactionClientSet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNotNull(ctx.SatisfactionConfigQuestionSet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNotNull(ctx.SatisfactionConfigSet.FirstOrDefault(p => p.Id == g.Id));
            Assert.IsNotNull(ctx.StatisticsSet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNotNull(ctx.SupplementRoomBookingSet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNotNull(ctx.SupplierSet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNotNull(ctx.TaxSet.FirstOrDefault(p => p.HomeId == g.Id));
        }

        [TestMethod]
        public void DeleteHome()
        {
            ClientRepository repoC = new ClientRepository(ctx);
            Home g = ctx.HomeSet.FirstOrDefault(p => p.Title == "LaCorderie");

            repo.Delete(g);
            repo.Save();

            Home nd = ctx.HomeSet.FirstOrDefault(p => p.Title == "La Taverne");
            Assert.IsNotNull(repoC.FindUserByMail("contact@manahost.fr"));
            Assert.IsNotNull(repoC.FindUserByMail("contact2@manahost.fr"));
            Assert.IsNotNull(repoC.FindUserByMail("contact3@manahost.fr"));
            Assert.IsNull(ctx.HomeSet.FirstOrDefault(p => p.Title == "LaCorderie"));
            Assert.IsNotNull(nd);
            Assert.IsNull(ctx.AdditionnalBookingSet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNull(ctx.BedSet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNull(ctx.BillItemCategorySet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNull(ctx.BillItemSet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNull(ctx.BillSet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNull(ctx.BookingDocumentSet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNull(ctx.BookingSet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNull(ctx.BookingStepConfigSet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNull(ctx.BookingStepSet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNull(ctx.DepositSet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNull(ctx.DinnerBookingSet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNull(ctx.DocumentCategorySet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNull(ctx.DocumentSet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNull(ctx.GroupBillItemSet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNull(ctx.FieldGroupSet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNull(ctx.HomeConfigSet.FirstOrDefault(p => p.Id == g.Id));
            Assert.IsNull(ctx.KeyGeneratorSet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNull(ctx.MailConfigSet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNull(ctx.MealBookingSet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNull(ctx.MealCategorySet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNull(ctx.MealPriceSet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNull(ctx.MealSet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNull(ctx.PaymentMethodSet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNull(ctx.PaymentTypeSet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNull(ctx.PeopleBookingSet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNull(ctx.PeopleCategorySet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNull(ctx.PeopleFieldSet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNull(ctx.PeopleSet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNull(ctx.PeriodSet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNull(ctx.PricePerPersonSet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNull(ctx.ProductBookingSet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNull(ctx.ProductCategorySet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNull(ctx.ProductSet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNull(ctx.RoomBookingSet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNull(ctx.RoomCategorySet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNull(ctx.RoomSet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNull(ctx.RoomSupplementSet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNull(ctx.SatisfactionClientAnsweredSet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNull(ctx.SatisfactionClientSet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNull(ctx.SatisfactionConfigQuestionSet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNull(ctx.SatisfactionConfigSet.FirstOrDefault(p => p.Id == g.Id));
            Assert.IsNull(ctx.StatisticsSet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNull(ctx.SupplementRoomBookingSet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNull(ctx.SupplierSet.FirstOrDefault(p => p.HomeId == g.Id));
            Assert.IsNull(ctx.TaxSet.FirstOrDefault(p => p.HomeId == g.Id));
        }
    }
}