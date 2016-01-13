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
//    public class PeopleFieldTest
//    {
//        private PeopleFieldController.AdditionalRepositories adds;
//        private PeopleFieldRepositoryTest repo;

//        private ModelStateDictionary dict;
//        private PeopleFieldService s;
//        private Client client0;
//        private PeopleField entity;

//        public PeopleFieldTest()
//        {
//            client0 = new Client();

//            client0.Id = 1;
//            client0.DefaultHomeId = 1;
//        }

//        [TestInitialize]
//        public void Init()
//        {
//            repo = new PeopleFieldRepositoryTest();

//            dict = new ModelStateDictionary();
//            s = new PeopleFieldService(dict, repo, new PeopleFieldValidation());
//            adds = new PeopleFieldController.AdditionalRepositories()
//            {
//                RepoFieldGroup = new FieldGroupRepositoryTest(),
//                RepoPeople = new PeopleRepositoryTest()
//            };

//            entity = new PeopleField()
//            {
//                Id = 1,
//                HomeId = 1,
//                Key = "key",
//                Value = "value",
//                FieldGroupId = 1,
//                PeopleId = null
//            };
//        }

//        [TestMethod]
//        public void doPostTestShouldWork()
//        {
//            s.PrePost(client0, ObjectCopier.Clone<PeopleField>(entity), adds);
//            Assert.IsTrue(true);
//            Assert.AreEqual(entity.FieldGroupId, repo.Entity.FieldGroupId);
//            Assert.AreEqual(entity.PeopleId, repo.Entity.PeopleId);
//            Assert.AreEqual(entity.Key, repo.Entity.Key);
//            Assert.AreEqual(entity.Value, repo.Entity.Value);
//        }

//        [TestMethod]
//        public void doPostTestEntityNullShouldntWork()
//        {
//            try
//            {
//                s.PrePost(client0, null, adds);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, TypeOfName.GetNameFromType<PeopleField>(),
//                    GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void doPostTestKeyNullShouldntWork()
//        {
//            try
//            {
//                entity.Key = null;
//                s.PrePost(client0, ObjectCopier.Clone<PeopleField>(entity), adds);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<PeopleField>(), "Key"),
//                    GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void doPostTestValueNullShouldntWork()
//        {
//            try
//            {
//                entity.Value = null;
//                s.PrePost(client0, ObjectCopier.Clone<PeopleField>(entity), adds);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<PeopleField>(), "Value"),
//                    GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void doPostTestPeopleAndFieldGroupIdNullShouldntWork()
//        {
//            try
//            {
//                entity.FieldGroupId = null;
//                entity.PeopleId = null;
//                s.PrePost(client0, ObjectCopier.Clone<PeopleField>(entity), adds);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<PeopleField>(), "PeopleId&FieldGroupId"),
//                    GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void doPostTestPeopleAndFieldGroupIdNotNullShouldntWork()
//        {
//            try
//            {
//                entity.FieldGroupId = 1;
//                entity.PeopleId = 1;
//                s.PrePost(client0, ObjectCopier.Clone<PeopleField>(entity), adds);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<PeopleField>(), "PeopleId&FieldGroupId"),
//                    GenericError.DOES_NOT_MEET_REQUIREMENTS);
//            }
//        }

//        [TestMethod]
//        public void doPostTestAuthorizationFieldGroupShouldntWork()
//        {
//            try
//            {
//                entity.PeopleId = null;
//                entity.FieldGroupId = 1337;
//                s.PrePost(client0, ObjectCopier.Clone<PeopleField>(entity), adds);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<PeopleField>(), "FieldGroupId"),
//                    GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }

//        [TestMethod]
//        public void doPostTestAuthorizationPeopleIdShouldntWork()
//        {
//            try
//            {
//                entity.FieldGroupId = null;
//                entity.PeopleId = 1337;
//                s.PrePost(client0, ObjectCopier.Clone<PeopleField>(entity), adds);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<PeopleField>(), "PeopleId"),
//                    GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }

//        [TestMethod]
//        public void doPutTestShouldWork()
//        {
//            entity.Key = "loiliool";
//            entity.Value = "dede";
//            s.PrePut(client0, ObjectCopier.Clone<PeopleField>(entity), adds);
//            Assert.IsTrue(true);
//            Assert.AreEqual(entity.Key, repo.Entity.Key);
//            Assert.AreEqual(entity.Value, repo.Entity.Value);
//        }

//        [TestMethod]
//        public void doPutTestEntityNullShouldntWork()
//        {
//            try
//            {
//                s.PrePut(client0, null, adds);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, TypeOfName.GetNameFromType<PeopleField>(),
//                    GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void doPutTestKeyNullShouldntWork()
//        {
//            try
//            {
//                entity.Key = null;
//                s.PrePut(client0, ObjectCopier.Clone<PeopleField>(entity), adds);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<PeopleField>(), "Key"),
//                    GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void doPutTestValueNullShouldntWork()
//        {
//            try
//            {
//                entity.Value = null;
//                s.PrePut(client0, ObjectCopier.Clone<PeopleField>(entity), adds);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<PeopleField>(), "Value"),
//                    GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void doPutTestPeopleAndFieldGroupIdNullShouldntWork()
//        {
//            try
//            {
//                entity.FieldGroupId = null;
//                entity.PeopleId = null;
//                s.PrePut(client0, ObjectCopier.Clone<PeopleField>(entity), adds);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<PeopleField>(), "PeopleId&FieldGroupId"),
//                    GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void doPutTestPeopleAndFieldGroupIdNotNullShouldntWork()
//        {
//            try
//            {
//                entity.FieldGroupId = 1;
//                entity.PeopleId = 1;
//                s.PrePut(client0, ObjectCopier.Clone<PeopleField>(entity), adds);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<PeopleField>(), "PeopleId&FieldGroupId"),
//                    GenericError.DOES_NOT_MEET_REQUIREMENTS);
//            }
//        }

//        [TestMethod]
//        public void doPutTestAuthorizationFieldGroupShouldntWork()
//        {
//            try
//            {
//                entity.PeopleId = null;
//                entity.FieldGroupId = 1337;
//                s.PrePut(client0, ObjectCopier.Clone<PeopleField>(entity), adds);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<PeopleField>(), "FieldGroupId"),
//                    GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }

//        [TestMethod]
//        public void doPutTestAuthorizationPeopleIdShouldntWork()
//        {
//            try
//            {
//                entity.FieldGroupId = null;
//                entity.PeopleId = 1337;
//                s.PrePut(client0, ObjectCopier.Clone<PeopleField>(entity), adds);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<PeopleField>(), "PeopleId"),
//                    GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }
//    }
//}