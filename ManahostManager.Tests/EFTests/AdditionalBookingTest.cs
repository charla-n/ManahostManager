using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;
using ManahostManager.Domain.Repository;
using ManahostManager.Domain.Tools;
using Microsoft.AspNet.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ManahostManager.Tests.EFTests
{
    [TestClass]
    public class AdditionalBookingTest
    {
        private ManahostManagerDAL ctx;
        private AdditionalBookingRepository repo;
        private AdditionalBooking entity;

        [TestInitialize]
        public void Init()
        {
            ctx = EFContext.CreateContext();
            repo = new AdditionalBookingRepository(ctx);
            entity = new AdditionalBooking()
            {
                BillItemCategory = new BillItemCategory()
                {
                    DateModification = DateTime.Now,
                    Home = ctx.HomeSet.FirstOrDefault(x => x.Title == "LaCorderie"),
                    Title = "EFTest"
                },
                Booking = new Booking()
                {
                    Comment = "I am a comment",
                    DateArrival = DateTime.Now.AddYears(1),
                    DateCreation = DateTime.Now,
                    DateDeparture = DateTime.Now.AddYears(3),
                    DateDesiredPayment = DateTime.Now.AddYears(3).AddMonths(1),
                    DateModification = DateTime.Now,
                    DateValidation = DateTime.Now.AddMonths(4),
                    Home = ctx.HomeSet.FirstOrDefault(x => x.Title == "LaCorderie"),
                    IsOnline = false,
                    IsSatisfactionSended = false,
                    People = new People()
                    {
                        AcceptMailing = true,
                        Addr = "4 place kleber",
                        City = "Strasbourg",
                        Civility = "Mr",
                        Comment = "A mis le feu à la chambre",
                        Country = "FRANCE",
                        DateBirth = DateTime.Now,
                        DateCreation = DateTime.Now,
                        Email = "contact@manahost.fr",
                        Firstname = "CHAABANE",
                        Home = ctx.HomeSet.FirstOrDefault(x => x.Title == "LaCorderie"),
                        Lastname = "Jalal",
                        Mark = 0,
                        Phone1 = "0600000000",
                        Phone2 = null,
                        State = null,
                        ZipCode = "67000",
                        Hide = false
                    },
                    TotalPeople = 4
                },
                DateModification = DateTime.Now,
                Home = ctx.HomeSet.FirstOrDefault(x => x.Title == "LaCorderie"),
                PriceHT = 999M,
                PriceTTC = 999M,
                Tax = new Tax()
                {
                    DateModification = DateTime.Now,
                    Home = ctx.HomeSet.FirstOrDefault(x => x.Title == "LaCorderie"),
                    Price = 50M,
                    Title = "EFTest",
                    ValueType = EValueType.PERCENT
                },
                Title = "EFTest"
            };
        }

        [TestMethod]
        public void ForbiddenResource()
        {
            repo.Add(entity);
            repo.Save();

            using (ClientUserManager manager = new ClientUserManager(new CustomUserStore(ctx)))
            {
                Assert.IsNull(repo.GetAdditionalBookingById(entity.Id, manager.FindByEmail("contact4@manahost.fr").Id));
            }
        }

        [TestMethod]
        public void DeleteAdditionalBooking()
        {
            repo.includes.Add("Booking");
            IEnumerable<AdditionalBooking> listAdditionalBooking = repo.GetList(p => p.HomeId == (int)ctx.HomeSet.FirstOrDefault(x => x.Title == "LaCorderie").ClientId);

            foreach (AdditionalBooking cur in listAdditionalBooking)
            {
                int savedBookingId = (int)cur.Booking.Id;
                int savedId = cur.Id;
                repo.Delete(cur);
                repo.Save();
                Assert.IsNotNull(repo.GetUniq<Booking>(p => p.Id == savedBookingId));
                Assert.IsNull(repo.GetUniq(p => p.Id == savedId));
            }
        }
    }
}