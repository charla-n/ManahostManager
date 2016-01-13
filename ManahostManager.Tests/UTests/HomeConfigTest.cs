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

//namespace ManahostManager.Tests
//{
//    [TestClass]
//    public class HomeConfigTest
//    {
//        private Client client0;

//        public HomeConfigTest()
//        {
//            client0 = new Client();
//            client0.Id = 1;
//            client0.DefaultHomeId = 1;
//        }

//        [TestMethod]
//        public void doPostTest()
//        {
//            HomeConfigRepositoryTest repo = new HomeConfigRepositoryTest();

//            ModelStateDictionary wrap = new ModelStateDictionary();
//            HomeConfigService s = new HomeConfigService(wrap, repo, new HomeConfigValidation());

//            HomeConfigController.AdditionnalRepositories addRepos = new HomeConfigController.AdditionnalRepositories(null);
//            addRepos.homeConfigRepo = repo;
//            addRepos.DocumentRepo = new DocumentRepositoryTest();
//            addRepos.MailConfigRepo = new MailConfigRepositoryTest();
//            {
//                HomeConfig c = new HomeConfig();
//                c.Id = 0;
//                c.AutoSendSatisfactionEmail = null;
//                c.DefaultCaution = 2;
//                c.DefaultValueType = null;
//                c.DepositNotifEnabled = null;
//                c.Devise = null;
//                c.DinnerCapacity = null;
//                c.EnableDinner = null;
//                c.EnableDisplayActivities = null;
//                c.EnableDisplayMeals = null;
//                c.EnableDisplayProducts = null;
//                c.EnableDisplayRooms = null;
//                c.EnableReferencing = null;
//                c.FollowStockEnable = null;
//                c.HourFormat24 = null;
//                s.PrePost(client0, c, addRepos);
//                Assert.IsTrue(true);
//                Assert.AreEqual<String>("€", repo.Entity.Devise);
//                Assert.AreEqual<bool?>(false, repo.Entity.AutoSendSatisfactionEmail);
//                Assert.AreEqual<bool?>(true, repo.Entity.EnableDinner);
//                Assert.AreEqual<bool?>(false, repo.Entity.EnableDisplayActivities);
//                Assert.AreEqual<bool?>(false, repo.Entity.EnableDisplayMeals);
//                Assert.AreEqual<bool?>(false, repo.Entity.FollowStockEnable);
//                Assert.AreEqual<bool?>(false, repo.Entity.EnableReferencing);
//                Assert.AreEqual<bool?>(false, repo.Entity.EnableDisplayProducts);
//                Assert.AreEqual<bool?>(false, repo.Entity.EnableDisplayRooms);
//                Assert.AreEqual<bool?>(true, repo.Entity.HourFormat24);
//                Assert.AreEqual(null, repo.Entity.DateModification);
//            }
//        }

//        [TestMethod]
//        public void doPostTestShouldnWorkBecauseOfForbiddenMailConfig()
//        {
//            HomeConfigRepositoryTest repo = new HomeConfigRepositoryTest();

//            ModelStateDictionary wrap = new ModelStateDictionary();
//            HomeConfigService s = new HomeConfigService(wrap, repo, new HomeConfigValidation());

//            HomeConfigController.AdditionnalRepositories addRepos = new HomeConfigController.AdditionnalRepositories(null);
//            addRepos.homeConfigRepo = repo;
//            addRepos.DocumentRepo = new DocumentRepositoryTest();
//            addRepos.MailConfigRepo = new MailConfigRepositoryTest();
//            {
//                HomeConfig c = new HomeConfig();
//                c.Id = 0;
//                c.AutoSendSatisfactionEmail = null;
//                c.DefaultCaution = 2;
//                c.DefaultValueType = null;
//                c.DepositNotifEnabled = null;
//                c.Devise = null;
//                c.DinnerCapacity = null;
//                c.EnableDinner = null;
//                c.EnableDisplayActivities = null;
//                c.EnableDisplayMeals = null;
//                c.EnableDisplayProducts = null;
//                c.EnableDisplayRooms = null;
//                c.EnableReferencing = null;
//                c.FollowStockEnable = null;
//                c.DefaultMailConfigId = -1;
//                try
//                {
//                    s.PrePost(client0, c, addRepos);
//                    Assert.IsTrue(false);
//                }
//                catch (ManahostValidationException)
//                {
//                    DictionnaryAssert.DictionnaryHasValueAndError(wrap, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<HomeConfig>(), "DefaultMailConfigId"),
//GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//                }
//            }
//        }

//        [TestMethod]
//        public void doPostTestShouldnWorkBecauseOfForbiddenCanceledTmplateMail()
//        {
//            HomeConfigRepositoryTest repo = new HomeConfigRepositoryTest();

//            ModelStateDictionary wrap = new ModelStateDictionary();
//            HomeConfigService s = new HomeConfigService(wrap, repo, new HomeConfigValidation());

//            HomeConfigController.AdditionnalRepositories addRepos = new HomeConfigController.AdditionnalRepositories(null);
//            addRepos.homeConfigRepo = repo;
//            addRepos.DocumentRepo = new DocumentRepositoryTest();
//            addRepos.MailConfigRepo = new MailConfigRepositoryTest();
//            {
//                HomeConfig c = new HomeConfig();
//                c.Id = 0;
//                c.AutoSendSatisfactionEmail = null;
//                c.DefaultCaution = 2;
//                c.DefaultValueType = null;
//                c.DepositNotifEnabled = null;
//                c.Devise = null;
//                c.DinnerCapacity = null;
//                c.EnableDinner = null;
//                c.EnableDisplayActivities = null;
//                c.EnableDisplayMeals = null;
//                c.EnableDisplayProducts = null;
//                c.EnableDisplayRooms = null;
//                c.EnableReferencing = null;
//                c.FollowStockEnable = null;
//                c.BookingCanceledMailTemplateId = -1;
//                try
//                {
//                    s.PrePost(client0, c, addRepos);
//                    Assert.IsTrue(false);
//                }
//                catch (ManahostValidationException)
//                {
//                    DictionnaryAssert.DictionnaryHasValueAndError(wrap, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<HomeConfig>(), "BookingCanceledMailTemplateId"),
//GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//                }
//            }
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ManahostValidationException))]
//        public void doPostTestDefaultCautionNegatifShouldntWork()
//        {
//            HomeConfigRepositoryTest repo = new HomeConfigRepositoryTest();

//            ModelStateDictionary wrap = new ModelStateDictionary();
//            HomeConfigService s = new HomeConfigService(wrap, repo, new HomeConfigValidation());

//            HomeConfigController.AdditionnalRepositories addRepos = new HomeConfigController.AdditionnalRepositories(null);
//            addRepos.homeConfigRepo = repo;
//            addRepos.DocumentRepo = new DocumentRepositoryTest();
//            addRepos.MailConfigRepo = new MailConfigRepositoryTest();
//            {
//                HomeConfig c = new HomeConfig();
//                c.Id = 0;
//                c.AutoSendSatisfactionEmail = null;
//                c.DefaultValueType = null;
//                c.DepositNotifEnabled = null;
//                c.Devise = null;
//                c.DinnerCapacity = null;
//                c.EnableDinner = null;
//                c.EnableDisplayActivities = null;
//                c.EnableDisplayMeals = null;
//                c.EnableDisplayProducts = null;
//                c.EnableDisplayRooms = null;
//                c.EnableReferencing = null;
//                c.FollowStockEnable = null;
//                c.DefaultCaution = -1;
//                s.PrePost(client0, ObjectCopier.Clone<HomeConfig>(c), addRepos);
//            }
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ManahostValidationException))]
//        public void doPostTestDinnerCapacityNegatifShouldntWork()
//        {
//            HomeConfigRepositoryTest repo = new HomeConfigRepositoryTest();

//            ModelStateDictionary wrap = new ModelStateDictionary();
//            HomeConfigService s = new HomeConfigService(wrap, repo, new HomeConfigValidation());

//            HomeConfigController.AdditionnalRepositories addRepos = new HomeConfigController.AdditionnalRepositories(null);
//            addRepos.homeConfigRepo = repo;
//            addRepos.DocumentRepo = new DocumentRepositoryTest();
//            addRepos.MailConfigRepo = new MailConfigRepositoryTest();
//            {
//                HomeConfig c = new HomeConfig();
//                c.Id = 0;
//                c.AutoSendSatisfactionEmail = null;
//                c.DefaultCaution = 2;
//                c.DefaultValueType = null;
//                c.DepositNotifEnabled = null;
//                c.Devise = null;
//                c.EnableDinner = null;
//                c.EnableDisplayActivities = null;
//                c.EnableDisplayMeals = null;
//                c.EnableDisplayProducts = null;
//                c.EnableDisplayRooms = null;
//                c.EnableReferencing = null;
//                c.FollowStockEnable = null;
//                c.DinnerCapacity = -1;
//                s.PrePost(client0, ObjectCopier.Clone<HomeConfig>(c), addRepos);
//            }
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ManahostValidationException))]
//        public void doPostTestBadValueTypeNegShouldntWork()
//        {
//            HomeConfigRepositoryTest repo = new HomeConfigRepositoryTest();

//            ModelStateDictionary wrap = new ModelStateDictionary();
//            HomeConfigService s = new HomeConfigService(wrap, repo, new HomeConfigValidation());

//            HomeConfigController.AdditionnalRepositories addRepos = new HomeConfigController.AdditionnalRepositories(null);
//            addRepos.homeConfigRepo = repo;
//            addRepos.DocumentRepo = new DocumentRepositoryTest();
//            addRepos.MailConfigRepo = new MailConfigRepositoryTest();
//            {
//                HomeConfig c = new HomeConfig();
//                c.Id = 0;
//                c.AutoSendSatisfactionEmail = null;
//                c.DefaultCaution = 2;
//                c.DepositNotifEnabled = null;
//                c.Devise = null;
//                c.DinnerCapacity = null;
//                c.EnableDinner = null;
//                c.EnableDisplayActivities = null;
//                c.EnableDisplayMeals = null;
//                c.EnableDisplayProducts = null;
//                c.EnableDisplayRooms = null;
//                c.EnableReferencing = null;
//                c.FollowStockEnable = null;
//                c.DefaultValueType = (EValueType)(-1);
//                s.PrePost(client0, ObjectCopier.Clone<HomeConfig>(c), addRepos);
//            }
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ManahostValidationException))]
//        public void doPostTestBadValueTypePosShouldntWork()
//        {
//            HomeConfigRepositoryTest repo = new HomeConfigRepositoryTest();

//            ModelStateDictionary wrap = new ModelStateDictionary();
//            HomeConfigService s = new HomeConfigService(wrap, repo, new HomeConfigValidation());

//            HomeConfigController.AdditionnalRepositories addRepos = new HomeConfigController.AdditionnalRepositories(null);
//            addRepos.homeConfigRepo = repo;
//            addRepos.DocumentRepo = new DocumentRepositoryTest();
//            addRepos.MailConfigRepo = new MailConfigRepositoryTest();
//            {
//                HomeConfig c = new HomeConfig();
//                c.Id = 0;
//                c.AutoSendSatisfactionEmail = null;
//                c.DefaultCaution = 2;
//                c.DepositNotifEnabled = null;
//                c.Devise = null;
//                c.DinnerCapacity = null;
//                c.EnableDinner = null;
//                c.EnableDisplayActivities = null;
//                c.EnableDisplayMeals = null;
//                c.EnableDisplayProducts = null;
//                c.EnableDisplayRooms = null;
//                c.EnableReferencing = null;
//                c.FollowStockEnable = null;
//                c.DefaultValueType = (EValueType)(3);
//                s.PrePost(client0, ObjectCopier.Clone<HomeConfig>(c), addRepos);
//            }
//        }

//        [TestMethod]
//        public void doPutTest()
//        {
//            HomeConfigRepositoryTest repo = new HomeConfigRepositoryTest();

//            ModelStateDictionary wrap = new ModelStateDictionary();
//            HomeConfigService s = new HomeConfigService(wrap, repo, new HomeConfigValidation());

//            HomeConfigController.AdditionnalRepositories addRepos = new HomeConfigController.AdditionnalRepositories(null);
//            addRepos.homeConfigRepo = repo;
//            addRepos.DocumentRepo = new DocumentRepositoryTest();
//            addRepos.MailConfigRepo = new MailConfigRepositoryTest();
//            {
//                HomeConfig c = repo.GetHomeConfigById(2, 0);
//                c.Id = 2;
//                c.DefaultValueType = EValueType.AMOUNT;
//                c.Devise = "£";
//                s.PrePut(client0, ObjectCopier.Clone<HomeConfig>(c), addRepos);
//                Assert.AreEqual<String>("£", repo.Entity.Devise);
//                Assert.AreEqual(EValueType.AMOUNT, repo.Entity.DefaultValueType);
//                Assert.IsNotNull(repo.Entity.DateModification);
//            }
//        }

//        [TestMethod]
//        public void doPutTestShouldntWorkForbiddenMailConfig()
//        {
//            HomeConfigRepositoryTest repo = new HomeConfigRepositoryTest();

//            ModelStateDictionary wrap = new ModelStateDictionary();
//            HomeConfigService s = new HomeConfigService(wrap, repo, new HomeConfigValidation());

//            HomeConfigController.AdditionnalRepositories addRepos = new HomeConfigController.AdditionnalRepositories(null);
//            addRepos.homeConfigRepo = repo;
//            addRepos.DocumentRepo = new DocumentRepositoryTest();
//            addRepos.MailConfigRepo = new MailConfigRepositoryTest();
//            {
//                HomeConfig c = repo.GetHomeConfigById(2, 0);
//                c.Id = 2;
//                c.DefaultValueType = EValueType.AMOUNT;
//                c.Devise = "£";
//                c.DefaultMailConfigId = -1;
//                try
//                {
//                    s.PrePut(client0, ObjectCopier.Clone<HomeConfig>(c), addRepos);
//                    Assert.IsTrue(false);
//                }
//                catch (ManahostValidationException)
//                {
//                    DictionnaryAssert.DictionnaryHasValueAndError(wrap, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<HomeConfig>(), "DefaultMailConfigId"),
//    GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//                }
//            }
//        }

//        [TestMethod]
//        public void doPutTestShouldntWorkForbiddenCanceledMailTemplate()
//        {
//            HomeConfigRepositoryTest repo = new HomeConfigRepositoryTest();

//            ModelStateDictionary wrap = new ModelStateDictionary();
//            HomeConfigService s = new HomeConfigService(wrap, repo, new HomeConfigValidation());

//            HomeConfigController.AdditionnalRepositories addRepos = new HomeConfigController.AdditionnalRepositories(null);
//            addRepos.homeConfigRepo = repo;
//            addRepos.DocumentRepo = new DocumentRepositoryTest();
//            addRepos.MailConfigRepo = new MailConfigRepositoryTest();
//            {
//                HomeConfig c = repo.GetHomeConfigById(2, 0);
//                c.Id = 2;
//                c.DefaultValueType = EValueType.AMOUNT;
//                c.Devise = "£";
//                c.BookingCanceledMailTemplateId = -1;
//                try
//                {
//                    s.PrePut(client0, ObjectCopier.Clone<HomeConfig>(c), addRepos);
//                    Assert.IsTrue(false);
//                }
//                catch (ManahostValidationException)
//                {
//                    DictionnaryAssert.DictionnaryHasValueAndError(wrap, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<HomeConfig>(), "BookingCanceledMailTemplateId"),
//    GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//                }
//            }
//        }

//        [TestMethod]
//        public void doPutTestShouldntWorkInvalidDefaultCheckIn()
//        {
//            HomeConfigRepositoryTest repo = new HomeConfigRepositoryTest();

//            ModelStateDictionary wrap = new ModelStateDictionary();
//            HomeConfigService s = new HomeConfigService(wrap, repo, new HomeConfigValidation());

//            HomeConfigController.AdditionnalRepositories addRepos = new HomeConfigController.AdditionnalRepositories(null);
//            addRepos.homeConfigRepo = repo;
//            addRepos.DocumentRepo = new DocumentRepositoryTest();
//            addRepos.MailConfigRepo = new MailConfigRepositoryTest();
//            {
//                HomeConfig c = repo.GetHomeConfigById(2, 0);
//                c.Id = 2;
//                c.DefaultHourCheckIn = -1;
//                try
//                {
//                    s.PrePut(client0, ObjectCopier.Clone<HomeConfig>(c), addRepos);
//                    Assert.IsTrue(false);
//                }
//                catch (ManahostValidationException)
//                {
//                    DictionnaryAssert.DictionnaryHasValueAndError(wrap, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<HomeConfig>(), "DefaultHourCheckIn"),
//    GenericError.DOES_NOT_MEET_REQUIREMENTS);
//                }
//            }
//        }

//        [TestMethod]
//        public void doPutTestShouldntWorkInvalidDefaultCheckInSuperiorTo24hours()
//        {
//            HomeConfigRepositoryTest repo = new HomeConfigRepositoryTest();

//            ModelStateDictionary wrap = new ModelStateDictionary();
//            HomeConfigService s = new HomeConfigService(wrap, repo, new HomeConfigValidation());

//            HomeConfigController.AdditionnalRepositories addRepos = new HomeConfigController.AdditionnalRepositories(null);
//            addRepos.homeConfigRepo = repo;
//            addRepos.DocumentRepo = new DocumentRepositoryTest();
//            addRepos.MailConfigRepo = new MailConfigRepositoryTest();
//            {
//                HomeConfig c = repo.GetHomeConfigById(2, 0);
//                c.Id = 2;
//                c.DefaultHourCheckIn = 24 * 60;
//                try
//                {
//                    s.PrePut(client0, ObjectCopier.Clone<HomeConfig>(c), addRepos);
//                    Assert.IsTrue(false);
//                }
//                catch (ManahostValidationException)
//                {
//                    DictionnaryAssert.DictionnaryHasValueAndError(wrap, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<HomeConfig>(), "DefaultHourCheckIn"),
//    GenericError.DOES_NOT_MEET_REQUIREMENTS);
//                }
//            }
//        }

//        [TestMethod]
//        public void doPutTestShouldntWorkInvalidDefaultCheckOut()
//        {
//            HomeConfigRepositoryTest repo = new HomeConfigRepositoryTest();

//            ModelStateDictionary wrap = new ModelStateDictionary();
//            HomeConfigService s = new HomeConfigService(wrap, repo, new HomeConfigValidation());

//            HomeConfigController.AdditionnalRepositories addRepos = new HomeConfigController.AdditionnalRepositories(null);
//            addRepos.homeConfigRepo = repo;
//            addRepos.DocumentRepo = new DocumentRepositoryTest();
//            addRepos.MailConfigRepo = new MailConfigRepositoryTest();
//            {
//                HomeConfig c = repo.GetHomeConfigById(2, 0);
//                c.Id = 2;
//                c.DefaultHourCheckOut = -1;
//                try
//                {
//                    s.PrePut(client0, ObjectCopier.Clone<HomeConfig>(c), addRepos);
//                    Assert.IsTrue(false);
//                }
//                catch (ManahostValidationException)
//                {
//                    DictionnaryAssert.DictionnaryHasValueAndError(wrap, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<HomeConfig>(), "DefaultHourCheckOut"),
//    GenericError.DOES_NOT_MEET_REQUIREMENTS);
//                }
//            }
//        }

//        [TestMethod]
//        public void doPutTestShouldntWorkInvalidDefaultCheckOutSuperiorTo24Hours()
//        {
//            HomeConfigRepositoryTest repo = new HomeConfigRepositoryTest();

//            ModelStateDictionary wrap = new ModelStateDictionary();
//            HomeConfigService s = new HomeConfigService(wrap, repo, new HomeConfigValidation());

//            HomeConfigController.AdditionnalRepositories addRepos = new HomeConfigController.AdditionnalRepositories(null);
//            addRepos.homeConfigRepo = repo;
//            addRepos.DocumentRepo = new DocumentRepositoryTest();
//            addRepos.MailConfigRepo = new MailConfigRepositoryTest();
//            {
//                HomeConfig c = repo.GetHomeConfigById(2, 0);
//                c.Id = 2;
//                c.DefaultHourCheckOut = 24 * 60;
//                try
//                {
//                    s.PrePut(client0, ObjectCopier.Clone<HomeConfig>(c), addRepos);
//                    Assert.IsTrue(false);
//                }
//                catch (ManahostValidationException)
//                {
//                    DictionnaryAssert.DictionnaryHasValueAndError(wrap, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<HomeConfig>(), "DefaultHourCheckOut"),
//    GenericError.DOES_NOT_MEET_REQUIREMENTS);
//                }
//            }
//        }

//        [TestMethod]
//        public void doPutTestShouldntWorkNullHourFormat()
//        {
//            HomeConfigRepositoryTest repo = new HomeConfigRepositoryTest();

//            ModelStateDictionary wrap = new ModelStateDictionary();
//            HomeConfigService s = new HomeConfigService(wrap, repo, new HomeConfigValidation());

//            HomeConfigController.AdditionnalRepositories addRepos = new HomeConfigController.AdditionnalRepositories(null);
//            addRepos.homeConfigRepo = repo;
//            addRepos.DocumentRepo = new DocumentRepositoryTest();
//            addRepos.MailConfigRepo = new MailConfigRepositoryTest();
//            {
//                HomeConfig c = repo.GetHomeConfigById(2, 0);
//                c.Id = 2;
//                c.HourFormat24 = null;
//                try
//                {
//                    s.PrePut(client0, ObjectCopier.Clone<HomeConfig>(c), addRepos);
//                    Assert.IsTrue(false);
//                }
//                catch (ManahostValidationException)
//                {
//                    DictionnaryAssert.DictionnaryHasValueAndError(wrap, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<HomeConfig>(), "HourFormat24"),
//    GenericError.CANNOT_BE_NULL_OR_EMPTY);
//                }
//            }
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ManahostValidationException))]
//        public void doPutTestShouldntWorkBecauseOfForbiddenResource()
//        {
//            HomeConfigRepositoryTest repo = new HomeConfigRepositoryTest();

//            ModelStateDictionary wrap = new ModelStateDictionary();
//            HomeConfigService s = new HomeConfigService(wrap, repo, new HomeConfigValidation());

//            HomeConfigController.AdditionnalRepositories addRepos = new HomeConfigController.AdditionnalRepositories(null);
//            addRepos.homeConfigRepo = repo;
//            addRepos.DocumentRepo = new DocumentRepositoryTest();
//            addRepos.MailConfigRepo = new MailConfigRepositoryTest();
//            {
//                HomeConfig c = repo.GetHomeConfigById(2, 0);
//                c.Id = -1;
//                c.DefaultValueType = EValueType.AMOUNT;
//                c.Devise = "£";
//                s.PrePut(client0, ObjectCopier.Clone<HomeConfig>(c), addRepos);
//                Assert.AreEqual<String>("£", repo.Entity.Devise);
//                Assert.AreEqual(EValueType.AMOUNT, repo.Entity.DefaultValueType);
//                Assert.IsNotNull(repo.Entity.DateModification);
//            }
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ManahostValidationException))]
//        public void doPutTestDinnerCapacityNegatifShouldntWork()
//        {
//            HomeConfigRepositoryTest repo = new HomeConfigRepositoryTest();

//            ModelStateDictionary wrap = new ModelStateDictionary();
//            HomeConfigService c = new HomeConfigService(wrap, repo, new HomeConfigValidation());

//            HomeConfigController.AdditionnalRepositories addRepos = new HomeConfigController.AdditionnalRepositories(null);
//            addRepos.homeConfigRepo = repo;
//            addRepos.DocumentRepo = new DocumentRepositoryTest();
//            addRepos.MailConfigRepo = new MailConfigRepositoryTest();
//            {
//                HomeConfig homec = repo.GetHomeConfigById(2, 0);
//                homec.DinnerCapacity = -1;
//                c.PrePut(client0, ObjectCopier.Clone<HomeConfig>(homec), addRepos);
//            }
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ManahostValidationException))]
//        public void doPutTestDefaultCautionNegatifShouldntWork()
//        {
//            HomeConfigRepositoryTest repo = new HomeConfigRepositoryTest();

//            ModelStateDictionary wrap = new ModelStateDictionary();
//            HomeConfigService c = new HomeConfigService(wrap, repo, new HomeConfigValidation());

//            HomeConfigController.AdditionnalRepositories addRepos = new HomeConfigController.AdditionnalRepositories(null);
//            addRepos.homeConfigRepo = repo;
//            addRepos.DocumentRepo = new DocumentRepositoryTest();
//            addRepos.MailConfigRepo = new MailConfigRepositoryTest();
//            {
//                HomeConfig homec = repo.GetHomeConfigById(2, 0);
//                homec.DefaultCaution = -1;
//                c.PrePut(client0, ObjectCopier.Clone<HomeConfig>(homec), addRepos);
//            }
//        }

//        [TestMethod]
//        public void doDeleteTest()
//        {
//            HomeConfigRepositoryTest repo = new HomeConfigRepositoryTest();

//            ModelStateDictionary wrap = new ModelStateDictionary();
//            HomeConfigService c = new HomeConfigService(wrap, repo, new HomeConfigValidation());

//            {
//                c.PreDelete(client0, 2, null);
//                Assert.IsTrue(true);
//            }
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ManahostValidationException))]
//        public void doDeleteTestNullEntityShouldntWork()
//        {
//            HomeConfigRepositoryTest repo = new HomeConfigRepositoryTest();

//            ModelStateDictionary wrap = new ModelStateDictionary();
//            HomeConfigService c = new HomeConfigService(wrap, repo, new HomeConfigValidation());

//            {
//                c.PreDelete(client0, 6, null);
//            }
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ManahostValidationException))]
//        public void doDeleteTestDifferentIdShouldntWork()
//        {
//            HomeConfigRepositoryTest repo = new HomeConfigRepositoryTest();

//            ModelStateDictionary wrap = new ModelStateDictionary();
//            HomeConfigService c = new HomeConfigService(wrap, repo, new HomeConfigValidation());

//            {
//                c.PreDelete(client0, 3, null);
//            }
//        }
//    }
//}