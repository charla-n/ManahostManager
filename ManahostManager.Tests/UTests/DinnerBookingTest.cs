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
//    public class DinnerBookingTest
//    {
//        private DinnerBookingController.AdditionalRepositories addrepo;
//        private DinnerBookingRepositoryTest repo;

//        private ModelStateDictionary dict;
//        private DinnerBookingService s;

//        private DinnerBooking entity;

//        [TestInitialize]
//        public void Init()
//        {
//            addrepo = new DinnerBookingController.AdditionalRepositories(null);
//            repo = new DinnerBookingRepositoryTest();

//            dict = new ModelStateDictionary();
//            s = new DinnerBookingService(dict, repo, new DinnerBookingValidation());

//            addrepo.BookingRepo = new BookingRepositoryTest();
//            entity = new DinnerBooking()
//            {
//                Id = 1,
//                Date = new DateTime(2015, 1, 1),
//                NumberOfPeople = 3,
//                PriceHT = 10M,
//                PriceTTC = 10M,
//                HomeId = 1,
//                BookingId = 1004,
//                Booking = new Booking()
//                {
//                    DateArrival = new DateTime(2015, 1, 1),
//                    DateDeparture = new DateTime(2015, 1, 4)
//                },
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
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, TypeOfName.GetNameFromType<DinnerBooking>(),
//        GenericError.CANNOT_BE_NULL_OR_EMPTY);
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
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, TypeOfName.GetNameFromType<DinnerBooking>(),
//                    GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void PostShouldPass()
//        {
//            s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//            Assert.IsTrue(true);
//            Assert.AreEqual(entity.BookingId, repo.Entity.BookingId);
//            Assert.AreEqual(entity.Date, repo.Entity.Date);
//            Assert.AreEqual(entity.HomeId, repo.Entity.HomeId);
//            Assert.AreEqual(entity.Id, repo.Entity.Id);
//            Assert.AreEqual(entity.NumberOfPeople, repo.Entity.NumberOfPeople);
//            Assert.AreEqual(entity.PriceHT, repo.Entity.PriceHT);
//            Assert.AreEqual(entity.PriceTTC, repo.Entity.PriceTTC);
//        }

//        [TestMethod]
//        public void PutShouldPass()
//        {
//            entity.Id = 2;
//            entity.PriceTTC = 999M;
//            entity.PriceHT = 999M;

//            s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//            Assert.IsTrue(true);
//            Assert.AreEqual(entity.BookingId, repo.Entity.BookingId);
//            Assert.AreEqual(entity.Date, repo.Entity.Date);
//            Assert.AreEqual(entity.HomeId, repo.Entity.HomeId);
//            Assert.AreEqual(entity.Id, repo.Entity.Id);
//            Assert.AreEqual(entity.NumberOfPeople, repo.Entity.NumberOfPeople);
//            Assert.AreEqual(entity.PriceHT, repo.Entity.PriceHT);
//            Assert.AreEqual(entity.PriceTTC, repo.Entity.PriceTTC);
//        }

//        [TestMethod]
//        public void PutShouldntPassBecauseOfForbiddenResource()
//        {
//            entity.Id = 2;
//            entity.PriceTTC = 999M;
//            entity.PriceHT = 999M;
//            entity.HomeId = -1;
//            repo.Invalid = true;

//            try
//            {
//                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, "HomeId",
//GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }

//        [TestMethod]
//        public void PostShouldntPassBecauseOfForbiddenResource()
//        {
//            entity.PriceTTC = 999M;
//            entity.PriceHT = 999M;
//            entity.HomeId = -1;
//            repo.Invalid = true;
//            try
//            {
//                s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, "HomeId",
//GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }

//        [TestMethod]
//        public void PostShouldntPassBecauseOfNullBookingId()
//        {
//            entity.BookingId = null;
//            try
//            {
//                s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<DinnerBooking>(), "BookingId"),
//GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void PostShouldntPassBecauseOfNullDate()
//        {
//            entity.Date = null;
//            try
//            {
//                s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<DinnerBooking>(), "Date"),
//GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void PostShouldntPassBecauseOfForbiddenBookingId()
//        {
//            entity.BookingId = -1;
//            try
//            {
//                s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<DinnerBooking>(), "BookingId"),
//GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }

//        [TestMethod]
//        public void PostShouldntPassBecauseOfInvalidPriceHT()
//        {
//            entity.PriceHT = -1M;
//            try
//            {
//                s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<DinnerBooking>(), "PriceHT"),
//GenericError.DOES_NOT_MEET_REQUIREMENTS);
//            }
//        }

//        [TestMethod]
//        public void PostShouldntPassBecauseOfInvalidPriceTTC()
//        {
//            entity.PriceTTC = -1M;
//            try
//            {
//                s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<DinnerBooking>(), "PriceTTC"),
//GenericError.DOES_NOT_MEET_REQUIREMENTS);
//            }
//        }

//        [TestMethod]
//        public void PutShouldntPassBecauseOfInvalidPriceHT()
//        {
//            entity.PriceHT = -1M;

//            try
//            {
//                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<DinnerBooking>(), "PriceHT"),
//GenericError.DOES_NOT_MEET_REQUIREMENTS);
//            }
//        }

//        [TestMethod]
//        public void PutShouldntPassBecauseOfInvalidPriceTTC()
//        {
//            entity.PriceTTC = -1M;

//            try
//            {
//                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<DinnerBooking>(), "PriceTTC"),
//GenericError.DOES_NOT_MEET_REQUIREMENTS);
//            }
//        }

//        [TestMethod]
//        public void PutShouldntPassBecauseOfForbiddenBookingId()
//        {
//            entity.BookingId = -1;

//            try
//            {
//                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<DinnerBooking>(), "BookingId"),
//GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }

//        [TestMethod]
//        public void PutShouldntPassBecauseOfNullDate()
//        {
//            entity.Date = null;

//            try
//            {
//                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<DinnerBooking>(), "Date"),
//GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void PutShouldntPassBecauseOfNullBookingId()
//        {
//            entity.BookingId = null;

//            try
//            {
//                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<DinnerBooking>(), "BookingId"),
//GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void ComputeShouldPass()
//        {
//            s.Compute(new ClientRepositoryTest().FindUserByMail("test@test.com"), 1, addrepo);
//            Assert.IsTrue(true);
//            Assert.AreEqual(51, repo.Entity.PriceTTC);
//            Assert.AreEqual(50, repo.Entity.PriceHT);
//        }

//        [TestMethod]
//        public void ComputeShouldnPassBecauseOfNullMealPrice()
//        {
//            try
//            {
//                s.Compute(new ClientRepositoryTest().FindUserByMail("test@test.com"), 2, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<DinnerBooking>(), "MealPrice"),
//GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void DeleteShouldPass()
//        {
//            s.PreDelete(new ClientRepositoryTest().FindUserByMail("test@test.com"), 1, null);
//            Assert.IsTrue(true);
//        }

//        [TestMethod]
//        public void DeleteShouldntPassBecauseOfForbiddenResource()
//        {
//            try
//            {
//                s.PreDelete(new ClientRepositoryTest().FindUserByMail("test@test.com"), -1, null);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, TypeOfName.GetNameFromType<DinnerBooking>(),
//    GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }
//    }
//}