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
//    public class PeriodTest
//    {
//        private PeriodRepositoryTest repo;

//        private ModelStateDictionary dict;
//        private PeriodService s;

//        private Client client0;
//        private Period entity;

//        public PeriodTest()
//        {
//            client0 = new Client();

//            client0.Id = 1;
//            client0.DefaultHomeId = 1;
//        }

//        [TestInitialize]
//        public void Init()
//        {
//            repo = new PeriodRepositoryTest();

//            dict = new ModelStateDictionary();
//            s = new PeriodService(dict, repo, new PeriodValidation());

//            entity = new Period()
//            {
//                Id = 1,
//                HomeId = 1,
//                Days = 1 | 2 | 4 | 8 | 16 | 32,
//                Begin = new DateTime(1970, 1, 1),
//                End = DateTime.Now.AddMonths(3),
//                Description = "prout",
//                Title = "pet"
//            };
//        }

//        [TestMethod]
//        public void doPostTestShouldWork()
//        {
//            s.PrePost(client0, ObjectCopier.Clone<Period>(entity), repo);
//            Assert.IsTrue(true);
//            Assert.AreEqual(null, repo.Entity.DateModification);
//            Assert.AreEqual(entity.Id, repo.Entity.Id);
//            Assert.AreEqual(entity.Title, repo.Entity.Title);
//            Assert.AreEqual(entity.Days, repo.Entity.Days);
//            Assert.AreEqual(entity.Description, repo.Entity.Description);
//            Assert.AreEqual<DateTime?>(entity.Begin, repo.Entity.Begin);
//            Assert.AreEqual<DateTime?>(entity.End, repo.Entity.End);
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
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, TypeOfName.GetNameFromType<Period>(),
//                    GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void doPostTestDaysCrossedShouldntWork()
//        {
//            try
//            {
//                repo.SetCrossedDays(true);
//                s.PrePost(client0, ObjectCopier.Clone<Period>(entity), repo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, TypeOfName.GetNameFromType<Period>(),
//                    GenericError.ALREADY_EXISTS);
//            }
//        }

//        [TestMethod]
//        public void doPostTestTitleNullShouldntWork()
//        {
//            try
//            {
//                entity.Title = null;
//                s.PrePost(client0, ObjectCopier.Clone<Period>(entity), repo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<Period>(), "Title"),
//                    GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void doPostTestBeginNullShouldntWork()
//        {
//            try
//            {
//                entity.Begin = null;
//                s.PrePost(client0, ObjectCopier.Clone<Period>(entity), repo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<Period>(), "Begin"),
//                    GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void doPostTestEndNullShouldntWork()
//        {
//            try
//            {
//                entity.End = null;
//                s.PrePost(client0, ObjectCopier.Clone<Period>(entity), repo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<Period>(), "End"),
//                    GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void doPostTestDaysNullShouldntWork()
//        {
//            try
//            {
//                entity.Days = null;
//                s.PrePost(client0, ObjectCopier.Clone<Period>(entity), repo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<Period>(), "Days"),
//                    GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void doPostTestDaysZeroOrNegShouldntWork()
//        {
//            try
//            {
//                repo.IgnoreFirstTest = true;
//                entity.Days = 0;
//                s.PrePost(client0, ObjectCopier.Clone<Period>(entity), repo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<Period>(), "Days"),
//                    GenericError.DOES_NOT_MEET_REQUIREMENTS);
//            }
//        }

//        [TestMethod]
//        public void doPostTestEndAlreadyFinishedShouldntWork()
//        {
//            try
//            {
//                repo.IgnoreFirstTest = true;
//                entity.Begin = DateTime.Now.AddDays(-5);
//                entity.End = DateTime.Now.AddDays(-2);
//                s.PrePost(client0, ObjectCopier.Clone<Period>(entity), repo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<Period>(), "End"),
//                    GenericError.DOES_NOT_MEET_REQUIREMENTS);
//            }
//        }

//        [TestMethod]
//        public void doPostTestPeriodImpossibleShouldntWork()
//        {
//            try
//            {
//                entity.End = DateTime.Now.AddMonths(1);
//                entity.Begin = DateTime.Now.AddMonths(2);
//                s.PrePost(client0, ObjectCopier.Clone<Period>(entity), repo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<Period>(), "Begin"),
//                    GenericError.DOES_NOT_MEET_REQUIREMENTS);
//            }
//        }

//        [TestMethod]
//        public void doPutTestShouldWork()
//        {
//            entity.Title = "loiliool";
//            entity.Days = 4;
//            entity.Description = "nananaenren";
//            entity.IsClosed = true;
//            s.PrePut(client0, ObjectCopier.Clone<Period>(entity), repo);
//            Assert.IsTrue(true);
//            Assert.AreEqual(entity.Title, repo.Entity.Title);
//            Assert.AreEqual(entity.Days, repo.Entity.Days);
//            Assert.AreEqual(entity.Description, repo.Entity.Description);
//        }

//        [TestMethod]
//        public void doPutTestIsClosedNullShouldntWork()
//        {
//            try
//            {
//                entity.IsClosed = null;

//                s.PrePut(client0, ObjectCopier.Clone<Period>(entity), repo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<Period>(), "IsClosed"),
//                    GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
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
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, TypeOfName.GetNameFromType<Period>(),
//                    GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void doPutTestTitleNullShouldntWork()
//        {
//            try
//            {
//                entity.Title = null;
//                entity.IsClosed = true;
//                s.PrePut(client0, ObjectCopier.Clone<Period>(entity), repo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<Period>(), "Title"),
//                    GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void doPutTestBeginNullShouldntWork()
//        {
//            try
//            {
//                entity.Begin = null;
//                entity.IsClosed = true;
//                s.PrePut(client0, ObjectCopier.Clone<Period>(entity), repo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<Period>(), "Begin"),
//                    GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void doPutTestEndNullShouldntWork()
//        {
//            try
//            {
//                entity.End = null;
//                entity.IsClosed = true;
//                s.PrePut(client0, ObjectCopier.Clone<Period>(entity), repo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<Period>(), "End"),
//                    GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void doPutTestDaysNullShouldntWork()
//        {
//            try
//            {
//                entity.Days = null;
//                entity.IsClosed = true;
//                s.PrePut(client0, ObjectCopier.Clone<Period>(entity), repo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<Period>(), "Days"),
//                    GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void doPutTestDaysZeroOrNegShouldntWork()
//        {
//            try
//            {
//                entity.Days = 0;
//                entity.IsClosed = true;
//                s.PrePut(client0, ObjectCopier.Clone<Period>(entity), repo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<Period>(), "Days"),
//                    GenericError.DOES_NOT_MEET_REQUIREMENTS);
//            }
//        }

//        [TestMethod]
//        public void doPutTestEndAlreadyFinishedShouldntWork()
//        {
//            try
//            {
//                entity.Begin = DateTime.Now.AddMonths(-8);
//                entity.End = DateTime.Now.AddMonths(-5);
//                entity.IsClosed = true;
//                s.PrePut(client0, ObjectCopier.Clone<Period>(entity), repo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<Period>(), "End"),
//                    GenericError.DOES_NOT_MEET_REQUIREMENTS);
//            }
//        }

//        [TestMethod]
//        public void doPutTestPeriodImpossibleShouldntWork()
//        {
//            try
//            {
//                entity.End = DateTime.Now.AddMonths(1);
//                entity.Begin = DateTime.Now.AddMonths(2);
//                entity.IsClosed = true;
//                s.PrePut(client0, ObjectCopier.Clone<Period>(entity), repo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<Period>(), "Begin"),
//                    GenericError.DOES_NOT_MEET_REQUIREMENTS);
//            }
//        }
//    }
//}