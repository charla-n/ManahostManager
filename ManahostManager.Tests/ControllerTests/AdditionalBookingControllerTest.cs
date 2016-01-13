using ManahostManager.Domain.DTOs;
using ManahostManager.Domain.Entity;
using ManahostManager.Tests.ControllerTests.Utils;
using ManahostManager.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace ManahostManager.Tests.ControllerTests
{
    [TestClass]
    public class AdditionalBookingControllerTest : ControllerTest<AdditionalBookingDTO>
    {
        private const string path = "/api/Booking/Additional";

        [TestInitialize]
        public void PostNestedEntities()
        {
            AdditionalBookingDTO dto = new AdditionalBookingDTO()
            {
                HomeId = 1,
                PriceHT = 10M,
                Tax = new TaxDTO()
                {
                    Id = 0,
                    HomeId = 1,
                    Price = 10M,
                    ValueType = EValueType.PERCENT,
                    Title = "Post",
                },
                Title = "Post",
                Booking = new BookingDTO()
                {
                    Id = 1
                },
                BillItemCategory = new BillItemCategoryDTO()
                {
                    Id = 0,
                    HomeId = 1,
                    Title = "Post"
                }
            };

            reqCreator.CreateRequest(st, path, HttpMethod.Post, dto, HttpStatusCode.OK, true, false);
            List<AdditionalBookingDTO> search = reqCreator.CallSearch(st, "AdditionalBooking/where/id/eq/" + reqCreator.deserializedEntity.Id,
                new List<string>() { "BillItemCategory", "Booking", "Tax" }, typeof(List<AdditionalBookingDTO>));
            Assert.AreEqual(dto.Title, search[0].Title);
            Assert.AreEqual(search[0].HomeId, 1);
            Assert.AreEqual(search[0].DateModification, null);
            Assert.AreEqual(dto.PriceHT, search[0].PriceHT);
            Assert.AreEqual(dto.PriceHT, search[0].PriceTTC);
            Assert.IsTrue(PropertiesComparison.PublicInstancePropertiesEqual<TaxDTO>(dto.Tax, search[0].Tax,
                new string[] { "Id" }));
            Assert.IsTrue(PropertiesComparison.PublicInstancePropertiesEqual<BillItemCategoryDTO>(dto.BillItemCategory, search[0].BillItemCategory,
                new string[] { "Id" }));
            entity = reqCreator.deserializedEntity;
        }

        [TestMethod]
        public void PostIgnoredBooking()
        {
            entity.Booking.Id = 0;
            reqCreator.CreateRequest(st, path, HttpMethod.Post, entity, HttpStatusCode.BadRequest, true, false);
        }

        [TestMethod]
        public void Post()
        {
            reqCreator.CreateRequest(st, path, HttpMethod.Post, entity, HttpStatusCode.OK, true, false);
            List<AdditionalBookingDTO> search = reqCreator.CallSearch(st, "AdditionalBooking/where/id/eq/" + reqCreator.deserializedEntity.Id,
                new List<string>() { "BillItemCategory", "Booking", "Tax" }, typeof(List<AdditionalBookingDTO>));
            Assert.AreEqual(search[0].HomeId, 1);
            Assert.AreEqual(search[0].DateModification, null);
            Assert.AreEqual(entity.PriceHT, search[0].PriceHT);
            Assert.AreEqual(entity.PriceHT, search[0].PriceTTC);
            Assert.IsTrue(PropertiesComparison.PublicInstancePropertiesEqual<TaxDTO>(entity.Tax, search[0].Tax,
                new string[] { "Id" }));
            Assert.IsTrue(PropertiesComparison.PublicInstancePropertiesEqual<BillItemCategoryDTO>(entity.BillItemCategory, search[0].BillItemCategory,
                new string[] { "Id" }));
        }

        [TestMethod]
        public void PostNegativePrice()
        {
            entity.PriceHT = -150M;
            entity.PriceTTC = -150M;
            reqCreator.CreateRequest(st, path, HttpMethod.Post, entity, HttpStatusCode.OK, true, false);
            List<AdditionalBookingDTO> search = reqCreator.CallSearch(st, "AdditionalBooking/where/id/eq/" + reqCreator.deserializedEntity.Id,
                new List<string>() { "BillItemCategory", "Booking", "Tax" }, typeof(List<AdditionalBookingDTO>));
            Assert.AreEqual(search[0].HomeId, 1);
            Assert.AreEqual(search[0].DateModification, null);
            Assert.AreEqual(entity.PriceHT, search[0].PriceHT);
            Assert.AreEqual(entity.PriceHT, search[0].PriceTTC);
            Assert.IsTrue(PropertiesComparison.PublicInstancePropertiesEqual<TaxDTO>(entity.Tax, search[0].Tax,
                new string[] { "Id" }));
            Assert.IsTrue(PropertiesComparison.PublicInstancePropertiesEqual<BillItemCategoryDTO>(entity.BillItemCategory, search[0].BillItemCategory,
                new string[] { "Id" }));
        }

        [TestMethod]
        public void PostForbiddenHome()
        {
            entity.HomeId = -1;
            reqCreator.CreateRequest(st, path, HttpMethod.Post, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("HomeId",
                new List<string>() { GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST }),
            });
        }

        [TestMethod]
        public void PutForbiddenHome()
        {
            entity.HomeId = -1;
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("HomeId",
                new List<string>() { GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST }),
            });
        }

        [TestMethod]
        public void Put()
        {
            entity.Title = "changed";
            entity.PriceHT = 55M;
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.OK, false, false);
            List<AdditionalBookingDTO> search = reqCreator.CallSearch(st, "AdditionalBooking/where/id/eq/" + entity.Id,
                new List<string>() { "BillItemCategory", "Booking", "Tax" }, typeof(List<AdditionalBookingDTO>));
            Assert.AreEqual(entity.Title, search[0].Title);
            Assert.AreEqual(search[0].HomeId, 1);
            Assert.IsNotNull(search[0].DateModification);
            Assert.AreEqual(entity.PriceHT, search[0].PriceHT);
            Assert.AreEqual(entity.PriceTTC, search[0].PriceTTC);
            Assert.IsTrue(PropertiesComparison.PublicInstancePropertiesEqual<TaxDTO>(entity.Tax, search[0].Tax,
                new string[] { "Id" }));
            Assert.IsTrue(PropertiesComparison.PublicInstancePropertiesEqual<BillItemCategoryDTO>(entity.BillItemCategory, search[0].BillItemCategory,
                new string[] { "Id" }));
        }

        [TestMethod]
        public void PutNegativePrice()
        {
            entity.Title = "changed";
            entity.PriceHT = -55M;
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.OK, false, false);
            List<AdditionalBookingDTO> search = reqCreator.CallSearch(st, "AdditionalBooking/where/id/eq/" + entity.Id,
                new List<string>() { "BillItemCategory", "Booking", "Tax" }, typeof(List<AdditionalBookingDTO>));
            Assert.AreEqual(entity.Title, search[0].Title);
            Assert.AreEqual(search[0].HomeId, 1);
            Assert.IsNotNull(search[0].DateModification);
            Assert.AreEqual(entity.PriceHT, search[0].PriceHT);
            Assert.AreEqual(entity.PriceTTC, search[0].PriceTTC);
            Assert.IsTrue(PropertiesComparison.PublicInstancePropertiesEqual<TaxDTO>(entity.Tax, search[0].Tax,
                new string[] { "Id" }));
            Assert.IsTrue(PropertiesComparison.PublicInstancePropertiesEqual<BillItemCategoryDTO>(entity.BillItemCategory, search[0].BillItemCategory,
                new string[] { "Id" }));
        }

        [TestMethod]
        public void PostForbiddenEntities()
        {
            entity.Booking.Id = -1;
            entity.BillItemCategory.Id = -1;
            entity.Tax.Id = -1;
            reqCreator.CreateRequest(st, path, HttpMethod.Post, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Booking",
                new List<string>() { GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST }),
                new KeyValuePair<string, List<string>>("BillItemCategory",
                new List<string>() { GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST }),
                new KeyValuePair<string, List<string>>("Tax",
                new List<string>() { GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST }),
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
                new List<string>() { GenericError.CANNOT_BE_NULL_OR_EMPTY }),
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
                new List<string>() { GenericError.DOES_NOT_MEET_REQUIREMENTS }),
            });
        }

        [TestMethod]
        public void PostNullBooking()
        {
            entity.Booking = null;
            reqCreator.CreateRequest(st, path, HttpMethod.Post, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Booking",
                new List<string>() { GenericError.CANNOT_BE_NULL_OR_EMPTY }),
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
                new List<string>() { GenericError.DOES_NOT_MEET_REQUIREMENTS }),
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
                new List<string>() { GenericError.CANNOT_BE_NULL_OR_EMPTY }),
            });
        }

        [TestMethod]
        public void PostNull()
        {
            reqCreator.CreateRequest(st, path, HttpMethod.Post, null, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>(TypeOfName.GetNameFromType<AdditionalBookingDTO>(), new List<string>() { GenericError.CANNOT_BE_NULL_OR_EMPTY })
            });
        }

        [TestMethod]
        public void PutNull()
        {
            reqCreator.CreateRequest(st, path + "/0", HttpMethod.Put, null, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>(TypeOfName.GetNameFromType<AdditionalBookingDTO>(), new List<string>() { GenericError.CANNOT_BE_NULL_OR_EMPTY })
            });
        }

        [TestMethod]
        public void Compute()
        {
            reqCreator.CreateRequest(st, path + "/Compute/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.OK, true, false);
            Assert.AreEqual(reqCreator.deserializedEntity.PriceTTC, 11M);
            Assert.AreEqual(reqCreator.deserializedEntity.PriceHT, entity.PriceHT);
            Assert.AreEqual(reqCreator.deserializedEntity.HomeId, 1);
            Assert.IsNotNull(reqCreator.deserializedEntity.DateModification);
            Assert.AreEqual(entity.PriceHT, reqCreator.deserializedEntity.PriceHT);
            Assert.IsTrue(PropertiesComparison.PublicInstancePropertiesEqual<TaxDTO>(entity.Tax, reqCreator.deserializedEntity.Tax,
                new string[] { "Id" }));
        }

        [TestMethod]
        public void Delete()
        {
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Delete, null, HttpStatusCode.OK, false);
            List<AdditionalBookingDTO> search = reqCreator.CallSearch(st, "AdditionalBooking/where/id/eq/" + entity.Id,
                new List<string>(), typeof(List<AdditionalBookingDTO>));
            Assert.AreEqual(0, search.Count);
        }

        [TestMethod]
        public void ForbiddenDelete()
        {
            reqCreator.CreateRequest(st, path + "/-1", HttpMethod.Delete, null, HttpStatusCode.BadRequest, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>(TypeOfName.GetNameFromType<AdditionalBookingDTO>(),
                new List<string>() { GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST })
            });
        }
    }
}