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
//    public class PeopleBookingTest
//    {
//        private PeopleBookingController.AdditionalRepositories addrepo;
//        private PeopleBookingRepositoryTest repo;

//        private ModelStateDictionary dict;
//        private PeopleBookingService s;

//        private PeopleBooking entity;

//        [TestInitialize]
//        public void Init()
//        {
//            addrepo = new PeopleBookingController.AdditionalRepositories(null);
//            repo = new PeopleBookingRepositoryTest();

//            dict = new ModelStateDictionary();
//            s = new PeopleBookingService(dict, repo, new PeopleBookingValidation());

//            addrepo.PeopleCategoryRepo = new PeopleCategoryRepositoryTest();
//            addrepo.RoomBookingRepo = new RoomBookingRepositoryTest();
//            entity = new PeopleBooking()
//            {
//                DateBegin = new DateTime(2015, 1, 1),
//                DateEnd = new DateTime(2015, 1, 4),
//                HomeId = 1,
//                Id = 2,
//                NumberOfPeople = 3,
//                PeopleCategoryId = 10,
//                RoomBookingId = 1,
//                RoomBooking = new RoomBooking()
//                {
//                    Booking = new Booking()
//                    {
//                        TotalPeople = 0
//                    },
//                    DateBegin = new DateTime(2015, 1, 1),
//                    DateEnd = new DateTime(2015, 1, 4),
//                }
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
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, TypeOfName.GetNameFromType<PeopleBooking>(),
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
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, TypeOfName.GetNameFromType<PeopleBooking>(),
//                        GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void DeleteShouldPass()
//        {
//            s.PreDelete(new ClientRepositoryTest().FindUserByMail("test@test.com"), 1, null);
//            Assert.IsTrue(true);
//            Assert.AreEqual(-1, ((Booking)repo.Updates[0]).TotalPeople);
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
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, TypeOfName.GetNameFromType<PeopleBooking>(),
//                        GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }

//        [TestMethod]
//        public void PostShouldPass()
//        {
//            s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//            Assert.IsTrue(true);
//            Assert.AreEqual(entity.DateBegin, repo.Entity.DateBegin);
//            Assert.AreEqual(entity.DateEnd, repo.Entity.DateEnd);
//            Assert.AreEqual(entity.HomeId, repo.Entity.HomeId);
//            Assert.AreEqual(entity.Id, repo.Entity.Id);
//            Assert.AreEqual(3, repo.Entity.NumberOfPeople);
//            Assert.AreEqual(3, ((Booking)repo.Updates[0]).TotalPeople);
//            Assert.AreEqual(entity.PeopleCategoryId, repo.Entity.PeopleCategoryId);
//            Assert.AreEqual(entity.RoomBookingId, repo.Entity.RoomBookingId);
//        }

//        [TestMethod]
//        public void PutShouldPass()
//        {
//            entity.NumberOfPeople = 10;
//            s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//            Assert.IsTrue(true);
//            Assert.AreEqual(entity.DateBegin, repo.Entity.DateBegin);
//            Assert.AreEqual(entity.DateEnd, repo.Entity.DateEnd);
//            Assert.AreEqual(entity.HomeId, repo.Entity.HomeId);
//            Assert.AreEqual(entity.Id, repo.Entity.Id);
//            Assert.AreEqual(10, repo.Entity.NumberOfPeople);
//            Assert.AreEqual(7, ((Booking)repo.Updates[0]).TotalPeople);
//            Assert.AreEqual(entity.PeopleCategoryId, repo.Entity.PeopleCategoryId);
//            Assert.AreEqual(entity.RoomBookingId, repo.Entity.RoomBookingId);
//        }

//        [TestMethod]
//        public void PostShouldntPassBecauseOfNullNumberOfPeople()
//        {
//            try
//            {
//                entity.NumberOfPeople = null;
//                s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<PeopleBooking>(), "NumberOfPeople"),
//                        GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void PostShouldntPassBecauseOfForbiddenHomeId()
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
//        public void PutShouldntPassBecauseOfForbiddenHomeId()
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
//        public void PutShouldntPassBecauseOfNullNumberOfPeople()
//        {
//            try
//            {
//                entity.NumberOfPeople = null;

//                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<PeopleBooking>(), "NumberOfPeople"),
//                        GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void PostShouldntPassBecauseOfInvalidNumberOfPeople()
//        {
//            try
//            {
//                entity.NumberOfPeople = -1;
//                s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<PeopleBooking>(), "NumberOfPeople"),
//                        GenericError.DOES_NOT_MEET_REQUIREMENTS);
//            }
//        }

//        [TestMethod]
//        public void PutShouldntPassBecauseOfNullRoomBookingId()
//        {
//            try
//            {
//                entity.RoomBookingId = null;

//                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<PeopleBooking>(), "RoomBookingId"),
//                        GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void PostShouldntPassBecauseOfNullRoomBookingId()
//        {
//            try
//            {
//                entity.RoomBookingId = null;
//                s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<PeopleBooking>(), "RoomBookingId"),
//                        GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void PostShouldntPassBecauseOfNullPeopleCategoryId()
//        {
//            try
//            {
//                entity.PeopleCategoryId = null;
//                s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<PeopleBooking>(), "PeopleCategoryId"),
//                        GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void PutShouldntPassBecauseOfNullPeopleCategoryId()
//        {
//            try
//            {
//                entity.PeopleCategoryId = null;

//                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<PeopleBooking>(), "PeopleCategoryId"),
//                        GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void PostShouldntPassBecauseOfForbiddenRoomBookingId()
//        {
//            try
//            {
//                entity.RoomBookingId = -1;
//                s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<PeopleBooking>(), "RoomBookingId"),
//                        GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }

//        [TestMethod]
//        public void PutShouldntPassBecauseOfForbiddenRoomBookingId()
//        {
//            try
//            {
//                entity.RoomBookingId = -1;

//                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<PeopleBooking>(), "RoomBookingId"),
//                        GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }

//        [TestMethod]
//        public void PostShouldntPassBecauseOfForbiddenPeopleCategoryId()
//        {
//            try
//            {
//                entity.PeopleCategoryId = -1;
//                s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<PeopleBooking>(), "PeopleCategoryId"),
//                        GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }

//        [TestMethod]
//        public void PutShouldntPassBecauseOfForbiddenPeopleCategoryId()
//        {
//            try
//            {
//                entity.PeopleCategoryId = -1;

//                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<PeopleBooking>(), "PeopleCategoryId"),
//                        GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }
//    }
//}