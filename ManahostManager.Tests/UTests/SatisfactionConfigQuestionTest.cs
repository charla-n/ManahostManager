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
//    public class SatisfactionConfigQuestionTest
//    {
//        private SatisfactionConfigQuestionRepositoryTest repo;

//        private ModelStateDictionary dict;
//        private SatisfactionConfigQuestionService s;

//        private SatisfactionConfigQuestion entity;
//        private Client client0;

//        public SatisfactionConfigQuestionTest()
//        {
//            client0 = new Client();
//            client0.Id = 1;
//            client0.DefaultHomeId = 1;
//        }

//        [TestInitialize]
//        public void Init()
//        {
//            repo = new SatisfactionConfigQuestionRepositoryTest();

//            dict = new ModelStateDictionary();
//            s = new SatisfactionConfigQuestionService(dict, repo, new SatisfactionConfigQuestionValidation());

//            entity = new SatisfactionConfigQuestion()
//            {
//                Id = 1,
//                SatisfactionConfigId = 1,
//                AnswerType = EAnswerType.NUMBER,
//                Question = "prout ?"
//            };
//        }

//        [TestMethod]
//        public void doPostTestShouldWork()
//        {
//            s.PrePost(client0, ObjectCopier.Clone<SatisfactionConfigQuestion>(entity), new SatisfactionConfigRepositoryTest());
//            Assert.IsTrue(true);
//            Assert.AreEqual(entity.Id, repo.Entity.Id);
//            Assert.AreEqual(entity.Question, repo.Entity.Question);
//            Assert.AreEqual<EAnswerType?>(entity.AnswerType, repo.Entity.AnswerType);
//        }

//        [TestMethod]
//        public void doPostTestQuestionNullShouldntWork()
//        {
//            try
//            {
//                entity.Question = null;
//                s.PrePost(client0, ObjectCopier.Clone<SatisfactionConfigQuestion>(entity), new SatisfactionConfigRepositoryTest());
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<SatisfactionConfigQuestion>(), "Question"),
//                    GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void doPostTestAnswerTypeNullShouldntWork()
//        {
//            try
//            {
//                entity.AnswerType = null;
//                s.PrePost(client0, ObjectCopier.Clone<SatisfactionConfigQuestion>(entity), new SatisfactionConfigRepositoryTest());
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<SatisfactionConfigQuestion>(), "AnswerType"),
//                    GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        /*[TestMethod]
//        public void doPostTestSatisfactionConfigNullShouldntWork()
//        {
//            try
//            {
//                entity.SatisfactionConfigId = null;
//                s.PrePost(client0, ObjectCopier.Clone<SatisfactionConfigQuestion>(entity), new SatisfactionConfigRepositoryTest());
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<SatisfactionConfigQuestion>(), "SatisfactionConfigId"),
//                    GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }*/

//        [TestMethod]
//        public void doPostTestEntityNullShouldntWork()
//        {
//            try
//            {
//                s.PrePost(client0, null, new SatisfactionConfigRepositoryTest());
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, TypeOfName.GetNameFromType<SatisfactionConfigQuestion>(),
//                    GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void doPostTestNotExistSatisfactionConfigShouldntWork()
//        {
//            try
//            {
//                entity.SatisfactionConfigId = 3;
//                s.PrePost(client0, ObjectCopier.Clone<SatisfactionConfigQuestion>(entity), new SatisfactionConfigRepositoryTest());
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<SatisfactionConfigQuestion>(), "SatisfactionConfigId"),
//                    GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }

//        [TestMethod]
//        public void doPutTestShouldWork()
//        {
//            entity.Question = "qqqqq";
//            entity.AnswerType = EAnswerType.RADIO;

//            s.PrePut(client0, ObjectCopier.Clone<SatisfactionConfigQuestion>(entity), new SatisfactionConfigRepositoryTest());
//            Assert.IsTrue(true);
//            Assert.AreEqual(entity.Id, repo.Entity.Id);
//            Assert.AreEqual(entity.Question, repo.Entity.Question);
//            Assert.AreEqual<EAnswerType?>(entity.AnswerType, repo.Entity.AnswerType);
//        }

//        [TestMethod]
//        public void doPutTestNotExistSatisfactionConfigShouldntWork()
//        {
//            try
//            {
//                entity.SatisfactionConfigId = 3;

//                s.PrePut(client0, ObjectCopier.Clone<SatisfactionConfigQuestion>(entity), new SatisfactionConfigRepositoryTest());
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<SatisfactionConfigQuestion>(), "SatisfactionConfigId"),
//                    GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }

//        [TestMethod]
//        public void doPutTestEntityNullShouldntWork()
//        {
//            try
//            {
//                s.PrePost(client0, null, new SatisfactionConfigRepositoryTest());
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, TypeOfName.GetNameFromType<SatisfactionConfigQuestion>(),
//                    GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void doPutTestQuestionNullShouldntWork()
//        {
//            try
//            {
//                entity.Question = null;

//                s.PrePut(client0, ObjectCopier.Clone<SatisfactionConfigQuestion>(entity), new SatisfactionConfigRepositoryTest());
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<SatisfactionConfigQuestion>(), "Question"),
//                    GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void doPutTestAnswerTypeNullShouldntWork()
//        {
//            try
//            {
//                entity.AnswerType = null;

//                s.PrePut(client0, ObjectCopier.Clone<SatisfactionConfigQuestion>(entity), new SatisfactionConfigRepositoryTest());
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<SatisfactionConfigQuestion>(), "AnswerType"),
//                    GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        /*[TestMethod]
//        public void doPutTestSatisfactionConfigNullShouldntWork()
//        {
//            try
//            {
//                entity.SatisfactionConfigId = null;

//                s.PrePut(client0, ObjectCopier.Clone<SatisfactionConfigQuestion>(entity), new SatisfactionConfigRepositoryTest());
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<SatisfactionConfigQuestion>(), "SatisfactionConfigId"),
//                    GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }*/
//    }
//}