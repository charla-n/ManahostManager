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
//    public class MealBookingTest
//    {
//        private MealBookingController.AdditionalRepositories addrepo;
//        private MealBookingRepositoryTest repo;

//        private ModelStateDictionary dict;
//        private MealBookingService s;

//        private MealBooking entity;

//        [TestInitialize]
//        public void Init()
//        {
//            addrepo = new MealBookingController.AdditionalRepositories(null);
//            repo = new MealBookingRepositoryTest();

//            dict = new ModelStateDictionary();
//            s = new MealBookingService(dict, repo, new MealBookingValidation());

//            addrepo.DinnerBookingRepo = new DinnerBookingRepositoryTest();
//            addrepo.MealRepo = new MealRepositoryTest();
//            addrepo.PeopleCategoryRepo = new PeopleCategoryRepositoryTest();
//            entity = new MealBooking()
//            {
//                DinnerBookingId = 1,
//                HomeId = 1,
//                Id = 1,
//                MealId = 1,
//                NumberOfPeople = 10,
//                PeopleCategoryId = 10
//            };
//        }

//        [TestMethod]
//        public void PostShouldFAilBecauseOfNull()
//        {
//            try
//            {
//                s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), null, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, TypeOfName.GetNameFromType<MealBooking>(),
//                        GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void PutShouldFAilBecauseOfNull()
//        {
//            try
//            {
//                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), null, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, TypeOfName.GetNameFromType<MealBooking>(),
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
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, TypeOfName.GetNameFromType<MealBooking>(),
//                        GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }

//        [TestMethod]
//        public void PostShouldPass()
//        {
//            entity.NumberOfPeople = null;
//            s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//            Assert.IsTrue(true);
//            Assert.AreEqual(entity.DinnerBookingId, repo.Entity.DinnerBookingId);
//            Assert.AreEqual(entity.HomeId, repo.Entity.HomeId);
//            Assert.AreEqual(entity.Id, repo.Entity.Id);
//            Assert.AreEqual(entity.MealId, repo.Entity.MealId);
//            Assert.AreEqual(1, repo.Entity.NumberOfPeople);
//            Assert.AreEqual(entity.PeopleCategoryId, repo.Entity.PeopleCategoryId);
//        }

//        [TestMethod]
//        public void PutShouldPass()
//        {
//            entity.NumberOfPeople = 111;
//            entity.PeopleCategoryId = 10;
//            s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//            Assert.IsTrue(true);
//            Assert.AreEqual(entity.DinnerBookingId, repo.Entity.DinnerBookingId);
//            Assert.AreEqual(entity.HomeId, repo.Entity.HomeId);
//            Assert.AreEqual(entity.Id, repo.Entity.Id);
//            Assert.AreEqual(entity.MealId, repo.Entity.MealId);
//            Assert.AreEqual(entity.NumberOfPeople, repo.Entity.NumberOfPeople);
//            Assert.AreEqual(entity.PeopleCategoryId, repo.Entity.PeopleCategoryId);
//        }

//        [TestMethod]
//        public void PostShouldntPassBecauseOfNullDinnerBookingId()
//        {
//            entity.DinnerBookingId = null;
//            try
//            {
//                s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<MealBooking>(), "DinnerBookingId"),
//        GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void PutShouldntPassBecauseOfNullDinnerBookingId()
//        {
//            entity.DinnerBookingId = null;
//            entity.PeopleCategoryId = 10;

//            try
//            {
//                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<MealBooking>(), "DinnerBookingId"),
//        GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void PostShouldntPassBecauseOfNullMealId()
//        {
//            entity.MealId = null;
//            try
//            {
//                s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<MealBooking>(), "MealId"),
//        GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void PutShouldntPassBecauseOfNullMealId()
//        {
//            entity.MealId = null;
//            entity.PeopleCategoryId = 10;

//            try
//            {
//                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<MealBooking>(), "MealId"),
//        GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void PutShouldntPassBecauseOfNullNumberOfPeople()
//        {
//            entity.NumberOfPeople = null;
//            entity.PeopleCategoryId = 10;

//            try
//            {
//                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<MealBooking>(), "NumberOfPeople"),
//        GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void PostShouldntPassBecauseOfNullPeopleCategoryId()
//        {
//            entity.PeopleCategoryId = null;
//            try
//            {
//                s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<MealBooking>(), "PeopleCategoryId"),
//        GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void PutShouldntPassBecauseOfNullPeopleCategoryId()
//        {
//            entity.PeopleCategoryId = null;

//            try
//            {
//                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<MealBooking>(), "PeopleCategoryId"),
//        GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void PostShouldntPassBecauseOfForbiddenDinnerBookingId()
//        {
//            entity.DinnerBookingId = -1;
//            try
//            {
//                s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<MealBooking>(), "DinnerBookingId"),
//        GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }

//        [TestMethod]
//        public void PutShouldntPassBecauseOfForbiddenDinnerBookingId()
//        {
//            entity.DinnerBookingId = -1;
//            entity.PeopleCategoryId = 10;

//            try
//            {
//                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<MealBooking>(), "DinnerBookingId"),
//        GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }

//        [TestMethod]
//        public void PostShouldntPassBecauseOfForbiddenMealId()
//        {
//            entity.MealId = -1;
//            try
//            {
//                s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<MealBooking>(), "MealId"),
//        GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }

//        [TestMethod]
//        public void PutShouldntPassBecauseOfForbiddenMealId()
//        {
//            entity.MealId = -1;
//            entity.PeopleCategoryId = 10;

//            try
//            {
//                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<MealBooking>(), "MealId"),
//        GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }

//        [TestMethod]
//        public void PostShouldntPassBecauseOfForbiddenPeopleCategoryId()
//        {
//            entity.PeopleCategoryId = -1;
//            try
//            {
//                s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<MealBooking>(), "PeopleCategoryId"),
//        GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }

//        [TestMethod]
//        public void PutShouldntPassBecauseOfForbiddenPeopleCategoryId()
//        {
//            entity.PeopleCategoryId = -1;

//            try
//            {
//                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<MealBooking>(), "PeopleCategoryId"),
//        GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }

//        [TestMethod]
//        public void PostShouldntPassBecauseOfInvalidNumberOfPeople()
//        {
//            entity.NumberOfPeople = 0;
//            try
//            {
//                s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<MealBooking>(), "NumberOfPeople"),
//        GenericError.DOES_NOT_MEET_REQUIREMENTS);
//            }
//        }

//        [TestMethod]
//        public void PutShouldntPassBecauseOfInvalidNumberOfPeople()
//        {
//            entity.NumberOfPeople = -1;
//            entity.PeopleCategoryId = 10;

//            try
//            {
//                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<MealBooking>(), "NumberOfPeople"),
//        GenericError.DOES_NOT_MEET_REQUIREMENTS);
//            }
//        }
//    }
//}