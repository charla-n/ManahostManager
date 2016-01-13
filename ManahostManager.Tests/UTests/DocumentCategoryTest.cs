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
//    public class DocumentCategoryTest
//    {
//        private DocumentCategoryRepositoryTest repo;

//        private ModelStateDictionary dict;
//        private DocumentCategoryService s;
//        private Client client0;

//        private DocumentCategory entity;

//        public DocumentCategoryTest()
//        {
//            client0 = new Client();

//            client0.Id = 1;
//            client0.DefaultHomeId = 1;
//        }

//        [TestInitialize]
//        public void Init()
//        {
//            repo = new DocumentCategoryRepositoryTest();

//            dict = new ModelStateDictionary();
//            s = new DocumentCategoryService(dict, repo, new DocumentCategoryValidation());

//            entity = new DocumentCategory()
//            {
//                Id = 1,
//                Title = "titleCategory",
//                DateModification = null
//            };
//        }

//        [TestMethod]
//        public void doPostTestShouldWork()
//        {
//            s.PrePost(client0, ObjectCopier.Clone<DocumentCategory>(entity), null);
//            Assert.IsTrue(true);
//            Assert.AreEqual(entity.Id, repo.Entity.Id);
//            Assert.AreEqual(entity.Title, repo.Entity.Title);
//        }

//        [TestMethod]
//        public void doPostTestTitleNullShouldntWork()
//        {
//            try
//            {
//                entity.Title = null;
//                s.PrePost(client0, ObjectCopier.Clone<DocumentCategory>(entity), null);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<DocumentCategory>(), "Title"),
//                    GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void doPutTestShouldWork()
//        {
//            entity.Title = "new title";

//            s.PrePut(client0, ObjectCopier.Clone<DocumentCategory>(entity), null);
//            Assert.IsTrue(true);
//            Assert.AreNotEqual(entity.DateModification, repo.Entity.DateModification);
//            Assert.AreEqual(entity.Id, repo.Entity.Id);
//            Assert.AreEqual<String>("new title", repo.Entity.Title);
//        }

//        [TestMethod]
//        public void doPutTestTitleNullShouldntWork()
//        {
//            try
//            {
//                entity.Title = null;

//                s.PrePut(client0, ObjectCopier.Clone<DocumentCategory>(entity), null);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<DocumentCategory>(), "Title"),
//                    GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }
//    }
//}