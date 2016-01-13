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
    public class PaymentTypeControllerTest : ControllerTest<PaymentTypeDTO>
    {
        private const string path = "/api/PaymentType";

        [TestInitialize]
        public void NestedPost()
        {
            PaymentTypeDTO dto = new PaymentTypeDTO()
            {
                HomeId = 1,
                Id = 0,
                Title = "POST"
            };

            reqCreator.CreateRequest(st, path, HttpMethod.Post, dto, HttpStatusCode.OK, true, false);
            entity = reqCreator.deserializedEntity;
            List<PaymentTypeDTO> search = (List<PaymentTypeDTO>)reqCreator.CallSearch(st, "PaymentType/where/Id/eq/" + entity.Id,
                new List<string>(), typeof(List<PaymentTypeDTO>));
            Assert.IsTrue(PropertiesComparison.PublicInstancePropertiesEqual(dto, entity, new string[]
                { "Id" }));
        }

        [TestMethod]
        public void PostNull()
        {
            reqCreator.CreateRequest(st, path, HttpMethod.Post, null, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>(TypeOfName.GetNameFromType<PaymentTypeDTO>(),
                new List<string>() { GenericError.CANNOT_BE_NULL_OR_EMPTY })
            });
        }

        [TestMethod]
        public void Put()
        {
            entity.Title = "PUT";
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.OK, false, false);
            List<PaymentTypeDTO> search = reqCreator.CallSearch(st, "PaymentType/where/Id/eq/" + entity.Id,
                new List<string>(), typeof(List<PaymentTypeDTO>));
            Assert.IsTrue(PropertiesComparison.PublicInstancePropertiesEqual(entity, search[0], new string[]
                { "Id", "DateModification" }));
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
                new KeyValuePair<string, List<string>>(TypeOfName.GetNameFromType<PaymentTypeDTO>(),
                new List<string>() { GenericError.CANNOT_BE_NULL_OR_EMPTY })
            });
        }

        [TestMethod]
        public void PutForbiddenResource()
        {
            reqCreator.CreateRequest(st, path + "/-1", HttpMethod.Put, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>(TypeOfName.GetNameFromType<PaymentTypeDTO>(),
                new List<string>() { GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST })
            });
        }

        [TestMethod]
        public void DeleteForbiddenResource()
        {
            reqCreator.CreateRequest(st, path + "/-1", HttpMethod.Delete, null, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>(TypeOfName.GetNameFromType<PaymentTypeDTO>(),
                new List<string>() { GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST })
            });
        }

        [TestMethod]
        public void Delete()
        {
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Delete, null, HttpStatusCode.OK, false, false);
            List<PaymentTypeDTO> search = reqCreator.CallSearch(st, "PaymentType/where/id/eq/" + entity.Id, new List<string>(), typeof(List<PaymentTypeDTO>));
            Assert.AreEqual(search.Count, 0);
        }
    }
}
