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
    public class MealControllerTest : ControllerTest<MealDTO>
    {
        private const string path = "/api/Meal";

        [TestInitialize]
        public void PostNestedEntities()
        {
            MealDTO dto = new MealDTO()
            {
                Title = "POST",
                BillItemCategory = new BillItemCategoryDTO()
                {
                    Id = 0,
                    Title = "BillItemCategory",
                },
                Description = "POST",
                Hide = false,
                MealCategory = new MealCategoryDTO()
                {
                    Id = 0,
                    Label = "POST",
                    RefHide = false
                },
                MealPrices = new System.Collections.Generic.List<MealPriceDTO>()
                {
                    new MealPriceDTO()
                    {
                        Id = 0,
                        PeopleCategory = new PeopleCategoryDTO()
                        {
                            Id = 1
                        },
                        PriceHT = 50M,
                        Tax = new TaxDTO()
                        {
                            Id = 1
                        }
                    },
                    new MealPriceDTO()
                    {
                        Id = 0,
                        PeopleCategory = new PeopleCategoryDTO()
                        {
                            Id = 2
                        },
                        PriceHT = 70M,
                        Tax = new TaxDTO()
                        {
                            Id = 2
                        }
                    }
                },
                HomeId = 1,
                RefHide = false,
                ShortDescription = "POST"
            };

            reqCreator.CreateRequest(st, path, HttpMethod.Post, dto, HttpStatusCode.OK);
            entity = reqCreator.deserializedEntity;
            List<MealDTO> search = reqCreator.CallSearch(st, "Meal/where/id/eq/" + entity.Id, new List<string>()
            {
                "MealCategory",
                "Documents",
                "MealPrices",
                "BillItemCategory"
            }, typeof(List<MealDTO>));
            Assert.IsTrue(PropertiesComparison.PublicInstancePropertiesEqual(dto, search[0], 
                new string[] { "Id", "MealCategory", "BillItemCategory", "Documents", "MealPrices" }));
            Assert.IsTrue(PropertiesComparison.PublicInstancePropertiesEqual(dto.BillItemCategory, search[0].BillItemCategory,
                new string[] { "Id", "HomeId" }));
            Assert.IsTrue(PropertiesComparison.PublicInstancePropertiesEqual(dto.MealCategory, search[0].MealCategory,
                new string[] { "Id", "HomeId" }));
            Assert.AreEqual(dto.MealPrices.Count, search[0].MealPrices.Count);
        }

        [TestMethod]
        public void Post()
        {
            MealDTO dto = new MealDTO()
            {
                HomeId = 1,
                Title = "POST",
                RefHide = false
            };
            reqCreator.CreateRequest(st, path, HttpMethod.Post, dto, HttpStatusCode.OK);
            List<MealDTO> search = reqCreator.CallSearch(st, "Meal/where/id/eq/" + reqCreator.deserializedEntity.Id, new List<string>()
            {
                "MealCategory",
                "Documents",
                "MealPrices",
                "BillItemCategory"
            }, typeof(List<MealDTO>));
            Assert.IsNull(search[0].MealCategory);
            Assert.IsNull(search[0].BillItemCategory);
            Assert.AreEqual(search[0].Documents.Count, 0);
            Assert.AreEqual(search[0].MealPrices.Count, 0);
        }

        [TestMethod]
        public void Put()
        {
            entity.Title = "PUT";
            entity.Hide = true;
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.OK, false);
            List<MealDTO> search = reqCreator.CallSearch(st, "Meal/where/id/eq/" + entity.Id, new List<string>()
            {
                "MealCategory",
                "Documents",
                "MealPrices",
                "BillItemCategory"
            }, typeof(List<MealDTO>));
            Assert.IsTrue(PropertiesComparison.PublicInstancePropertiesEqual(entity, search[0],
                new string[] { "Id", "MealCategory", "BillItemCategory", "Documents", "MealPrices", "DateModification" }));
            Assert.IsTrue(PropertiesComparison.PublicInstancePropertiesEqual(entity.BillItemCategory, search[0].BillItemCategory,
                new string[] { "Id", "HomeId" }));
            Assert.IsTrue(PropertiesComparison.PublicInstancePropertiesEqual(entity.MealCategory, search[0].MealCategory,
                new string[] { "Id", "HomeId" }));
            Assert.AreEqual(entity.MealPrices.Count, search[0].MealPrices.Count);
        }

        [TestMethod]
        public void PostForbiddenNestedEntities()
        {
            entity.BillItemCategory.Id = -1;
            entity.MealCategory.Id = -1;
            entity.MealPrices[0].Id = -1;
            entity.MealPrices[1].Id = -1;
            reqCreator.CreateRequest(st, path, HttpMethod.Post, entity, HttpStatusCode.BadRequest, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("BillItemCategory",
                new List<string>() { GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST }),
                new KeyValuePair<string, List<string>>("MealCategory",
                new List<string>() { GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST }),
                new KeyValuePair<string, List<string>>("MealPrice",
                new List<string>() { GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST,
                    GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST })
            });
        }

        [TestMethod]
        public void PostBadHomeId()
        {
            entity.HomeId = -1;
            reqCreator.CreateRequest(st, path, HttpMethod.Post, entity, HttpStatusCode.BadRequest, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("HomeId",
                new List<string>() { GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST }),
            });
        }

        [TestMethod]
        public void PutBadHomeId()
        {
            entity.HomeId = -1;
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.BadRequest, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("HomeId",
                new List<string>() { GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST }),
            });
        }

        [TestMethod]
        public void Delete()
        {
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Delete, null, HttpStatusCode.OK, false);
            List<MealDTO> search = reqCreator.CallSearch(st, "Meal/where/id/eq/" + entity.Id, new List<string>(), typeof(List<MealDTO>));
            Assert.AreEqual(search.Count, 0);
        }

        [TestMethod]
        public void DeleteForbiddenResource()
        {
            reqCreator.CreateRequest(st, path + "/-1", HttpMethod.Delete, null, HttpStatusCode.BadRequest, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("MealDTO",
                new List<string>() { GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST }),
            });
        }

        [TestMethod]
        public void PutForbiddenNestedEntities()
        {
            entity.BillItemCategory.Id = -1;
            entity.MealCategory.Id = -1;
            entity.MealPrices[0].Id = -1;
            entity.MealPrices[1].Id = -1;
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.BadRequest, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("BillItemCategory",
                new List<string>() { GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST }),
                new KeyValuePair<string, List<string>>("MealCategory",
                new List<string>() { GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST }),
                new KeyValuePair<string, List<string>>("MealPrice",
                new List<string>() { GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST,
                    GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST })
            });
        }

        [TestMethod]
        public void PostNullTitle()
        {
            entity.Title = null;
            reqCreator.CreateRequest(st, path, HttpMethod.Post, entity, HttpStatusCode.BadRequest, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Title",
                new List<string>() { GenericError.CANNOT_BE_NULL_OR_EMPTY }),
            });
        }

        [TestMethod]
        public void PostExceededDesc()
        {
            entity.Description = new string('*', 4001);
            reqCreator.CreateRequest(st, path, HttpMethod.Post, entity, HttpStatusCode.BadRequest, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Description",
                new List<string>() { GenericError.DOES_NOT_MEET_REQUIREMENTS }),
            });
        }

        [TestMethod]
        public void PutExceededDesc()
        {
            entity.Description = new string('*', 4001);
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.BadRequest, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Description",
                new List<string>() { GenericError.DOES_NOT_MEET_REQUIREMENTS }),
            });
        }

        [TestMethod]
        public void PostExceededShortDesc()
        {
            entity.ShortDescription = new string('*', 257);
            reqCreator.CreateRequest(st, path, HttpMethod.Post, entity, HttpStatusCode.BadRequest, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("ShortDescription",
                new List<string>() { GenericError.DOES_NOT_MEET_REQUIREMENTS }),
            });
        }

        [TestMethod]
        public void PutExceededShortDesc()
        {
            entity.ShortDescription = new string('*', 257);
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.BadRequest, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("ShortDescription",
                new List<string>() { GenericError.DOES_NOT_MEET_REQUIREMENTS }),
            });
        }

        [TestMethod]
        public void PostExceededTitle()
        {
            entity.Title = new string('*', 257);
            reqCreator.CreateRequest(st, path, HttpMethod.Post, entity, HttpStatusCode.BadRequest, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Title",
                new List<string>() { GenericError.DOES_NOT_MEET_REQUIREMENTS }),
            });
        }

        [TestMethod]
        public void PutExceededTitle()
        {
            entity.Title = new string('*', 257);
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.BadRequest, false);
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
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.BadRequest, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Title",
                new List<string>() { GenericError.CANNOT_BE_NULL_OR_EMPTY }),
            });
        }

        [TestMethod]
        public void PostNull()
        {
            reqCreator.CreateRequest(st, path, HttpMethod.Post, null, HttpStatusCode.BadRequest, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("MealDTO",
                new List<string>() { GenericError.CANNOT_BE_NULL_OR_EMPTY }),
            });
        }

        [TestMethod]
        public void PutNull()
        {
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, null, HttpStatusCode.BadRequest, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("MealDTO",
                new List<string>() { GenericError.CANNOT_BE_NULL_OR_EMPTY }),
            });
        }
    }
}