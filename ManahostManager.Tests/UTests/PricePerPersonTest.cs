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
//    public class PricePerPersonTest
//    {
//        private PricePerPersonController.AdditionalRepositories addrepo;
//        private PricePerPersonRepositoryTest repo;

//        private ModelStateDictionary dict;
//        private PricePerPersonService s;

//        private Client client0;
//        private PricePerPerson entity;

//        public PricePerPersonTest()
//        {
//            client0 = new Client();

//            client0.Id = 1;
//            client0.DefaultHomeId = 1;
//        }

//        [TestInitialize]
//        public void Init()
//        {
//            addrepo = new PricePerPersonController.AdditionalRepositories();
//            repo = new PricePerPersonRepositoryTest();

//            dict = new ModelStateDictionary();
//            s = new PricePerPersonService(dict, repo, new PricePerPersonValidation());

//            addrepo.PeriodRepo = new PeriodRepositoryTest();
//            addrepo.RoomRepo = new RoomRepositoryTest();
//            addrepo.TaxRepo = new TaxRepositoryTest();
//            addrepo.PeopleCatRepo = new PeopleCategoryRepositoryTest();
//            addrepo.PPPRepo = repo;
//            entity = new PricePerPerson()
//            {
//                Id = 1,
//                Title = "ToPutTitle.png",
//                PriceHT = 5,
//                PeopleCategoryId = 1,
//                PeriodId = 1,
//                RoomId = 1,
//                TaxId = 1,
//            };
//        }

//        [TestMethod]
//        public void doPostTestShouldWork()
//        {
//            s.PrePost(client0, ObjectCopier.Clone<PricePerPerson>(entity), addrepo);
//            Assert.IsTrue(true);
//            Assert.AreEqual(null, repo.Entity.DateModification);
//            Assert.AreEqual(entity.Id, repo.Entity.Id);
//            Assert.AreEqual(entity.Title, repo.Entity.Title);
//            Assert.AreEqual(entity.TaxId, repo.Entity.TaxId);
//        }

//        [TestMethod]
//        public void doPostTestAlreadyExistInPeriodShouldntWork()
//        {
//            try
//            {
//                entity.PeriodId = 42;
//                entity.RoomId = 42;
//                s.PrePost(client0, ObjectCopier.Clone<PricePerPerson>(entity), addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, TypeOfName.GetNameFromType<PricePerPerson>(), GenericError.ALREADY_EXISTS);
//            }
//        }

//        [TestMethod]
//        public void doPostTestEntityNullShouldntWork()
//        {
//            try
//            {
//                s.PrePost(client0, null, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, TypeOfName.GetNameFromType<PricePerPerson>(), GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void doPostTestTitleNullShouldntWork()
//        {
//            try
//            {
//                entity.Title = null;
//                s.PrePost(client0, ObjectCopier.Clone<PricePerPerson>(entity), addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<PricePerPerson>(), "Title"),
//                    GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void doPostTestPeriodNullShouldntWork()
//        {
//            try
//            {
//                entity.PeriodId = null;
//                s.PrePost(client0, ObjectCopier.Clone<PricePerPerson>(entity), addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<PricePerPerson>(), "PeriodId"),
//                    GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void doPostTestRoomNullShouldntWork()
//        {
//            try
//            {
//                entity.RoomId = null;
//                s.PrePost(client0, ObjectCopier.Clone<PricePerPerson>(entity), addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<PricePerPerson>(), "RoomId"),
//                    GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void doPostTestPriceHTNegativeShouldntWork()
//        {
//            try
//            {
//                entity.PriceHT = -42;
//                s.PrePost(client0, ObjectCopier.Clone<PricePerPerson>(entity), addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<PricePerPerson>(), "PriceHT"),
//                    GenericError.DOES_NOT_MEET_REQUIREMENTS);
//            }
//        }

//        [TestMethod]
//        public void doPostTestPriceHTNullShouldntWork()
//        {
//            try
//            {
//                entity.PriceHT = null;
//                s.PrePost(client0, ObjectCopier.Clone<PricePerPerson>(entity), addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<PricePerPerson>(), "PriceHT"),
//                    GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void doPostTestPeopleCategoryUnauthorizedShouldntWork()
//        {
//            try
//            {
//                entity.PeopleCategoryId = 42;
//                s.PrePost(client0, ObjectCopier.Clone<PricePerPerson>(entity), addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<PricePerPerson>(), "PeopleCategoryId"),
//                    GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }

//        [TestMethod]
//        public void doPostTestRoomUnauthorizedShouldntWork()
//        {
//            try
//            {
//                entity.RoomId = 422;
//                s.PrePost(client0, ObjectCopier.Clone<PricePerPerson>(entity), addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<PricePerPerson>(), "RoomId"),
//                    GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }

//        [TestMethod]
//        public void doPostTestPeriodUnauthorizedShouldntWork()
//        {
//            try
//            {
//                entity.PeriodId = 422;
//                s.PrePost(client0, ObjectCopier.Clone<PricePerPerson>(entity), addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<PricePerPerson>(), "PeriodId"),
//                    GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }

//        [TestMethod]
//        public void doPutTestShouldWork()
//        {
//            entity.HomeId = 1;
//            entity.Title = "newTitleOfDoom";

//            s.PrePut(client0, ObjectCopier.Clone<PricePerPerson>(entity), addrepo);
//            Assert.IsTrue(true);
//            Assert.AreEqual(entity.Title, repo.Entity.Title);
//            Assert.AreEqual(entity.Id, repo.Entity.Id);
//        }

//        [TestMethod]
//        public void doPutTestEntityNullShouldntWork()
//        {
//            try
//            {
//                s.PrePut(client0, null, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, TypeOfName.GetNameFromType<PricePerPerson>(), GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void doPutTestTitleNullShouldntWork()
//        {
//            try
//            {
//                entity.Title = null;

//                s.PrePut(client0, ObjectCopier.Clone<PricePerPerson>(entity), addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<PricePerPerson>(), "Title"),
//                    GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void doPutTestPeriodNullShouldntWork()
//        {
//            try
//            {
//                entity.PeriodId = null;

//                s.PrePut(client0, ObjectCopier.Clone<PricePerPerson>(entity), addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<PricePerPerson>(), "PeriodId"),
//                    GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void doPutTestRoomNullShouldntWork()
//        {
//            try
//            {
//                entity.RoomId = null;

//                s.PrePut(client0, ObjectCopier.Clone<PricePerPerson>(entity), addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<PricePerPerson>(), "RoomId"),
//                    GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void doPutTestPriceHTNegativeShouldntWork()
//        {
//            try
//            {
//                entity.PriceHT = -42;

//                s.PrePut(client0, ObjectCopier.Clone<PricePerPerson>(entity), addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<PricePerPerson>(), "PriceHT"),
//                    GenericError.DOES_NOT_MEET_REQUIREMENTS);
//            }
//        }

//        [TestMethod]
//        public void doPutTestPriceHTNullShouldntWork()
//        {
//            try
//            {
//                entity.PriceHT = null;

//                s.PrePut(client0, ObjectCopier.Clone<PricePerPerson>(entity), addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<PricePerPerson>(), "PriceHT"),
//                    GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void doPutTestPeopleCategoryUnauthorizedShouldntWork()
//        {
//            try
//            {
//                entity.PeopleCategoryId = 42;

//                s.PrePut(client0, ObjectCopier.Clone<PricePerPerson>(entity), addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<PricePerPerson>(), "PeopleCategoryId"),
//                    GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }

//        [TestMethod]
//        public void doPutTestRoomUnauthorizedShouldntWork()
//        {
//            try
//            {
//                entity.RoomId = 422;

//                s.PrePut(client0, ObjectCopier.Clone<PricePerPerson>(entity), addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<PricePerPerson>(), "RoomId"),
//                    GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }

//        [TestMethod]
//        public void doPutTestPeriodUnauthorizedShouldntWork()
//        {
//            try
//            {
//                entity.PeriodId = 422;

//                s.PrePut(client0, ObjectCopier.Clone<PricePerPerson>(entity), addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<PricePerPerson>(), "PeriodId"),
//                    GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }
//    }
//}