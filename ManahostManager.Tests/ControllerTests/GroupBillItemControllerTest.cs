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
    public class GroupBillItemControllerTest : ControllerTest<GroupBillItemDTO>
    {
        private const string path = "/api/Bill/Item/Group";

        [TestInitialize]
        public void NestedPost()
        {
            GroupBillItemDTO dto = new GroupBillItemDTO()
            {
                Discount = 50M,
                HomeId = 1,
                Id = 0,
                ValueType = Domain.Entity.EValueType.AMOUNT
            };

            reqCreator.CreateRequest(st, path, HttpMethod.Post, dto, HttpStatusCode.OK, true, false);
            entity = reqCreator.deserializedEntity;
            List<GroupBillItemDTO> search = reqCreator.CallSearch(st, "GroupBillItem/where/Id/eq/" + entity.Id,
                new List<string>(), typeof(List<GroupBillItemDTO>));
            Assert.IsTrue(PropertiesComparison.PublicInstancePropertiesEqual(dto, search[0], new string[]
                { "Id" }));
        }

        [TestMethod]
        public void PostNull()
        {
            reqCreator.CreateRequest(st, path, HttpMethod.Post, null, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>(TypeOfName.GetNameFromType<GroupBillItemDTO>(),
                new List<string>() { GenericError.CANNOT_BE_NULL_OR_EMPTY })
            });
        }

        [TestMethod]
        public void Put()
        {
            entity.Discount = 777M;
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.OK, false, false);
            List<GroupBillItemDTO> search = reqCreator.CallSearch(st, "GroupBillItem/where/Id/eq/" + entity.Id,
                new List<string>(), typeof(List<GroupBillItemDTO>));
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
                new KeyValuePair<string, List<string>>(TypeOfName.GetNameFromType<GroupBillItemDTO>(),
                new List<string>() { GenericError.CANNOT_BE_NULL_OR_EMPTY })
            });
        }

        [TestMethod]
        public void PutForbiddenResource()
        {
            reqCreator.CreateRequest(st, path + "/-1", HttpMethod.Put, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>(TypeOfName.GetNameFromType<GroupBillItemDTO>(),
                new List<string>() { GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST })
            });
        }

        [TestMethod]
        public void DeleteForbiddenResource()
        {
            reqCreator.CreateRequest(st, path + "/-1", HttpMethod.Delete, null, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>(TypeOfName.GetNameFromType<GroupBillItemDTO>(),
                new List<string>() { GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST })
            });
        }

        [TestMethod]
        public void Delete()
        {
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Delete, null, HttpStatusCode.OK, false, false);
            List<GroupBillItemDTO> search = reqCreator.CallSearch(st, "GroupBillItem/where/id/eq/" + entity.Id, new List<string>(), typeof(List<GroupBillItemDTO>));
            Assert.AreEqual(search.Count, 0);
        }
    }
}
