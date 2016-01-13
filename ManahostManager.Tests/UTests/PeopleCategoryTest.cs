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
//    public class PeopleCategoryTest
//    {
//        private PeopleCategoryController.AdditionalRepositories addrepo;
//        private PeopleCategoryRepositoryTest repo;

//        private ModelStateDictionary dict;
//        private PeopleCategoryService s;

//        private Client client0;
//        private PeopleCategory entity;

//        public PeopleCategoryTest()
//        {
//            client0 = new Client();

//            client0.Id = 1;
//            client0.DefaultHomeId = 1;
//        }

//        [TestInitialize]
//        public void Init()
//        {
//            addrepo = new PeopleCategoryController.AdditionalRepositories(null);
//            repo = new PeopleCategoryRepositoryTest();

//            dict = new ModelStateDictionary();
//            s = new PeopleCategoryService(dict, repo, new PeopleCategoryValidation());

//            addrepo.TaxRepo = new TaxRepositoryTest();
//            entity = new PeopleCategory()
//            {
//                Id = 1,
//                Label = "Prout",
//                TaxId = 1
//            };
//        }

//        [TestMethod]
//        public void doPostTestShouldWork()
//        {
//            s.PrePost(client0, ObjectCopier.Clone<PeopleCategory>(entity), addrepo);
//            Assert.IsTrue(true);
//            Assert.AreEqual(null, repo.Entity.DateModification);
//            Assert.AreEqual(entity.Id, repo.Entity.Id);
//            Assert.AreEqual(entity.Label, repo.Entity.Label);
//            Assert.AreEqual(entity.TaxId, repo.Entity.TaxId);
//        }

//        [TestMethod]
//        public void doPostTestLabelNullShouldntWork()
//        {
//            try
//            {
//                entity.Label = null;
//                s.PrePost(client0, ObjectCopier.Clone<PeopleCategory>(entity), addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<PeopleCategory>(), "Label"),
//                    GenericError.CANNOT_BE_NULL_OR_EMPTY);
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
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, TypeOfName.GetNameFromType<PeopleCategory>(), GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void doPostTaxIdAuthorizationShouldntWork()
//        {
//            try
//            {
//                entity.TaxId = 1337;
//                s.PrePost(client0, ObjectCopier.Clone<PeopleCategory>(entity), addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<PeopleCategory>(), "TaxId"),
//                    GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }

//        [TestMethod]
//        public void doPutTestShouldWork()
//        {
//            entity.Label = "loiliool";
//            entity.TaxId = 3;

//            s.PrePut(client0, ObjectCopier.Clone<PeopleCategory>(entity), addrepo);
//            Assert.IsTrue(true);
//            Assert.AreEqual(entity.Label, repo.Entity.Label);
//            Assert.AreEqual(entity.TaxId, repo.Entity.TaxId);
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
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, TypeOfName.GetNameFromType<PeopleCategory>(), GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void doPutTestLabelNullShouldntWork()
//        {
//            try
//            {
//                entity.Label = null;

//                s.PrePut(client0, ObjectCopier.Clone<PeopleCategory>(entity), addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<PeopleCategory>(), "Label"),
//                    GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void doPutTestTaxIdAuthorizationShouldntWork()
//        {
//            try
//            {
//                entity.TaxId = 1337;

//                s.PrePut(client0, ObjectCopier.Clone<PeopleCategory>(entity), addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<PeopleCategory>(), "TaxId"),
//                    GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }
//    }
//}