using ManahostManager.Domain.DTOs;
using ManahostManager.Domain.Entity;
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
    public class RoomControllerTest : ControllerTest<RoomDTO>
    {
        public const string path = "/api/Room";

        [TestInitialize]
        public void PostNestedEntities()
        {
            RoomDTO dto = new RoomDTO()
            {
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
                        NumberPeople = 3
                    },
                    new BedDTO()
                    {
                        Id = 0,
                        NumberPeople = 3
                    }
                }
            };

            reqCreator.CreateRequest(st, path, HttpMethod.Post, dto, HttpStatusCode.OK, true, false);
            List<RoomDTO> search = (List<RoomDTO>)reqCreator.CallSearch(st, "Room/where/id/eq/" + reqCreator.deserializedEntity.Id,
                new List<string>() { "RoomCategory", "BillItemCategory", "Beds" }, typeof(List<RoomDTO>));
            Assert.AreEqual(null, search[0].DateModification);
            Assert.AreEqual(0xFFFFFF, search[0].Color);
            Assert.AreEqual(true, search[0].RefHide);
            Assert.AreEqual(false, search[0].IsClosed);
            Assert.AreEqual(false, search[0].Hide);
            Assert.AreEqual(dto.RoomCategory.Title, search[0].RoomCategory.Title);
            Assert.AreEqual(dto.BillItemCategory.Title, search[0].BillItemCategory.Title);
            Assert.AreEqual(dto.Beds[0].NumberPeople, search[0].Beds[0].NumberPeople);
            Assert.AreEqual(dto.Beds[1].NumberPeople, search[0].Beds[1].NumberPeople);
            entity = reqCreator.deserializedEntity;
        }

        [TestMethod]
        public void Post()
        {
            RoomDTO dto = new RoomDTO()
            {
                HomeId = 1,
                Capacity = 3,
                Title = "Post"
            };

            reqCreator.CreateRequest(st, path, HttpMethod.Post, dto, HttpStatusCode.OK, true, false);
            Assert.AreEqual(null, reqCreator.deserializedEntity.DateModification);
            Assert.AreEqual(0xFFFFFF, reqCreator.deserializedEntity.Color);
            Assert.AreEqual(true, reqCreator.deserializedEntity.RefHide);
            Assert.AreEqual(false, reqCreator.deserializedEntity.IsClosed);
            Assert.AreEqual(false, reqCreator.deserializedEntity.Hide);
        }

        [TestMethod]
        public void PostNestedEntitiesAlreadyExist()
        {
            RoomDTO dto2 = new RoomDTO()
            {
                HomeId = 1,
                Capacity = 3,
                Title = "Post",
                RoomCategory = new RoomCategoryDTO()
                {
                    Id = entity.RoomCategory.Id
                },
                BillItemCategory = new BillItemCategoryDTO()
                {
                    Id = entity.BillItemCategory.Id
                },
                Beds = new List<BedDTO>()
                {
                    new BedDTO()
                    {
                        Id = entity.Beds[0].Id
                    },
                    new BedDTO()
                    {
                        Id = entity.Beds[1].Id
                    }
                }
            };

            reqCreator.CreateRequest(st, path, HttpMethod.Post, dto2, HttpStatusCode.OK, true, false);
            List<RoomDTO> search = (List<RoomDTO>)reqCreator.CallSearch(st, "Room/where/id/eq/" + reqCreator.deserializedEntity.Id,
                new List<string>() { "RoomCategory", "BillItemCategory", "Beds" }, typeof(List<RoomDTO>));
            Assert.AreEqual(null, search[0].DateModification);
            Assert.AreEqual(0xFFFFFF, search[0].Color);
            Assert.AreEqual(true, search[0].RefHide);
            Assert.AreEqual(false, search[0].IsClosed);
            Assert.AreEqual(false, search[0].Hide);
            Assert.AreEqual(dto2.RoomCategory.Id, search[0].RoomCategory.Id);
            Assert.AreEqual(dto2.BillItemCategory.Id, search[0].BillItemCategory.Id);
            Assert.AreEqual(dto2.Beds[0].Id, search[0].Beds[0].Id);
            Assert.AreEqual(dto2.Beds[1].Id, search[0].Beds[1].Id);
        }

        [TestMethod]
        public void PostForbiddenNestedEntities()
        {
            RoomDTO dto = new RoomDTO()
            {
                HomeId = 1,
                Capacity = 3,
                Title = "Post",
                RoomCategory = new RoomCategoryDTO()
                {
                    Id = -1
                },
                BillItemCategory = new BillItemCategoryDTO()
                {
                    Id = -1
                },
                Beds = new List<BedDTO>()
                {
                    new BedDTO()
                    {
                        Id = -1
                    },
                    new BedDTO()
                    {
                        Id = -2
                    }
                }
            };

            reqCreator.CreateRequest(st, path, HttpMethod.Post, dto, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("BillItemCategory",
                new List<string>() { GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST }),
                new KeyValuePair<string, List<string>>("RoomCategory",
                new List<string>() { GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST }),
                new KeyValuePair<string, List<string>>("Bed",
                new List<string>() { GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST,
                    GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST })
            });
        }

        [TestMethod]
        public void PutNestedEntitiesAlreadyExist()
        {
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.OK, false, false);
            List<RoomDTO> search = (List<RoomDTO>)reqCreator.CallSearch(st, "Room/where/id/eq/" + entity.Id,
                new List<string>() { "RoomCategory", "BillItemCategory", "Beds" }, typeof(List<RoomDTO>));
            Assert.IsNotNull(search[0].DateModification);
            Assert.AreEqual(0xFFFFFF, search[0].Color);
            Assert.AreEqual(true, search[0].RefHide);
            Assert.AreEqual(false, search[0].IsClosed);
            Assert.AreEqual(false, search[0].Hide);
            Assert.AreEqual("Post", search[0].Title);
            Assert.AreEqual(entity.RoomCategory.Id, search[0].RoomCategory.Id);
            Assert.AreEqual(entity.BillItemCategory.Id, search[0].BillItemCategory.Id);
            Assert.AreEqual(entity.Beds.Count, search[0].Beds.Count);
        }

        [TestMethod]
        public void Put()
        {
            entity.Title = "PUT";
            entity.Caution = 500M;
            entity.Color = 0x00FF00;
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.OK, false, false);
            List<RoomDTO> search = (List<RoomDTO>)reqCreator.CallSearch(st, "Room/where/id/eq/" + entity.Id, new List<string>(), typeof(List<RoomDTO>));
            Assert.IsNotNull(search[0].DateModification);
            Assert.AreEqual(search[0].Title, entity.Title);
            Assert.AreEqual(search[0].Caution, entity.Caution);
            Assert.AreEqual(search[0].Color, entity.Color);
        }

        [TestMethod]
        public void Delete()
        {
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Delete, null, HttpStatusCode.OK, false, true);
            List<RoomDTO> search = (List<RoomDTO>)reqCreator.CallSearch(st, "Room/where/id/eq/" + entity.Id, new List<string>(), typeof(List<RoomDTO>));
            Assert.AreEqual(0, search.Count);
        }

        [TestMethod]
        public void DeleteForbiddenResource()
        {
            reqCreator.CreateRequest(st, path + "/-1", HttpMethod.Delete, null, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>(TypeOfName.GetNameFromType<RoomDTO>(),
                new List<string>() { GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST })
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
        public void PostInvalidCaution()
        {
            entity.Caution = -1;
            reqCreator.CreateRequest(st, path, HttpMethod.Post, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Caution",
                new List<string>() { GenericError.WRONG_DATA })
            });
        }

        [TestMethod]
        public void PostExceededClassification()
        {
            entity.Classification = new string('*', 257);
            reqCreator.CreateRequest(st, path, HttpMethod.Post, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Classification",
                new List<string>() { GenericError.DOES_NOT_MEET_REQUIREMENTS })
            });
        }

        [TestMethod]
        public void PostExceededDescription()
        {
            entity.Description = new string('*', 4001);
            reqCreator.CreateRequest(st, path, HttpMethod.Post, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Description",
                new List<string>() { GenericError.DOES_NOT_MEET_REQUIREMENTS })
            });
        }

        [TestMethod]
        public void PostExceededShortDescription()
        {
            entity.ShortDescription = new string('*', 257);
            reqCreator.CreateRequest(st, path, HttpMethod.Post, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("ShortDescription",
                new List<string>() { GenericError.DOES_NOT_MEET_REQUIREMENTS })
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
        public void PostExceededRoomState()
        {
            entity.RoomState = new string('*', 4001);
            reqCreator.CreateRequest(st, path, HttpMethod.Post, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("RoomState",
                new List<string>() { GenericError.DOES_NOT_MEET_REQUIREMENTS })
            });
        }

        [TestMethod]
        public void PutExceededRoomState()
        {
            entity.RoomState = new string('*', 4001);
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("RoomState",
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
        public void PutExceededShortDescription()
        {
            entity.ShortDescription = new string('*', 257);
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("ShortDescription",
                new List<string>() { GenericError.DOES_NOT_MEET_REQUIREMENTS })
            });
        }

        [TestMethod]
        public void PutExceededDescription()
        {
            entity.Description = new string('*', 4001);
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>(String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<Room>(), "Description"),
                new List<string>() { GenericError.DOES_NOT_MEET_REQUIREMENTS })
            });
        }

        [TestMethod]
        public void PutExceededClassification()
        {
            entity.Classification = new string('*', 257);
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Classification",
                new List<string>() { GenericError.DOES_NOT_MEET_REQUIREMENTS })
            });
        }

        [TestMethod]
        public void PostInvalidColor()
        {
            entity.Color = -1;
            reqCreator.CreateRequest(st, path, HttpMethod.Post, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>(String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<Room>(), "Color"),
                new List<string>() { GenericError.WRONG_DATA })
            });
            entity.Color = 0xFFFFFF + 1;
            reqCreator.CreateRequest(st, path, HttpMethod.Post, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Color",
                new List<string>() { GenericError.WRONG_DATA })
            });
        }

        [TestMethod]
        public void PostInvalidSize()
        {
            entity.Size = -1;
            reqCreator.CreateRequest(st, path, HttpMethod.Post, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Size",
                new List<string>() { GenericError.WRONG_DATA })
            });
        }

        [TestMethod]
        public void PostInvalidCapacity()
        {
            entity.Capacity = -1;
            reqCreator.CreateRequest(st, path, HttpMethod.Post, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Capacity",
                new List<string>() { GenericError.WRONG_DATA })
            });
        }

        [TestMethod]
        public void PostNullCapacity()
        {
            entity.Capacity = null;
            reqCreator.CreateRequest(st, path, HttpMethod.Post, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Capacity",
                new List<string>() { GenericError.CANNOT_BE_NULL_OR_EMPTY })
            });
        }

        [TestMethod]
        public void PutNullCapacity()
        {
            entity.Capacity = null;
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Capacity",
                new List<string>() { GenericError.CANNOT_BE_NULL_OR_EMPTY })
            });
        }

        [TestMethod]
        public void PutInvalidCapacity()
        {
            entity.Capacity = -1;
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Capacity",
                new List<string>() { GenericError.WRONG_DATA })
            });
        }

        [TestMethod]
        public void PutInvalidSize()
        {
            entity.Size = -1;
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Size",
                new List<string>() { GenericError.WRONG_DATA })
            });
        }

        [TestMethod]
        public void PutInvalidColor()
        {
            entity.Color = -1;
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Color",
                new List<string>() { GenericError.WRONG_DATA })
            });
            entity.Color = 0xFFFFFF + 1;
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Color",
                new List<string>() { GenericError.WRONG_DATA })
            });
        }

        [TestMethod]
        public void PutInvalidCaution()
        {
            entity.Caution = -1;
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Caution",
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
        public void PostNull()
        {
            reqCreator.CreateRequest(st, path, HttpMethod.Post, null, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>(TypeOfName.GetNameFromType<RoomDTO>(), new List<string>() { GenericError.CANNOT_BE_NULL_OR_EMPTY })
            });
        }

        [TestMethod]
        public void PutNull()
        {
            reqCreator.CreateRequest(st, path + "/0", HttpMethod.Put, null, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>(TypeOfName.GetNameFromType<RoomDTO>(), new List<string>() { GenericError.CANNOT_BE_NULL_OR_EMPTY })
            });
        }
    }
}