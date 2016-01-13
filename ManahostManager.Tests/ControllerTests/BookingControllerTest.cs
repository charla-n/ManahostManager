using ManahostManager.Domain.DTOs;
using ManahostManager.Domain.Entity;
using ManahostManager.Model;
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
    public class BookingControllerTest : ControllerTest<BookingDTO>
    {
        public const string path = "/api/Booking";

        [TestInitialize]
        public void PostNestedEntities()
        {
            BookingDTO dto = new BookingDTO()
            {
                AdditionalBookings = new System.Collections.Generic.List<AdditionalBookingDTO>()
                {
                    new AdditionalBookingDTO()
                    {
                        BillItemCategory = new BillItemCategoryDTO()
                        {
                            Id = 0,
                            HomeId = 1,
                            Title = "Post"
                        },
                        Id = 0,
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
                BookingStepBooking = new BookingStepBookingDTO()
                {
                    Id = 0,
                    BookingStepConfig = new BookingStepConfigDTO()
                    {
                        Id = 1
                    },
                    HomeId = 1
                },
                DateArrival = DateTime.Now.AddDays(30),
                DateDeparture = DateTime.Now.AddDays(35),
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
                        Date = DateTime.Now.AddDays(120)
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

            reqCreator.CreateRequest(st, path, HttpMethod.Post, dto, HttpStatusCode.OK, true, false);
            entity = reqCreator.deserializedEntity;
            List<BookingDTO> search = reqCreator.CallSearch(st, "Booking/where/id/eq/" + entity.Id,
                new List<string>()
                {
                    "People",
                    "AdditionalBookings",
                    "AdditionalBookings.Tax",
                    "AdditionalBookings.BillItemCategory",
                    "BookingStepBooking",
                    "Deposits",
                    "DinnerBookings",
                    "DinnerBookings.MealBookings",
                    "DinnerBookings.MealBookings.PeopleCategory",
                    "ProductBookings.Product",
                    "ProductBookings",
                    "RoomBookings",
                    "RoomBookings.Room",
                    "RoomBookings.PeopleBookings",
                    "RoomBookings.SupplementRoomBookings"
                }, typeof(List<BookingDTO>));
            Assert.AreEqual(dto.Comment, search[0].Comment);
            for (int i = 0; i < dto.AdditionalBookings.Count; i++)
            {
                Assert.IsTrue(PropertiesComparison.PublicInstancePropertiesEqual<AdditionalBookingDTO>(
                    dto.AdditionalBookings[i], search[0].AdditionalBookings[i], new string[] { "Id", "PriceTTC", "BillItemCategory", "Tax", "Booking" }));
                Assert.AreEqual(dto.AdditionalBookings[i].PriceHT, search[0].AdditionalBookings[i].PriceTTC);
                Assert.IsTrue(PropertiesComparison.PublicInstancePropertiesEqual<BillItemCategoryDTO>(
                    dto.AdditionalBookings[i].BillItemCategory, search[0].AdditionalBookings[i].BillItemCategory, new string[] { "Id" }));
                Assert.IsTrue(PropertiesComparison.PublicInstancePropertiesEqual<TaxDTO>(
                    dto.AdditionalBookings[i].Tax, search[0].AdditionalBookings[i].Tax, new string[] { "Id" }));
            }
            Assert.AreEqual(dto.DateArrival.Date, search[0].DateArrival.Date);
            Assert.IsNotNull(search[0].DateCreation);
            Assert.AreNotEqual(search[0].DateCreation, dto.DateCreation);
            Assert.AreEqual(dto.DateDeparture.Date, search[0].DateDeparture.Date);
            Assert.AreEqual(dto.DateDesiredPayment, search[0].DateDesiredPayment);
            Assert.IsNull(search[0].DateModification);
            Assert.AreEqual(dto.DateValidation, search[0].DateValidation);
            for (int i = 0; i < dto.Deposits.Count; i++)
            {
                Assert.IsTrue(PropertiesComparison.PublicInstancePropertiesEqual<DepositDTO>(
                    dto.Deposits[i], search[0].Deposits[i], new string[] { "Id", "Booking", "DateCreation" }));
            }
            for (int i = 0; i < dto.DinnerBookings.Count; i++)
            {
                Assert.IsTrue(PropertiesComparison.PublicInstancePropertiesEqual<DinnerBookingDTO>(
                    dto.DinnerBookings[i], search[0].DinnerBookings[i],
                    new string[] { "Id", "Booking", "MealBookings", "Date", "PriceHT", "PriceTTC" }));
                Assert.AreEqual(0, search[0].DinnerBookings[i].PriceHT);
                Assert.AreEqual(0, search[0].DinnerBookings[i].PriceTTC);
                Assert.AreEqual(((DateTime)search[0].DinnerBookings[i].Date).Date, ((DateTime)dto.DinnerBookings[i].Date).Date);
                for (int j = 0; j < dto.DinnerBookings[i].MealBookings.Count; j++)
                {
                    Assert.IsTrue(PropertiesComparison.PublicInstancePropertiesEqual<MealBookingDTO>(
                        dto.DinnerBookings[i].MealBookings[j], search[0].DinnerBookings[i].MealBookings[j],
                        new string[] { "Id", "Meal", "PeopleCategory", "DinnerBooking" }));
                    Assert.IsTrue(PropertiesComparison.PublicInstancePropertiesEqual<PeopleCategoryDTO>(
                        dto.DinnerBookings[i].MealBookings[j].PeopleCategory, search[0].DinnerBookings[i].MealBookings[j].PeopleCategory,
                        new string[] { "Id", "Tax" }));
                }
            }
            Assert.AreEqual(dto.HomeId, search[0].HomeId);
            Assert.AreEqual(dto.IsOnline, search[0].IsOnline);
            Assert.AreEqual(dto.IsSatisfactionSended, search[0].IsSatisfactionSended);
            Assert.AreEqual(0, search[0].TotalPeople);
            Assert.IsNotNull(search[0].BookingStepBooking);
            Assert.IsTrue(PropertiesComparison.PublicInstancePropertiesEqual<PeopleDTO>(
                dto.People, search[0].People, new string[] { "Id", "DateCreation" }));
            for (int i = 0; i < dto.ProductBookings.Count; i++)
            {
                Assert.IsTrue(PropertiesComparison.PublicInstancePropertiesEqual<ProductBookingDTO>(
                    dto.ProductBookings[i], search[0].ProductBookings[i], new string[] { "Id", "Booking", "Product", "PriceHT", "PriceTTC" }));
                Assert.IsNotNull(dto.ProductBookings[i].Product);
                Assert.AreEqual(0, search[0].ProductBookings[i].PriceHT);
                Assert.AreEqual(0, search[0].ProductBookings[i].PriceTTC);
            }
            for (int i = 0; i < dto.RoomBookings.Count; i++)
            {
                Assert.IsTrue(PropertiesComparison.PublicInstancePropertiesEqual<RoomBookingDTO>(
                    dto.RoomBookings[i], search[0].RoomBookings[i], new string[] { "Id", "Booking", "Room", "PriceHT", "PriceTTC", "NbNights", "PeopleBookings",
                    "SupplementRoomBookings"}));
                Assert.IsNotNull(search[0].RoomBookings[i].Room);
                Assert.AreNotEqual(0, search[0].RoomBookings[i].PeopleBookings);
                Assert.AreNotEqual(0, search[0].RoomBookings[i].SupplementRoomBookings);
                Assert.AreEqual(0, search[0].RoomBookings[i].PriceHT);
                Assert.AreEqual(0, search[0].RoomBookings[i].PriceTTC);
                Assert.AreEqual(0, search[0].RoomBookings[i].NbNights);
            }
        }

        [TestMethod]
        public void Put()
        {
            entity.Comment = "Put";
            entity.IsOnline = true;
            entity.DateDeparture.AddMonths(1);
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.OK, false, false);
            List<BookingDTO> search = reqCreator.CallSearch(st, "Booking/where/id/eq/" + entity.Id,
                new List<string>()
                {
                    "People",
                    "AdditionalBookings",
                    "AdditionalBookings.Tax",
                    "AdditionalBookings.BillItemCategory",
                    "BookingStepBooking",
                    "Deposits",
                    "DinnerBookings",
                    "DinnerBookings.MealBookings",
                    "DinnerBookings.MealBookings.PeopleCategory",
                    "ProductBookings.Product",
                    "ProductBookings",
                    "RoomBookings",
                    "RoomBookings.Room",
                    "RoomBookings.PeopleBookings",
                    "RoomBookings.SupplementRoomBookings"
                }, typeof(List<BookingDTO>));
            Assert.AreEqual(search[0].Comment, entity.Comment);
            for (int i = 0; i < search[0].AdditionalBookings.Count; i++)
            {
                Assert.IsTrue(PropertiesComparison.PublicInstancePropertiesEqual<AdditionalBookingDTO>(
                    search[0].AdditionalBookings[i], entity.AdditionalBookings[i], new string[] { "Id", "PriceTTC", "BillItemCategory", "Tax", "Booking" }));
                Assert.AreEqual(search[0].AdditionalBookings[i].PriceHT, entity.AdditionalBookings[i].PriceTTC);
                Assert.IsTrue(PropertiesComparison.PublicInstancePropertiesEqual<BillItemCategoryDTO>(
                    search[0].AdditionalBookings[i].BillItemCategory, entity.AdditionalBookings[i].BillItemCategory, new string[] { "Id" }));
                Assert.IsTrue(PropertiesComparison.PublicInstancePropertiesEqual<TaxDTO>(
                    search[0].AdditionalBookings[i].Tax, entity.AdditionalBookings[i].Tax, new string[] { "Id" }));
            }
            Assert.AreEqual(search[0].DateArrival.Date, entity.DateArrival.Date);
            Assert.IsNotNull(search[0].DateCreation);
            Assert.AreNotEqual(entity.DateCreation, search[0].DateCreation);
            Assert.AreEqual(search[0].DateDeparture.Date, entity.DateDeparture.Date);
            Assert.AreEqual(search[0].DateDesiredPayment, entity.DateDesiredPayment);
            Assert.IsNotNull(search[0].DateModification);
            Assert.AreEqual(search[0].DateValidation, entity.DateValidation);
            for (int i = 0; i < search[0].Deposits.Count; i++)
            {
                Assert.IsTrue(PropertiesComparison.PublicInstancePropertiesEqual<DepositDTO>(
                    search[0].Deposits[i], entity.Deposits[i], new string[] { "Id", "Booking", "DateCreation" }));
            }
            for (int i = 0; i < search[0].DinnerBookings.Count; i++)
            {
                Assert.IsTrue(PropertiesComparison.PublicInstancePropertiesEqual<DinnerBookingDTO>(
                    search[0].DinnerBookings[i], entity.DinnerBookings[i],
                    new string[] { "Id", "Booking", "MealBookings", "Date", "PriceHT", "PriceTTC" }));
                Assert.AreEqual(0, entity.DinnerBookings[i].PriceHT);
                Assert.AreEqual(0, entity.DinnerBookings[i].PriceTTC);
                Assert.AreEqual(((DateTime)entity.DinnerBookings[i].Date).Date, ((DateTime)search[0].DinnerBookings[i].Date).Date);
                for (int j = 0; j < search[0].DinnerBookings[i].MealBookings.Count; j++)
                {
                    Assert.IsTrue(PropertiesComparison.PublicInstancePropertiesEqual<MealBookingDTO>(
                        search[0].DinnerBookings[i].MealBookings[j], entity.DinnerBookings[i].MealBookings[j],
                        new string[] { "Id", "Meal", "PeopleCategory", "DinnerBooking" }));
                    Assert.IsTrue(PropertiesComparison.PublicInstancePropertiesEqual<PeopleCategoryDTO>(
                        search[0].DinnerBookings[i].MealBookings[j].PeopleCategory, entity.DinnerBookings[i].MealBookings[j].PeopleCategory,
                        new string[] { "Id", "Tax" }));
                }
            }
            Assert.AreEqual(search[0].HomeId, entity.HomeId);
            Assert.AreEqual(search[0].IsOnline, entity.IsOnline);
            Assert.AreEqual(search[0].IsSatisfactionSended, entity.IsSatisfactionSended);
            Assert.AreEqual(0, entity.TotalPeople);
            Assert.IsNotNull(entity.BookingStepBooking);
            Assert.IsTrue(PropertiesComparison.PublicInstancePropertiesEqual<PeopleDTO>(
                search[0].People, entity.People, new string[] { "Id", "DateCreation" }));
            for (int i = 0; i < search[0].ProductBookings.Count; i++)
            {
                Assert.IsTrue(PropertiesComparison.PublicInstancePropertiesEqual<ProductBookingDTO>(
                    search[0].ProductBookings[i], entity.ProductBookings[i], new string[] { "Id", "Booking", "Product", "PriceHT", "PriceTTC" }));
                Assert.IsNotNull(search[0].ProductBookings[i].Product);
                Assert.AreEqual(0, entity.ProductBookings[i].PriceHT);
                Assert.AreEqual(0, entity.ProductBookings[i].PriceTTC);
            }
            for (int i = 0; i < search[0].RoomBookings.Count; i++)
            {
                Assert.IsTrue(PropertiesComparison.PublicInstancePropertiesEqual<RoomBookingDTO>(
                    search[0].RoomBookings[i], entity.RoomBookings[i], new string[] { "Id", "Booking", "Room", "PriceHT", "PriceTTC", "NbNights", "PeopleBookings",
                    "SupplementRoomBookings"}));
                Assert.IsNotNull(entity.RoomBookings[i].Room);
                Assert.AreNotEqual(0, entity.RoomBookings[i].PeopleBookings);
                Assert.AreNotEqual(0, entity.RoomBookings[i].SupplementRoomBookings);
                Assert.AreEqual(0, entity.RoomBookings[i].PriceHT);
                Assert.AreEqual(0, entity.RoomBookings[i].PriceTTC);
                Assert.AreEqual(0, entity.RoomBookings[i].NbNights);
            }
        }

        [TestMethod]
        public void PostForbiddenResources()
        {
            entity.AdditionalBookings[0].Id = -1;
            entity.BookingStepBooking.Id = -1;
            entity.DinnerBookings[0].Id = -1;
            entity.People.Id = -1;
            entity.ProductBookings[0].Id = -1;
            entity.RoomBookings[0].Id = -1;
            reqCreator.CreateRequest(st, path, HttpMethod.Post, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("AdditionalBooking",
                new List<string>() { GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST }),
                new KeyValuePair<string, List<string>>("BookingStepBooking",
                new List<string>() { GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST }),
                new KeyValuePair<string, List<string>>("DinnerBooking",
                new List<string>() { GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST }),
                new KeyValuePair<string, List<string>>("People",
                new List<string>() { GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST }),
                new KeyValuePair<string, List<string>>("ProductBooking",
                new List<string>() { GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST }),
                new KeyValuePair<string, List<string>>("RoomBooking",
                new List<string>() { GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST })
            });
        }

        [TestMethod]
        public void PostInvalidHomeId()
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
        public void PostExceededNbNights()
        {
            entity.DateDeparture = DateTime.Now.AddYears(2);
            reqCreator.CreateRequest(st, path, HttpMethod.Post, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Dates",
                new List<string>() { GenericError.WRONG_DATA }),
            });
        }

        [TestMethod]
        public void PostNegativeNbNights()
        {
            entity.DateDeparture = DateTime.MinValue;
            reqCreator.CreateRequest(st, path, HttpMethod.Post, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Dates",
                new List<string>() { GenericError.WRONG_DATA }),
            });
        }

        [TestMethod]
        public void PostPeriodNotAllCovered()
        {
            entity.DateArrival = new DateTime(1970, 1, 1);
            entity.DateDeparture = new DateTime(1970, 5, 1);
            reqCreator.CreateRequest(st, path, HttpMethod.Post, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Period",
                new List<string>() { GenericError.DOES_NOT_MEET_REQUIREMENTS }),
            });
        }

        [TestMethod]
        public void PutPeriodNotAllCovered()
        {
            entity.DateArrival = new DateTime(1970, 1, 1);
            entity.DateDeparture = new DateTime(1970, 5, 1);
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Period",
                new List<string>() { GenericError.DOES_NOT_MEET_REQUIREMENTS }),
            });
        }

        [TestMethod]
        public void PutNegativeNbNights()
        {
            entity.DateDeparture = DateTime.MinValue;
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Dates",
                new List<string>() { GenericError.WRONG_DATA }),
            });
        }

        [TestMethod]
        public void PutExceededNbNights()
        {
            entity.DateDeparture = DateTime.Now.AddYears(2);
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Dates",
                new List<string>() { GenericError.WRONG_DATA }),
            });
        }

        [TestMethod]
        public void PutInvalidHomeId()
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
        public void PutForbiddenResources()
        {
            entity.AdditionalBookings[0].Id = -1;
            entity.BookingStepBooking.Id = -1;
            entity.DinnerBookings[0].Id = -1;
            entity.People.Id = -1;
            entity.ProductBookings[0].Id = -1;
            entity.RoomBookings[0].Id = -1;
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, entity, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("AdditionalBooking",
                new List<string>() { GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST }),
                new KeyValuePair<string, List<string>>("BookingStepBooking",
                new List<string>() { GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST }),
                new KeyValuePair<string, List<string>>("DinnerBooking",
                new List<string>() { GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST }),
                new KeyValuePair<string, List<string>>("People",
                new List<string>() { GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST }),
                new KeyValuePair<string, List<string>>("ProductBooking",
                new List<string>() { GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST }),
                new KeyValuePair<string, List<string>>("RoomBooking",
                new List<string>() { GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST })
            });
        }

        [TestMethod]
        public void PostNull()
        {
            reqCreator.CreateRequest(st, path, HttpMethod.Post, null, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>(TypeOfName.GetNameFromType<BookingDTO>(),
                new List<string>() { GenericError.CANNOT_BE_NULL_OR_EMPTY })
            });
        }

        [TestMethod]
        public void Delete()
        {
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Delete, null, HttpStatusCode.OK, false, false);
            List<BookingDTO> search = reqCreator.CallSearch(st, "Booking/where/id/eq/" + entity.Id,
                new List<string>() { }, typeof(List<BookingDTO>));
            Assert.AreEqual(0, search.Count);
        }

        [TestMethod]
        public void DeleteForbidden()
        {
            reqCreator.CreateRequest(st, path + "/-1", HttpMethod.Delete, null, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
        {
                new KeyValuePair<string, List<string>>(TypeOfName.GetNameFromType<BookingDTO>(),
                new List<string>() { GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST })
            });
        }

        [TestMethod]
        public void PutNull()
        {
            reqCreator.CreateRequest(st, path + "/" + entity.Id, HttpMethod.Put, null, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>(TypeOfName.GetNameFromType<BookingDTO>(),
                new List<string>() { GenericError.CANNOT_BE_NULL_OR_EMPTY })
            });
        }

        [TestMethod]
        public void Post()
        {
            entity.BookingStepBooking = new BookingStepBookingDTO()
            {
                BookingStepConfig = new BookingStepConfigDTO()
                {
                    Id = 1
                },
                HomeId = 1
            };
            reqCreator.CreateRequest(st, path, HttpMethod.Post, entity, HttpStatusCode.OK, true, false);
            List<BookingDTO> search = reqCreator.CallSearch(st, "Booking/where/id/eq/" + reqCreator.deserializedEntity.Id,
    new List<string>()
    {
                    "People",
                    "AdditionalBookings",
                    "AdditionalBookings.Tax",
                    "AdditionalBookings.BillItemCategory",
                    "BookingStepBooking",
                    "Deposits",
                    "DinnerBookings",
                    "DinnerBookings.MealBookings",
                    "DinnerBookings.MealBookings.PeopleCategory",
                    "ProductBookings.Product",
                    "ProductBookings",
                    "RoomBookings",
                    "RoomBookings.Room",
                    "RoomBookings.PeopleBookings",
                    "RoomBookings.SupplementRoomBookings"
    }, typeof(List<BookingDTO>));
            Assert.AreEqual(search[0].Comment, entity.Comment);
            for (int i = 0; i < search[0].AdditionalBookings.Count; i++)
            {
                Assert.IsTrue(PropertiesComparison.PublicInstancePropertiesEqual<AdditionalBookingDTO>(
                    search[0].AdditionalBookings[i], entity.AdditionalBookings[i], new string[] { "Id", "PriceTTC", "BillItemCategory", "Tax", "Booking" }));
                Assert.AreEqual(search[0].AdditionalBookings[i].PriceHT, entity.AdditionalBookings[i].PriceTTC);
                Assert.IsTrue(PropertiesComparison.PublicInstancePropertiesEqual<BillItemCategoryDTO>(
                    search[0].AdditionalBookings[i].BillItemCategory, entity.AdditionalBookings[i].BillItemCategory, new string[] { "Id" }));
                Assert.IsTrue(PropertiesComparison.PublicInstancePropertiesEqual<TaxDTO>(
                    search[0].AdditionalBookings[i].Tax, entity.AdditionalBookings[i].Tax, new string[] { "Id" }));
            }
            Assert.AreEqual(search[0].DateArrival.Date, entity.DateArrival.Date);
            Assert.IsNotNull(entity.DateCreation);
            Assert.AreNotEqual(entity.DateCreation, search[0].DateCreation);
            Assert.AreEqual(search[0].DateDeparture.Date, entity.DateDeparture.Date);
            Assert.AreEqual(search[0].DateDesiredPayment, entity.DateDesiredPayment);
            Assert.IsNull(entity.DateModification);
            Assert.AreEqual(search[0].DateValidation, entity.DateValidation);
            for (int i = 0; i < search[0].Deposits.Count; i++)
            {
                Assert.IsTrue(PropertiesComparison.PublicInstancePropertiesEqual<DepositDTO>(
                    search[0].Deposits[i], entity.Deposits[i], new string[] { "Id", "Booking", "DateCreation" }));
            }
            for (int i = 0; i < search[0].DinnerBookings.Count; i++)
            {
                Assert.IsTrue(PropertiesComparison.PublicInstancePropertiesEqual<DinnerBookingDTO>(
                    search[0].DinnerBookings[i], entity.DinnerBookings[i],
                    new string[] { "Id", "Booking", "MealBookings", "Date", "PriceHT", "PriceTTC" }));
                Assert.AreEqual(0, entity.DinnerBookings[i].PriceHT);
                Assert.AreEqual(0, entity.DinnerBookings[i].PriceTTC);
                Assert.AreEqual(((DateTime)entity.DinnerBookings[i].Date).Date, ((DateTime)search[0].DinnerBookings[i].Date).Date);
                for (int j = 0; j < search[0].DinnerBookings[i].MealBookings.Count; j++)
                {
                    Assert.IsTrue(PropertiesComparison.PublicInstancePropertiesEqual<MealBookingDTO>(
                        search[0].DinnerBookings[i].MealBookings[j], entity.DinnerBookings[i].MealBookings[j],
                        new string[] { "Id", "Meal", "PeopleCategory", "DinnerBooking" }));
                    Assert.IsTrue(PropertiesComparison.PublicInstancePropertiesEqual<PeopleCategoryDTO>(
                        search[0].DinnerBookings[i].MealBookings[j].PeopleCategory, entity.DinnerBookings[i].MealBookings[j].PeopleCategory,
                        new string[] { "Id", "Tax" }));
                }
            }
            Assert.AreEqual(search[0].HomeId, entity.HomeId);
            Assert.AreEqual(search[0].IsOnline, entity.IsOnline);
            Assert.AreEqual(search[0].IsSatisfactionSended, entity.IsSatisfactionSended);
            Assert.AreEqual(0, entity.TotalPeople);
            Assert.IsNotNull(entity.BookingStepBooking);
            Assert.IsTrue(PropertiesComparison.PublicInstancePropertiesEqual<PeopleDTO>(
                search[0].People, entity.People, new string[] { "Id", "DateCreation" }));
            for (int i = 0; i < search[0].ProductBookings.Count; i++)
            {
                Assert.IsTrue(PropertiesComparison.PublicInstancePropertiesEqual<ProductBookingDTO>(
                    search[0].ProductBookings[i], entity.ProductBookings[i], new string[] { "Id", "Booking", "Product", "PriceHT", "PriceTTC" }));
                Assert.IsNotNull(search[0].ProductBookings[i].Product);
                Assert.AreEqual(0, entity.ProductBookings[i].PriceHT);
                Assert.AreEqual(0, entity.ProductBookings[i].PriceTTC);
            }
            for (int i = 0; i < search[0].RoomBookings.Count; i++)
            {
                Assert.IsTrue(PropertiesComparison.PublicInstancePropertiesEqual<RoomBookingDTO>(
                    search[0].RoomBookings[i], entity.RoomBookings[i], new string[] { "Id", "Booking", "Room", "PriceHT", "PriceTTC", "NbNights", "PeopleBookings",
                    "SupplementRoomBookings"}));
                Assert.IsNotNull(entity.RoomBookings[i].Room);
                Assert.AreNotEqual(0, entity.RoomBookings[i].PeopleBookings);
                Assert.AreNotEqual(0, entity.RoomBookings[i].SupplementRoomBookings);
                Assert.AreEqual(0, entity.RoomBookings[i].PriceHT);
                Assert.AreEqual(0, entity.RoomBookings[i].PriceTTC);
                Assert.AreEqual(0, entity.RoomBookings[i].NbNights);
            }
        }

        [TestMethod]
        public void MailNull()
        {
            reqCreator.CreateRequest(st, path + "/Mail", HttpMethod.Post, null, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("MailBookingModel",
                new List<string>() { GenericError.CANNOT_BE_NULL_OR_EMPTY }),
            });
        }

        [TestMethod]
        public void MailForbiddenBooking()
        {
            RequestCreator<MailBookingModel> reqCreator = new RequestCreator<MailBookingModel>();

            reqCreator.CreateRequest(st, path + "/Mail", HttpMethod.Post, new MailBookingModel()
            {
                BookingId = -1,
                Password = "dGVzdA=="
            }, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("Booking",
                new List<string>() { GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST }),
            });
        }

        [TestMethod]
        public void MailNullMailTemplate()
        {
            RequestCreator<MailBookingModel> reqCreator = new RequestCreator<MailBookingModel>();

            reqCreator.CreateRequest(st, path + "/Mail", HttpMethod.Post, new MailBookingModel()
            {
                BookingId = 1,
                Password = "MWFhYWFhYWE="
            }, HttpStatusCode.BadRequest, false, false);
            reqCreator.AssertHttpError(1, new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>(String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<Booking>(), "MailTemplate"),
                new List<string>() { GenericError.CANNOT_BE_NULL_OR_EMPTY }),
            });
        }
    }
}