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
//    public class RoomSupplementTest
//    {
//        [TestMethod]
//        [ExpectedException(typeof(ManahostValidationException))]
//        public void ShouldFailTryPostNull()
//        {
//            RoomSupplementRepositoryTest repo = new RoomSupplementRepositoryTest();

//            ModelStateDictionary dict = new ModelStateDictionary();
//            RoomSupplementService s = new RoomSupplementService(dict, repo, new RoomSupplementValidation());

//            s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), null, null);
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ManahostValidationException))]
//        public void ShouldFailTryPutNull()
//        {
//            RoomSupplementRepositoryTest repo = new RoomSupplementRepositoryTest();

//            ModelStateDictionary dict = new ModelStateDictionary();
//            RoomSupplementService s = new RoomSupplementService(dict, repo, new RoomSupplementValidation());

//            s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), null, null);
//        }

//        [TestMethod]
//        public void PostShouldPass()
//        {
//            RoomSupplementRepositoryTest repo = new RoomSupplementRepositoryTest();

//            ModelStateDictionary dict = new ModelStateDictionary();
//            RoomSupplementService s = new RoomSupplementService(dict, repo, new RoomSupplementValidation());
//            RoomSupplement rs = repo.GetRoomSupplementById(1, 0);

//            RoomSupplementController.AdditionalRepositories repos = new RoomSupplementController.AdditionalRepositories(null);
//            repos.RepoTax = new TaxRepositoryTest();

//            s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), ObjectCopier.Clone<RoomSupplement>(rs), repos);
//            Assert.AreEqual(repo.Entity.Hide, false);
//            Assert.AreEqual(repo.Entity.PriceHT, 555M);
//            Assert.AreEqual(repo.Entity.Title, "Test");
//            Assert.AreEqual(repo.Entity.HomeId, 36);
//            Assert.AreEqual(null, repo.Entity.DateModification);
//        }

//        [TestMethod]
//        public void PostShouldntPassBecauseOfForbiddenHomeId()
//        {
//            RoomSupplementRepositoryTest repo = new RoomSupplementRepositoryTest();

//            ModelStateDictionary dict = new ModelStateDictionary();
//            RoomSupplementService s = new RoomSupplementService(dict, repo, new RoomSupplementValidation());
//            RoomSupplement rs = repo.GetRoomSupplementById(1, 0);

//            RoomSupplementController.AdditionalRepositories repos = new RoomSupplementController.AdditionalRepositories(null);
//            repos.RepoTax = new TaxRepositoryTest();

//            repo.Invalid = true;
//            try
//            {
//                s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), ObjectCopier.Clone<RoomSupplement>(rs), repos);
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
//        public void PostShouldntPassBecauseOfNonExistentTax()
//        {
//            RoomSupplementRepositoryTest repo = new RoomSupplementRepositoryTest();

//            ModelStateDictionary dict = new ModelStateDictionary();
//            RoomSupplementService s = new RoomSupplementService(dict, repo, new RoomSupplementValidation());
//            RoomSupplement rs = repo.GetRoomSupplementById(2, 0);

//            RoomSupplementController.AdditionalRepositories repos = new RoomSupplementController.AdditionalRepositories(null);
//            repos.RepoTax = new TaxRepositoryTest();

//            s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), ObjectCopier.Clone<RoomSupplement>(rs), repos);
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ManahostValidationException))]
//        public void PostShouldntPassBecauseOfNullTitle()
//        {
//            RoomSupplementRepositoryTest repo = new RoomSupplementRepositoryTest();

//            ModelStateDictionary dict = new ModelStateDictionary();
//            RoomSupplementService s = new RoomSupplementService(dict, repo, new RoomSupplementValidation());
//            RoomSupplement rs = repo.GetRoomSupplementById(1, 0);

//            rs.Title = null;
//            RoomSupplementController.AdditionalRepositories repos = new RoomSupplementController.AdditionalRepositories(null);
//            repos.RepoTax = new TaxRepositoryTest();

//            s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), ObjectCopier.Clone<RoomSupplement>(rs), repos);
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ManahostValidationException))]
//        public void PostShouldntPassBecauseOfNullPriceHT()
//        {
//            RoomSupplementRepositoryTest repo = new RoomSupplementRepositoryTest();

//            ModelStateDictionary dict = new ModelStateDictionary();
//            RoomSupplementService s = new RoomSupplementService(dict, repo, new RoomSupplementValidation());
//            RoomSupplement rs = repo.GetRoomSupplementById(1, 0);

//            rs.PriceHT = null;
//            RoomSupplementController.AdditionalRepositories repos = new RoomSupplementController.AdditionalRepositories(null);
//            repos.RepoTax = new TaxRepositoryTest();

//            s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), ObjectCopier.Clone<RoomSupplement>(rs), repos);
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ManahostValidationException))]
//        public void PostShouldntPassBecauseOfInvalidPriceHT()
//        {
//            RoomSupplementRepositoryTest repo = new RoomSupplementRepositoryTest();

//            ModelStateDictionary dict = new ModelStateDictionary();
//            RoomSupplementService s = new RoomSupplementService(dict, repo, new RoomSupplementValidation());
//            RoomSupplement rs = repo.GetRoomSupplementById(1, 0);

//            rs.PriceHT = -1;
//            RoomSupplementController.AdditionalRepositories repos = new RoomSupplementController.AdditionalRepositories(null);
//            repos.RepoTax = new TaxRepositoryTest();

//            s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), ObjectCopier.Clone<RoomSupplement>(rs), repos);
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ManahostValidationException))]
//        public void PostShouldntPassBecauseOfInvalidTax()
//        {
//            RoomSupplementRepositoryTest repo = new RoomSupplementRepositoryTest();

//            ModelStateDictionary dict = new ModelStateDictionary();
//            RoomSupplementService s = new RoomSupplementService(dict, repo, new RoomSupplementValidation());
//            RoomSupplement rs = repo.GetRoomSupplementById(1, 0);

//            rs.TaxId = -1;
//            RoomSupplementController.AdditionalRepositories repos = new RoomSupplementController.AdditionalRepositories(null);
//            repos.RepoTax = new TaxRepositoryTest();

//            s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), ObjectCopier.Clone<RoomSupplement>(rs), repos);
//        }

//        [TestMethod]
//        public void PutShouldPass()
//        {
//            RoomSupplementRepositoryTest repo = new RoomSupplementRepositoryTest();

//            ModelStateDictionary dict = new ModelStateDictionary();
//            RoomSupplementService s = new RoomSupplementService(dict, repo, new RoomSupplementValidation());
//            RoomSupplement rs = repo.GetRoomSupplementById(1, 0);

//            rs.HomeId = 36;
//            rs.Hide = true;
//            rs.Title = "changed";
//            rs.PriceHT = 999M;

//            RoomSupplementController.AdditionalRepositories repos = new RoomSupplementController.AdditionalRepositories(null);
//            repos.RepoTax = new TaxRepositoryTest();

//            s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), ObjectCopier.Clone<RoomSupplement>(rs), repos);
//            Assert.AreEqual(repo.Entity.Title, "changed");
//            Assert.AreEqual(repo.Entity.TaxId, 2);
//            Assert.AreEqual(repo.Entity.PriceHT, 999M);
//            Assert.AreEqual(repo.Entity.Id, 1);
//            Assert.AreEqual(repo.Entity.HomeId, 36);
//            Assert.AreEqual(repo.Entity.Hide, true);
//            Assert.IsNotNull(repo.Entity.DateModification);
//        }

//        [TestMethod]
//        public void PutShouldntPassBecauseOfForbiddenHomeId()
//        {
//            RoomSupplementRepositoryTest repo = new RoomSupplementRepositoryTest();

//            ModelStateDictionary dict = new ModelStateDictionary();
//            RoomSupplementService s = new RoomSupplementService(dict, repo, new RoomSupplementValidation());
//            RoomSupplement rs = repo.GetRoomSupplementById(1, 0);

//            rs.HomeId = 36;
//            rs.Hide = true;
//            rs.Title = "changed";
//            rs.PriceHT = 999M;

//            RoomSupplementController.AdditionalRepositories repos = new RoomSupplementController.AdditionalRepositories(null);
//            repos.RepoTax = new TaxRepositoryTest();
//            repo.Invalid = true;
//            try
//            {
//                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), ObjectCopier.Clone<RoomSupplement>(rs), repos);
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
//        public void PutShouldntPassBecauseOfNullHide()
//        {
//            RoomSupplementRepositoryTest repo = new RoomSupplementRepositoryTest();

//            ModelStateDictionary dict = new ModelStateDictionary();
//            RoomSupplementService s = new RoomSupplementService(dict, repo, new RoomSupplementValidation());
//            RoomSupplement rs = repo.GetRoomSupplementById(1, 0);

//            rs.HomeId = 999;
//            rs.Hide = null;
//            rs.Title = "changed";
//            rs.PriceHT = 999M;

//            RoomSupplementController.AdditionalRepositories repos = new RoomSupplementController.AdditionalRepositories(null);
//            repos.RepoTax = new TaxRepositoryTest();

//            s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), ObjectCopier.Clone<RoomSupplement>(rs), repos);
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ManahostValidationException))]
//        public void PutShouldntPassBecauseOfInvalidTax()
//        {
//            RoomSupplementRepositoryTest repo = new RoomSupplementRepositoryTest();

//            ModelStateDictionary dict = new ModelStateDictionary();
//            RoomSupplementService s = new RoomSupplementService(dict, repo, new RoomSupplementValidation());
//            RoomSupplement rs = repo.GetRoomSupplementById(1, 0);

//            rs.HomeId = 999;
//            rs.Hide = true;
//            rs.Title = "changed";
//            rs.PriceHT = 999M;
//            rs.TaxId = 999;

//            RoomSupplementController.AdditionalRepositories repos = new RoomSupplementController.AdditionalRepositories(null);
//            repos.RepoTax = new TaxRepositoryTest();

//            s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), ObjectCopier.Clone<RoomSupplement>(rs),
//                repos);
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ManahostValidationException))]
//        public void PutShouldntPassBecauseOfInvalidPrice()
//        {
//            RoomSupplementRepositoryTest repo = new RoomSupplementRepositoryTest();

//            ModelStateDictionary dict = new ModelStateDictionary();
//            RoomSupplementService s = new RoomSupplementService(dict, repo, new RoomSupplementValidation());
//            RoomSupplement rs = repo.GetRoomSupplementById(1, 0);

//            rs.HomeId = 999;
//            rs.Hide = true;
//            rs.Title = "changed";
//            rs.PriceHT = -999M;

//            RoomSupplementController.AdditionalRepositories repos = new RoomSupplementController.AdditionalRepositories(null);
//            repos.RepoTax = new TaxRepositoryTest();

//            s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), ObjectCopier.Clone<RoomSupplement>(rs), repos);
//        }

//        [TestMethod]
//        public void DeleteShouldPass()
//        {
//            RoomSupplementRepositoryTest repo = new RoomSupplementRepositoryTest();

//            ModelStateDictionary dict = new ModelStateDictionary();
//            RoomSupplementService s = new RoomSupplementService(dict, repo, new RoomSupplementValidation());

//            s.PreDelete(new ClientRepositoryTest().FindUserByMail("test@test.com"), 1, null);
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ManahostValidationException))]
//        public void DeleteShouldntPassBecauseOfInvalidResource()
//        {
//            RoomSupplementRepositoryTest repo = new RoomSupplementRepositoryTest();

//            ModelStateDictionary dict = new ModelStateDictionary();
//            RoomSupplementService s = new RoomSupplementService(dict, repo, new RoomSupplementValidation());

//            s.PreDelete(new ClientRepositoryTest().FindUserByMail("test@test.com"), -1, null);
//        }
//    }
//}