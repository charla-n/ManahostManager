using ManahostManager.Domain.DTOs;
using ManahostManager.Tests.ControllerTests.Utils;
using ManahostManager.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ManahostManager.Tests.ControllerTests
{
    [TestClass]
    public class BillItemControllerTest : ControllerTest<BillItemDTO>
    {
        private const string path = "/api/Bill/Item";

        [TestInitialize]
        public void NestedPost()
        {
            BillItemDTO dto = new BillItemDTO()
            {
                Id = 0,
                Bill = new BillDTO()
                {
                    Id = 1
                },
                BillItemCategory = new BillItemCategoryDTO()
                {
                    Id = 0,
                    HomeId = 1,
                    Title = "POST"
                },
                Description = "POST",
                GroupBillItem = new GroupBillItemDTO()
                {
                    Discount = 50M,
                    HomeId = 1,
                    Id = 0,
                    ValueType = Domain.Entity.EValueType.AMOUNT
                },
                PriceHT = 100M,
                PriceTTC = 100M,
                Quantity = 1,
                Title = "POST",
                HomeId = 1
            };

            reqCreator.CreateRequest(st, path, HttpMethod.Post, dto, HttpStatusCode.OK, true, false);
            entity = reqCreator.deserializedEntity;
            List<BillItemDTO> search = (List<BillItemDTO>)reqCreator.CallSearch(st, "BillItem/where/Id/eq/" + entity.Id,
                new List<string>() { "Bill", "GroupBillItem", "BillItemCategory" }, typeof(List<BillItemDTO>));
            Assert.IsTrue(PropertiesComparison.PublicInstancePropertiesEqual(dto, entity, new string[]
                { "Id", "GroupBillItem", "BillItemCategory", "Bill" }));
            Assert.IsNotNull(entity.Bill);
            Assert.IsTrue(PropertiesComparison.PublicInstancePropertiesEqual(dto.BillItemCategory, entity.BillItemCategory, new string[]
                { "Id" }));
            Assert.IsTrue(PropertiesComparison.PublicInstancePropertiesEqual(dto.GroupBillItem, entity.GroupBillItem, new string[]
            {
                "Id"
            }));
        }

        [TestMethod]
        public void Post()
        {
            BillItemDTO dto = new BillItemDTO()
            {
                Id = 0,
                Bill = new BillDTO()
                {
                    Id = 1
                },
                Description = "POST",
                PriceHT = 100M,
                PriceTTC = 100M,
                Quantity = 1,
                Title = "POST",
                HomeId = 1
            };

            reqCreator.CreateRequest(st, path, HttpMethod.Post, dto, HttpStatusCode.OK, true, false);
            List<BillItemDTO> search = (List<BillItemDTO>)reqCreator.CallSearch(st, "BillItem/where/Id/eq/" + reqCreator.deserializedEntity.Id,
                new List<string>() { "Bill", "GroupBillItem", "BillItemCategory" }, typeof(List<BillItemDTO>));
            Assert.IsTrue(PropertiesComparison.PublicInstancePropertiesEqual(dto, search[0], new string[]
                { "Id", "GroupBillItem", "BillItemCategory", "Bill" }));
            Assert.IsNotNull(search[0].Bill);
            Assert.IsNull(search[0].GroupBillItem);
            Assert.IsNull(search[0].BillItemCategory);
        }

        [TestMethod]
        public void PostNull()
        {
            reqCreator.CreateRequest(st, path, HttpMethod.Post, null, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>(TypeOfName.GetNameFromType<BillItemDTO>(), 
                new List<string>() { GenericError.CANNOT_BE_NULL_OR_EMPTY })
            });
        }

        [TestMethod]
        public void Put()
        {
            entity.Description = "PUT";
            entity.PriceHT = 0M;
            entity.Title = "PUT";
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.OK, false, false);
            List<BillItemDTO> search = reqCreator.CallSearch(st, "BillItem/where/Id/eq/" + entity.Id,
                new List<string>() { "Bill", "GroupBillItem", "BillItemCategory" }, typeof(List<BillItemDTO>));
            Assert.IsTrue(PropertiesComparison.PublicInstancePropertiesEqual(entity, search[0], new string[]
                { "Id", "GroupBillItem", "BillItemCategory", "Bill", "DateModification" }));
            Assert.IsNotNull(search[0].Bill);
            Assert.IsNotNull(search[0].GroupBillItem);
            Assert.IsNotNull(search[0].BillItemCategory);
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
        public void PostExceededDescription()
        {
            entity.Description = new string('*', 4001);
            reqCreator.CreateRequest(st, path, HttpMethod.Post, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Description",
                new List<string>() { GenericError.DOES_NOT_MEET_REQUIREMENTS })
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
        public void PostNegativeQuantity()
        {
            entity.Quantity = -1;
            reqCreator.CreateRequest(st, path, HttpMethod.Post, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Quantity",
                new List<string>() { GenericError.WRONG_DATA })
            });
        }

        [TestMethod]
        public void PutNegativeQuantity()
        {
            entity.Quantity = -1;
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Quantity",
                new List<string>() { GenericError.WRONG_DATA })
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
        public void PutExceededDescription()
        {
            entity.Description = new string('*', 4001);
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Description",
                new List<string>() { GenericError.DOES_NOT_MEET_REQUIREMENTS })
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
        public void PutNull()
        {
            reqCreator.CreateRequest(st, path + "/1", HttpMethod.Put, null, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>(TypeOfName.GetNameFromType<BillItemDTO>(),
                new List<string>() { GenericError.CANNOT_BE_NULL_OR_EMPTY })
            });
        }

        [TestMethod]
        public void PutForbiddenResource()
        {
            reqCreator.CreateRequest(st, path + "/-1", HttpMethod.Put, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>(TypeOfName.GetNameFromType<BillItemDTO>(),
                new List<string>() { GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST })
            });
        }

        [TestMethod]
        public void DeleteForbiddenResource()
        {
            reqCreator.CreateRequest(st, path + "/-1", HttpMethod.Delete, null, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>(TypeOfName.GetNameFromType<BillItemDTO>(),
                new List<string>() { GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST })
            });
        }

        [TestMethod]
        public void Delete()
        {
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Delete, null, HttpStatusCode.OK, false, false);
            List<BillItemDTO> search = reqCreator.CallSearch(st, "BillItem/where/id/eq/" + entity.Id, new List<string>(), typeof(List<BillItemDTO>));
            Assert.AreEqual(search.Count, 0);
        }
    }
}
