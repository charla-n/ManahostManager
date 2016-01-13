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
//    public class FieldGroupTest
//    {
//        private FieldGroupRepositoryTest repo;

//        private ModelStateDictionary dict;
//        private FieldGroupService s;
//        private Client client0;
//        private FieldGroup entity;

//        public FieldGroupTest()
//        {
//            client0 = new Client();

//            client0.Id = 1;
//            client0.DefaultHomeId = 1;
//        }

//        [TestInitialize]
//        public void Init()
//        {
//            repo = new FieldGroupRepositoryTest();

//            dict = new ModelStateDictionary();
//            s = new FieldGroupService(dict, repo, new FieldGroupValidation());

//            entity = new FieldGroup()
//            {
//                Id = 1,
//                HomeId = 1,
//                Title = "pet"
//            };
//        }

//        [TestMethod]
//        public void doPostTestShouldWork()
//        {
//            s.PrePost(client0, ObjectCopier.Clone<FieldGroup>(entity), repo);
//            Assert.IsTrue(true);
//            Assert.AreEqual(entity.Title, repo.Entity.Title);
//        }

//        [TestMethod]
//        public void doPostTestEntityNullShouldntWork()
//        {
//            try
//            {
//                s.PrePost(client0, null, null);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, TypeOfName.GetNameFromType<FieldGroup>(),
//                    GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void doPostTestTitleNullShouldntWork()
//        {
//            try
//            {
//                entity.Title = null;
//                s.PrePost(client0, ObjectCopier.Clone<FieldGroup>(entity), repo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<FieldGroup>(), "Title"),
//                    GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void doPutTestShouldWork()
//        {
//            entity.Title = "loiliool";
//            s.PrePut(client0, ObjectCopier.Clone<FieldGroup>(entity), repo);
//            Assert.IsTrue(true);
//            Assert.AreEqual(entity.Title, repo.Entity.Title);
//        }

//        [TestMethod]
//        public void doPutTestEntityNullShouldntWork()
//        {
//            try
//            {
//                s.PrePut(client0, null, repo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, TypeOfName.GetNameFromType<FieldGroup>(),
//                    GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void doPutTestTitleNullShouldntWork()
//        {
//            try
//            {
//                entity.Title = null;
//                s.PrePut(client0, ObjectCopier.Clone<FieldGroup>(entity), repo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<FieldGroup>(), "Title"),
//                    GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }
//    }
//}