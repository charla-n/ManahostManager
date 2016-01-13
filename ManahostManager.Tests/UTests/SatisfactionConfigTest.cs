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
//    public class SatisfactionConfigTest
//    {
//        private SatisfactionConfigRepositoryTest repo;

//        private ModelStateDictionary dict;
//        private SatisfactionConfigService s;

//        private SatisfactionConfig entity;
//        private Client client0;

//        public SatisfactionConfigTest()
//        {
//            client0 = new Client();
//            client0.Id = 1;
//            client0.DefaultHomeId = 1;
//        }

//        [TestInitialize]
//        public void Init()
//        {
//            repo = new SatisfactionConfigRepositoryTest();

//            dict = new ModelStateDictionary();
//            s = new SatisfactionConfigService(dict, repo, new SatisfactionConfigValidation());

//            entity = new SatisfactionConfig()
//            {
//                Id = 1,
//                Title = "okokTitle",
//                Description = "BigBangDescriptionTheory",
//                AdditionalInfo = "Ceci est additionnel"
//            };
//        }

//        [TestMethod]
//        public void doPostTestShouldWork()
//        {
//            s.PrePost(client0, ObjectCopier.Clone<SatisfactionConfig>(entity), null);
//            Assert.IsTrue(true);
//            Assert.AreEqual(entity.Id, repo.Entity.Id);
//            Assert.AreEqual(entity.Title, repo.Entity.Title);
//            Assert.AreEqual(entity.Description, repo.Entity.Description);
//            Assert.AreEqual(entity.AdditionalInfo, repo.Entity.AdditionalInfo);
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
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, TypeOfName.GetNameFromType<SatisfactionConfig>(),
//                        GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void doPostTestTitleNullShouldntWork()
//        {
//            try
//            {
//                entity.Title = null;
//                s.PrePost(client0, ObjectCopier.Clone<SatisfactionConfig>(entity), null);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<SatisfactionConfig>(), "Title"),
//                    GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void doPutTestShouldWork()
//        {
//            entity.AdditionalInfo = "change";
//            entity.Title = "EHEHE";
//            entity.Description = "OHOHOOH";

//            s.PrePut(client0, ObjectCopier.Clone<SatisfactionConfig>(entity), null);
//            Assert.IsTrue(true);
//            Assert.AreEqual(entity.Id, repo.Entity.Id);
//            Assert.AreEqual(entity.Title, repo.Entity.Title);
//            Assert.AreEqual(entity.Description, repo.Entity.Description);
//            Assert.AreEqual(entity.AdditionalInfo, repo.Entity.AdditionalInfo);
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
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, TypeOfName.GetNameFromType<SatisfactionConfig>(),
//                        GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void doPutTestTitleNullShouldntWork()
//        {
//            try
//            {
//                entity.Title = null;

//                s.PrePut(client0, ObjectCopier.Clone<SatisfactionConfig>(entity), null);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<SatisfactionConfig>(), "Title"),
//                    GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }
//    }
//}