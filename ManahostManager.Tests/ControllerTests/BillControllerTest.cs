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
    public class BillControllerTest : ControllerTest<BillDTO>
    {
        private const string path = "/api/Bill";

        [TestInitialize]
        public void PostNestedEntities()
        {
            BillDTO dto = new BillDTO()
            {
                Id = 0,
                BillItems = new List<BillItemDTO>()
                {
                    new BillItemDTO()
                    {
                        Id = 0,
                        PriceHT = 10M,
                        PriceTTC = 10M,
                        Title = "POST",
                        HomeId = 1
                    }
                },
                CreationDate = DateTime.Now,
                HomeId = 1,
                IsPayed = false,
                Reference = "R2014-33",
                PaymentMethods = new List<PaymentMethodDTO>()
                {
                    new PaymentMethodDTO()
                    {
                        Id = 0,
                        HomeId = 1,
                        PaymentType = new PaymentTypeDTO()
                        {
                            Id = 1,
                            Title = "CB"
                        },
                        Price = 30M
                    },
                    new PaymentMethodDTO()
                    {
                        Id = 0,
                        HomeId = 1,
                        PaymentType = new PaymentTypeDTO()
                        {
                            Id = 2,
                            Title = "CASH"
                        },
                        Price = 50M
                    }
                },
                TotalHT = 10M,
                TotalTTC = 15M,
                Supplier = new SupplierDTO()
                {
                    Id = 1
                }
            };

            reqCreator.CreateRequest(st, path, HttpMethod.Post, dto, HttpStatusCode.OK, true, false);
            entity = reqCreator.deserializedEntity;
            List<BillDTO> search = (List<BillDTO>)reqCreator.CallSearch(st, "Bill/where/Id/eq/" + entity.Id,
                new List<string>() { "Document", "Supplier", "BillItems", "PaymentMethods" }, typeof(List<BillDTO>));
            Assert.IsTrue(PropertiesComparison.PublicInstancePropertiesEqual(dto, search[0], new string[]
                { "Id", "Document", "Supplier", "BillItems", "PaymentMethods", "CreationDate" }));
            Assert.IsNotNull(search[0].Supplier);
            Assert.IsTrue(PropertiesComparison.PublicInstancePropertiesEqual(dto.BillItems[0], search[0].BillItems[0], new string[]
            { "Id", "Bill", "BillItemCategory", "GroupBillItem" }));
            Assert.AreEqual(2, search[0].PaymentMethods.Count);
        }

        [TestMethod]
        public void Post()
        {
            BillDTO dto = new BillDTO()
            {
                Id = 0,
                CreationDate = DateTime.Now,
                HomeId = 1,
                IsPayed = false,
                Reference = "R2014-33",
                TotalHT = 10M,
                TotalTTC = 15M,
            };

            reqCreator.CreateRequest(st, path, HttpMethod.Post, dto, HttpStatusCode.OK, true, false);
            entity = reqCreator.deserializedEntity;
            List<BillDTO> search = (List<BillDTO>)reqCreator.CallSearch(st, "Bill/where/Id/eq/" + entity.Id,
                new List<string>() { "Document", "Supplier", "BillItems", "PaymentMethods" }, typeof(List<BillDTO>));
            Assert.IsTrue(PropertiesComparison.PublicInstancePropertiesEqual(dto, search[0], new string[]
                { "Id", "Document", "Supplier", "BillItems", "PaymentMethods", "CreationDate" }));
            Assert.IsNull(search[0].Supplier);
            Assert.AreEqual(0, search[0].BillItems.Count);
            Assert.IsNull(search[0].Booking);
            Assert.IsNull(search[0].Document);
            Assert.AreEqual(0, search[0].PaymentMethods.Count);
        }

        [TestMethod]
        public void PostNull()
        {
            reqCreator.CreateRequest(st, path, HttpMethod.Post, null, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>(TypeOfName.GetNameFromType<BillDTO>(),
                new List<string>() { GenericError.CANNOT_BE_NULL_OR_EMPTY })
            });
        }

        [TestMethod]
        public void PutNull()
        {
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, null, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>(TypeOfName.GetNameFromType<BillDTO>(),
                new List<string>() { GenericError.CANNOT_BE_NULL_OR_EMPTY })
            });
        }

        [TestMethod]
        public void Put()
        {
            entity.IsPayed = true;
            entity.TotalHT = 10M;
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.OK, false, false);
            List<BillDTO> search = reqCreator.CallSearch(st, "Bill/where/Id/eq/" + entity.Id,
                new List<string>() { "Document", "Supplier", "BillItems", "PaymentMethods" }, typeof(List<BillDTO>));
            Assert.IsTrue(PropertiesComparison.PublicInstancePropertiesEqual(entity, search[0], new string[]
                { "Id", "Document", "Supplier", "BillItems", "PaymentMethods", "DateModification", "CreationDate" }));
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
        public void PostExceededReference()
        {
            entity.Reference = new string('*', 65);
            reqCreator.CreateRequest(st, path, HttpMethod.Post, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Reference",
                new List<string>() { GenericError.DOES_NOT_MEET_REQUIREMENTS })
            });
        }

        [TestMethod]
        public void PutExceededReference()
        {
            entity.Reference = new string('*', 65);
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Reference",
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
        public void DeleteForbiddenResource()
        {
            reqCreator.CreateRequest(st, path + "/-1", HttpMethod.Delete, null, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>(TypeOfName.GetNameFromType<BillDTO>(),
                new List<string>() { GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST })
            });
        }

        [TestMethod]
        public void Delete()
        {
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Delete, null, HttpStatusCode.OK, false, false);
            List<BillDTO> search = reqCreator.CallSearch(st, "Bill/where/id/eq/" + entity.Id, new List<string>(), typeof(List<BillDTO>));
            Assert.AreEqual(search.Count, 0);
        }
    }
}
