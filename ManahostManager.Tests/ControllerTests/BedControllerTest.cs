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
    public class BedControllerTest : ControllerTest<BedDTO>
    {
        private const string path = "/api/Room/Bed";

        [TestInitialize]
        public void PostNestedEntity()
        {
            BedDTO dto = new BedDTO()
            {
                Description = "DESC",
                HomeId = 1,
                NumberPeople = 1,
                Room = new RoomDTO()
                {
                    Id = 0,
                    HomeId = 1,
                    Capacity = 3,
                    Title = "Post",
                    RoomCategory = new RoomCategoryDTO()
                    {
                        Id = 0,
                        Title = "Post"
                    },
                    BillItemCategory = new BillItemCategoryDTO()
                    {
                        Id = 0,
                        Title = "Post"
                    },
                    Beds = new List<BedDTO>()
                    {
                        new BedDTO()
                        {
                            Id = 0,
                            NumberPeople = 3,
                            Room = new RoomDTO()
                            {
                                Title = "SHOULD BE IGNORED",
                                Capacity = 3,
                            }
                        },
                        new BedDTO()
                        {
                            Id = 0,
                            NumberPeople = 3
                        }
                    }
                }
            };

            reqCreator.CreateRequest(st, path, HttpMethod.Post, dto, HttpStatusCode.OK, true, false);
            List<BedDTO> search = (List<BedDTO>)reqCreator.CallSearch(st, "Bed/where/Id/eq/" + reqCreator.deserializedEntity.Id,
                new List<string>() { "Room", "Room.RoomCategory", "Room.BillItemCategory", "Room.Beds" }, typeof(List<BedDTO>));
            Assert.AreEqual(search[0].HomeId, 1);
            Assert.AreEqual(search[0].DateModification, null);
            Assert.AreEqual(dto.Room.Title, search[0].Room.Title);
            Assert.AreEqual(true, search[0].Room.RefHide);
            Assert.AreEqual(0xFFFFFF, search[0].Room.Color);
            Assert.AreEqual(false, search[0].Room.IsClosed);
            Assert.AreEqual(false, search[0].Room.Hide);
            Assert.AreEqual(dto.Room.Capacity, search[0].Room.Capacity);
            Assert.AreEqual(dto.Room.RoomCategory.Title, search[0].Room.RoomCategory.Title);
            Assert.AreEqual(dto.Room.BillItemCategory.Title, search[0].Room.BillItemCategory.Title);
            Assert.AreEqual(3, search[0].Room.Beds.Count);
            entity = reqCreator.deserializedEntity;
        }

        [TestMethod]
        public void Post()
        {
            reqCreator.CreateRequest(st, path, HttpMethod.Post, entity, HttpStatusCode.OK, true, false);
            Assert.AreEqual(reqCreator.deserializedEntity.HomeId, 1);
            Assert.AreEqual(reqCreator.deserializedEntity.DateModification, null);
        }

        [TestMethod]
        public void PostNestedEntityAlreadyExists()
        {
            entity.Room = new RoomDTO() { Id = entity.Room.Id };
            reqCreator.CreateRequest(st, path, HttpMethod.Post, entity, HttpStatusCode.OK, true, false);
            List<BedDTO> search = (List<BedDTO>)reqCreator.CallSearch(st, "Bed/where/Id/eq/" + reqCreator.deserializedEntity.Id,
                new List<string>() { "Room", "Room.RoomCategory", "Room.BillItemCategory", "Room.Beds" }, typeof(List<BedDTO>));
            Assert.AreEqual(search[0].HomeId, 1);
            Assert.AreEqual(search[0].DateModification, null);
            Assert.AreEqual(entity.Room.Id, search[0].Room.Id);
        }

        [TestMethod]
        public void PostForbiddenNestedEntity()
        {
            entity.Room = new RoomDTO() { Id = -1 };
            reqCreator.CreateRequest(st, path, HttpMethod.Post, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Room",
                new List<string>() { GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST }),
            });
        }

        [TestMethod]
        public void PutNestedEntityAlreadyExists()
        {
            entity.Room = new RoomDTO() { Id = entity.Room.Id };
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.OK, false, false);
            List<BedDTO> search = (List<BedDTO>)reqCreator.CallSearch(st, "Bed/where/Id/eq/" + entity.Id, new List<string>() { "Room" }, typeof(List<BedDTO>));
            Assert.AreEqual(search[0].HomeId, 1);
            Assert.IsNotNull(search[0].DateModification);
            Assert.AreEqual(entity.Room.Id, search[0].Room.Id);
        }

        [TestMethod]
        public void PostNull()
        {
            reqCreator.CreateRequest(st, path, HttpMethod.Post, null, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>(TypeOfName.GetNameFromType<BedDTO>(), new List<string>() { GenericError.CANNOT_BE_NULL_OR_EMPTY })
            });
        }

        [TestMethod]
        public void PutNull()
        {
            reqCreator.CreateRequest(st, path + "/0", HttpMethod.Put, null, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>(TypeOfName.GetNameFromType<BedDTO>(), new List<string>() { GenericError.CANNOT_BE_NULL_OR_EMPTY })
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
                new List<string>() { GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST })
            });
        }

        [TestMethod]
        public void PostInvalidNumberPeople()
        {
            entity.NumberPeople = -1;
            reqCreator.CreateRequest(st, path, HttpMethod.Post, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("NumberPeople",
                new List<string>() { GenericError.WRONG_DATA })
            });
        }

        [TestMethod]
        public void PostExceededDescription()
        {
            entity.Description = new string('*', 257);
            reqCreator.CreateRequest(st, path, HttpMethod.Post, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Description",
                new List<string>() { GenericError.DOES_NOT_MEET_REQUIREMENTS })
            });
        }

        [TestMethod]
        public void PutExceededDescription()
        {
            entity.Description = new string('*', 257);
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Description",
                new List<string>() { GenericError.DOES_NOT_MEET_REQUIREMENTS })
            });
        }

        [TestMethod]
        public void PutInvalidNumberPeople()
        {
            entity.NumberPeople = -1;
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("NumberPeople",
                new List<string>() { GenericError.WRONG_DATA })
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
                new List<string>() { GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST })
            });
        }

        [TestMethod]
        public void Put()
        {
            entity.Description = "PUT";
            entity.NumberPeople = 99;
            entity.Room = new RoomDTO() { Id = 2 };
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.OK, false, false);
            List<BedDTO> search = (List<BedDTO>)reqCreator.CallSearch(st, "Bed/where/Id/eq/" + entity.Id, new List<string>() { "Room" }, typeof(List<BedDTO>));
            Assert.AreEqual(entity.Description, search[0].Description);
            Assert.AreEqual(entity.NumberPeople, search[0].NumberPeople);
            Assert.AreEqual(entity.Room.Id, search[0].Room.Id);
            Assert.IsNotNull(search[0].DateModification);
        }

        [TestMethod]
        public void PutSetRoomToNull()
        {
            entity.Room.Id = null;
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.OK, false, false);
            List<BedDTO> search = (List<BedDTO>)reqCreator.CallSearch(st, "Bed/where/Id/eq/" + entity.Id, new List<string>() { "Room" }, typeof(List<BedDTO>));
            Assert.AreEqual(entity.Description, search[0].Description);
            Assert.AreEqual(entity.NumberPeople, search[0].NumberPeople);
            Assert.IsNull(search[0].Room);
            Assert.IsNotNull(search[0].DateModification);
        }

        [TestMethod]
        public void Delete()
        {
            int roomId = 0;

            List<BedDTO> search = (List<BedDTO>)reqCreator.CallSearch(st, "Bed/where/Id/eq/" + entity.Id, new List<string>() { "Room" }, typeof(List<BedDTO>));
            roomId = (int)search[0].Room.Id;
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Delete, null, HttpStatusCode.OK, false, true);
            search = (List<BedDTO>)reqCreator.CallSearch(st, "Bed/where/Id/eq/" + entity.Id, new List<string>() { "Room" }, typeof(List<BedDTO>));
            Assert.AreEqual(0, search.Count);
            List<RoomDTO> searchRoom = (List<RoomDTO>)reqCreator.CallSearch(st, "Room/where/Id/eq/" + roomId, new List<string>(), typeof(List<RoomDTO>));
            Assert.AreEqual(1, searchRoom.Count);
        }

        [TestMethod]
        public void DeleteForbiddenResource()
        {
            reqCreator.CreateRequest(st, path + "/-1", HttpMethod.Delete, null, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>(TypeOfName.GetNameFromType<BedDTO>(),
                new List<string>() { GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST })
            });
        }
    }
}