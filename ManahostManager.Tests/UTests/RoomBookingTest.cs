//using ManahostManager.Controllers;
//using ManahostManager.Domain.Entity;
//using ManahostManager.Services;
//using ManahostManager.Tests.Repository;
//using ManahostManager.Tests.UTests.UTsUtils;
//using ManahostManager.Utils;
//using ManahostManager.Validation;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System;
//using System.Web.Http.ModelBinding;

//namespace ManahostManager.Tests.UTests
//{
//    [TestClass]
//    public class RoomBookingTest
//    {
//        private RoomBookingController.AdditionalRepositories addrepo;
//        private RoomBookingRepositoryTest repo;

//        private ModelStateDictionary dict;
//        private RoomBookingService s;

//        private RoomBooking entity;

//        [TestInitialize]
//        public void Init()
//        {
//            addrepo = new RoomBookingController.AdditionalRepositories(null);
//            repo = new RoomBookingRepositoryTest();

//            dict = new ModelStateDictionary();
//            s = new RoomBookingService(dict, repo, new RoomBookingValidation());

//            addrepo.BookingRepo = new BookingRepositoryTest();
//            addrepo.RoomBookingRepo = repo;
//            addrepo.RoomRepo = new RoomRepositoryTest();
//            entity = new RoomBooking()
//            {
//                Id = 1,
//                RoomId = 1,
//                NbNights = 1,
//                HomeId = 1,
//                BookingId = 1004,
//                Booking = new Booking()
//                {
//                    DateArrival = new DateTime(2015, 1, 1),
//                    DateDeparture = new DateTime(2015, 1, 4)
//                },
//                DateBegin = new DateTime(2015, 1, 1),
//                DateEnd = new DateTime(2015, 1, 4)
//            };
//        }

//        [TestMethod]
//        public void PostShouldFailBecauseOfNull()
//        {
//            try
//            {
//                s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), null, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, TypeOfName.GetNameFromType<RoomBooking>(),
//                        GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void PutShouldFailBecauseOfNull()
//        {
//            try
//            {
//                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), null, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, TypeOfName.GetNameFromType<RoomBooking>(),
//                        GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void ShouldPassDelete()
//        {
//            s.PreDelete(new ClientRepositoryTest().FindUserByMail("test@test.com"), 1, addrepo);
//            Assert.IsTrue(true);
//        }

//        [TestMethod]
//        public void ShouldntPassDeleteBecauseOfForbiddenResource()
//        {
//            try
//            {
//                s.PreDelete(new ClientRepositoryTest().FindUserByMail("test@test.com"), -1, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, TypeOfName.GetNameFromType<RoomBooking>(),
//        GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }

//        [TestMethod]
//        public void PostShouldFailBecauseOfForbiddenHomeId()
//        {
//            try
//            {
//                entity.HomeId = -1;
//                repo.Invalid = true;
//                s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, "HomeId",
//    GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }

//        [TestMethod]
//        public void PutShouldFailBecauseOfForbiddenHomeId()
//        {
//            try
//            {
//                entity.HomeId = -1;
//                repo.Invalid = true;
//                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, "HomeId",
//    GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }

//        [TestMethod]
//        public void PostShouldPass()
//        {
//            RoomBooking saved = ObjectCopier.Clone<RoomBooking>(entity);
//            s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//            Assert.IsTrue(true);
//            Assert.AreEqual(saved.BookingId, repo.Entity.BookingId);
//            Assert.AreEqual(saved.HomeId, repo.Entity.HomeId);
//            Assert.AreEqual(saved.Id, repo.Entity.Id);
//            Assert.AreEqual(3, repo.Entity.NbNights);
//            Assert.AreEqual(0, repo.Entity.PriceHT);
//            Assert.AreEqual(0, repo.Entity.PriceTTC);
//            Assert.AreEqual(saved.RoomId, repo.Entity.RoomId);
//        }

//        [TestMethod]
//        public void PostShouldPassWithNullDateBeginAndDateEnd()
//        {
//            RoomBooking saved = ObjectCopier.Clone<RoomBooking>(entity);
//            entity.DateBegin = null;
//            entity.DateEnd = null;
//            s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//            Assert.IsTrue(true);
//            Assert.AreEqual(saved.BookingId, repo.Entity.BookingId);
//            Assert.AreEqual(saved.HomeId, repo.Entity.HomeId);
//            Assert.AreEqual(saved.Id, repo.Entity.Id);
//            Assert.AreEqual(3, repo.Entity.NbNights);
//            Assert.AreEqual(0, repo.Entity.PriceHT);
//            Assert.AreEqual(0, repo.Entity.PriceTTC);
//            Assert.AreEqual(new DateTime(2015, 1, 1), repo.Entity.DateBegin);
//            Assert.AreEqual(new DateTime(2015, 1, 4), repo.Entity.DateEnd);
//            Assert.AreEqual(saved.RoomId, repo.Entity.RoomId);
//        }

//        [TestMethod]
//        public void PutShouldPass()
//        {
//            entity.NbNights = 10;
//            entity.PriceHT = 500M;
//            entity.PriceTTC = 600M;
//            RoomBooking saved = ObjectCopier.Clone<RoomBooking>(entity);
//            s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//            Assert.IsTrue(true);
//            Assert.AreEqual(saved.BookingId, repo.Entity.BookingId);
//            Assert.AreEqual(saved.HomeId, repo.Entity.HomeId);
//            Assert.AreEqual(saved.Id, repo.Entity.Id);
//            Assert.AreEqual(3, repo.Entity.NbNights);
//            Assert.AreEqual(500M, repo.Entity.PriceHT);
//            Assert.AreEqual(600M, repo.Entity.PriceTTC);
//            Assert.AreEqual(saved.RoomId, repo.Entity.RoomId);
//        }

//        [TestMethod]
//        public void PostShouldntPassBecauseOfNullRoomId()
//        {
//            try
//            {
//                entity.RoomId = null;
//                s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<RoomBooking>(), "RoomId"),
//    GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void PutShouldntPassBecauseOfNullRoomId()
//        {
//            try
//            {
//                entity.PriceHT = 0M;
//                entity.PriceTTC = 0M;
//                entity.RoomId = null;
//                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<RoomBooking>(), "RoomId"),
//    GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void PutShouldntPassBecauseOfNullPriceHt()
//        {
//            try
//            {
//                entity.PriceTTC = 0M;
//                entity.PriceHT = null;
//                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<RoomBooking>(), "PriceHT"),
//    GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void PutShouldntPassBecauseOfNullPriceTTC()
//        {
//            try
//            {
//                entity.PriceHT = 0M;
//                entity.PriceTTC = null;
//                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<RoomBooking>(), "PriceTTC"),
//    GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void PutShouldntPassBecauseOfNullBookingId()
//        {
//            try
//            {
//                entity.PriceHT = 0M;
//                entity.PriceTTC = 0M;
//                entity.BookingId = null;
//                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<RoomBooking>(), "BookingId"),
//    GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void PostShouldntPassBecauseOfNullBookingId()
//        {
//            try
//            {
//                entity.BookingId = null;
//                s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<RoomBooking>(), "BookingId"),
//    GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void PutShouldntPassBecauseOfForbiddenBookingId()
//        {
//            try
//            {
//                entity.PriceHT = 0M;
//                entity.PriceTTC = 0M;
//                entity.BookingId = -1;
//                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<RoomBooking>(), "BookingId"),
//    GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }

//        [TestMethod]
//        public void PostShouldntPassBecauseOfForbiddenBookingId()
//        {
//            try
//            {
//                entity.BookingId = -1;
//                s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<RoomBooking>(), "BookingId"),
//    GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }

//        [TestMethod]
//        public void PutShouldntPassBecauseOfForbiddenRoomId()
//        {
//            try
//            {
//                entity.PriceHT = 0M;
//                entity.PriceTTC = 0M;
//                entity.RoomId = -1;
//                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<RoomBooking>(), "RoomId"),
//    GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }

//        [TestMethod]
//        public void PostShouldntPassBecauseOfForbiddenRoomId()
//        {
//            try
//            {
//                entity.RoomId = -1;
//                s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<RoomBooking>(), "RoomId"),
//    GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }

//        [TestMethod]
//        public void PutShouldntPassBecauseOfInvalidPriceHT()
//        {
//            try
//            {
//                entity.PriceTTC = 0M;
//                entity.PriceHT = -1;
//                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<RoomBooking>(), "PriceHT"),
//    GenericError.DOES_NOT_MEET_REQUIREMENTS);
//            }
//        }

//        [TestMethod]
//        public void PostShouldntPassBecauseOfInvalidPriceHT()
//        {
//            try
//            {
//                entity.PriceTTC = 0M;
//                entity.PriceHT = -1M;
//                s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<RoomBooking>(), "PriceHT"),
//    GenericError.DOES_NOT_MEET_REQUIREMENTS);
//            }
//        }

//        [TestMethod]
//        public void PostShouldntPassBecauseOfInvalidDateBegin()
//        {
//            try
//            {
//                entity.DateBegin = DateTime.MinValue;
//                s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<RoomBooking>(), "DateBegin"),
//    GenericError.DOES_NOT_MEET_REQUIREMENTS);
//            }
//            try
//            {
//                entity.DateBegin = DateTime.MaxValue;
//                s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<RoomBooking>(), "DateBegin"),
//    GenericError.DOES_NOT_MEET_REQUIREMENTS);
//            }
//            try
//            {
//                entity.DateEnd = new DateTime(2015, 1, 2);
//                entity.DateBegin = new DateTime(2015, 1, 2);
//                s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<RoomBooking>(), "DateBegin"),
//    GenericError.DOES_NOT_MEET_REQUIREMENTS);
//            }
//        }

//        [TestMethod]
//        public void PutShouldntPassBecauseOfInvalidDateBegin()
//        {
//            try
//            {
//                entity.PriceHT = 0M;
//                entity.PriceTTC = 0M;
//                entity.DateBegin = DateTime.MinValue;
//                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<RoomBooking>(), "DateBegin"),
//    GenericError.DOES_NOT_MEET_REQUIREMENTS);
//            }
//            try
//            {
//                entity.DateBegin = DateTime.MaxValue;
//                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<RoomBooking>(), "DateBegin"),
//    GenericError.DOES_NOT_MEET_REQUIREMENTS);
//            }
//            try
//            {
//                entity.DateEnd = new DateTime(2015, 1, 2);
//                entity.DateBegin = new DateTime(2015, 1, 2);
//                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<RoomBooking>(), "DateBegin"),
//    GenericError.DOES_NOT_MEET_REQUIREMENTS);
//            }
//        }

//        [TestMethod]
//        public void PutShouldntPassBecauseOfInvalidPriceTTC()
//        {
//            try
//            {
//                entity.PriceHT = 0M;
//                entity.PriceTTC = -1M;
//                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<RoomBooking>(), "PriceTTC"),
//    GenericError.DOES_NOT_MEET_REQUIREMENTS);
//            }
//        }

//        [TestMethod]
//        public void PostShouldntPassBecauseOfPriceTTC()
//        {
//            try
//            {
//                entity.PriceTTC = -1M;
//                s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<RoomBooking>(), "PriceTTC"),
//    GenericError.DOES_NOT_MEET_REQUIREMENTS);
//            }
//        }

//        [TestMethod]
//        public void PutShouldntPassBecauseOfRoomAlreadyBooked()
//        {
//            try
//            {
//                entity.PriceHT = 0M;
//                entity.PriceTTC = 0M;
//                entity.RoomId = 2;
//                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<RoomBooking>(), "RoomId"),
//    GenericError.ALREADY_EXISTS);
//            }
//        }

//        [TestMethod]
//        public void PutShouldntPassBecauseOfNullDateBegin()
//        {
//            try
//            {
//                entity.PriceHT = 0M;
//                entity.PriceTTC = 0M;
//                entity.DateBegin = null;
//                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<RoomBooking>(), "DateBegin"),
//    GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void PutShouldntPassBecauseOfNullDateEnd()
//        {
//            try
//            {
//                entity.PriceHT = 0M;
//                entity.PriceTTC = 0M;
//                entity.DateEnd = null;
//                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<RoomBooking>(), "DateEnd"),
//    GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void PostShouldntPassBecauseOfRoomAlreadyBooked()
//        {
//            try
//            {
//                entity.RoomId = 2;
//                s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<RoomBooking>(), "RoomId"),
//    GenericError.ALREADY_EXISTS);
//            }
//        }

//        [TestMethod]
//        public void ComputeShouldntPassBecauseOfForbiddenRoomBooking()
//        {
//            try
//            {
//                s.Compute(new ClientRepositoryTest().FindUserByMail("test@test.com"), -1, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, TypeOfName.GetNameFromType<RoomBooking>(),
//    GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }

//        [TestMethod]
//        public void ComputeShouldntPassBecauseOfTooManyPeopleBooking()
//        {
//            try
//            {
//                s.Compute(new ClientRepositoryTest().FindUserByMail("test@test.com"), 4, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<RoomBooking>(), "PeopleBooking"),
//    GenericError.DOES_NOT_MEET_REQUIREMENTS);
//            }
//        }

//        [TestMethod]
//        public void ComputeShouldPassMultiplePeopleCategory()
//        {
//            s.Compute(new ClientRepositoryTest().FindUserByMail("test@test.com"), 2, addrepo);
//            Assert.IsTrue(true);
//            Assert.AreEqual(1, repo.Entity.BookingId);
//            Assert.AreEqual(entity.HomeId, repo.Entity.HomeId);
//            Assert.AreEqual(2, repo.Entity.Id);
//            Assert.AreEqual(6, repo.Entity.NbNights);
//            Assert.AreEqual(1900M, repo.Entity.PriceHT);
//            Assert.AreEqual(2205M, repo.Entity.PriceTTC);
//            Assert.AreEqual(entity.RoomId, repo.Entity.RoomId);
//        }

//        [TestMethod]
//        public void ComputeShouldPassMultiplePeopleCategoryLeavingEarlier()
//        {
//            s.Compute(new ClientRepositoryTest().FindUserByMail("test@test.com"), 5, addrepo);
//            Assert.IsTrue(true);
//            Assert.AreEqual(1, repo.Entity.BookingId);
//            Assert.AreEqual(entity.HomeId, repo.Entity.HomeId);
//            Assert.AreEqual(5, repo.Entity.Id);
//            Assert.AreEqual(6, repo.Entity.NbNights);
//            Assert.AreEqual(1550M, repo.Entity.PriceHT);
//            Assert.AreEqual(1820M, repo.Entity.PriceTTC);
//            Assert.AreEqual(entity.RoomId, repo.Entity.RoomId);
//        }

//        [TestMethod]
//        public void ComputeShouldPassNoPeopleCategory()
//        {
//            s.Compute(new ClientRepositoryTest().FindUserByMail("test@test.com"), 3, addrepo);
//            Assert.IsTrue(true);
//            Assert.AreEqual(1, repo.Entity.BookingId);
//            Assert.AreEqual(entity.HomeId, repo.Entity.HomeId);
//            Assert.AreEqual(3, repo.Entity.Id);
//            Assert.AreEqual(6, repo.Entity.NbNights);
//            Assert.AreEqual(325M, repo.Entity.PriceHT);
//            Assert.AreEqual(357.5M, repo.Entity.PriceTTC);
//            Assert.AreEqual(entity.RoomId, repo.Entity.RoomId);
//        }
//    }
//}