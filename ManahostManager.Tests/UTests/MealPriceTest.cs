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
//    public class MealPriceTest
//    {
//        private Client client0;

//        public MealPriceTest()
//        {
//            client0 = new Client();
//            client0.Id = 0;
//            client0.DefaultHomeId = 0;
//        }

//        [TestMethod]
//        public void doPostTest()
//        {
//            MealPriceRepositoryTest repo = new MealPriceRepositoryTest();

//            ModelStateDictionary wrap = new ModelStateDictionary();
//            MealPriceService c = new MealPriceService(wrap, repo, new MealPriceValidation());
//            MealPriceController.AdditionalRepositories addRepo = new MealPriceController.AdditionalRepositories()
//            {
//                RepoMeal = new MealRepositoryTest(),
//                RepoTax = new TaxRepositoryTest(),
//                RepoPeopleCat = new PeopleCategoryRepositoryTest()
//            };

//            {
//                MealPrice mealPrice = repo.GetMealPriceById(0, 0);
//                c.PrePost(client0, ObjectCopier.Clone<MealPrice>(mealPrice), addRepo);
//                Assert.IsTrue(true);
//                Assert.AreEqual<Decimal?>(50, repo.Entity.PriceHT);
//            }
//        }

//        [TestMethod]
//        public void doPostTestShouldFailBecauseOfForbiddenHomeId()
//        {
//            MealPriceRepositoryTest repo = new MealPriceRepositoryTest();

//            ModelStateDictionary wrap = new ModelStateDictionary();
//            MealPriceService c = new MealPriceService(wrap, repo, new MealPriceValidation());
//            MealPriceController.AdditionalRepositories addRepo = new MealPriceController.AdditionalRepositories()
//            {
//                RepoMeal = new MealRepositoryTest(),
//                RepoTax = new TaxRepositoryTest(),
//                RepoPeopleCat = new PeopleCategoryRepositoryTest()
//            };

//            {
//                MealPrice mealPrice = repo.GetMealPriceById(0, 0);
//                repo.Invalid = true;
//                try
//                {
//                    c.PrePost(client0, ObjectCopier.Clone<MealPrice>(mealPrice), addRepo);
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
//        public void doPostTestPriceHTNullShouldntWork()
//        {
//            MealPriceRepositoryTest repo = new MealPriceRepositoryTest();

//            ModelStateDictionary wrap = new ModelStateDictionary();
//            MealPriceService c = new MealPriceService(wrap, repo, new MealPriceValidation());
//            MealPriceController.AdditionalRepositories addRepo = new MealPriceController.AdditionalRepositories()
//            {
//                RepoMeal = new MealRepositoryTest(),
//                RepoTax = new TaxRepositoryTest(),
//                RepoPeopleCat = new PeopleCategoryRepositoryTest()
//            };

//            {
//                MealPrice mealPrice = repo.GetMealPriceById(0, 0);
//                mealPrice.PriceHT = null;
//                c.PrePost(client0, ObjectCopier.Clone<MealPrice>(mealPrice), addRepo);
//            }
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ManahostValidationException))]
//        public void doPostTestPriceHTNegShouldntWork()
//        {
//            MealPriceRepositoryTest repo = new MealPriceRepositoryTest();

//            ModelStateDictionary wrap = new ModelStateDictionary();
//            MealPriceService c = new MealPriceService(wrap, repo, new MealPriceValidation());
//            MealPriceController.AdditionalRepositories addRepo = new MealPriceController.AdditionalRepositories()
//            {
//                RepoMeal = new MealRepositoryTest(),
//                RepoTax = new TaxRepositoryTest(),
//                RepoPeopleCat = new PeopleCategoryRepositoryTest()
//            };

//            {
//                MealPrice mealPrice = repo.GetMealPriceById(0, 0);
//                mealPrice.PriceHT = -1;
//                c.PrePost(client0, ObjectCopier.Clone<MealPrice>(mealPrice), addRepo);
//            }
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ManahostValidationException))]
//        public void doPostTestMealIdNullShouldntWork()
//        {
//            MealPriceRepositoryTest repo = new MealPriceRepositoryTest();

//            ModelStateDictionary wrap = new ModelStateDictionary();
//            MealPriceService c = new MealPriceService(wrap, repo, new MealPriceValidation());
//            MealPriceController.AdditionalRepositories addRepo = new MealPriceController.AdditionalRepositories()
//            {
//                RepoMeal = new MealRepositoryTest(),
//                RepoTax = new TaxRepositoryTest(),
//                RepoPeopleCat = new PeopleCategoryRepositoryTest()
//            };

//            {
//                MealPrice mealPrice = repo.GetMealPriceById(0, 0);
//                mealPrice.MealId = null;
//                c.PrePost(client0, ObjectCopier.Clone<MealPrice>(mealPrice), addRepo);
//            }
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ManahostValidationException))]
//        public void doPostTestEntityNullShouldntWork()
//        {
//            MealPriceRepositoryTest repo = new MealPriceRepositoryTest();

//            ModelStateDictionary wrap = new ModelStateDictionary();
//            MealPriceService c = new MealPriceService(wrap, repo, new MealPriceValidation());
//            MealPriceController.AdditionalRepositories addRepo = new MealPriceController.AdditionalRepositories()
//            {
//                RepoMeal = new MealRepositoryTest(),
//                RepoTax = new TaxRepositoryTest(),
//                RepoPeopleCat = new PeopleCategoryRepositoryTest()
//            };

//            {
//                c.PrePost(client0, null, addRepo);
//            }
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ManahostValidationException))]
//        public void doPostTestUnauthorizedMealIdShouldntWork()
//        {
//            MealPriceRepositoryTest repo = new MealPriceRepositoryTest();

//            ModelStateDictionary wrap = new ModelStateDictionary();
//            MealPriceService c = new MealPriceService(wrap, repo, new MealPriceValidation());
//            MealPriceController.AdditionalRepositories addRepo = new MealPriceController.AdditionalRepositories()
//            {
//                RepoMeal = new MealRepositoryTest(),
//                RepoTax = new TaxRepositoryTest(),
//                RepoPeopleCat = new PeopleCategoryRepositoryTest()
//            };

//            {
//                MealPrice mealPrice = repo.GetMealPriceById(0, 0);
//                mealPrice.MealId = 42;
//                c.PrePost(client0, ObjectCopier.Clone<MealPrice>(mealPrice), addRepo);
//            }
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ManahostValidationException))]
//        public void doPostTestUnauthorizedPeopleCategoryIdShouldntWork()
//        {
//            MealPriceRepositoryTest repo = new MealPriceRepositoryTest();

//            ModelStateDictionary wrap = new ModelStateDictionary();
//            MealPriceService c = new MealPriceService(wrap, repo, new MealPriceValidation());
//            MealPriceController.AdditionalRepositories addRepo = new MealPriceController.AdditionalRepositories()
//            {
//                RepoMeal = new MealRepositoryTest(),
//                RepoTax = new TaxRepositoryTest(),
//                RepoPeopleCat = new PeopleCategoryRepositoryTest()
//            };

//            {
//                MealPrice mealPrice = repo.GetMealPriceById(0, 0);
//                mealPrice.PeopleCategoryId = 42;
//                c.PrePost(client0, ObjectCopier.Clone<MealPrice>(mealPrice), addRepo);
//            }
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ManahostValidationException))]
//        public void doPostTestUnauthorizedTaxIdIdShouldntWork()
//        {
//            MealPriceRepositoryTest repo = new MealPriceRepositoryTest();

//            ModelStateDictionary wrap = new ModelStateDictionary();
//            MealPriceService c = new MealPriceService(wrap, repo, new MealPriceValidation());
//            MealPriceController.AdditionalRepositories addRepo = new MealPriceController.AdditionalRepositories()
//            {
//                RepoMeal = new MealRepositoryTest(),
//                RepoTax = new TaxRepositoryTest(),
//                RepoPeopleCat = new PeopleCategoryRepositoryTest()
//            };

//            {
//                MealPrice mealPrice = repo.GetMealPriceById(0, 0);
//                mealPrice.TaxId = 42;
//                c.PrePost(client0, ObjectCopier.Clone<MealPrice>(mealPrice), addRepo);
//            }
//        }

//        [TestMethod]
//        public void doPutTest()
//        {
//            MealPriceRepositoryTest repo = new MealPriceRepositoryTest();

//            ModelStateDictionary wrap = new ModelStateDictionary();
//            MealPriceService c = new MealPriceService(wrap, repo, new MealPriceValidation());
//            MealPriceController.AdditionalRepositories addRepo = new MealPriceController.AdditionalRepositories()
//            {
//                RepoMeal = new MealRepositoryTest(),
//                RepoTax = new TaxRepositoryTest(),
//                RepoPeopleCat = new PeopleCategoryRepositoryTest()
//            };

//            {
//                MealPrice mealPrice = repo.GetMealPriceById(0, 0);
//                repo.Entity = ObjectCopier.Clone<MealPrice>(mealPrice);
//                mealPrice.PriceHT = 42;
//                c.PrePut(client0, mealPrice, addRepo);
//                Assert.IsTrue(true);
//                Assert.AreEqual<Decimal?>(42, repo.Entity.PriceHT);
//                Assert.AreEqual<int?>(0, repo.Entity.MealId);
//                Assert.AreEqual<int?>(0, repo.Entity.PeopleCategoryId);
//                Assert.AreEqual<int?>(0, repo.Entity.TaxId);
//            }
//        }

//        [TestMethod]
//        public void doPutTestShouldFailBecauseOfForbiddenHomeId()
//        {
//            MealPriceRepositoryTest repo = new MealPriceRepositoryTest();

//            ModelStateDictionary wrap = new ModelStateDictionary();
//            MealPriceService c = new MealPriceService(wrap, repo, new MealPriceValidation());
//            MealPriceController.AdditionalRepositories addRepo = new MealPriceController.AdditionalRepositories()
//            {
//                RepoMeal = new MealRepositoryTest(),
//                RepoTax = new TaxRepositoryTest(),
//                RepoPeopleCat = new PeopleCategoryRepositoryTest()
//            };

//            {
//                MealPrice mealPrice = repo.GetMealPriceById(0, 0);
//                repo.Entity = ObjectCopier.Clone<MealPrice>(mealPrice);
//                mealPrice.PriceHT = 42;
//                repo.Invalid = true;
//                try
//                {
//                    c.PrePut(client0, mealPrice, addRepo);
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
//            MealPriceRepositoryTest repo = new MealPriceRepositoryTest();

//            ModelStateDictionary wrap = new ModelStateDictionary();
//            MealPriceService c = new MealPriceService(wrap, repo, new MealPriceValidation());
//            MealPriceController.AdditionalRepositories addRepo = new MealPriceController.AdditionalRepositories()
//            {
//                RepoMeal = new MealRepositoryTest(),
//                RepoTax = new TaxRepositoryTest(),
//                RepoPeopleCat = new PeopleCategoryRepositoryTest()
//            };

//            {
//                MealPrice mealPrice = repo.GetMealPriceById(0, 0);
//                mealPrice.Id = -1;
//                repo.Entity = ObjectCopier.Clone<MealPrice>(mealPrice);
//                client0.DefaultHomeId = 42;
//                c.PrePut(client0, mealPrice, addRepo);
//            }
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ManahostValidationException))]
//        public void doPutTestEntityNullShouldntWork()
//        {
//            MealPriceRepositoryTest repo = new MealPriceRepositoryTest();

//            ModelStateDictionary wrap = new ModelStateDictionary();
//            MealPriceService c = new MealPriceService(wrap, repo, new MealPriceValidation());
//            MealPriceController.AdditionalRepositories addRepo = new MealPriceController.AdditionalRepositories()
//            {
//                RepoMeal = new MealRepositoryTest(),
//                RepoTax = new TaxRepositoryTest(),
//                RepoPeopleCat = new PeopleCategoryRepositoryTest()
//            };

//            {
//                c.PrePut(client0, null, addRepo);
//            }
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ManahostValidationException))]
//        public void doPutTestUnauthorizedMealIdShouldntWork()
//        {
//            MealPriceRepositoryTest repo = new MealPriceRepositoryTest();

//            ModelStateDictionary wrap = new ModelStateDictionary();
//            MealPriceService c = new MealPriceService(wrap, repo, new MealPriceValidation());
//            MealPriceController.AdditionalRepositories addRepo = new MealPriceController.AdditionalRepositories()
//            {
//                RepoMeal = new MealRepositoryTest(),
//                RepoTax = new TaxRepositoryTest(),
//                RepoPeopleCat = new PeopleCategoryRepositoryTest()
//            };

//            {
//                MealPrice mealPrice = repo.GetMealPriceById(0, 0);
//                repo.Entity = ObjectCopier.Clone<MealPrice>(mealPrice);
//                mealPrice.MealId = 42;
//                c.PrePut(client0, mealPrice, addRepo);
//            }
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ManahostValidationException))]
//        public void doPutTestUnauthorizedPeopleCategoryIdShouldntWork()
//        {
//            MealPriceRepositoryTest repo = new MealPriceRepositoryTest();

//            ModelStateDictionary wrap = new ModelStateDictionary();
//            MealPriceService c = new MealPriceService(wrap, repo, new MealPriceValidation());
//            MealPriceController.AdditionalRepositories addRepo = new MealPriceController.AdditionalRepositories()
//            {
//                RepoMeal = new MealRepositoryTest(),
//                RepoTax = new TaxRepositoryTest(),
//                RepoPeopleCat = new PeopleCategoryRepositoryTest()
//            };

//            {
//                MealPrice mealPrice = repo.GetMealPriceById(0, 0);
//                repo.Entity = ObjectCopier.Clone<MealPrice>(mealPrice);
//                mealPrice.PeopleCategoryId = 42;
//                c.PrePut(client0, mealPrice, addRepo);
//            }
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ManahostValidationException))]
//        public void doPutTestUnauthorizedTaxIdIdShouldntWork()
//        {
//            MealPriceRepositoryTest repo = new MealPriceRepositoryTest();

//            ModelStateDictionary wrap = new ModelStateDictionary();
//            MealPriceService c = new MealPriceService(wrap, repo, new MealPriceValidation());
//            MealPriceController.AdditionalRepositories addRepo = new MealPriceController.AdditionalRepositories()
//            {
//                RepoMeal = new MealRepositoryTest(),
//                RepoTax = new TaxRepositoryTest(),
//                RepoPeopleCat = new PeopleCategoryRepositoryTest()
//            };

//            {
//                MealPrice mealPrice = repo.GetMealPriceById(0, 0);
//                repo.Entity = ObjectCopier.Clone<MealPrice>(mealPrice);
//                mealPrice.TaxId = 42;
//                c.PrePut(client0, mealPrice, addRepo);
//            }
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ManahostValidationException))]
//        public void doPutTestPriceHTNegShouldntWork()
//        {
//            MealPriceRepositoryTest repo = new MealPriceRepositoryTest();

//            ModelStateDictionary wrap = new ModelStateDictionary();
//            MealPriceService c = new MealPriceService(wrap, repo, new MealPriceValidation());
//            MealPriceController.AdditionalRepositories addRepo = new MealPriceController.AdditionalRepositories()
//            {
//                RepoMeal = new MealRepositoryTest(),
//                RepoTax = new TaxRepositoryTest(),
//                RepoPeopleCat = new PeopleCategoryRepositoryTest()
//            };

//            {
//                MealPrice mealPrice = repo.GetMealPriceById(0, 0);
//                repo.Entity = ObjectCopier.Clone<MealPrice>(mealPrice);
//                mealPrice.PriceHT = -42;
//                c.PrePut(client0, mealPrice, addRepo);
//            }
//        }

//        [TestMethod]
//        public void doDeleteTest()
//        {
//            MealPriceRepositoryTest repo = new MealPriceRepositoryTest();

//            ModelStateDictionary wrap = new ModelStateDictionary();
//            MealPriceService c = new MealPriceService(wrap, repo, new MealPriceValidation());
//            MealPriceController.AdditionalRepositories addRepo = new MealPriceController.AdditionalRepositories()
//            {
//                RepoMeal = new MealRepositoryTest(),
//                RepoTax = new TaxRepositoryTest(),
//                RepoPeopleCat = new PeopleCategoryRepositoryTest()
//            };

//            {
//                c.PreDelete(client0, 0, addRepo);
//                Assert.IsTrue(true);
//            }
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ManahostValidationException))]
//        public void doDeleteTestInexistantEntityShouldntWork()
//        {
//            MealPriceRepositoryTest repo = new MealPriceRepositoryTest();

//            ModelStateDictionary wrap = new ModelStateDictionary();
//            MealPriceService c = new MealPriceService(wrap, repo, new MealPriceValidation());
//            MealPriceController.AdditionalRepositories addRepo = new MealPriceController.AdditionalRepositories()
//            {
//                RepoMeal = new MealRepositoryTest(),
//                RepoTax = new TaxRepositoryTest(),
//                RepoPeopleCat = new PeopleCategoryRepositoryTest()
//            };

//            {
//                c.PreDelete(client0, 42, addRepo);
//            }
//        }
//    }
//}