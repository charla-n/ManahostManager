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
    public class BookingStepBookingControllerTest : ControllerTest<BookingStepBookingDTO>
    {
        private const string path = "/api/Booking/StepBooking";

        [TestInitialize]
        public void PostNestedEntities()
        {
            RequestCreator<BookingStepConfigDTO> reqCreatorBookingStepConfig = new RequestCreator<BookingStepConfigDTO>();

            BookingStepConfigDTO stepConfigDto = new BookingStepConfigDTO()
            {
                HomeId = 1,
                Title = "Post"
            };

            reqCreatorBookingStepConfig.CreateRequest(st, "/api/Booking/StepConfig", HttpMethod.Post, stepConfigDto, HttpStatusCode.OK, true);

            RequestCreator<BookingStepDTO> reqCreatorBookingStep = new RequestCreator<BookingStepDTO>();

            BookingStepDTO step1 = new BookingStepDTO()
            {
                BookingStepConfig = new BookingStepConfigDTO()
                {
                    Id = reqCreatorBookingStepConfig.deserializedEntity.Id
                },
                HomeId = 1,
                Title = "Step1"
            };

            reqCreatorBookingStep.CreateRequest(st, "/api/Booking/Step", HttpMethod.Post, step1, HttpStatusCode.OK, true);
            step1 = reqCreatorBookingStep.deserializedEntity;

            BookingStepDTO step2 = new BookingStepDTO()
            {
                BookingStepConfig = new BookingStepConfigDTO()
                {
                    Id = reqCreatorBookingStepConfig.deserializedEntity.Id
                },
                HomeId = 1,
                Title = "Step2",
                BookingStepIdPrevious = reqCreatorBookingStep.deserializedEntity.Id
            };

            reqCreatorBookingStep.CreateRequest(st, "/api/Booking/Step", HttpMethod.Post, step2, HttpStatusCode.OK, true);
            step2 = reqCreatorBookingStep.deserializedEntity;

            BookingDTO dto = new BookingDTO()
            {
                AdditionalBookings = new System.Collections.Generic.List<AdditionalBookingDTO>()
                {
                    new AdditionalBookingDTO()
                    {
                        Id = 0,
                        BillItemCategory = new BillItemCategoryDTO()
                        {
                            Id = 0,
                            HomeId = 1,
                            Title = "Post"
                        },
                        HomeId = 1,
                        PriceHT = 1M,
                        Title = "Post",
                        Tax = new TaxDTO()
                        {
                            Id = 0,
                            HomeId = 1,
                            Price = 1M,
                            Title = "Post",
                            ValueType = EValueType.AMOUNT
                        }
                    }
                },
                DateArrival = DateTime.Now.AddDays(2),
                DateDeparture = DateTime.Now.AddDays(4),
                Deposits = new System.Collections.Generic.List<DepositDTO>()
                {
                    new DepositDTO()
                    {
                        Id = 0,
                        HomeId = 1,
                        Price = 10M,
                        ValueType = EValueType.PERCENT
                    }
                },
                DinnerBookings = new System.Collections.Generic.List<DinnerBookingDTO>()
                {
                    new DinnerBookingDTO()
                    {
                        Id = 0,
                        HomeId = 1,
                        MealBookings = new System.Collections.Generic.List<MealBookingDTO>()
                        {
                            new MealBookingDTO()
                            {
                                Id = 0,
                                HomeId = 1,
                                Meal = new MealDTO()
                                {
                                    Id = 1
                                },
                                NumberOfPeople = 1,
                            }
                        },
                        NumberOfPeople = 1,
                        Date = DateTime.Now.AddDays(2)
                    }
                },
                HomeId = 1,
                People = new PeopleDTO()
                {
                    Id = 0,
                    Firstname = "Chaabane",
                    Lastname = "Jalal",
                    HomeId = 1
                },
                ProductBookings = new System.Collections.Generic.List<ProductBookingDTO>()
                {
                    new ProductBookingDTO()
                    {
                        Id = 0,
                        HomeId = 1,
                        Product = new ProductDTO()
                        {
                            Id = 1
                        },
                        Quantity = 1
                    }
                },
                RoomBookings = new System.Collections.Generic.List<RoomBookingDTO>()
                {
                    new RoomBookingDTO()
                    {
                        Id = 0,
                        HomeId = 1,
                        NbNights = 1,
                        PeopleBookings = new System.Collections.Generic.List<PeopleBookingDTO>()
                        {
                            new PeopleBookingDTO()
                            {
                                Id = 0,
                                HomeId = 1,
                                NumberOfPeople = 1,
                                PeopleCategory = new PeopleCategoryDTO()
                                {
                                    Id = 1
                                },
                            }
                        },
                        Room = new RoomDTO()
                        {
                            Id = 1
                        },
                        SupplementRoomBookings = new System.Collections.Generic.List<SupplementRoomBookingDTO>()
                        {
                            new SupplementRoomBookingDTO()
                            {
                                Id = 0,
                                HomeId = 1,
                                RoomSupplement = new RoomSupplementDTO()
                                {
                                    Id = 1
                                }
                            }
                        }
                    }
                }
            };

            RequestCreator<BookingDTO> reqCreatorBooking = new RequestCreator<BookingDTO>();

            reqCreatorBooking.CreateRequest(st, BookingControllerTest.path, HttpMethod.Post, dto, HttpStatusCode.OK, true);

            BookingStepBookingDTO dtoBSB = new BookingStepBookingDTO()
            {
                Id = 0,
                Booking = new BookingDTO()
                {
                    Id = reqCreatorBooking.deserializedEntity.Id,
                },
                BookingStepConfig = new BookingStepConfigDTO()
                {
                    Id = reqCreatorBookingStepConfig.deserializedEntity.Id
                },
                HomeId = 1,
                MailSent = 10
            };

            reqCreator.CreateRequest(st, path, HttpMethod.Post, dtoBSB, HttpStatusCode.OK, true, false);
            entity = reqCreator.deserializedEntity;
            List<BookingStepBookingDTO> search = reqCreator.CallSearch(st, "BookingStepBooking/where/id/eq/" + entity.Id,
                new List<string>() { "BookingStepConfig", "CurrentStep" }, typeof(List<BookingStepBookingDTO>));
            Assert.AreEqual(entity.MailSent, 0);
            Assert.AreEqual(dtoBSB.HomeId, entity.HomeId);
            Assert.IsNull(entity.DateModification);
            Assert.IsNotNull(entity.DateCurrentStepChanged);
            Assert.AreEqual(entity.Canceled, false);
        }

        [TestMethod]
        public void PostNull()
        {
            reqCreator.CreateRequest(st, path, HttpMethod.Post, null, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>(TypeOfName.GetNameFromType<BookingStepBookingDTO>(),
                new List<string>() { GenericError.CANNOT_BE_NULL_OR_EMPTY })
            });
        }

        [TestMethod]
        public void PutNull()
        {
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, null, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>(TypeOfName.GetNameFromType<BookingStepBookingDTO>(),
                new List<string>() { GenericError.CANNOT_BE_NULL_OR_EMPTY })
            });
        }

        [TestMethod]
        public void PostNullBookingStep()
        {
            entity.BookingStepConfig = new BookingStepConfigDTO()
            {
                Title = "Post",
                HomeId = 1
            };
            entity.CurrentStep = null;
            reqCreator.CreateRequest(st, path, HttpMethod.Post, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("CurrentStep",
                new List<string>() { GenericError.CANNOT_BE_NULL_OR_EMPTY })
            });
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
        public void PostForbiddenBookingStepConfig()
        {
            entity.BookingStepConfig.Id = -1;
            reqCreator.CreateRequest(st, path, HttpMethod.Post, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("BookingStepConfig",
                new List<string>() { GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST })
            });
        }

        [TestMethod]
        public void PutForbiddenBookingStepConfig()
        {
            entity.BookingStepConfig.Id = -1;
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("BookingStepConfig",
                new List<string>() { GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST })
            });
        }

        [TestMethod]
        public void PostNullBookingStepConfig()
        {
            entity.BookingStepConfig = null;
            reqCreator.CreateRequest(st, path, HttpMethod.Post, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("BookingStep",
                new List<string>() { GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST })
            });
        }

        [TestMethod]
        public void PostForbiddenBooking()
        {
            entity.Booking.Id = -1;
            reqCreator.CreateRequest(st, path, HttpMethod.Post, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Booking",
                new List<string>() { GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST })
            });
        }

        [TestMethod]
        public void PostNullCurrentStepBecauseOfBookingStepConfig()
        {
            entity.BookingStepConfig = new BookingStepConfigDTO()
            {
                HomeId = 1,
                Title = "Post"
            };
            reqCreator.CreateRequest(st, path, HttpMethod.Post, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("BookingStep",
                new List<string>() { GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST })
            });
        }

        [TestMethod]
        public void PutForbiddenBooking()
        {
            entity.Booking.Id = -1;
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Booking",
                new List<string>() { GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST })
            });
        }

        [TestMethod]
        public void PutNullBookingStepConfig()
        {
            entity.BookingStepConfig = null;
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("CurrentStep",
                new List<string>() { GenericError.CANNOT_BE_NULL_OR_EMPTY })
            });
        }

        [TestMethod]
        public void Put()
        {
            entity.Canceled = true;
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.OK, false, false);
            List<BookingStepBookingDTO> search = reqCreator.CallSearch(st, "BookingStepBooking/where/id/eq/" + entity.Id,
                new List<string>() { "BookingStepConfig", "CurrentStep" }, typeof(List<BookingStepBookingDTO>));
            Assert.AreEqual(search[0].MailSent, 0);
            Assert.AreEqual(search[0].HomeId, entity.HomeId);
            Assert.IsNotNull(search[0].DateModification);
            Assert.IsNotNull(search[0].DateCurrentStepChanged);
            Assert.AreEqual(search[0].Canceled, true);
            Assert.IsTrue(PropertiesComparison.PublicInstancePropertiesEqual<BookingStepConfigDTO>(
                search[0].BookingStepConfig, entity.BookingStepConfig, new string[] { "Id", "BookingSteps" }));
        }

        [TestMethod]
        public void DeleteForbidden()
        {
            reqCreator.CreateRequest(st, path + "/-1", HttpMethod.Delete, null, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("BookingStepBookingDTO",
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
    }
}