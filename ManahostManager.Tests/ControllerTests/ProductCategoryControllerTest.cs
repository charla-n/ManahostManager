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
    public class ProductCategoryControllerTest : ControllerTest<ProductCategoryDTO>
    {
        private const string path = "/api/Product/Category";

        [TestInitialize]
        public void PostNestedEntities()
        {
            ProductCategoryDTO dto = new ProductCategoryDTO()
            {
                HomeId = 1,
                RefHide = true,
                Title = "POST"
            };

            reqCreator.CreateRequest(st, path, HttpMethod.Post, dto, HttpStatusCode.OK, true, false);
            Assert.AreEqual(reqCreator.deserializedEntity.HomeId, 1);
            Assert.AreEqual(reqCreator.deserializedEntity.DateModification, null);
            Assert.AreEqual(dto.Title, reqCreator.deserializedEntity.Title);
            Assert.AreEqual(true, reqCreator.deserializedEntity.RefHide);
            entity = reqCreator.deserializedEntity;
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
        public void PutInvalidEntity()
        {
            entity.Id = -1;
            reqCreator.CreateRequest(st, path + "/-1", HttpMethod.Put, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>(TypeOfName.GetNameFromType<ProductCategoryDTO>(),
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
        public void PostNull()
        {
            reqCreator.CreateRequest(st, path, HttpMethod.Post, null, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>(TypeOfName.GetNameFromType<ProductCategoryDTO>(),
                new List<string>() { GenericError.CANNOT_BE_NULL_OR_EMPTY }),
            });
        }

        [TestMethod]
        public void PutNull()
        {
            reqCreator.CreateRequest(st, path + "/0", HttpMethod.Put, null, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>(TypeOfName.GetNameFromType<ProductCategoryDTO>(),
                new List<string>() { GenericError.CANNOT_BE_NULL_OR_EMPTY }),
            });
        }

        [TestMethod]
        public void Put()
        {
            entity.Title = "PUT";
            entity.RefHide = false;
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.OK, false, false);
            List<ProductCategoryDTO> search = (List<ProductCategoryDTO>)reqCreator.CallSearch(st, "ProductCategory/where/id/eq/" + entity.Id,
                new List<string>(), typeof(List<ProductCategoryDTO>));
            Assert.AreEqual(search[0].HomeId, 1);
            Assert.IsNotNull(search[0].DateModification, null);
            Assert.AreEqual(search[0].Title, entity.Title);
            Assert.AreEqual(false, reqCreator.deserializedEntity.RefHide);
        }

        [TestMethod]
        public void Delete()
        {
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Delete, null, HttpStatusCode.OK, false, false);
            List<ProductCategoryDTO> search = (List<ProductCategoryDTO>)reqCreator.CallSearch(st, "ProductCategory/where/id/eq/" + entity.Id,
                new List<string>(), typeof(List<ProductCategoryDTO>));
            Assert.AreEqual(0, search.Count);
        }

        [TestMethod]
        public void DeleteForbiddenResource()
        {
            reqCreator.CreateRequest(st, path + "/-1", HttpMethod.Delete, null, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("ProductCategoryDTO",
                new List<string>() { GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST }),
            });
        }
    }
}