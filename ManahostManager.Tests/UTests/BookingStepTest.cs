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
//    public class BookingStepTest
//    {
//        private BookingStepController.AdditionnalRepositories addrepo;
//        private BookingStepRepositoryTest repo;

//        private ModelStateDictionary dict;
//        private BookingStepService s;

//        private BookingStep entity;

//        [TestInitialize]
//        public void Init()
//        {
//            addrepo = new BookingStepController.AdditionnalRepositories(null);
//            repo = new BookingStepRepositoryTest();

//            dict = new ModelStateDictionary();
//            s = new BookingStepService(dict, repo, new BookingStepValidation());

//            addrepo.BookingStepConfigRepo = new BookingStepConfigRepositoryTest();
//            addrepo.BookingStepRepo = new BookingStepRepositoryTest();
//            addrepo.DocumentRepo = new DocumentRepositoryTest();
//            entity = addrepo.BookingStepRepo.GetBookingStepById(1, 0);
//        }

//        [TestMethod]
//        public void ShouldFailBecausePostNullBookingStep()
//        {
//            try
//            {
//                s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), null, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, TypeOfName.GetNameFromType<BookingStep>(),
//        GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void ShouldFailBecausePutNullBookingStep()
//        {
//            try
//            {
//                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), null, null);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, TypeOfName.GetNameFromType<BookingStep>(),
//        GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void PostShouldPass()
//        {
//            BookingStep entity = repo.GetBookingStepById(1, 0);
//            entity.BookingValidated = null;
//            entity.BookingArchived = null;
//            s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);

//            Assert.AreEqual(repo.Entity.BookingArchived, false);
//            Assert.AreEqual(repo.Entity.BookingValidated, false);
//            Assert.AreEqual(null, repo.Entity.DateModification);
//        }

//        [TestMethod]
//        public void PostShouldntPassBecauseOfForbiddenDocument()
//        {
//            BookingStep entity = repo.GetBookingStepById(1, 0);
//            entity.BookingValidated = null;
//            entity.BookingArchived = null;
//            entity.MailTemplateId = -1;
//            try
//            {
//                s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<BookingStep>(), "MailTemplateId"),
//GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }

//        [TestMethod]
//        public void PostShouldntPassBecauseOfInvalidHomeId()
//        {
//            BookingStep entity = repo.GetBookingStepById(1, 0);
//            entity.BookingValidated = null;
//            entity.BookingArchived = null;
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
//        public void PostShouldPassWithNullBookingPrevious()
//        {
//            BookingStep entity = repo.GetBookingStepById(1, 0);
//            entity.BookingStepIdPrevious = null;
//            entity.BookingStepPrevious = null;
//            entity.BookingValidated = null;
//            entity.BookingArchived = null;
//            s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);

//            Assert.AreEqual(repo.Entity.BookingArchived, false);
//            Assert.AreEqual(repo.Entity.BookingValidated, false);
//            Assert.AreEqual(null, repo.Entity.DateModification);
//        }

//        [TestMethod]
//        public void PostShouldPassWithNullBookingStepNexts()
//        {
//            BookingStep entity = repo.GetBookingStepById(1, 0);
//            entity.BookingStepIdNext = null;
//            entity.BookingStepNext = null;
//            entity.BookingValidated = null;
//            entity.BookingArchived = null;
//            s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);

//            Assert.AreEqual(repo.Entity.BookingArchived, false);
//            Assert.AreEqual(repo.Entity.BookingValidated, false);
//            Assert.AreEqual(null, repo.Entity.DateModification);
//        }

//        [TestMethod]
//        public void PutShouldPass()
//        {
//            BookingStep entity = repo.GetBookingStepById(1, 0);
//            entity.Title = "modified";
//            entity.BookingArchived = false;
//            entity.BookingValidated = false;
//            s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);

//            Assert.AreEqual(repo.Entity.BookingArchived, false);
//            Assert.AreEqual(repo.Entity.BookingValidated, false);
//            Assert.AreEqual(repo.Entity.BookingStepConfigId, 1);
//            Assert.AreEqual(repo.Entity.BookingStepIdNext, 3);
//            Assert.AreEqual(repo.Entity.BookingStepIdPrevious, 2);
//            Assert.AreEqual(repo.Entity.HomeId, 1);
//            Assert.AreEqual(repo.Entity.Id, 1);
//            Assert.AreEqual(repo.Entity.Title, "modified");
//            Assert.IsNotNull(repo.Entity.DateModification);
//        }

//        [TestMethod]
//        public void PutShouldntPassBecauseOfForbiddenHome()
//        {
//            BookingStep entity = repo.GetBookingStepById(1, 0);
//            entity.Title = "modified";
//            entity.BookingArchived = false;
//            entity.BookingValidated = false;
//            repo.Invalid = true;
//            try
//            {
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
//        public void PutShouldPassSetNextToNull()
//        {
//            BookingStep entity = repo.GetBookingStepById(1, 0);
//            entity.Title = "modified";
//            entity.BookingArchived = true;
//            entity.BookingValidated = false;
//            entity.BookingStepIdNext = null;
//            s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);

//            Assert.AreEqual(repo.Entity.BookingArchived, true);
//            Assert.AreEqual(repo.Entity.BookingValidated, false);
//            Assert.AreEqual(repo.Entity.BookingStepConfigId, 1);
//            Assert.AreEqual(repo.Entity.BookingStepIdNext, null);
//            Assert.AreEqual(repo.Entity.BookingStepIdPrevious, 2);
//            Assert.AreEqual(repo.Entity.HomeId, 1);
//            Assert.AreEqual(repo.Entity.Id, 1);
//            Assert.AreEqual(repo.Entity.Title, "modified");
//            Assert.IsNotNull(repo.Entity.DateModification);
//        }

//        [TestMethod]
//        public void PutShouldntPassForbiddenDocument()
//        {
//            BookingStep entity = repo.GetBookingStepById(1, 0);
//            entity.Title = "modified";
//            entity.BookingArchived = true;
//            entity.BookingValidated = false;
//            entity.BookingStepIdNext = null;
//            entity.MailTemplateId = -1;
//            try
//            {
//                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<BookingStep>(), "MailTemplateId"),
//GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }

//        [TestMethod]
//        public void PutShouldNotPassSetStepConfigToNull()
//        {
//            BookingStep entity = repo.GetBookingStepById(1, 0);
//            entity.Title = "modified";
//            entity.BookingArchived = true;
//            entity.BookingValidated = false;
//            entity.BookingStepIdNext = null;
//            entity.BookingStepConfigId = null;
//            try
//            {
//                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<BookingStep>(), "BookingStepConfigId"),
//GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void DeleteShouldPass()
//        {
//            s.PreDelete(new ClientRepositoryTest().FindUserByMail("test@test.com"), 1, null);
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
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, TypeOfName.GetNameFromType<BookingStep>(),
//GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }

//        [TestMethod]
//        public void PutShouldntPassBecauseOfForbiddenResource()
//        {
//            BookingStep entity = repo.GetBookingStepById(1, 0);
//            entity.Id = -1;
//            entity.Title = "modified";
//            entity.BookingArchived = true;
//            entity.BookingValidated = false;
//            try
//            {
//                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, TypeOfName.GetNameFromType<BookingStep>(),
//GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }

//        [TestMethod]
//        public void PostShouldntPassBecauseOfNullTitle()
//        {
//            BookingStep entity = repo.GetBookingStepById(1, 0);
//            entity.Title = null;
//            try
//            {
//                s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<BookingStep>(), "Title"),
//GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void PutShouldntPassBecauseOfNullTitle()
//        {
//            BookingStep entity = repo.GetBookingStepById(1, 0);
//            entity.BookingArchived = false;
//            entity.BookingValidated = false;
//            entity.Title = null;
//            try
//            {
//                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<BookingStep>(), "Title"),
//GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void PostShouldntPassBecauseOfEmptyTitle()
//        {
//            BookingStep entity = repo.GetBookingStepById(1, 0);
//            entity.Title = "";
//            try
//            {
//                s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<BookingStep>(), "Title"),
//GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void PutShouldntPassBecauseOfEmptyTitle()
//        {
//            BookingStep entity = repo.GetBookingStepById(1, 0);
//            entity.BookingArchived = false;
//            entity.BookingValidated = false;
//            entity.Title = "";
//            try
//            {
//                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<BookingStep>(), "Title"),
//GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void PostShouldntPassBecauseOfNullBookingStepConfigId()
//        {
//            BookingStep entity = repo.GetBookingStepById(1, 0);
//            entity.BookingStepConfigId = null;
//            try
//            {
//                s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<BookingStep>(), "BookingStepConfigId"),
//GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void PutShouldntPassBecauseOfNullBookingStepConfigId()
//        {
//            BookingStep entity = repo.GetBookingStepById(1, 0);
//            entity.BookingArchived = false;
//            entity.BookingValidated = false;
//            entity.BookingStepConfigId = null;
//            try
//            {
//                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<BookingStep>(), "BookingStepConfigId"),
//GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void PostShouldntPassBecauseOfForbiddenBookingStepIdNext()
//        {
//            BookingStep entity = repo.GetBookingStepById(1, 0);
//            entity.BookingStepIdNext = 9999;
//            try
//            {
//                s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<BookingStep>(), "BookingStepIdNext"),
//GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }

//        [TestMethod]
//        public void PostShouldntPassBecauseOfForbiddenBookingStepIdPrevious()
//        {
//            BookingStep entity = repo.GetBookingStepById(1, 0);
//            entity.BookingStepIdPrevious = 9999;
//            try
//            {
//                s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<BookingStep>(), "BookingStepIdPrevious"),
//GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }

//        [TestMethod]
//        public void PostShouldntPassBecauseOfPreviousAlreadyValidated()
//        {
//            BookingStep entity = repo.GetBookingStepById(1, 0);
//            entity.BookingStepPrevious.BookingValidated = true;
//            entity.BookingValidated = false;
//            entity.BookingStepIdPrevious = 4;
//            try
//            {
//                s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//            }
//        }

//        [TestMethod]
//        public void PostShouldntPassBecauseOfPreviousAlreadyArchived()
//        {
//            BookingStep entity = repo.GetBookingStepById(1, 0);
//            entity.BookingStepPrevious.BookingArchived = true;
//            entity.BookingArchived = true;
//            entity.BookingStepIdPrevious = 4;
//            try
//            {
//                s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//            }
//        }

//        [TestMethod]
//        public void PutShouldntPassBecauseOfForbiddenBookingStepIdNext()
//        {
//            BookingStep entity = repo.GetBookingStepById(1, 0);
//            entity.BookingStepIdNext = 9999;
//            entity.BookingArchived = false;
//            entity.BookingValidated = false;
//            try
//            {
//                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<BookingStep>(), "BookingStepIdNext"),
//GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }

//        [TestMethod]
//        public void PutShouldntPassBecauseOfNullBookingArchived()
//        {
//            BookingStep entity = repo.GetBookingStepById(1, 0);
//            entity.BookingValidated = false;
//            entity.BookingArchived = null;
//            try
//            {
//                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<BookingStep>(), "BookingArchived"),
//GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void PutShouldntPassBecauseOfNullBookingValidated()
//        {
//            BookingStep entity = repo.GetBookingStepById(1, 0);
//            entity.BookingValidated = null;
//            entity.BookingArchived = false;
//            try
//            {
//                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<BookingStep>(), "BookingValidated"),
//GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void PutShouldntPassBecauseOfForbiddenBookingStepIdPrevious()
//        {
//            BookingStep entity = repo.GetBookingStepById(1, 0);
//            entity.BookingStepIdPrevious = 9999;
//            entity.BookingArchived = false;
//            entity.BookingValidated = false;
//            try
//            {
//                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<BookingStep>(), "BookingStepIdPrevious"),
//GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }

//        [TestMethod]
//        public void PutShouldntPassBecauseOfPreviousAlreadyArchived()
//        {
//            BookingStep entity = repo.GetBookingStepById(1, 0);
//            entity.BookingStepPrevious.BookingArchived = true;
//            entity.BookingArchived = true;
//            entity.BookingValidated = false;
//            try
//            {
//                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//            }
//        }
//    }
//}