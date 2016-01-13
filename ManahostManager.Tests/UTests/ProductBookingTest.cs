using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ManahostManager.Tests.UTests
{
    [TestClass]
    public class ProductBookingTest
    {
        //        private ProductBookingController.AdditionalRepositories addrepo;
        //        private ProductBookingRepositoryTest repo;

        //        private ModelStateDictionary dict;
        //        private ProductBookingService s;

        //        private ProductBooking entity;

        //        [TestInitialize]
        //        public void Init()
        //        {
        //            addrepo = new ProductBookingController.AdditionalRepositories(null);
        //            repo = new ProductBookingRepositoryTest();

        //            dict = new ModelStateDictionary();
        //            s = new ProductBookingService(dict, repo, new ProductBookingValidation());

        //            addrepo.BookingRepo = new BookingRepositoryTest();
        //            addrepo.ProductRepo = new ProductRepositoryTest();
        //            entity = new ProductBooking()
        //            {
        //                BookingId = 1,
        //                Date = DateTime.Now,
        //                HomeId = 1,
        //                Id = 1,
        //                ProductId = 11,
        //                Quantity = 2,
        //                Product = new Product()
        //                {
        //                    BillItemCategoryId = 1,
        //                    Duration = 10,
        //                    Hide = false,
        //                    HomeId = 1,
        //                    Id = 1,
        //                    PeopleId = 1,
        //                    PriceHT = 50M,
        //                    ProductCategoryId = 1,
        //                    RefHide = false,
        //                    Stock = 1,
        //                    SupplierId = 1,
        //                    TaxId = 2,
        //                    Threshold = 5,
        //                    Title = "Title",
        //                    Tax = new Tax()
        //                    {
        //                        HomeId = 1,
        //                        Id = 1,
        //                        Price = 555M,
        //                        Title = "Tax",
        //                        ValueType = EValueType.AMOUNT
        //                    }
        //                },
        //            };
        //        }

        //        [TestMethod]
        //        [ExpectedException(typeof(ManahostValidationException))]
        //        public void PostNullShouldFail()
        //        {
        //            s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), null, addrepo);
        //        }

        //        [TestMethod]
        //        [ExpectedException(typeof(ManahostValidationException))]
        //        public void PutNullShouldFail()
        //        {
        //            s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), null, addrepo);
        //        }

        //        [TestMethod]
        //        public void ShouldPassPost()
        //        {
        //            try
        //            {
        //                ProductBooking saved = ObjectCopier.Clone<ProductBooking>(entity);
        //                s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
        //                Assert.IsTrue(true);
        //                Assert.AreEqual(saved.BookingId, repo.Entity.BookingId);
        //                Assert.AreEqual(saved.Date, repo.Entity.Date);
        //                Assert.AreEqual(null, repo.Entity.Duration);
        //                Assert.AreEqual(saved.HomeId, repo.Entity.HomeId);
        //                Assert.AreEqual(saved.Id, repo.Entity.Id);
        //                Assert.AreEqual(0M, repo.Entity.PriceHT);
        //                Assert.AreEqual(0M, repo.Entity.PriceTTC);
        //                Assert.AreEqual(saved.ProductId, repo.Entity.ProductId);
        //                Assert.AreEqual(saved.Quantity, repo.Entity.Quantity);
        //            }
        //            catch (ManahostValidationException)
        //            {
        //                Assert.IsTrue(false);
        //            }
        //        }

        //        [TestMethod]
        //        public void ShouldntPassPostBecauseOfForbiddenResource()
        //        {
        //            try
        //            {
        //                repo.Invalid = true;
        //                entity.Product.Tax = null;
        //                ProductBooking saved = ObjectCopier.Clone<ProductBooking>(entity);
        //                s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
        //                Assert.IsTrue(false);
        //            }
        //            catch (ManahostValidationException)
        //            {
        //                DictionnaryAssert.DictionnaryHasValueAndError(dict, "HomeId",
        //GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
        //            }
        //        }

        //        [TestMethod]
        //        public void ShouldPassDelete()
        //        {
        //            s.PreDelete(new ClientRepositoryTest().FindUserByMail("test@test.com"), 1, null);
        //        }

        //        [TestMethod]
        //        public void ShouldntPassDeleteBecauseOfForbiddenResource()
        //        {
        //            try
        //            {
        //                s.PreDelete(new ClientRepositoryTest().FindUserByMail("test@test.com"), -1, null);
        //            }
        //            catch (ManahostValidationException)
        //            {
        //                DictionnaryAssert.DictionnaryHasValueAndError(dict, TypeOfName.GetNameFromType<ProductBooking>(), GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
        //            }
        //        }

        //        [TestMethod]
        //        public void ComputeShouldPass()
        //        {
        //            try
        //            {
        //                ProductBooking saved = ObjectCopier.Clone<ProductBooking>(entity);
        //                s.Compute(new ClientRepositoryTest().FindUserByMail("test@test.com"), 1, addrepo);
        //                Assert.IsTrue(true);
        //                Assert.AreEqual(saved.BookingId, repo.Entity.BookingId);
        //                Assert.AreEqual(((DateTime)saved.Date).Date, ((DateTime)repo.Entity.Date).Date);
        //                Assert.AreEqual(20, repo.Entity.Duration);
        //                Assert.AreEqual(saved.HomeId, repo.Entity.HomeId);
        //                Assert.AreEqual(saved.Id, repo.Entity.Id);
        //                Assert.AreEqual(100M, repo.Entity.PriceHT);
        //                Assert.AreEqual(1210M, repo.Entity.PriceTTC);
        //                Assert.AreEqual(saved.ProductId, repo.Entity.ProductId);
        //                Assert.AreEqual(saved.Quantity, repo.Entity.Quantity);
        //            }
        //            catch (ManahostValidationException)
        //            {
        //                Assert.IsTrue(false);
        //            }
        //        }

        //        [TestMethod]
        //        public void ComputeShouldPassWithPercent()
        //        {
        //            try
        //            {
        //                ProductBooking saved = ObjectCopier.Clone<ProductBooking>(entity);
        //                s.Compute(new ClientRepositoryTest().FindUserByMail("test@test.com"), 3, addrepo);
        //                Assert.IsTrue(true);
        //                Assert.AreEqual(saved.BookingId, repo.Entity.BookingId);
        //                Assert.AreEqual(((DateTime)saved.Date).Date, ((DateTime)repo.Entity.Date).Date);
        //                Assert.AreEqual(20, repo.Entity.Duration);
        //                Assert.AreEqual(saved.HomeId, repo.Entity.HomeId);
        //                Assert.AreEqual(saved.Id, repo.Entity.Id);
        //                Assert.AreEqual(100M, repo.Entity.PriceHT);
        //                Assert.AreEqual(110M, repo.Entity.PriceTTC);
        //                Assert.AreEqual(12, repo.Entity.ProductId);
        //                Assert.AreEqual(saved.Quantity, repo.Entity.Quantity);
        //            }
        //            catch (ManahostValidationException)
        //            {
        //                Assert.IsTrue(false);
        //            }
        //        }

        //        [TestMethod]
        //        public void ShouldPassPut()
        //        {
        //            try
        //            {
        //                entity.PriceTTC = 0M;
        //                entity.PriceHT = 0M;
        //                entity.Quantity = 3;
        //                ProductBooking saved = ObjectCopier.Clone<ProductBooking>(entity);
        //                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
        //                Assert.IsTrue(true);
        //                Assert.AreEqual(saved.BookingId, repo.Entity.BookingId);
        //                Assert.AreEqual(((DateTime)saved.Date).Date, ((DateTime)repo.Entity.Date).Date);
        //                Assert.AreEqual(null, repo.Entity.Duration);
        //                Assert.AreEqual(saved.HomeId, repo.Entity.HomeId);
        //                Assert.AreEqual(saved.Id, repo.Entity.Id);
        //                Assert.AreEqual(0M, repo.Entity.PriceHT);
        //                Assert.AreEqual(0M, repo.Entity.PriceTTC);
        //                Assert.AreEqual(saved.ProductId, repo.Entity.ProductId);
        //                Assert.AreEqual(saved.Quantity, repo.Entity.Quantity);
        //            }
        //            catch (ManahostValidationException)
        //            {
        //                Assert.IsTrue(false);
        //            }
        //        }

        //        [TestMethod]
        //        public void ShouldntPassPutBecauseOfForbiddenResource()
        //        {
        //            try
        //            {
        //                repo.Invalid = true;

        //                entity.Quantity = 3;
        //                ProductBooking saved = ObjectCopier.Clone<ProductBooking>(entity);
        //                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
        //                Assert.IsTrue(false);
        //            }
        //            catch (ManahostValidationException)
        //            {
        //                DictionnaryAssert.DictionnaryHasValueAndError(dict, "HomeId",
        //GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
        //            }
        //        }

        //        [TestMethod]
        //        public void ShouldntPassPostBecauseOfNullBookingId()
        //        {
        //            try
        //            {
        //                entity.BookingId = null;
        //                s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
        //                Assert.IsTrue(false);
        //            }
        //            catch (ManahostValidationException)
        //            {
        //                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<ProductBooking>(), "BookingId"),
        //GenericError.CANNOT_BE_NULL_OR_EMPTY);
        //            }
        //        }

        //        [TestMethod]
        //        public void ShouldntPassPutBecauseOfNullBookingId()
        //        {
        //            try
        //            {
        //                entity.PriceHT = 0M;
        //                entity.PriceTTC = 0M;
        //                entity.BookingId = null;
        //                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
        //                Assert.IsTrue(false);
        //            }
        //            catch (ManahostValidationException)
        //            {
        //                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<ProductBooking>(), "BookingId"),
        //GenericError.CANNOT_BE_NULL_OR_EMPTY);
        //            }
        //        }

        //        [TestMethod]
        //        public void ShouldntPassPostBecauseOfNullProductId()
        //        {
        //            try
        //            {
        //                entity.ProductId = null;
        //                s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
        //                Assert.IsTrue(false);
        //            }
        //            catch (ManahostValidationException)
        //            {
        //                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<ProductBooking>(), "ProductId"),
        //GenericError.CANNOT_BE_NULL_OR_EMPTY);
        //            }
        //        }

        //        [TestMethod]
        //        public void ShouldntPassPutBecauseOfNullProductId()
        //        {
        //            try
        //            {
        //                entity.PriceHT = 0M;
        //                entity.PriceTTC = 0M;
        //                entity.ProductId = null;
        //                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
        //                Assert.IsTrue(false);
        //            }
        //            catch (ManahostValidationException)
        //            {
        //                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<ProductBooking>(), "ProductId"),
        //GenericError.CANNOT_BE_NULL_OR_EMPTY);
        //            }
        //        }

        //        [TestMethod]
        //        public void ShouldntPassPostBecauseOfNullQuantity()
        //        {
        //            try
        //            {
        //                entity.Quantity = null;
        //                s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
        //                Assert.IsTrue(false);
        //            }
        //            catch (ManahostValidationException)
        //            {
        //                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<ProductBooking>(), "Quantity"),
        //GenericError.CANNOT_BE_NULL_OR_EMPTY);
        //            }
        //        }

        //        [TestMethod]
        //        public void ShouldntPassPutBecauseOfNullQuantity()
        //        {
        //            try
        //            {
        //                entity.PriceHT = 0M;
        //                entity.PriceTTC = 0M;
        //                entity.Quantity = null;
        //                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
        //                Assert.IsTrue(false);
        //            }
        //            catch (ManahostValidationException)
        //            {
        //                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<ProductBooking>(), "Quantity"),
        //GenericError.CANNOT_BE_NULL_OR_EMPTY);
        //            }
        //        }

        //        [TestMethod]
        //        public void ShouldntPassPostBecauseOfForbiddenBookingId()
        //        {
        //            try
        //            {
        //                entity.BookingId = -1;
        //                s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
        //                Assert.IsTrue(false);
        //            }
        //            catch (ManahostValidationException)
        //            {
        //                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<ProductBooking>(), "BookingId"),
        //GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
        //            }
        //        }

        //        [TestMethod]
        //        public void ShouldntPassPutBecauseOfForbiddenBookingId()
        //        {
        //            try
        //            {
        //                entity.PriceHT = 0M;
        //                entity.PriceTTC = 0M;
        //                entity.BookingId = -1;
        //                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
        //                Assert.IsTrue(false);
        //            }
        //            catch (ManahostValidationException)
        //            {
        //                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<ProductBooking>(), "BookingId"),
        //GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
        //            }
        //        }

        //        [TestMethod]
        //        public void ShouldntPassPostBecauseOfInvalidDuration()
        //        {
        //            try
        //            {
        //                entity.Duration = -1;
        //                s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
        //                Assert.IsTrue(false);
        //            }
        //            catch (ManahostValidationException)
        //            {
        //                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<ProductBooking>(), "Duration"),
        //GenericError.DOES_NOT_MEET_REQUIREMENTS);
        //            }
        //        }

        //        [TestMethod]
        //        public void ShouldntPassPutBecauseOfInvalidDuration()
        //        {
        //            try
        //            {
        //                entity.PriceHT = 0M;
        //                entity.PriceTTC = 0M;
        //                entity.Duration = -1;
        //                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
        //                Assert.IsTrue(false);
        //            }
        //            catch (ManahostValidationException)
        //            {
        //                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<ProductBooking>(), "Duration"),
        //GenericError.DOES_NOT_MEET_REQUIREMENTS);
        //            }
        //        }

        //        [TestMethod]
        //        public void ShouldntPassPostBecauseOfInvalidPriceHT()
        //        {
        //            try
        //            {
        //                entity.PriceHT = -1M;
        //                entity.PriceTTC = 0M;
        //                s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
        //                Assert.IsTrue(false);
        //            }
        //            catch (ManahostValidationException)
        //            {
        //                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<ProductBooking>(), "PriceHT"),
        //GenericError.DOES_NOT_MEET_REQUIREMENTS);
        //            }
        //        }

        //        [TestMethod]
        //        public void ShouldntPassPutBecauseOfInvalidPriceHT()
        //        {
        //            try
        //            {
        //                entity.PriceTTC = 0M;
        //                entity.PriceHT = -1M;
        //                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
        //                Assert.IsTrue(false);
        //            }
        //            catch (ManahostValidationException)
        //            {
        //                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<ProductBooking>(), "PriceHT"),
        //GenericError.DOES_NOT_MEET_REQUIREMENTS);
        //            }
        //        }

        //        [TestMethod]
        //        public void ShouldntPassPostBecauseOfInvalidQuantity()
        //        {
        //            try
        //            {
        //                entity.Quantity = -1;
        //                s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
        //                Assert.IsTrue(false);
        //            }
        //            catch (ManahostValidationException)
        //            {
        //                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<ProductBooking>(), "Quantity"),
        //GenericError.DOES_NOT_MEET_REQUIREMENTS);
        //            }
        //        }

        //        [TestMethod]
        //        public void ShouldntPassPutBecauseOfInvalidQuantity()
        //        {
        //            try
        //            {
        //                entity.PriceHT = 0M;
        //                entity.PriceTTC = 0M;
        //                entity.Quantity = -1;
        //                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
        //                Assert.IsTrue(false);
        //            }
        //            catch (ManahostValidationException)
        //            {
        //                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<ProductBooking>(), "Quantity"),
        //GenericError.DOES_NOT_MEET_REQUIREMENTS);
        //            }
        //        }

        //        [TestMethod]
        //        public void ShouldntPassPostBecauseOfForbiddenProductId()
        //        {
        //            try
        //            {
        //                entity.ProductId = -1;
        //                s.PrePost(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
        //                Assert.IsTrue(false);
        //            }
        //            catch (ManahostValidationException)
        //            {
        //                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<ProductBooking>(), "ProductId"),
        //GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
        //            }
        //        }

        //        [TestMethod]
        //        public void ShouldntPassPutBecauseOfForbiddenProductId()
        //        {
        //            try
        //            {
        //                entity.PriceHT = 0M;
        //                entity.PriceTTC = 0M;
        //                entity.ProductId = -1;
        //                s.PrePut(new ClientRepositoryTest().FindUserByMail("test@test.com"), entity, addrepo);
        //                Assert.IsTrue(false);
        //            }
        //            catch (ManahostValidationException)
        //            {
        //                DictionnaryAssert.DictionnaryHasValueAndError(dict, String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<ProductBooking>(), "ProductId"),
        //GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
        //            }
        //}
    }
}