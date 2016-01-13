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
    public class BookingStepBookingTest
    {
        private ManahostManagerDAL ctx;
        private BookingStepBookingRepository repo;
        private BookingStepBooking entity;

        [TestInitialize]
        public void Init()
        {
            ctx = EFContext.CreateContext();
            repo = new BookingStepBookingRepository(ctx);
            BookingStepConfig tmpconfig = new BookingStepConfig()
            {
                Home = ctx.HomeSet.FirstOrDefault(x => x.Title == "LaCorderie"),
                Title = "EFTest"
            };
            entity = new BookingStepBooking()
            {
                Booking = new Booking()
                {
                    Comment = "I am a comment",
                    DateArrival = DateTime.Now.AddYears(4),
                    DateCreation = DateTime.Now,
                    DateDeparture = DateTime.Now.AddYears(8),
                    DateDesiredPayment = DateTime.Now.AddYears(1).AddMonths(10),
                    DateModification = DateTime.Now,
                    DateValidation = DateTime.Now.AddMonths(18),
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
                        Hide = false,
                    },
                    TotalPeople = 4
                },
                BookingStepConfig = tmpconfig,
                CurrentStep = new BookingStep()
                {
                    Home = ctx.HomeSet.FirstOrDefault(x => x.Title == "LaCorderie"),
                    BookingStepConfig = tmpconfig,
                    Title = "EFTest",
                    BookingValidated = true,
                    BookingArchived = false
                },
                Home = ctx.HomeSet.FirstOrDefault(x => x.Title == "LaCorderie"),
                DateCurrentStepChanged = DateTime.Now,
                MailSent = 0,
                Canceled = false
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

            using (ClientUserManager manager = new ClientUserManager(new CustomUserStore(ctx)))
            {
                Assert.IsNull(repo.GetBookingStepBookingById(entity.Id, manager.FindByEmail("contact4@manahost.fr").Id));
            }
        }

        [TestMethod]
        public void DeletBookingStepBooking()
        {
            IEnumerable<BookingStepBooking> listBooking = repo.GetList(p => p.HomeId == (int)ctx.HomeSet.FirstOrDefault(x => x.Title == "LaCorderie").ClientId);

            foreach (var cur in listBooking)
            {
                repo.Delete(cur);
                repo.Save();
                Assert.IsNotNull(repo.GetUniq<Booking>(p => p.Id == cur.Id));
            }
        }
    }
}