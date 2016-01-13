using ManahostManager.Domain.DTOs;
using ManahostManager.Domain.Entity;
using ManahostManager.Tests.ControllerTests.Utils;
using ManahostManager.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace ManahostManager.Tests.ControllerTests
{
    [TestClass]
    public class ProductControllerTest : ControllerTest<ProductDTO>
    {
        private const string path = "/api/Product";

        [TestInitialize]
        public void PostNestedEntities()
        {
            ProductDTO dto = new ProductDTO()
            {
                Duration = 10,
                Hide = false,
                HomeId = 1,
                Id = 1,
                PriceHT = 50M,
                RefHide = false,
                Stock = 1,
                Threshold = 5,
                Title = "Title",
                IsUnderThreshold = false,
                BillItemCategory = new BillItemCategoryDTO()
                {
                    HomeId = 1,
                    Id = 1,
                    Title = "BillItemCategoryTest"
                },
                ProductCategory = new ProductCategoryDTO()
                {
                    Id = 0,
                    HomeId = 1,
                    RefHide = true,
                    Title = "POST"
                },
                Supplier = new SupplierDTO()
                {
                    Id = 0,
                    SocietyName = "Nongfu",
                    Email = "nongfu@nongfu.cn",
                    HomeId = 1,
                    Comment = "Ceci est un commentaire",
                    Addr = "Ceci est une addresse",
                    Contact = "popop",
                    Phone1 = "0384158418518548",
                    Phone2 = "0384158418518548",
                    DateCreation = DateTime.MinValue
                },
                Tax = new TaxDTO()
                {
                    Id = 0,
                    HomeId = 1,
                    Price = 10M,
                    Title = "POST",
                    ValueType = EValueType.AMOUNT
                },
                ShortDescription = "short desc"
            };

            reqCreator.CreateRequest(st, path, HttpMethod.Post, dto, HttpStatusCode.OK, true, false);
            List<ProductDTO> search = (List<ProductDTO>)reqCreator.CallSearch(st, "Product/where/id/eq/" + reqCreator.deserializedEntity.Id,
                new List<string>() { "BillItemCategory", "Tax", "Supplier", "ProductCategory" }, typeof(List<ProductDTO>));
            Assert.AreEqual(search[0].Hide, dto.Hide);
            Assert.AreEqual(search[0].Duration, dto.Duration);
            Assert.AreEqual(search[0].HomeId, dto.HomeId);
            Assert.AreEqual(search[0].IsUnderThreshold, true);
            Assert.AreEqual(search[0].PriceHT, dto.PriceHT);
            Assert.AreEqual(search[0].RefHide, dto.RefHide);
            Assert.AreEqual(search[0].ShortDescription, dto.ShortDescription);
            Assert.AreEqual(search[0].Stock, dto.Stock);
            Assert.AreEqual(search[0].Threshold, dto.Threshold);
            Assert.AreEqual(search[0].Title, dto.Title);
            Assert.AreEqual(search[0].IsUnderThreshold, true);
            PropertiesComparison.PublicInstancePropertiesEqual<BillItemCategoryDTO>(dto.BillItemCategory, search[0].BillItemCategory);
            PropertiesComparison.PublicInstancePropertiesEqual<ProductCategoryDTO>(dto.ProductCategory, search[0].ProductCategory);
            PropertiesComparison.PublicInstancePropertiesEqual<SupplierDTO>(dto.Supplier, search[0].Supplier);
            PropertiesComparison.PublicInstancePropertiesEqual<TaxDTO>(dto.Tax, search[0].Tax);
            Assert.AreEqual(search[0].DateModification, null);
            entity = reqCreator.deserializedEntity;
        }

        [TestMethod]
        public void PutNestedEntities()
        {
            entity.Title = "PUT";
            entity.Supplier.SocietyName = "PUT";
            entity.BillItemCategory.Title = "PUT";
            entity.ProductCategory.Title = "PUT";
            entity.Tax.Title = "PUT";
            entity.IsUnderThreshold = false;
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.OK, false, false);
            List<ProductDTO> search = (List<ProductDTO>)reqCreator.CallSearch(st, "Product/where/id/eq/" + entity.Id,
                new List<string>() { "BillItemCategory", "Tax", "Supplier", "ProductCategory" }, typeof(List<ProductDTO>));
            Assert.AreEqual(entity.Hide, search[0].Hide);
            Assert.AreEqual(entity.Duration, search[0].Duration);
            Assert.AreEqual(entity.HomeId, search[0].HomeId);
            Assert.AreEqual(search[0].IsUnderThreshold, true);
            Assert.AreEqual(entity.PriceHT, search[0].PriceHT);
            Assert.AreEqual(entity.RefHide, search[0].RefHide);
            Assert.AreEqual(entity.ShortDescription, search[0].ShortDescription);
            Assert.AreEqual(entity.Stock, search[0].Stock);
            Assert.AreEqual(entity.Threshold, search[0].Threshold);
            Assert.AreEqual(entity.Title, search[0].Title);
            PropertiesComparison.PublicInstancePropertiesEqual<BillItemCategoryDTO>(search[0].BillItemCategory, entity.BillItemCategory);
            PropertiesComparison.PublicInstancePropertiesEqual<ProductCategoryDTO>(search[0].ProductCategory, entity.ProductCategory);
            PropertiesComparison.PublicInstancePropertiesEqual<SupplierDTO>(search[0].Supplier, entity.Supplier);
            PropertiesComparison.PublicInstancePropertiesEqual<TaxDTO>(search[0].Tax, entity.Tax);
            Assert.IsNotNull(search[0].DateModification);
        }

        [TestMethod]
        public void PutInvalidNestedEntities()
        {
            entity.Supplier.Id = -1;
            entity.BillItemCategory.Id = -1;
            entity.ProductCategory.Id = -1;
            entity.Tax.Id = -1;
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.BadRequest, false, false);
            List<ProductDTO> search = (List<ProductDTO>)reqCreator.CallSearch(st, "Product/where/id/eq/" + entity.Id,
                new List<string>() { "BillItemCategory", "Tax", "Supplier", "ProductCategory" }, typeof(List<ProductDTO>));
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("BillItemCategory",
                new List<string>() { GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST }),
                new KeyValuePair<string, List<string>>("Tax",
                new List<string>() { GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST }),
                new KeyValuePair<string, List<string>>("ProductCategory",
                new List<string>() { GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST }),
                new KeyValuePair<string, List<string>>("Supplier",
                new List<string>() { GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST })
            });
        }

        [TestMethod]
        public void PostForbiddenHomeId()
        {
            entity.HomeId = -1;
            reqCreator.CreateRequest(st, path, HttpMethod.Post, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("HomeId",
                new List<string>() { GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST })
            });
        }

        [TestMethod]
        public void PutForbiddenHomeId()
        {
            entity.HomeId = -1;
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("HomeId",
                new List<string>() { GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST })
            });
        }

        [TestMethod]
        public void PostNull()
        {
            reqCreator.CreateRequest(st, path, HttpMethod.Post, null, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>(TypeOfName.GetNameFromType<ProductDTO>(),
                new List<string>() { GenericError.CANNOT_BE_NULL_OR_EMPTY })
            });
        }

        [TestMethod]
        public void PostExceededTitle()
        {
            entity.Title = new string('*', 257);
            reqCreator.CreateRequest(st, path, HttpMethod.Post, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Title",
                new List<string>() { GenericError.DOES_NOT_MEET_REQUIREMENTS })
            });
        }

        [TestMethod]
        public void PostExceededShortDescription()
        {
            entity.ShortDescription = new string('*', 257);
            reqCreator.CreateRequest(st, path, HttpMethod.Post, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("ShortDescription",
                new List<string>() { GenericError.DOES_NOT_MEET_REQUIREMENTS })
            });
        }

        [TestMethod]
        public void PostInvalidPriceHT()
        {
            entity.PriceHT = -1;
            reqCreator.CreateRequest(st, path, HttpMethod.Post, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("PriceHT",
                new List<string>() { GenericError.WRONG_DATA })
            });
        }

        [TestMethod]
        public void PostInvalidStock()
        {
            entity.Stock = -1;
            reqCreator.CreateRequest(st, path, HttpMethod.Post, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Stock",
                new List<string>() { GenericError.WRONG_DATA })
            });
        }

        [TestMethod]
        public void PostInvalidThreshold()
        {
            entity.Threshold = -1;
            reqCreator.CreateRequest(st, path, HttpMethod.Post, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Threshold",
                new List<string>() { GenericError.WRONG_DATA })
            });
        }

        [TestMethod]
        public void PutInvalidThreshold()
        {
            entity.Threshold = -1;
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Threshold",
                new List<string>() { GenericError.WRONG_DATA })
            });
        }

        [TestMethod]
        public void PutInvalidStock()
        {
            entity.Stock = -1;
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Stock",
                new List<string>() { GenericError.WRONG_DATA })
            });
        }

        [TestMethod]
        public void PostNullPriceHT()
        {
            entity.PriceHT = null;
            reqCreator.CreateRequest(st, path, HttpMethod.Post, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("PriceHT",
                new List<string>() { GenericError.CANNOT_BE_NULL_OR_EMPTY })
            });
        }

        [TestMethod]
        public void PutNullPriceHT()
        {
            entity.PriceHT = null;
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("PriceHT",
                new List<string>() { GenericError.CANNOT_BE_NULL_OR_EMPTY })
            });
        }

        [TestMethod]
        public void PutInvalidPriceHT()
        {
            entity.PriceHT = -1;
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("PriceHT",
                new List<string>() { GenericError.WRONG_DATA })
            });
        }

        [TestMethod]
        public void PutExceededShortDescription()
        {
            entity.ShortDescription = new string('*', 257);
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("ShortDescription",
                new List<string>() { GenericError.DOES_NOT_MEET_REQUIREMENTS })
            });
        }

        [TestMethod]
        public void PostNullTitle()
        {
            entity.Title = null;
            reqCreator.CreateRequest(st, path, HttpMethod.Post, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Title",
                new List<string>() { GenericError.CANNOT_BE_NULL_OR_EMPTY })
            });
        }

        [TestMethod]
        public void PutExceededTitle()
        {
            entity.Title = new string('*', 257);
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Title",
                new List<string>() { GenericError.DOES_NOT_MEET_REQUIREMENTS })
            });
        }

        [TestMethod]
        public void PutNullTitle()
        {
            entity.Title = null;
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Title",
                new List<string>() { GenericError.CANNOT_BE_NULL_OR_EMPTY })
            });
        }

        [TestMethod]
        public void Delete()
        {
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Delete, null, HttpStatusCode.OK, false, false);
            List<ProductDTO> search = (List<ProductDTO>)reqCreator.CallSearch(st, "Product/where/id/eq/" + entity.Id,
                new List<string>(), typeof(List<ProductDTO>));
            Assert.AreEqual(0, search.Count);
        }

        [TestMethod]
        public void DeleteForbidden()
        {
            reqCreator.CreateRequest(st, path + "/-1", HttpMethod.Delete, null, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>(TypeOfName.GetNameFromType<ProductDTO>(),
                new List<string>() { GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST })
            });
        }

        [TestMethod]
        public void PutNull()
        {
            reqCreator.CreateRequest(st, path + "/0", HttpMethod.Put, null, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>(TypeOfName.GetNameFromType<ProductDTO>(),
                new List<string>() { GenericError.CANNOT_BE_NULL_OR_EMPTY })
            });
        }
    }
}