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
//    public class DepositTest
//    {
//        private DepositController.AdditionalRepositories addrepo;
//        private DepositRepositoryTest repo;

//        private ModelStateDictionary dict;
//        private DepositService s;

//        private Deposit entity;

//        [TestInitialize]
//        public void Init()
//        {
//            addrepo = new DepositController.AdditionalRepositories(null);
//            repo = new DepositRepositoryTest();

//            dict = new ModelStateDictionary();
//            s = new DepositService(dict, repo, new DepositValidation());

//            addrepo.BookingRepo = new BookingRepositoryTest();
//            entity = new Deposit()
//            {
//                ValueType = EValueType.AMOUNT,
//                Price = 500M,
//                Id = 2,
//                HomeId = 1,
//                DateModification = null,
//                DateCreation = null,
//                BookingId = 1
//            };
//        }

//        [TestMethod]
//        public void ShouldFailBecausePostNullDeposit()
//        {
//            try
//            {
//                s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), null, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, TypeOfName.GetNameFromType<Deposit>(), GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void ShouldFailBecausePutNullDeposit()
//        {
//            try
//            {
//                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), null, addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, TypeOfName.GetNameFromType<Deposit>(), GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void PostShouldPass()
//        {
//            entity.ValueType = null;
//            entity.HomeId = 1;
//            s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), ObjectCopier.Clone<Deposit>(entity), addrepo);
//            Assert.IsTrue(true);
//            Assert.AreEqual(entity.BookingId, repo.Entity.BookingId);
//            Assert.AreNotEqual(null, repo.Entity.DateCreation);
//            Assert.AreEqual(null, repo.Entity.DateModification);
//            Assert.AreEqual(1, repo.Entity.HomeId);
//            Assert.AreEqual(entity.Id, repo.Entity.Id);
//            Assert.AreEqual(entity.Price, repo.Entity.Price);
//            Assert.AreEqual(EValueType.AMOUNT, repo.Entity.ValueType);
//            Assert.AreEqual(null, repo.Entity.ComputedPrice);
//        }

//        [TestMethod]
//        public void PostShouldntPassBecauseOfForbiddenHomeId()
//        {
//            entity.ValueType = null;
//            entity.HomeId = 1;
//            repo.Invalid = true;
//            try
//            {
//                s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), ObjectCopier.Clone<Deposit>(entity), addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, "HomeId",
//    GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }

//        [TestMethod]
//        public void PostShouldPassWithPercentCompute()
//        {
//            entity.Price = 50M;
//            entity.ValueType = EValueType.PERCENT;
//            entity.HomeId = 1;
//            s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), ObjectCopier.Clone<Deposit>(entity), addrepo);
//            Assert.IsTrue(true);
//            Assert.AreEqual(entity.BookingId, repo.Entity.BookingId);
//            Assert.AreNotEqual(null, repo.Entity.DateCreation);
//            Assert.AreEqual(null, repo.Entity.DateModification);
//            Assert.AreEqual(1, repo.Entity.HomeId);
//            Assert.AreEqual(entity.Id, repo.Entity.Id);
//            Assert.AreEqual(entity.Price, repo.Entity.Price);
//            Assert.AreEqual(EValueType.PERCENT, repo.Entity.ValueType);
//            Assert.AreEqual(500M, repo.Entity.ComputedPrice);
//        }

//        [TestMethod]
//        public void PutShouldPass()
//        {
//            entity = repo.GetDepositById(1, 0);
//            entity.HomeId = 10;
//            entity.DateModification = DateTime.MinValue;
//            entity.Price = 50M;
//            entity.ValueType = EValueType.PERCENT;

//            s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), ObjectCopier.Clone<Deposit>(entity), addrepo);
//            Assert.IsTrue(true);
//            Assert.AreEqual(entity.BookingId, repo.Entity.BookingId);
//            Assert.AreNotEqual(null, repo.Entity.DateCreation);
//            Assert.AreNotEqual(DateTime.MinValue, repo.Entity.DateModification);
//            Assert.AreNotEqual(null, repo.Entity.DateModification);
//            Assert.AreEqual(10, repo.Entity.HomeId);
//            Assert.AreEqual(entity.Id, repo.Entity.Id);
//            Assert.AreEqual(entity.Price, repo.Entity.Price);
//            Assert.AreEqual(EValueType.PERCENT, repo.Entity.ValueType);
//            Assert.AreEqual(500M, repo.Entity.ComputedPrice);
//        }

//        [TestMethod]
//        public void PutShouldntPassBecauseOfForbiddenHomeId()
//        {
//            entity = repo.GetDepositById(1, 0);
//            entity.HomeId = 10;
//            entity.DateModification = DateTime.MinValue;
//            entity.Price = 50M;
//            entity.ValueType = EValueType.PERCENT;

//            repo.Invalid = true;
//            try
//            {
//                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), ObjectCopier.Clone<Deposit>(entity), addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, "HomeId",
//                    GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }

//        [TestMethod]
//        public void DeleteShouldPass()
//        {
//            s.PreDelete(new ClientRepositoryTest().FindUserByMail("test@test.com"), 1, null);
//            Assert.IsTrue(true);
//        }

//        [TestMethod]
//        public void DeleteShouldnPassBecauseOfForbiddenResource()
//        {
//            try
//            {
//                s.PreDelete(new ClientRepositoryTest().FindUserByMail("test@test.com"), -1, null);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, TypeOfName.GetNameFromType<Deposit>(),
//                    GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }

//        [TestMethod]
//        public void PutShouldntPassBecauseOfNullValueType()
//        {
//            entity = repo.GetDepositById(1, 0);
//            entity.HomeId = 99999;
//            entity.DateModification = DateTime.MinValue;
//            entity.Price = 50M;
//            entity.ValueType = null;

//            try
//            {
//                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), ObjectCopier.Clone<Deposit>(entity), addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<Deposit>(), "ValueType"),
//                    GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void PostShouldnPassBecauseOfNullBookingId()
//        {
//            entity.ValueType = null;
//            entity.HomeId = 99999;
//            entity.BookingId = null;
//            try
//            {
//                s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), ObjectCopier.Clone<Deposit>(entity), addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<Deposit>(), "BookingId"),
//                    GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void PutShouldntPassBecauseOfNullBookingId()
//        {
//            entity = repo.GetDepositById(1, 0);
//            entity.HomeId = 99999;
//            entity.DateModification = DateTime.MinValue;
//            entity.Price = 50M;
//            entity.BookingId = null;

//            try
//            {
//                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), ObjectCopier.Clone<Deposit>(entity), addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<Deposit>(), "BookingId"),
//                    GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void PostShouldnPassBecauseOfNullPrice()
//        {
//            entity.ValueType = null;
//            entity.HomeId = 99999;
//            entity.Price = null;
//            try
//            {
//                s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), ObjectCopier.Clone<Deposit>(entity), addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<Deposit>(), "Price"),
//                    GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void PutShouldntPassBecauseOfNullPrice()
//        {
//            entity = repo.GetDepositById(1, 0);
//            entity.HomeId = 99999;
//            entity.DateModification = DateTime.MinValue;
//            entity.Price = 50M;
//            entity.Price = null;

//            try
//            {
//                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), ObjectCopier.Clone<Deposit>(entity), addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<Deposit>(), "Price"),
//                    GenericError.CANNOT_BE_NULL_OR_EMPTY);
//            }
//        }

//        [TestMethod]
//        public void PostShouldnPassBecauseOfForbiddenBookingId()
//        {
//            entity.ValueType = null;
//            entity.HomeId = 99999;
//            entity.BookingId = -1;
//            try
//            {
//                s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), ObjectCopier.Clone<Deposit>(entity), addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<Deposit>(), "BookingId"),
//                    GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }

//        [TestMethod]
//        public void PutShouldntPassBecauseOfForbiddenBookingId()
//        {
//            entity = repo.GetDepositById(1, 0);
//            entity.HomeId = 99999;
//            entity.DateModification = DateTime.MinValue;
//            entity.Price = 50M;
//            entity.BookingId = -1;

//            try
//            {
//                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), ObjectCopier.Clone<Deposit>(entity), addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<Deposit>(), "BookingId"),
//                    GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }

//        [TestMethod]
//        public void PostShouldnPassBecauseOfInvalidPrice()
//        {
//            entity.ValueType = null;
//            entity.HomeId = 99999;
//            entity.Price = -1M;
//            try
//            {
//                s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), ObjectCopier.Clone<Deposit>(entity), addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<Deposit>(), "Price"),
//                    GenericError.INVALID_GIVEN_PARAMETER);
//            }
//        }

//        [TestMethod]
//        public void PutShouldntPassBecauseOfInvalidPrice()
//        {
//            entity = repo.GetDepositById(1, 0);
//            entity.HomeId = 99999;
//            entity.DateModification = DateTime.MinValue;
//            entity.Price = -1M;

//            try
//            {
//                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), ObjectCopier.Clone<Deposit>(entity), addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<Deposit>(), "Price"),
//                    GenericError.INVALID_GIVEN_PARAMETER);
//            }
//        }

//        [TestMethod]
//        public void PostShouldnPassBecauseOfInvalidPrice2()
//        {
//            entity.ValueType = null;
//            entity.HomeId = 99999;
//            entity.ValueType = EValueType.PERCENT;
//            entity.Price = 101;
//            try
//            {
//                s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), ObjectCopier.Clone<Deposit>(entity), addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<Deposit>(), "Price"),
//                    GenericError.INVALID_GIVEN_PARAMETER);
//            }
//        }

//        [TestMethod]
//        public void PutShouldntPassBecauseOfInvalidPrice2()
//        {
//            entity = repo.GetDepositById(1, 0);
//            entity.HomeId = 99999;
//            entity.DateModification = DateTime.MinValue;
//            entity.Price = 101M;
//            entity.ValueType = EValueType.PERCENT;

//            try
//            {
//                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), ObjectCopier.Clone<Deposit>(entity), addrepo);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<Deposit>(), "Price"),
//                    GenericError.INVALID_GIVEN_PARAMETER);
//            }
//        }
//    }
//}