//using ManahostManager.Controllers;
//using ManahostManager.Domain.Entity;
//using ManahostManager.Services;
//using ManahostManager.Tests.Repository;
//using ManahostManager.Tests.UTests.UTsUtils;
//using ManahostManager.Utils;
//using ManahostManager.Validation;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System;
//using System.Net.Http;
//using System.Web.Http;
//using System.Web.Http.Hosting;
//using System.Web.Http.ModelBinding;

//namespace ManahostManager.Tests.UTests
//{
//    [TestClass]
//    public class DocumentTest
//    {
//        private DocumentController.AdditionalRepositories addrepo;
//        private DocumentRepositoryTest repo;
//        private HttpRequestMessage req;
//        private ModelStateDictionary dict;
//        private DocumentService s;

//        private Client client0;
//        private Document entity;

//        public DocumentTest()
//        {
//            client0 = new Client();

//            client0.Id = 0;
//            client0.DefaultHomeId = 0;
//        }

//        [TestInitialize]
//        public void Init()
//        {
//            addrepo = new DocumentController.AdditionalRepositories(null);
//            repo = new DocumentRepositoryTest();
//            req = new HttpRequestMessage();
//            req.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
//            dict = new ModelStateDictionary();
//            s = new DocumentService(dict, repo, new DocumentValidation());

//            addrepo.RepoMeal = new MealRepositoryTest();
//            addrepo.RepoProduct = new ProductRepositoryTest();
//            addrepo.RepoRoom = new RoomRepositoryTest();
//            addrepo.RepoLog = new DocumentLogRepositoryTest();
//            addrepo.BookingStepRepo = new BookingStepRepositoryTest();
//            addrepo.RepoDocumentCategory = new DocumentCategoryRepositoryTest();
//            addrepo.RepoHome = new HomeRepositoryTest();
//            addrepo.RepoHomeConfig = new HomeConfigRepositoryTest();
//            entity = new Document()
//            {
//                Id = 42,
//                Title = "ToPutTitle.png",
//                IsPrivate = false,
//                ProductId = null,
//                MealId = null,
//                DocumentCategoryId = null,
//                Hide = null,
//                RoomId = null,
//                Url = "POPOPO",
//            };
//        }

//        [TestMethod]
//        public void doPostTestShouldWork()
//        {
//            entity.DateModification = DateTime.Now;
//            entity.DateUpload = DateTime.Now;
//            s.PrePost(client0, ObjectCopier.Clone<Document>(entity), addrepo);
//            Assert.AreEqual(entity.MealId, repo.Entity.MealId);
//            Assert.AreEqual(null, repo.Entity.DateUpload);
//            Assert.AreEqual(null, repo.Entity.DateModification);
//            Assert.AreEqual(entity.Id, repo.Entity.Id);
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
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, TypeOfName.GetNameFromType<Document>(), GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void doPostTestTitleNullShouldntWork()
//        {
//            try
//            {
//                entity.Title = null;
//                s.PrePost(client0, ObjectCopier.Clone<Document>(entity), addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<Document>(), "Title"),
//                    GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void doPostTestIsPrivateNullShouldntWork()
//        {
//            try
//            {
//                entity.IsPrivate = null;
//                s.PrePost(client0, ObjectCopier.Clone<Document>(entity), addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<Document>(), "IsPrivate"),
//                    GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void doPostTestMealUnauthorizedShouldntWork()
//        {
//            try
//            {
//                entity.MealId = 42;
//                s.PrePost(client0, ObjectCopier.Clone<Document>(entity), addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<Document>(), "MealId"),
//                    GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }

//        [TestMethod]
//        public void doPostTestBookingStepUnauthorizedShouldntWork()
//        {
//            try
//            {
//                entity.BookingStepId = -1;
//                s.PrePost(client0, ObjectCopier.Clone<Document>(entity), addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<Document>(), "BookingStepId"),
//                    GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }

//        [TestMethod]
//        public void doPostTestDocumentCategoryUnauthorizedShouldntWork()
//        {
//            try
//            {
//                entity.DocumentCategoryId = 42;
//                s.PrePost(client0, ObjectCopier.Clone<Document>(entity), addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<Document>(), "DocumentCategoryId"),
//                    GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }

//        [TestMethod]
//        public void doPostTestProductUnauthorizedShouldntWork()
//        {
//            try
//            {
//                entity.ProductId = 42;
//                s.PrePost(client0, entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<Document>(), "ProductId"),
//                    GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }

//        [TestMethod]
//        public void doPostTestRoomUnauthorizedShouldntWork()
//        {
//            try
//            {
//                entity.RoomId = 422;
//                s.PrePost(client0, ObjectCopier.Clone<Document>(entity), addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<Document>(), "RoomId"),
//                    GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }

//        [TestMethod]
//        public void doPutTestShouldWork()
//        {
//            entity.HomeId = 99999;
//            entity.Id = 0;
//            entity.HomeId = 0;
//            entity.ProductId = 0;
//            entity.Hide = true;
//            entity.Title = "t.png";

//            s.PrePut(client0, ObjectCopier.Clone<Document>(entity), addrepo);
//            Assert.AreEqual(entity.Title, repo.Entity.Title);
//            Assert.AreEqual(entity.MealId, repo.Entity.MealId);
//            Assert.AreNotEqual(null, repo.Entity.DateModification);
//            Assert.AreEqual(0, repo.Entity.HomeId);
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
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, TypeOfName.GetNameFromType<Document>(), GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void doPutTestTitleNullShouldntWork()
//        {
//            try
//            {
//                entity.Id = 0;
//                entity.HomeId = 0;
//                entity.Hide = true;
//                entity.Title = null;

//                s.PrePut(client0, entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<Document>(), "Title"),
//                    GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void doPutTestIsPrivateNullShouldntWork()
//        {
//            try
//            {
//                entity.Id = 0;
//                entity.HomeId = 0;
//                entity.Hide = true;
//                entity.Title = "t.png";

//                entity.IsPrivate = null;
//                s.PrePut(client0, ObjectCopier.Clone<Document>(entity), addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<Document>(), "IsPrivate"),
//                    GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void doPutTestMealUnauthorizedShouldntWork()
//        {
//            try
//            {
//                entity.Id = 0;
//                entity.HomeId = 0;
//                entity.Hide = true;
//                entity.Title = "t.png";

//                entity.MealId = 42;
//                s.PrePut(client0, ObjectCopier.Clone<Document>(entity), addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<Document>(), "MealId"),
//                    GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }

//        [TestMethod]
//        public void doPutTestBookingStepUnauthorizedShouldntWork()
//        {
//            try
//            {
//                entity.Id = 0;
//                entity.HomeId = 0;
//                entity.Hide = true;
//                entity.Title = "t.png";

//                entity.BookingStepId = -1;
//                s.PrePut(client0, ObjectCopier.Clone<Document>(entity), addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<Document>(), "BookingStepId"),
//                    GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }

//        [TestMethod]
//        public void doPutTestDocumentCategoryUnauthorizedShouldntWork()
//        {
//            try
//            {
//                entity.Id = 0;
//                entity.HomeId = 0;
//                entity.Hide = true;
//                entity.Title = "t.png";
//                entity.DocumentCategoryId = 42;

//                s.PrePost(client0, ObjectCopier.Clone<Document>(entity), addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<Document>(), "DocumentCategoryId"),
//                    GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }

//        [TestMethod]
//        public void doPutTestProductUnauthorizedShouldntWork()
//        {
//            try
//            {
//                entity.Id = 0;
//                entity.HomeId = 0;
//                entity.Hide = true;
//                entity.Title = "t.png";

//                entity.ProductId = 1337;
//                s.PrePut(client0, ObjectCopier.Clone<Document>(entity), addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<Document>(), "ProductId"),
//                    GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }

//        [TestMethod]
//        public void doPutTestRoomUnauthorizedShouldntWork()
//        {
//            try
//            {
//                entity.Id = 0;
//                entity.HomeId = 0;
//                entity.Hide = true;
//                entity.Title = "t.png";

//                entity.RoomId = 422;
//                s.PrePut(client0, entity, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<Document>(), "RoomId"),
//                    GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }
//    }
//}