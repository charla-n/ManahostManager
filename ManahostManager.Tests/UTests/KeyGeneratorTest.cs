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
//    public class KeyGeneratorTest
//    {
//        private KeyGeneratorRepositoryTest repo;

//        private ModelStateDictionary dict;
//        private KeyGeneratorService s;

//        private Client client0;
//        private KeyGenerator entity;

//        public KeyGeneratorTest()
//        {
//            client0 = new Client();

//            client0.Id = 1;
//            client0.DefaultHomeId = 1;
//            client0.IsManager = false;
//        }

//        [TestInitialize]
//        public void Init()
//        {
//            repo = new KeyGeneratorRepositoryTest();

//            dict = new ModelStateDictionary();
//            s = new KeyGeneratorService(dict, repo, new KeyGeneratorValidation());

//            entity = new KeyGenerator()
//            {
//                Id = 1,
//                HomeId = 1,
//                ValueType = EValueType.AMOUNT,
//                Price = 42,
//                KeyType = EKeyType.CLIENT,
//                DateExp = DateTime.Now.AddDays(1337),
//                Description = "GROSSE DESCRIPTION OF DOOM"
//            };
//        }

//        [TestMethod]
//        public void doPostTestShouldWork()
//        {
//            s.PrePost(client0, ObjectCopier.Clone<KeyGenerator>(entity), null);
//            Assert.IsTrue(true);
//            Assert.AreEqual(entity.Description, repo.Entity.Description);
//            Assert.AreEqual(entity.DateExp, repo.Entity.DateExp);
//            Assert.AreEqual(entity.KeyType, repo.Entity.KeyType);
//            Assert.AreEqual(entity.Price, repo.Entity.Price);
//            Assert.AreEqual(entity.ValueType, repo.Entity.ValueType);
//            Assert.AreEqual(entity.Id, repo.Entity.Id);
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
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, TypeOfName.GetNameFromType<KeyGenerator>(), GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void doPostTestPriceNullWithManagerShouldntWork()
//        {
//            try
//            {
//                client0.IsManager = true;
//                entity.Price = null;
//                s.PrePost(client0, ObjectCopier.Clone<KeyGenerator>(entity), null);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<KeyGenerator>(), "Price"),
//                    GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void doPostTestDateExpNullShouldntWork()
//        {
//            try
//            {
//                entity.DateExp = null;
//                s.PrePost(client0, ObjectCopier.Clone<KeyGenerator>(entity), null);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<KeyGenerator>(), "DateExp"),
//                    GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void doPostTestValueTypeNullShouldntWork()
//        {
//            try
//            {
//                client0.IsManager = true;
//                entity.ValueType = null;
//                s.PrePost(client0, ObjectCopier.Clone<KeyGenerator>(entity), null);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<KeyGenerator>(), "ValueType"),
//                    GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void doPostTestKeyTypeNullShouldntWork()
//        {
//            try
//            {
//                entity.KeyType = null;
//                s.PrePost(client0, ObjectCopier.Clone<KeyGenerator>(entity), null);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<KeyGenerator>(), "KeyType"),
//                    GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void doPostTestPriceNegShouldntWork()
//        {
//            try
//            {
//                entity.Price = -5;
//                s.PrePost(client0, ObjectCopier.Clone<KeyGenerator>(entity), null);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<KeyGenerator>(), "Price"),
//                    GenericError.DOES_NOT_MEET_REQUIREMENTS);
//            }
//        }

//        [TestMethod]
//        public void doPostTestValueTypeWrongShouldntWork()
//        {
//            try
//            {
//                entity.ValueType = (EValueType)(-5);
//                s.PrePost(client0, ObjectCopier.Clone<KeyGenerator>(entity), null);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<KeyGenerator>(), "ValueType"),
//                    GenericError.DOES_NOT_MEET_REQUIREMENTS);
//            }
//        }

//        [TestMethod]
//        public void doPostTestKeyTypeWrongShouldntWork()
//        {
//            try
//            {
//                entity.KeyType = (EKeyType)(-5);
//                s.PrePost(client0, ObjectCopier.Clone<KeyGenerator>(entity), null);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<KeyGenerator>(), "KeyType"),
//                    GenericError.DOES_NOT_MEET_REQUIREMENTS);
//            }
//        }

//        [TestMethod]
//        public void doPostTestDateExpAlreadyPassedShouldntWork()
//        {
//            try
//            {
//                entity.DateExp = DateTime.Now.AddDays(-1337);
//                s.PrePost(client0, ObjectCopier.Clone<KeyGenerator>(entity), null);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<KeyGenerator>(), "DateExp"),
//                    GenericError.DOES_NOT_MEET_REQUIREMENTS);
//            }
//        }

//        [TestMethod]
//        public void doPostTestKeyTypeNotAuthorizedShouldntWork()
//        {
//            try
//            {
//                entity.KeyType = EKeyType.MANAHOST;
//                s.PrePost(client0, ObjectCopier.Clone<KeyGenerator>(entity), null);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<KeyGenerator>(), "KeyType"),
//                    GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }

//        [TestMethod]
//        public void doPutTestShouldWork()
//        {
//            entity.Price = 390;
//            entity.DateExp = DateTime.Now.AddDays(42);
//            entity.Description = "LEA ELLE EST PAS TERRORISTE";
//            entity.ValueType = EValueType.AMOUNT;

//            s.PrePut(client0, ObjectCopier.Clone<KeyGenerator>(entity), null);
//            Assert.IsTrue(true);
//            Assert.AreEqual(entity.Price, repo.Entity.Price);
//            Assert.AreEqual(entity.DateExp, repo.Entity.DateExp);
//            Assert.AreEqual(entity.Description, repo.Entity.Description);
//            Assert.AreEqual(entity.ValueType, repo.Entity.ValueType);
//            Assert.AreEqual(entity.Id, repo.Entity.Id);
//        }

//        [TestMethod]
//        public void doPutTestEntityNullShouldntWork()
//        {
//            try
//            {
//                s.PrePut(client0, null, null);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, TypeOfName.GetNameFromType<KeyGenerator>(), GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void doPutTestDateExpNullShouldntWork()
//        {
//            try
//            {
//                entity.DateExp = null;

//                s.PrePut(client0, ObjectCopier.Clone<KeyGenerator>(entity), null);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<KeyGenerator>(), "DateExp"),
//                    GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void doPutTestValueTypeNullShouldntWork()
//        {
//            try
//            {
//                client0.IsManager = true;
//                entity.ValueType = null;

//                s.PrePut(client0, ObjectCopier.Clone<KeyGenerator>(entity), null);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<KeyGenerator>(), "ValueType"),
//                    GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void doPutTestKeyTypeNullShouldntWork()
//        {
//            try
//            {
//                entity.KeyType = null;

//                s.PrePut(client0, ObjectCopier.Clone<KeyGenerator>(entity), null);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<KeyGenerator>(), "KeyType"),
//                    GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void doPutTestPriceNegShouldntWork()
//        {
//            try
//            {
//                entity.Price = -5;

//                s.PrePut(client0, ObjectCopier.Clone<KeyGenerator>(entity), null);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<KeyGenerator>(), "Price"),
//                    GenericError.DOES_NOT_MEET_REQUIREMENTS);
//            }
//        }

//        [TestMethod]
//        public void doPutTestValueTypeWrongShouldntWork()
//        {
//            try
//            {
//                entity.ValueType = (EValueType)(-5);

//                s.PrePut(client0, ObjectCopier.Clone<KeyGenerator>(entity), null);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<KeyGenerator>(), "ValueType"),
//                    GenericError.DOES_NOT_MEET_REQUIREMENTS);
//            }
//        }

//        [TestMethod]
//        public void doPutTestKeyTypeWrongShouldntWork()
//        {
//            try
//            {
//                entity.KeyType = (EKeyType)(-5);

//                s.PrePut(client0, ObjectCopier.Clone<KeyGenerator>(entity), null);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<KeyGenerator>(), "KeyType"),
//                    GenericError.DOES_NOT_MEET_REQUIREMENTS);
//            }
//        }

//        [TestMethod]
//        public void doPutTestDateExpAlreadyPassedShouldntWork()
//        {
//            try
//            {
//                entity.DateExp = DateTime.Now.AddDays(-1337);

//                s.PrePut(client0, ObjectCopier.Clone<KeyGenerator>(entity), null);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<KeyGenerator>(), "DateExp"),
//                    GenericError.DOES_NOT_MEET_REQUIREMENTS);
//            }
//        }

//        [TestMethod]
//        public void doPutTestPriceNullWithManagerShouldntWork()
//        {
//            try
//            {
//                client0.IsManager = true;
//                entity.Price = null;

//                s.PrePut(client0, ObjectCopier.Clone<KeyGenerator>(entity), null);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<KeyGenerator>(), "Price"),
//                    GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void doPutTestKeyTypeNotAuthorizedShouldntWork()
//        {
//            try
//            {
//                entity.KeyType = EKeyType.MANAHOST;

//                s.PrePut(client0, ObjectCopier.Clone<KeyGenerator>(entity), null);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<KeyGenerator>(), "KeyType"),
//                    GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }
//    }
//}