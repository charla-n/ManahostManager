using ManahostManager.Domain.DTOs;
using ManahostManager.Tests.ControllerTests.Utils;
using ManahostManager.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace ManahostManager.Tests.ControllerTests
{
    [TestClass]
    public class BookingStepConfigControllerTest : ControllerTest<BookingStepConfigDTO>
    {
        private const string path = "/api/Booking/StepConfig";

        [TestInitialize]
        public void PostNestedEntities()
        {
            BookingStepConfigDTO dto = new BookingStepConfigDTO()
            {
                HomeId = 1,
                Title = "Post",
                BookingSteps = new System.Collections.Generic.List<BookingStepDTO>()
                {
                    new BookingStepDTO()
                    {
                        Id = 0,
                        HomeId = 1,
                        Title = "Post"
                    }
                }
            };

            reqCreator.CreateRequest(st, path, HttpMethod.Post, dto, HttpStatusCode.OK, true);
            entity = reqCreator.deserializedEntity;
            List<BookingStepConfigDTO> search = reqCreator.CallSearch(st, "BookingStepConfig/where/id/eq/" + entity.Id,
                new List<string>() { "BookingSteps" }, typeof(List<BookingStepConfigDTO>));
            Assert.IsNull(dto.DateModification);
            Assert.AreEqual(dto.HomeId, search[0].HomeId);
            Assert.AreEqual(dto.Title, search[0].Title);
            for (int i = 0; i < dto.BookingSteps.Count; i++)
            {
                Assert.IsTrue(PropertiesComparison.PublicInstancePropertiesEqual<BookingStepDTO>(
                    dto.BookingSteps[i], search[0].BookingSteps[i], new string[] { "Id", "BookingStepConfig", "Documents", "MailTemplate" }));
            }
        }

        [TestMethod]
        public void PostNull()
        {
            reqCreator.CreateRequest(st, path, HttpMethod.Post, null, HttpStatusCode.BadRequest, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>(TypeOfName.GetNameFromType<BookingStepConfigDTO>(),
                new List<string>() { GenericError.CANNOT_BE_NULL_OR_EMPTY })
            });
        }

        [TestMethod]
        public void Delete()
        {
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Delete, null, HttpStatusCode.OK, false);
            List<BookingStepConfigDTO> search = reqCreator.CallSearch(st, "BookingStepConfig/where/id/eq/" + entity.Id,
                new List<string>() { }, typeof(List<BookingStepConfigDTO>));
            Assert.AreEqual(0, search.Count);
        }

        [TestMethod]
        public void DeleteForbidden()
        {
            reqCreator.CreateRequest(st, path + "/-1", HttpMethod.Delete, null, HttpStatusCode.BadRequest, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>(TypeOfName.GetNameFromType<BookingStepConfigDTO>(),
                new List<string>() { GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST })
            });
        }

        [TestMethod]
        public void PostForbiddenResource()
        {
            entity.BookingSteps[0].Id = -1;
            reqCreator.CreateRequest(st, path, HttpMethod.Post, entity, HttpStatusCode.BadRequest, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("BookingStep",
                new List<string>() { GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST })
            });
        }

        [TestMethod]
        public void PostExceededTitle()
        {
            entity.BookingSteps[0].Id = 0;
            entity.Title = new string('*', 257);
            reqCreator.CreateRequest(st, path, HttpMethod.Post, entity, HttpStatusCode.BadRequest, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Title",
                new List<string>() { GenericError.DOES_NOT_MEET_REQUIREMENTS })
            });
        }

        [TestMethod]
        public void Put()
        {
            entity.Title = "Put";
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.OK, false);
            List<BookingStepConfigDTO> search = reqCreator.CallSearch(st, "BookingStepConfig/where/id/eq/" + entity.Id,
                new List<string>() { "BookingSteps" }, typeof(List<BookingStepConfigDTO>));
            Assert.IsNotNull(search[0].DateModification);
            Assert.AreEqual(search[0].HomeId, entity.HomeId);
            Assert.AreEqual(search[0].Title, entity.Title);
            for (int i = 0; i < search[0].BookingSteps.Count; i++)
            {
                Assert.IsTrue(PropertiesComparison.PublicInstancePropertiesEqual<BookingStepDTO>(
                    search[0].BookingSteps[i], entity.BookingSteps[i], new string[] { "Id", "BookingStepConfig", "Documents", "MailTemplate" }));
            }
        }

        [TestMethod]
        public void PutExceededTitle()
        {
            entity.Title = new string('*', 257);
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.BadRequest, false);
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
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.BadRequest, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Title",
                new List<string>() { GenericError.CANNOT_BE_NULL_OR_EMPTY })
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
                new List<string>() { GenericError.CANNOT_BE_NULL_OR_EMPTY })
            });
        }

        [TestMethod]
        public void PostForbiddenHomeId()
        {
            entity.BookingSteps[0].Id = 0;
            entity.HomeId = -1;
            reqCreator.CreateRequest(st, path, HttpMethod.Post, entity, HttpStatusCode.BadRequest, false);
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
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.BadRequest, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("HomeId",
                new List<string>() { GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST })
            });
        }

        [TestMethod]
        public void PutForbiddenResource()
        {
            entity.BookingSteps[0].Id = -1;
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.BadRequest, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("BookingStep",
                new List<string>() { GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST })
            });
        }

        [TestMethod]
        public void PutNull()
        {
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, null, HttpStatusCode.BadRequest, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>(TypeOfName.GetNameFromType<BookingStepConfigDTO>(),
                new List<string>() { GenericError.CANNOT_BE_NULL_OR_EMPTY })
            });
        }
    }
}