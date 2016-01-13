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
    public class BookingTest
    {
        private ManahostManagerDAL ctx;
        private BookingRepository repo;
        private Booking b;

        [TestInitialize]
        public void Init()
        {
            ctx = EFContext.CreateContext();
            repo = new BookingRepository(ctx);
            b = new Booking()
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
            repo.Add(b);
            repo.Save();

            using (UserManager<Client, int> manager = new ClientUserManager(new CustomUserStore(ctx)))
            {
                Assert.IsNull(repo.GetBookingById(b.Id, manager.FindByEmail("contact4@manahost.fr").Id));
            }
        }

        [TestMethod]
        public void DeleteBooking()
        {
            foreach (var cur in repo.GetList(p => p.HomeId == (int)ctx.HomeSet.FirstOrDefault(x => x.Title == "LaCorderie").ClientId))
            {
                List<int> dinnerBookingId = ctx.DinnerBookingSet.Include("Booking").Where(p => p.Booking.Id == cur.Id).Select(p => p.Id).ToList();
                List<int> roomBookingId = ctx.RoomBookingSet.Include("Booking").Where(p => p.Booking.Id == cur.Id).Select(p => p.Id).ToList();
                repo.Delete(cur);
                repo.Save();
                Assert.AreEqual(0, ctx.AdditionnalBookingSet.Include("Booking").Where(p => p.Booking.Id == cur.Id).ToList().Count);
                Assert.AreEqual(0, ctx.BillSet.Include("Booking").Where(p => p.Booking.Id == cur.Id).ToList().Count);
                Assert.AreEqual(0, ctx.BookingDocumentSet.Include("Booking").Where(p => p.Booking.Id == cur.Id).ToList().Count);
                Assert.AreEqual(0, ctx.BookingStepBookingSet.Where(p => p.Id == cur.Id).ToList().Count);
                Assert.AreEqual(0, ctx.DepositSet.Include("Booking").Where(p => p.Booking.Id == cur.Id).ToList().Count);
                Assert.AreEqual(0, ctx.DinnerBookingSet.Include("Booking").Where(p => p.Booking.Id == cur.Id).ToList().Count);
                foreach (var curdinner in dinnerBookingId)
                    Assert.AreEqual(0, ctx.MealBookingSet.Include("DinnerBooking").Where(p => p.DinnerBooking.Id == curdinner).ToList().Count);
                foreach (var curroom in roomBookingId)
                {
                    Assert.AreEqual(0, ctx.PeopleBookingSet.Include("RoomBooking").Where(p => p.RoomBooking.Id == curroom).ToList().Count);
                    Assert.AreEqual(0, ctx.SupplementRoomBookingSet.Include("RoomBooking").Where(p => p.RoomBooking.Id == curroom).ToList().Count);
                }
                Assert.AreEqual(0, ctx.ProductBookingSet.Include("Booking").Where(p => p.Booking.Id == cur.Id).ToList().Count);
                Assert.AreEqual(0, ctx.RoomBookingSet.Include("Booking").Where(p => p.Booking.Id == cur.Id).ToList().Count);
            }
        }
    }
}