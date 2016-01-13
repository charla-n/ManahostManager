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
    public class PaymentMethodControllerTest : ControllerTest<PaymentMethodDTO>
    {
        private const string path = "/api/Bill/Payment";

        [TestInitialize]
        public void NestedPost()
        {
            PaymentMethodDTO dto = new PaymentMethodDTO()
            {
                Bill = new BillDTO()
                {
                    Id = 1
                },
                HomeId = 1,
                Id = 0,
                PaymentType = new PaymentTypeDTO()
                {
                    Id = 0,
                    HomeId = 1,
                    Title = "POST",
                },
                Price = 300M
            };

            reqCreator.CreateRequest(st, path, HttpMethod.Post, dto, HttpStatusCode.OK, true, false);
            entity = reqCreator.deserializedEntity;
            List<PaymentMethodDTO> search = reqCreator.CallSearch(st, "PaymentMethod/where/Id/eq/" + entity.Id,
                new List<string>() { "Bill", "PaymentType" } , typeof(List<PaymentMethodDTO>));
            Assert.IsTrue(PropertiesComparison.PublicInstancePropertiesEqual(dto, search[0], new string[]
                { "Id", "Bill", "PaymentType" }));
            Assert.AreEqual(dto.Bill.Id, search[0].Bill.Id);
            Assert.IsTrue(PropertiesComparison.PublicInstancePropertiesEqual(dto.PaymentType, search[0].PaymentType, new string[]
                { "Id" }));
        }

        [TestMethod]
        public void PostNull()
        {
            reqCreator.CreateRequest(st, path, HttpMethod.Post, null, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>(TypeOfName.GetNameFromType<PaymentMethodDTO>(), 
                new List<string>() { GenericError.CANNOT_BE_NULL_OR_EMPTY })
            });
        }

        [TestMethod]
        public void Put()
        {
            entity.Price = 777M;
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.OK, false, false);
            List<PaymentMethodDTO> search = reqCreator.CallSearch(st, "PaymentMethod/where/Id/eq/" + entity.Id,
                new List<string>(), typeof(List<PaymentMethodDTO>));
            Assert.IsTrue(PropertiesComparison.PublicInstancePropertiesEqual(entity, search[0], new string[]
                { "Id", "DateModification", "Bill", "PaymentType" }));
            Assert.IsNotNull(search[0].DateModification);
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
        public void PostNullBill()
        {
            entity.Bill = null;
            reqCreator.CreateRequest(st, path, HttpMethod.Post, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("DB",
                new List<string>() { GenericError.SQLEXCEPTION })
            });
        }

        [TestMethod]
        public void PostNullPaymentMethod()
        {
            entity.PaymentType = null;
            reqCreator.CreateRequest(st, path, HttpMethod.Post, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("DB",
                new List<string>() { GenericError.SQLEXCEPTION })
            });
        }

        [TestMethod]
        public void PutNullPaymentType()
        {
            entity.PaymentType.Id = null;
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("DB",
                new List<string>() { GenericError.SQLEXCEPTION })
            });
        }

        [TestMethod]
        public void PutNullBill()
        {
            entity.Bill.Id = null;
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.OK, false, false);
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
                new KeyValuePair<string, List<string>>(TypeOfName.GetNameFromType<PaymentMethodDTO>(),
                new List<string>() { GenericError.CANNOT_BE_NULL_OR_EMPTY })
            });
        }

        [TestMethod]
        public void PutForbiddenResource()
        {
            reqCreator.CreateRequest(st, path + "/-1", HttpMethod.Put, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>(TypeOfName.GetNameFromType<PaymentMethodDTO>(),
                new List<string>() { GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST })
            });
        }

        [TestMethod]
        public void DeleteForbiddenResource()
        {
            reqCreator.CreateRequest(st, path + "/-1", HttpMethod.Delete, null, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>(TypeOfName.GetNameFromType<PaymentMethodDTO>(),
                new List<string>() { GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST })
            });
        }

        [TestMethod]
        public void Delete()
        {
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Delete, null, HttpStatusCode.OK, false, false);
            List<PaymentMethodDTO> search = reqCreator.CallSearch(st, "PaymentMethod/where/id/eq/" + entity.Id, new List<string>(), typeof(List<PaymentMethodDTO>));
            Assert.AreEqual(search.Count, 0);
        }
    }
}
