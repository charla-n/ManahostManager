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
//    public class PaymentTypeTest
//    {
//        [TestMethod]
//        [ExpectedException(typeof(ManahostValidationException))]
//        public void ShouldFailTryPostNull()
//        {
//            PaymentTypeRepositoryTest repo = new PaymentTypeRepositoryTest();

//            ModelStateDictionary dict = new ModelStateDictionary();
//            PaymentTypeService s = new PaymentTypeService(dict, repo, new PaymentTypeValidation());

//            s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), null, null);
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ManahostValidationException))]
//        public void ShouldFailTryPutNull()
//        {
//            PaymentTypeRepositoryTest repo = new PaymentTypeRepositoryTest();

//            ModelStateDictionary dict = new ModelStateDictionary();
//            PaymentTypeService s = new PaymentTypeService(dict, repo, new PaymentTypeValidation());

//            s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), null, null);
//        }

//        [TestMethod]
//        public void PostShouldPass()
//        {
//            PaymentTypeRepositoryTest repo = new PaymentTypeRepositoryTest();

//            ModelStateDictionary dict = new ModelStateDictionary();
//            PaymentTypeService s = new PaymentTypeService(dict, repo, new PaymentTypeValidation());
//            PaymentType entity = repo.GetPaymentTypeById(1, 0);

//            s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), ObjectCopier.Clone<PaymentType>(entity), null);

//            Assert.AreEqual(repo.Entity.HomeId, 36);
//            Assert.AreEqual(repo.Entity.Id, 1);
//            Assert.AreEqual(repo.Entity.Title, "TEST1");
//            Assert.AreEqual(null, repo.Entity.DateModification);
//        }

//        [TestMethod]
//        public void PostShouldntPassBecauseOfForbiddenHomeId()
//        {
//            PaymentTypeRepositoryTest repo = new PaymentTypeRepositoryTest();

//            ModelStateDictionary dict = new ModelStateDictionary();
//            PaymentTypeService s = new PaymentTypeService(dict, repo, new PaymentTypeValidation());
//            PaymentType entity = repo.GetPaymentTypeById(1, 0);

//            repo.Invalid = true;
//            try
//            {
//                s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), ObjectCopier.Clone<PaymentType>(entity), null);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, "HomeId",
//    GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ManahostValidationException))]
//        public void PostShouldntPassBecauseOfNullTitle()
//        {
//            PaymentTypeRepositoryTest repo = new PaymentTypeRepositoryTest();

//            ModelStateDictionary dict = new ModelStateDictionary();
//            PaymentTypeService s = new PaymentTypeService(dict, repo, new PaymentTypeValidation());
//            PaymentType entity = repo.GetPaymentTypeById(1, 0);

//            entity.Title = null;
//            s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), ObjectCopier.Clone<PaymentType>(entity), null);
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ManahostValidationException))]
//        public void PutShouldntPassBecauseOfNonExistentResource()
//        {
//            PaymentTypeRepositoryTest repo = new PaymentTypeRepositoryTest();

//            ModelStateDictionary dict = new ModelStateDictionary();
//            PaymentTypeService s = new PaymentTypeService(dict, repo, new PaymentTypeValidation());
//            PaymentType entity = repo.GetPaymentTypeById(36, 0);

//            s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), ObjectCopier.Clone<PaymentType>(entity), null);
//        }

//        [TestMethod]
//        public void PutShouldPass()
//        {
//            PaymentTypeRepositoryTest repo = new PaymentTypeRepositoryTest();

//            ModelStateDictionary dict = new ModelStateDictionary();
//            PaymentTypeService s = new PaymentTypeService(dict, repo, new PaymentTypeValidation());
//            PaymentType entity = repo.GetPaymentTypeById(1, 0);

//            entity.Title = "changed";
//            s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), ObjectCopier.Clone<PaymentType>(entity), null);

//            Assert.AreEqual(repo.Entity.Title, "changed");
//            Assert.AreEqual(repo.Entity.Id, entity.Id);
//            Assert.AreEqual(repo.Entity.HomeId, entity.HomeId);
//            Assert.IsNotNull(repo.Entity.DateModification);
//        }

//        [TestMethod]
//        public void PutShouldntPassBecauseOfForbiddenHomeId()
//        {
//            PaymentTypeRepositoryTest repo = new PaymentTypeRepositoryTest();

//            ModelStateDictionary dict = new ModelStateDictionary();
//            PaymentTypeService s = new PaymentTypeService(dict, repo, new PaymentTypeValidation());
//            PaymentType entity = repo.GetPaymentTypeById(1, 0);

//            entity.Title = "changed";
//            repo.Invalid = true;
//            try
//            {
//                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), ObjectCopier.Clone<PaymentType>(entity), null);
//                Assert.IsTrue(false);
//            }
//            catch (ManahostValidationException)
//            {
//                DictionnaryAssert.DictionnaryHasValueAndError(dict, "HomeId",
//                    GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//            }
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ManahostValidationException))]
//        public void PutShouldntPassBecauseOfNullTitle()
//        {
//            PaymentTypeRepositoryTest repo = new PaymentTypeRepositoryTest();

//            ModelStateDictionary dict = new ModelStateDictionary();
//            PaymentTypeService s = new PaymentTypeService(dict, repo, new PaymentTypeValidation());
//            PaymentType entity = repo.GetPaymentTypeById(1, 0);

//            entity.Title = null;
//            s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), ObjectCopier.Clone<PaymentType>(entity), null);

//            Assert.AreEqual(repo.Entity.Title, "TEST1");
//            Assert.AreEqual(repo.Entity.Id, entity.Id);
//            Assert.AreEqual(repo.Entity.HomeId, entity.HomeId);
//        }

//        [TestMethod]
//        public void DeleteShouldPass()
//        {
//            PaymentTypeRepositoryTest repo = new PaymentTypeRepositoryTest();

//            ModelStateDictionary dict = new ModelStateDictionary();
//            PaymentTypeService s = new PaymentTypeService(dict, repo, new PaymentTypeValidation());

//            s.PreDelete(new ClientRepositoryTest().FindUserByMail("test@test.com"), 1, null);
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ManahostValidationException))]
//        public void DeleteShouldntPassBecauseOfInvalidResource()
//        {
//            PaymentTypeRepositoryTest repo = new PaymentTypeRepositoryTest();

//            ModelStateDictionary dict = new ModelStateDictionary();
//            PaymentTypeService s = new PaymentTypeService(dict, repo, new PaymentTypeValidation());

//            s.PreDelete(new ClientRepositoryTest().FindUserByMail("test@test.com"), -1, null);
//        }
//    }
//}