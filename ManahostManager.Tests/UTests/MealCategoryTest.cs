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
//    public class MealCategoryTest
//    {
//        private Client client0;

//        public MealCategoryTest()
//        {
//            client0 = new Client();
//            client0.Id = 0;
//            client0.DefaultHomeId = 0;
//        }

//        [TestMethod]
//        public void doPostTest()
//        {
//            MealCategoryRepositoryTest repo = new MealCategoryRepositoryTest();

//            ModelStateDictionary wrap = new ModelStateDictionary();
//            MealCategoryService c = new MealCategoryService(wrap, repo, new MealCategoryValidation());

//            {
//                MealCategory mealCat = repo.GetMealCategoryById(0, 0);
//                c.PrePost(client0, ObjectCopier.Clone<MealCategory>(mealCat), null);
//                Assert.IsTrue(true);
//                Assert.AreEqual<bool?>(true, repo.Entity.RefHide);
//            }
//        }

//        [TestMethod]
//        public void doPostTestShouldFailBecauseOfForbiddenHomeId()
//        {
//            MealCategoryRepositoryTest repo = new MealCategoryRepositoryTest();

//            ModelStateDictionary wrap = new ModelStateDictionary();
//            MealCategoryService c = new MealCategoryService(wrap, repo, new MealCategoryValidation());

//            {
//                MealCategory mealCat = repo.GetMealCategoryById(0, 0);
//                repo.Invalid = true;
//                try
//                {
//                    c.PrePost(client0, ObjectCopier.Clone<MealCategory>(mealCat), null);
//                    Assert.IsTrue(false);
//                }
//                catch (ManahostValidationException)
//                {
//                    DictionnaryAssert.DictionnaryHasValueAndError(wrap, "HomeId",
//    GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//                }
//            }
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ManahostValidationException))]
//        public void doPostTestLabelNullShouldntWork()
//        {
//            MealCategoryRepositoryTest repo = new MealCategoryRepositoryTest();

//            ModelStateDictionary wrap = new ModelStateDictionary();
//            MealCategoryService c = new MealCategoryService(wrap, repo, new MealCategoryValidation());

//            {
//                MealCategory mealCat = repo.GetMealCategoryById(0, 0);
//                mealCat.Label = null;
//                c.PrePost(client0, ObjectCopier.Clone<MealCategory>(mealCat), null);
//            }
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ManahostValidationException))]
//        public void doPostTestUnauthorizedShouldntWork()
//        {
//            MealCategoryRepositoryTest repo = new MealCategoryRepositoryTest();

//            ModelStateDictionary wrap = new ModelStateDictionary();
//            MealCategoryService c = new MealCategoryService(wrap, repo, new MealCategoryValidation());

//            {
//                MealCategory mealCat = repo.GetMealCategoryById(0, 0);
//                mealCat.Label = null;
//                c.PrePost(client0, ObjectCopier.Clone<MealCategory>(mealCat), null);
//            }
//        }

//        [TestMethod]
//        public void doPutTest()
//        {
//            MealCategoryRepositoryTest repo = new MealCategoryRepositoryTest();

//            ModelStateDictionary wrap = new ModelStateDictionary();
//            MealCategoryService c = new MealCategoryService(wrap, repo, new MealCategoryValidation());

//            {
//                MealCategory mealCat = repo.GetMealCategoryById(0, 0);
//                repo.Entity = ObjectCopier.Clone<MealCategory>(mealCat);
//                mealCat.Label = "Prout";
//                mealCat.RefHide = true;
//                c.PrePut(client0, mealCat, null);
//                Assert.IsTrue(true);
//                Assert.AreEqual<String>("Prout", repo.Entity.Label);
//            }
//        }

//        [TestMethod]
//        public void doPutTestShouldFailBecauseOfForbiddenHomeId()
//        {
//            MealCategoryRepositoryTest repo = new MealCategoryRepositoryTest();

//            ModelStateDictionary wrap = new ModelStateDictionary();
//            MealCategoryService c = new MealCategoryService(wrap, repo, new MealCategoryValidation());

//            {
//                MealCategory mealCat = repo.GetMealCategoryById(0, 0);
//                repo.Entity = ObjectCopier.Clone<MealCategory>(mealCat);
//                mealCat.Label = "Prout";
//                mealCat.RefHide = true;
//                repo.Invalid = true;
//                try
//                {
//                    c.PrePut(client0, mealCat, null);
//                    Assert.IsTrue(false);
//                }
//                catch (ManahostValidationException)
//                {
//                    DictionnaryAssert.DictionnaryHasValueAndError(wrap, "HomeId",
//GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//                }
//            }
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ManahostValidationException))]
//        public void doPutTestUnauthorizedShouldntWork()
//        {
//            MealCategoryRepositoryTest repo = new MealCategoryRepositoryTest();

//            ModelStateDictionary wrap = new ModelStateDictionary();
//            MealCategoryService c = new MealCategoryService(wrap, repo, new MealCategoryValidation());

//            {
//                MealCategory mealCat = repo.GetMealCategoryById(0, 0);
//                mealCat.Id = -1;
//                repo.Entity = ObjectCopier.Clone<MealCategory>(mealCat);
//                client0.DefaultHomeId = 1;
//                c.PrePut(client0, mealCat, null);
//            }
//        }

//        [TestMethod]
//        public void doDeleteTest()
//        {
//            MealCategoryRepositoryTest repo = new MealCategoryRepositoryTest();

//            ModelStateDictionary wrap = new ModelStateDictionary();
//            MealCategoryService c = new MealCategoryService(wrap, repo, new MealCategoryValidation());

//            {
//                c.PreDelete(client0, 0, null);
//                Assert.IsTrue(true);
//            }
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ManahostValidationException))]
//        public void doDeleteTestUnauthorizedShouldntWork()
//        {
//            MealCategoryRepositoryTest repo = new MealCategoryRepositoryTest();

//            ModelStateDictionary wrap = new ModelStateDictionary();
//            MealCategoryService c = new MealCategoryService(wrap, repo, new MealCategoryValidation());

//            {
//                client0.DefaultHomeId = 1;
//                c.PreDelete(client0, -1, null);
//                Assert.IsTrue(true);
//            }
//        }
//    }
//}