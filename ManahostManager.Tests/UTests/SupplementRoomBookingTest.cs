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
//    public class SupplementSupplementRoomBookingTest
//    {
//        private SupplementRoomBookingController.AdditionalRepositories addrepo;
//        private SupplementRoomBookingRepositoryTest repo;

//        private ModelStateDictionary dict;
//        private SupplementRoomBookingService s;

//        private SupplementRoomBooking entity;

//        [TestInitialize]
//        public void Init()
//        {
//            addrepo = new SupplementRoomBookingController.AdditionalRepositories(null);
//            repo = new SupplementRoomBookingRepositoryTest();

//            dict = new ModelStateDictionary();
//            s = new SupplementRoomBookingService(dict, repo, new SupplementRoomBookingValidation());

//            addrepo.RoomBookingRepo = new RoomBookingRepositoryTest();
//            addrepo.RoomSupplementRepo = new RoomSupplementRepositoryTest();
//            entity = new SupplementRoomBooking()
//            {
//                HomeId = 1,
//                Id = 1,
//                PriceHT = 10M,
//                PriceTTC = 11M,
//                RoomBookingId = 1,
//                RoomSupplementId = 1
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
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, TypeOfName.GetNameFromType<SupplementRoomBooking>(),
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
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, TypeOfName.GetNameFromType<SupplementRoomBooking>(),
//                        GenericError.CANNOT_BE_NULL_OR_EMPTY);
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
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, TypeOfName.GetNameFromType<SupplementRoomBooking>(),
//        GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }

//        [TestMethod]
//        public void PostShouldFailBecauseOfForbiddenHome()
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
//                        GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }

//        [TestMethod]
//        public void PutShouldFailBecauseOfForbiddenHome()
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
//                        GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }

//        [TestMethod]
//        public void PostShouldFailBecauseOfInvalidPriceHT()
//        {
//            try
//            {
//                entity.PriceHT = -1M;
//                s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<SupplementRoomBooking>(), "PriceHT"),
//                        GenericError.DOES_NOT_MEET_REQUIREMENTS);
//            }
//        }

//        [TestMethod]
//        public void PutShouldFailBecauseOfInvalidPriceHT()
//        {
//            try
//            {
//                entity.PriceHT = -1M;

//                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<SupplementRoomBooking>(), "PriceHT"),
//                        GenericError.DOES_NOT_MEET_REQUIREMENTS);
//            }
//        }

//        [TestMethod]
//        public void PostShouldFailBecauseOfInvalidPriceTTC()
//        {
//            try
//            {
//                entity.PriceTTC = -1M;
//                s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<SupplementRoomBooking>(), "PriceTTC"),
//                        GenericError.DOES_NOT_MEET_REQUIREMENTS);
//            }
//        }

//        [TestMethod]
//        public void PutShouldFailBecauseOfInvalidPriceTTC()
//        {
//            try
//            {
//                entity.PriceTTC = -1M;

//                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<SupplementRoomBooking>(), "PriceTTC"),
//                        GenericError.DOES_NOT_MEET_REQUIREMENTS);
//            }
//        }

//        [TestMethod]
//        public void PostShouldFailBecauseOfNullRoomBookingId()
//        {
//            try
//            {
//                entity.RoomBookingId = null;
//                s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<SupplementRoomBooking>(), "RoomBookingId"),
//                        GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void PutShouldFailBecauseOfNullRoomBookingId()
//        {
//            try
//            {
//                entity.RoomBookingId = null;

//                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<SupplementRoomBooking>(), "RoomBookingId"),
//                        GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void PostShouldFailBecauseOfNullRoomSupplementId()
//        {
//            try
//            {
//                entity.RoomSupplementId = null;
//                s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<SupplementRoomBooking>(), "RoomSupplementId"),
//                        GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void PutShouldFailBecauseOfNullRoomSupplementId()
//        {
//            try
//            {
//                entity.RoomSupplementId = null;

//                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<SupplementRoomBooking>(), "RoomSupplementId"),
//                        GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void PostShouldFailBecauseOfForbiddenRoomBooking()
//        {
//            try
//            {
//                entity.RoomBookingId = -1;
//                s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<SupplementRoomBooking>(), "RoomBookingId"),
//                        GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }

//        [TestMethod]
//        public void PutShouldFailBecauseOfForbiddenRoomBooking()
//        {
//            try
//            {
//                entity.RoomBookingId = -1;

//                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<SupplementRoomBooking>(), "RoomBookingId"),
//                        GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }

//        [TestMethod]
//        public void PostShouldFailBecauseOfForbiddenRoomSupplement()
//        {
//            try
//            {
//                entity.RoomSupplementId = -1;
//                s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<SupplementRoomBooking>(), "RoomSupplementId"),
//                        GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }

//        [TestMethod]
//        public void PutShouldFailBecauseOfForbiddenRoomSupplement()
//        {
//            try
//            {
//                entity.RoomSupplementId = -1;

//                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<SupplementRoomBooking>(), "RoomSupplementId"),
//                        GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }

//        [TestMethod]
//        public void PostShouldPass()
//        {
//            s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), ObjectCopier.Clone<SupplementRoomBooking>(entity), addrepo);
//            Assert.IsTrue(true);
//            Assert.AreEqual(entity.HomeId, repo.Entity.HomeId);
//            Assert.AreEqual(entity.Id, repo.Entity.Id);
//            Assert.AreEqual(entity.PriceHT, repo.Entity.PriceHT);
//            Assert.AreEqual(entity.PriceTTC, repo.Entity.PriceTTC);
//            Assert.AreEqual(entity.RoomBookingId, repo.Entity.RoomBookingId);
//            Assert.AreEqual(entity.RoomSupplementId, repo.Entity.RoomSupplementId);
//        }

//        [TestMethod]
//        public void PutShouldPass()
//        {
//            entity.PriceHT = 999M;
//            s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), ObjectCopier.Clone<SupplementRoomBooking>(entity), addrepo);
//            Assert.IsTrue(true);
//            Assert.AreEqual(entity.HomeId, repo.Entity.HomeId);
//            Assert.AreEqual(entity.Id, repo.Entity.Id);
//            Assert.AreEqual(entity.PriceHT, repo.Entity.PriceHT);
//            Assert.AreEqual(entity.PriceTTC, repo.Entity.PriceTTC);
//            Assert.AreEqual(entity.RoomBookingId, repo.Entity.RoomBookingId);
//            Assert.AreEqual(entity.RoomSupplementId, repo.Entity.RoomSupplementId);
//        }

//        [TestMethod]
//        public void ComputeShouldPass()
//        {
//            s.Compute(new ClientRepositoryTest().FindUserByMail("test@test.com"), 1, addrepo);
//            Assert.IsTrue(true);
//            Assert.AreEqual(entity.HomeId, repo.Entity.HomeId);
//            Assert.AreEqual(entity.Id, repo.Entity.Id);
//            Assert.AreEqual(555M, repo.Entity.PriceHT);
//            Assert.AreEqual(1110M, repo.Entity.PriceTTC);
//            Assert.AreEqual(entity.RoomBookingId, repo.Entity.RoomBookingId);
//            Assert.AreEqual(entity.RoomSupplementId, repo.Entity.RoomSupplementId);
//        }
//    }
//}