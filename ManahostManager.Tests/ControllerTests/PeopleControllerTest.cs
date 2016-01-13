using ManahostManager.Domain.DTOs;
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
    public class PeopleControllerTest : ControllerTest<PeopleDTO>
    {
        private const string path = "/api/People";

        [TestInitialize]
        public void PostNestedEntities()
        {
            PeopleDTO dto = new PeopleDTO()
            {
                AcceptMailing = true,
                Addr = "4 place kleber",
                City = "Strasbourg",
                Civility = "Mr",
                Comment = "A mis le feu à la chambre",
                Country = "FRANCE",
                DateBirth = DateTime.UtcNow,
                DateCreation = DateTime.Now,
                Email = "contact@manahost.fr",
                Firstname = "CHAABANE",
                HomeId = 1,
                Lastname = "Jalal",
                Mark = 0,
                Phone1 = "0600000000",
                Phone2 = null,
                State = null,
                ZipCode = "67000",
                Hide = false
            };

            reqCreator.CreateRequest(st, path, HttpMethod.Post, dto, HttpStatusCode.OK, true, false);
            Assert.AreEqual(reqCreator.deserializedEntity.AcceptMailing, dto.AcceptMailing);
            Assert.AreEqual(reqCreator.deserializedEntity.Addr, dto.Addr);
            Assert.AreEqual(reqCreator.deserializedEntity.City, dto.City);
            Assert.AreEqual(reqCreator.deserializedEntity.Civility, dto.Civility);
            Assert.AreEqual(reqCreator.deserializedEntity.Comment, dto.Comment);
            Assert.AreEqual(reqCreator.deserializedEntity.Country, dto.Country);
            Assert.AreNotEqual(reqCreator.deserializedEntity.DateCreation, dto.DateCreation);
            Assert.AreEqual(reqCreator.deserializedEntity.Email, dto.Email);
            Assert.AreEqual(reqCreator.deserializedEntity.Firstname, dto.Firstname);
            Assert.AreEqual(reqCreator.deserializedEntity.HomeId, dto.HomeId);
            Assert.AreEqual(reqCreator.deserializedEntity.Lastname, dto.Lastname);
            Assert.AreEqual(reqCreator.deserializedEntity.Phone1, dto.Phone1);
            Assert.AreEqual(reqCreator.deserializedEntity.Phone2, dto.Phone2);
            Assert.AreEqual(reqCreator.deserializedEntity.State, dto.State);
            Assert.AreEqual(reqCreator.deserializedEntity.ZipCode, dto.ZipCode);
            Assert.AreEqual(reqCreator.deserializedEntity.Hide, dto.Hide);
            Assert.AreEqual(reqCreator.deserializedEntity.DateModification, null);
            entity = reqCreator.deserializedEntity;
        }

        [TestMethod]
        public void PostForbiddenHomeId()
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
        public void PutForbiddenHomeId()
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
        public void PostExceededAddr()
        {
            entity.Addr = new string('*', 1025);
            reqCreator.CreateRequest(st, path, HttpMethod.Post, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Addr",
                new List<string>() { GenericError.DOES_NOT_MEET_REQUIREMENTS }),
            });
        }

        [TestMethod]
        public void Put()
        {
            entity.Addr = "PUT";
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.OK, false, false);
            List<PeopleDTO> search = (List<PeopleDTO>)reqCreator.CallSearch(st, "People/where/id/eq/" + entity.Id,
                new List<string>(), typeof(List<PeopleDTO>));
            Assert.AreEqual(entity.AcceptMailing, search[0].AcceptMailing);
            Assert.AreEqual(entity.Addr, search[0].Addr);
            Assert.AreEqual(entity.City, search[0].City);
            Assert.AreEqual(entity.Civility, search[0].Civility);
            Assert.AreEqual(entity.Comment, search[0].Comment);
            Assert.AreEqual(entity.Country, search[0].Country);
            Assert.AreNotEqual(entity.DateCreation, search[0].DateCreation);
            Assert.AreEqual(entity.Email, search[0].Email);
            Assert.AreEqual(entity.Firstname, search[0].Firstname);
            Assert.AreEqual(entity.HomeId, search[0].HomeId);
            Assert.AreEqual(entity.Lastname, search[0].Lastname);
            Assert.AreEqual(entity.Phone1, search[0].Phone1);
            Assert.AreEqual(entity.Phone2, search[0].Phone2);
            Assert.AreEqual(entity.State, search[0].State);
            Assert.AreEqual(entity.ZipCode, search[0].ZipCode);
            Assert.AreEqual(entity.Hide, search[0].Hide);
            Assert.AreEqual(entity.DateModification, null);
        }

        [TestMethod]
        public void PutForbiddenResource()
        {
            reqCreator.CreateRequest(st, path + "/-1", HttpMethod.Put, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>(TypeOfName.GetNameFromType<PeopleDTO>(),
                new List<string>() { GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST }),
            });
        }

        [TestMethod]
        public void Delete()
        {
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Delete, entity, HttpStatusCode.OK, false, false);
            List<PeopleDTO> search = (List<PeopleDTO>)reqCreator.CallSearch(st, "People/where/id/eq/" + entity.Id,
                new List<string>(), typeof(List<PeopleDTO>));
            Assert.AreEqual(0, search.Count);
        }

        [TestMethod]
        public void DeleteForbidden()
        {
            reqCreator.CreateRequest(st, path + "/-1", HttpMethod.Delete, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>(TypeOfName.GetNameFromType<PeopleDTO>(),
                new List<string>() { GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST }),
            });
        }

        [TestMethod]
        public void PostExceededCity()
        {
            entity.City = new string('*', 257);
            reqCreator.CreateRequest(st, path, HttpMethod.Post, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("City",
                new List<string>() { GenericError.DOES_NOT_MEET_REQUIREMENTS }),
            });
        }

        [TestMethod]
        public void PostExceededFirstName()
        {
            entity.Firstname = new string('*', 257);
            reqCreator.CreateRequest(st, path, HttpMethod.Post, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Firstname",
                new List<string>() { GenericError.DOES_NOT_MEET_REQUIREMENTS }),
            });
        }

        [TestMethod]
        public void PostNullFirstName()
        {
            entity.Firstname = null;
            reqCreator.CreateRequest(st, path, HttpMethod.Post, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Firstname",
                new List<string>() { GenericError.CANNOT_BE_NULL_OR_EMPTY }),
            });
        }

        [TestMethod]
        public void PostNullLastName()
        {
            entity.Lastname = null;
            reqCreator.CreateRequest(st, path, HttpMethod.Post, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Lastname",
                new List<string>() { GenericError.CANNOT_BE_NULL_OR_EMPTY }),
            });
        }

        [TestMethod]
        public void PostInvalidEmail()
        {
            entity.Email = "notanemail";
            reqCreator.CreateRequest(st, path, HttpMethod.Post, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Email",
                new List<string>() { GenericError.WRONG_DATA }),
            });
        }

        [TestMethod]
        public void PostNeededEmailWithAcceptMailingAtTrue()
        {
            entity.AcceptMailing = true;
            entity.Email = null;
            reqCreator.CreateRequest(st, path, HttpMethod.Post, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Email",
                new List<string>() { GenericError.CANNOT_BE_NULL_OR_EMPTY }),
            });
        }

        [TestMethod]
        public void PutNeededEmailWithAcceptMailingAtTrue()
        {
            entity.AcceptMailing = true;
            entity.Email = null;
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Email",
                new List<string>() { GenericError.CANNOT_BE_NULL_OR_EMPTY }),
            });
        }

        [TestMethod]
        public void PutInvalidEmail()
        {
            entity.Email = "notanemail";
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Email",
                new List<string>() { GenericError.WRONG_DATA }),
            });
        }

        [TestMethod]
        public void PutNullLastName()
        {
            entity.Lastname = null;
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Lastname",
                new List<string>() { GenericError.CANNOT_BE_NULL_OR_EMPTY }),
            });
        }

        [TestMethod]
        public void PutNullFirstName()
        {
            entity.Firstname = null;
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Firstname",
                new List<string>() { GenericError.CANNOT_BE_NULL_OR_EMPTY }),
            });
        }

        [TestMethod]
        public void PostExceededLastName()
        {
            entity.Lastname = new string('*', 257);
            reqCreator.CreateRequest(st, path, HttpMethod.Post, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Lastname",
                new List<string>() { GenericError.DOES_NOT_MEET_REQUIREMENTS }),
            });
        }

        [TestMethod]
        public void PostBadMark()
        {
            entity.Mark = -1;
            reqCreator.CreateRequest(st, path, HttpMethod.Post, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Mark",
                new List<string>() { GenericError.WRONG_DATA }),
            });
            entity.Mark = 6;
            reqCreator.CreateRequest(st, path, HttpMethod.Post, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Mark",
                new List<string>() { GenericError.WRONG_DATA }),
            });
        }

        [TestMethod]
        public void PutBadMark()
        {
            entity.Mark = -1;
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Mark",
                new List<string>() { GenericError.WRONG_DATA }),
            });
            entity.Mark = 6;
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Mark",
                new List<string>() { GenericError.WRONG_DATA }),
            });
        }

        [TestMethod]
        public void PutExceededLastName()
        {
            entity.Lastname = new string('*', 257);
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Lastname",
                new List<string>() { GenericError.DOES_NOT_MEET_REQUIREMENTS }),
            });
        }

        [TestMethod]
        public void PutExceededFirstName()
        {
            entity.Firstname = new string('*', 257);
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Firstname",
                new List<string>() { GenericError.DOES_NOT_MEET_REQUIREMENTS }),
            });
        }

        [TestMethod]
        public void PostExceededCivility()
        {
            entity.Civility = new string('*', 257);
            reqCreator.CreateRequest(st, path, HttpMethod.Post, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Civility",
                new List<string>() { GenericError.DOES_NOT_MEET_REQUIREMENTS }),
            });
        }

        [TestMethod]
        public void PostExceededPhone1()
        {
            entity.Phone1 = new string('0', 257);
            reqCreator.CreateRequest(st, path, HttpMethod.Post, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Phone1",
                new List<string>() { GenericError.DOES_NOT_MEET_REQUIREMENTS }),
            });
        }

        [TestMethod]
        public void PostExceededPhone2()
        {
            entity.Phone2 = new string('0', 257);
            reqCreator.CreateRequest(st, path, HttpMethod.Post, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Phone2",
                new List<string>() { GenericError.DOES_NOT_MEET_REQUIREMENTS }),
            });
        }

        [TestMethod]
        public void PutExceededPhone2()
        {
            entity.Phone2 = new string('0', 257);
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Phone2",
                new List<string>() { GenericError.DOES_NOT_MEET_REQUIREMENTS }),
            });
        }

        [TestMethod]
        public void PutExceededPhone1()
        {
            entity.Phone1 = new string('0', 257);
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Phone1",
                new List<string>() { GenericError.DOES_NOT_MEET_REQUIREMENTS }),
            });
        }

        [TestMethod]
        public void PostExceededComment()
        {
            entity.Comment = new string('*', 4001);
            reqCreator.CreateRequest(st, path, HttpMethod.Post, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Comment",
                new List<string>() { GenericError.DOES_NOT_MEET_REQUIREMENTS }),
            });
        }

        [TestMethod]
        public void PostExceededState()
        {
            entity.State = new string('*', 257);
            reqCreator.CreateRequest(st, path, HttpMethod.Post, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("State",
                new List<string>() { GenericError.DOES_NOT_MEET_REQUIREMENTS }),
            });
        }

        [TestMethod]
        public void PostExceededZipCode()
        {
            entity.ZipCode = new string('*', 257);
            reqCreator.CreateRequest(st, path, HttpMethod.Post, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("ZipCode",
                new List<string>() { GenericError.DOES_NOT_MEET_REQUIREMENTS }),
            });
        }

        [TestMethod]
        public void PutExceededZipCode()
        {
            entity.ZipCode = new string('*', 257);
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("ZipCode",
                new List<string>() { GenericError.DOES_NOT_MEET_REQUIREMENTS }),
            });
        }

        [TestMethod]
        public void PutExceededState()
        {
            entity.State = new string('*', 257);
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("State",
                new List<string>() { GenericError.DOES_NOT_MEET_REQUIREMENTS }),
            });
        }

        [TestMethod]
        public void PostExceededCountry()
        {
            entity.Country = new string('*', 257);
            reqCreator.CreateRequest(st, path, HttpMethod.Post, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Country",
                new List<string>() { GenericError.DOES_NOT_MEET_REQUIREMENTS }),
            });
        }

        [TestMethod]
        public void PostExceededEmail()
        {
            entity.Email = new string('*', 257);
            reqCreator.CreateRequest(st, path, HttpMethod.Post, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Email",
                new List<string>() { GenericError.DOES_NOT_MEET_REQUIREMENTS }),
            });
        }

        [TestMethod]
        public void PutExceededEmail()
        {
            entity.Email = new string('*', 257);
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Email",
                new List<string>() { GenericError.DOES_NOT_MEET_REQUIREMENTS }),
            });
        }

        [TestMethod]
        public void PutExceededCountry()
        {
            entity.Country = new string('*', 257);
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Country",
                new List<string>() { GenericError.DOES_NOT_MEET_REQUIREMENTS }),
            });
        }

        [TestMethod]
        public void PutExceededComment()
        {
            entity.Comment = new string('*', 4001);
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Comment",
                new List<string>() { GenericError.DOES_NOT_MEET_REQUIREMENTS }),
            });
        }

        [TestMethod]
        public void PutExceededCivility()
        {
            entity.Civility = new string('*', 257);
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Civility",
                new List<string>() { GenericError.DOES_NOT_MEET_REQUIREMENTS }),
            });
        }

        [TestMethod]
        public void PutExceededCity()
        {
            entity.City = new string('*', 257);
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("City",
                new List<string>() { GenericError.DOES_NOT_MEET_REQUIREMENTS }),
            });
        }

        [TestMethod]
        public void PutExceededAddr()
        {
            entity.Addr = new string('*', 1025);
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Addr",
                new List<string>() { GenericError.DOES_NOT_MEET_REQUIREMENTS }),
            });
        }

        [TestMethod]
        public void PostNull()
        {
            reqCreator.CreateRequest(st, path, HttpMethod.Post, null, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>(TypeOfName.GetNameFromType<PeopleDTO>(),
                new List<string>() { GenericError.CANNOT_BE_NULL_OR_EMPTY })
            });
        }

        [TestMethod]
        public void PutNull()
        {
            reqCreator.CreateRequest(st, path + "/0", HttpMethod.Put, null, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>(TypeOfName.GetNameFromType<PeopleDTO>(),
                new List<string>() { GenericError.CANNOT_BE_NULL_OR_EMPTY })
            });
        }
    }
}