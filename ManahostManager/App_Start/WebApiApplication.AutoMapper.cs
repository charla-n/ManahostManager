using ManahostManager.Domain.DTOs;
using ManahostManager.Domain.Entity;
using ManahostManager.Utils.Mapper;
using Microsoft.Practices.Unity;

namespace ManahostManager.App_Start
{
    public partial class WebApiApplication
    {
        public static readonly int MAX_DEPTH = 2;

        private void SetupAutoMapper()
        {
            IMapper instance = container.Resolve<IMapper>();

            instance.Reset();
            instance.Register<Room, RoomDTO>()
                .MaxDepth(MAX_DEPTH);
            instance.Register<RoomDTO, Room>()
                .Ignore(p => p.Beds)
                .Ignore(p => p.RoomCategory)
                .Ignore(p => p.BillItemCategory)
                .Ignore(p => p.Documents)
                .MaxDepth(MAX_DEPTH);

            instance.Register<RoomCategory, RoomCategoryDTO>();
            instance.Register<RoomCategoryDTO, RoomCategory>();

            instance.Register<BillItemCategory, BillItemCategoryDTO>();
            instance.Register<BillItemCategoryDTO, BillItemCategory>();

            instance.Register<Bed, BedDTO>()
                .Before((src, dest) =>
                {
                    if (src.Room != null)
                        src.Room.Beds = null;
                })
                .MaxDepth(MAX_DEPTH);
            instance.Register<BedDTO, Bed>()
                .Ignore(p => p.Room)
                .MaxDepth(MAX_DEPTH);

            instance.Register<Tax, TaxDTO>();
            instance.Register<TaxDTO, Tax>();

            instance.Register<Product, ProductDTO>()
                .MaxDepth(MAX_DEPTH);
            instance.Register<ProductDTO, Product>()
                .Ignore(p => p.BillItemCategory)
                .Ignore(p => p.ProductCategory)
                .Ignore(p => p.Supplier)
                .Ignore(p => p.Documents)
                .Ignore(p => p.IsUnderThreshold)
                .MaxDepth(MAX_DEPTH);

            instance.Register<People, PeopleDTO>();
            instance.Register<PeopleDTO, People>()
                .Ignore(p => p.DateCreation);

            instance.Register<ProductCategory, ProductCategoryDTO>();
            instance.Register<ProductCategoryDTO, ProductCategory>();

            instance.Register<Supplier, SupplierDTO>();
            instance.Register<SupplierDTO, Supplier>()
                .Ignore(p => p.DateCreation);

            instance.Register<Booking, BookingDTO>()
                .MaxDepth(MAX_DEPTH);
            instance.Register<BookingDTO, Booking>()
                .Ignore(p => p.People)
                .Ignore(p => p.AdditionalBookings)
                .Ignore(p => p.BookingDocuments)
                .Ignore(p => p.BookingStepBooking)
                .Ignore(p => p.Deposits)
                .Ignore(p => p.DinnerBookings)
                .Ignore(p => p.ProductBookings)
                .Ignore(p => p.RoomBookings)
                .Ignore(p => p.DateCreation)
                .Ignore(p => p.TotalPeople)
                .MaxDepth(MAX_DEPTH);

            instance.Register<AdditionalBooking, AdditionalBookingDTO>()
                .MaxDepth(MAX_DEPTH);
            instance.Register<AdditionalBookingDTO, AdditionalBooking>()
                .Ignore(p => p.Tax)
                .Ignore(p => p.Booking)
                .Ignore(p => p.BillItemCategory)
                .MaxDepth(MAX_DEPTH);

            instance.Register<BookingDocument, BookingDocumentDTO>()
                .MaxDepth(MAX_DEPTH);
            instance.Register<BookingDocumentDTO, BookingDocument>()
                .Ignore(p => p.Booking)
                .Ignore(p => p.Document)
                .MaxDepth(MAX_DEPTH);

            instance.Register<BookingStepBooking, BookingStepBookingDTO>()
                .MaxDepth(MAX_DEPTH);
            instance.Register<BookingStepBookingDTO, BookingStepBooking>()
                .Ignore(p => p.Booking)
                .Ignore(p => p.BookingStepConfig)
                .Ignore(p => p.CurrentStep)
                .Ignore(p => p.MailLog)
                .Ignore(p => p.DateCurrentStepChanged)
                .Ignore(p => p.MailSent)
                .MaxDepth(MAX_DEPTH);

            instance.Register<Deposit, DepositDTO>()
                .MaxDepth(MAX_DEPTH);
            instance.Register<DepositDTO, Deposit>()
                .Ignore(p => p.Booking)
                .Ignore(p => p.DateCreation)
                .MaxDepth(MAX_DEPTH);

            instance.Register<DinnerBooking, DinnerBookingDTO>()
                .MaxDepth(MAX_DEPTH);
            instance.Register<DinnerBookingDTO, DinnerBooking>()
                .Ignore(p => p.Booking)
                .Ignore(p => p.MealBookings)
                .MaxDepth(MAX_DEPTH);

            instance.Register<ProductBooking, ProductBookingDTO>()
                .MaxDepth(MAX_DEPTH);
            instance.Register<ProductBookingDTO, ProductBooking>()
                .Ignore(p => p.Booking)
                .Ignore(p => p.Product)
                .Ignore(p => p.TimeSpanValue)
                .MaxDepth(MAX_DEPTH);

            instance.Register<RoomBooking, RoomBookingDTO>()
                .MaxDepth(MAX_DEPTH);
            instance.Register<RoomBookingDTO, RoomBooking>()
                .Ignore(p => p.Booking)
                .Ignore(p => p.Room)
                .Ignore(p => p.PeopleBookings)
                .Ignore(p => p.SupplementRoomBookings)
                .Ignore(p => p.NbNights)
                .MaxDepth(MAX_DEPTH);

            instance.Register<BookingStepConfig, BookingStepConfigDTO>()
                .MaxDepth(MAX_DEPTH);
            instance.Register<BookingStepConfigDTO, BookingStepConfig>()
                .Ignore(p => p.BookingSteps)
                .MaxDepth(MAX_DEPTH);

            instance.Register<Document, DocumentDTO>()
                .MaxDepth(MAX_DEPTH);
            instance.Register<DocumentDTO, Document>()
                .Ignore(p => p.DocumentCategory)
                .Ignore(p => p.SizeDocument)
                .MaxDepth(MAX_DEPTH);

            instance.Register<DocumentCategory, DocumentCategoryDTO>()
                .MaxDepth(MAX_DEPTH);
            instance.Register<DocumentCategoryDTO, DocumentCategory>()
                .MaxDepth(MAX_DEPTH);

            instance.Register<DocumentLog, DocumentLogDTO>()
                .MaxDepth(MAX_DEPTH);

            instance.Register<FieldGroup, FieldGroupDTO>()
                .MaxDepth(MAX_DEPTH);
            instance.Register<FieldGroupDTO, FieldGroup>()
                .Ignore(p => p.PeopleFields)
                .MaxDepth(MAX_DEPTH);

            instance.Register<GroupBillItem, GroupBillItemDTO>()
                .MaxDepth(MAX_DEPTH);
            instance.Register<GroupBillItemDTO, GroupBillItem>()
                .MaxDepth(MAX_DEPTH);

            instance.Register<Home, HomeDTO>()
                .Ignore(p => p.HomeId)
                .MaxDepth(MAX_DEPTH);
            instance.Register<HomeDTO, Home>()
                .Ignore(p => p.ClientId)
                .Ignore(p => p.EncryptionPassword)
                .MaxDepth(MAX_DEPTH);

            instance.Register<HomeConfig, HomeConfigDTO>()
                .Ignore(p => p.HomeId)
                .MaxDepth(MAX_DEPTH);
            instance.Register<HomeConfigDTO, HomeConfig>()
                .Ignore(p => p.DefaultMailConfig)
                .Ignore(p => p.BookingCanceledMailTemplate)
                .Ignore(p => p.DefaultBillItemCategoryMeal)
                .Ignore(p => p.DefaultBillItemCategoryProduct)
                .Ignore(p => p.DefaultBillItemCategoryRoom)
                .MaxDepth(MAX_DEPTH);

            instance.Register<KeyGenerator, KeyGeneratorDTO>()
                .MaxDepth(MAX_DEPTH);
            instance.Register<KeyGeneratorDTO, KeyGenerator>()
                .Ignore(p => p.Key)
                .Ignore(p => p.KeyType)
                .MaxDepth(MAX_DEPTH);

            instance.Register<MailConfig, MailConfigDTO>()
                .MaxDepth(MAX_DEPTH);
            instance.Register<MailConfigDTO, MailConfig>()
                .MaxDepth(MAX_DEPTH);

            instance.Register<MailLog, MailLogDTO>()
                .MaxDepth(MAX_DEPTH);
            instance.Register<MailLogDTO, MailLog>()
                .MaxDepth(MAX_DEPTH);

            instance.Register<Meal, MealDTO>()
                .MaxDepth(MAX_DEPTH);
            instance.Register<MealDTO, Meal>()
                .Ignore(p => p.BillItemCategory)
                .Ignore(p => p.Documents)
                .Ignore(p => p.MealCategory)
                .Ignore(p => p.MealPrices)
                .MaxDepth(MAX_DEPTH);

            instance.Register<MealBooking, MealBookingDTO>()
                .MaxDepth(MAX_DEPTH);
            instance.Register<MealBookingDTO, MealBooking>()
                .Ignore(p => p.DinnerBooking)
                .Ignore(p => p.Meal)
                .Ignore(p => p.PeopleCategory)
                .MaxDepth(MAX_DEPTH);

            instance.Register<MealCategory, MealCategoryDTO>()
                .MaxDepth(MAX_DEPTH);
            instance.Register<MealCategoryDTO, MealCategory>()
                .MaxDepth(MAX_DEPTH);

            instance.Register<MealPrice, MealPriceDTO>()
                .MaxDepth(MAX_DEPTH);
            instance.Register<MealPriceDTO, MealPrice>()
                .Ignore(p => p.Meal)
                .Ignore(p => p.PeopleCategory)
                .Ignore(p => p.Tax)
                .MaxDepth(MAX_DEPTH);

            instance.Register<PaymentMethod, PaymentMethodDTO>()
                .MaxDepth(MAX_DEPTH);
            instance.Register<PaymentMethodDTO, PaymentMethod>()
                .Ignore(p => p.Bill)
                .Ignore(p => p.PaymentType)
                .MaxDepth(MAX_DEPTH);

            instance.Register<Bill, BillDTO>()
                .MaxDepth(MAX_DEPTH);
            instance.Register<BillDTO, Bill>()
                .Ignore(p => p.BillItems)
                .Ignore(p => p.Booking)
                .Ignore(p => p.Document)
                .Ignore(p => p.PaymentMethods)
                .Ignore(p => p.Supplier)
                .Ignore(p => p.CreationDate)
                .MaxDepth(MAX_DEPTH);

            instance.Register<BillItem, BillItemDTO>()
                .MaxDepth(MAX_DEPTH);
            instance.Register<BillItemDTO, BillItem>()
                .Ignore(p => p.Bill)
                .Ignore(p => p.BillItemCategory)
                .Ignore(p => p.GroupBillItem)
                .MaxDepth(MAX_DEPTH);

            instance.Register<PaymentType, PaymentTypeDTO>()
                .MaxDepth(MAX_DEPTH);
            instance.Register<PaymentTypeDTO, PaymentType>()
                .MaxDepth(MAX_DEPTH);

            instance.Register<PeopleBooking, PeopleBookingDTO>()
                .MaxDepth(MAX_DEPTH);
            instance.Register<PeopleBookingDTO, PeopleBooking>()
                .Ignore(p => p.PeopleCategory)
                .Ignore(p => p.RoomBooking)
                .MaxDepth(MAX_DEPTH);

            instance.Register<PeopleCategory, PeopleCategoryDTO>()
                .MaxDepth(MAX_DEPTH);
            instance.Register<PeopleCategoryDTO, PeopleCategory>()
                .Ignore(p => p.Tax)
                .MaxDepth(MAX_DEPTH);

            instance.Register<PeopleField, PeopleFieldDTO>()
                .MaxDepth(MAX_DEPTH);
            instance.Register<PeopleFieldDTO, PeopleField>()
                .Ignore(p => p.FieldGroup)
                .Ignore(p => p.People)
                .MaxDepth(MAX_DEPTH);

            instance.Register<Period, PeriodDTO>()
                .MaxDepth(MAX_DEPTH);
            instance.Register<PeriodDTO, Period>()
                .MaxDepth(MAX_DEPTH);

            instance.Register<PricePerPerson, PricePerPersonDTO>()
                .MaxDepth(MAX_DEPTH);
            instance.Register<PricePerPersonDTO, PricePerPerson>()
                .Ignore(p => p.PeopleCategory)
                .Ignore(p => p.Period)
                .Ignore(p => p.Room)
                .Ignore(p => p.Tax)
                .MaxDepth(MAX_DEPTH);

            instance.Register<RoomSupplement, RoomSupplementDTO>()
                .MaxDepth(MAX_DEPTH);
            instance.Register<RoomSupplementDTO, RoomSupplement>()
                .Ignore(p => p.Tax)
                .MaxDepth(MAX_DEPTH);

            instance.Register<SatisfactionClient, SatisfactionClientDTO>()
                .MaxDepth(MAX_DEPTH);
            instance.Register<SatisfactionClientDTO, SatisfactionClient>()
                .Ignore(p => p.Booking)
                .Ignore(p => p.ClientDest)
                .Ignore(p => p.PeopleDest)
                .Ignore(p => p.SatisfactionClientAnswers)
                .MaxDepth(MAX_DEPTH);

            instance.Register<SatisfactionClientAnswer, SatisfactionClientAnswerDTO>()
                .MaxDepth(MAX_DEPTH);
            instance.Register<SatisfactionClientAnswerDTO, SatisfactionClientAnswer>()
                .Ignore(p => p.SatisfactionClient)
                .MaxDepth(MAX_DEPTH);

            instance.Register<SatisfactionConfig, SatisfactionConfigDTO>()
                .Ignore(p => p.HomeId)
                .MaxDepth(MAX_DEPTH);
            instance.Register<SatisfactionConfigDTO, SatisfactionConfig>()
                .Ignore(p => p.SatisfactionConfigQuestions)
                .MaxDepth(MAX_DEPTH);

            instance.Register<SatisfactionConfigQuestion, SatisfactionConfigQuestionDTO>()
                .MaxDepth(MAX_DEPTH);
            instance.Register<SatisfactionConfigQuestionDTO, SatisfactionConfigQuestion>()
                .Ignore(p => p.SatisfactionConfig)
                .MaxDepth(MAX_DEPTH);

            instance.Register<MStatistics, MStatisticsDTO>()
                .MaxDepth(MAX_DEPTH);
            instance.Register<MStatisticsDTO, MStatistics>()
                .MaxDepth(MAX_DEPTH);

            instance.Register<SupplementRoomBooking, SupplementRoomBookingDTO>()
                .MaxDepth(MAX_DEPTH);
            instance.Register<SupplementRoomBookingDTO, SupplementRoomBooking>()
                .Ignore(p => p.RoomBooking)
                .Ignore(p => p.RoomSupplement)
                .MaxDepth(MAX_DEPTH);

            instance.Register<Client, ClientDTO>()
                .MaxDepth(MAX_DEPTH);
            instance.Register<ClientDTO, Client>()
                .Ignore(p => p.PrincipalPhone)
                .Ignore(p => p.SecondaryPhone)
                .MaxDepth(MAX_DEPTH);

            instance.Register<PhoneNumber, PhoneNumberDTO>()
                .Ignore(p => p.HomeId)
                .MaxDepth(MAX_DEPTH);
            instance.Register<PhoneNumberDTO, PhoneNumber>()
                .MaxDepth(MAX_DEPTH);

            instance.Register<ResourceConfig, ResourceConfigDTO>()
                .Ignore(p => p.HomeId)
                .MaxDepth(MAX_DEPTH);
            instance.Register<ResourceConfigDTO, ResourceConfig>()
                .MaxDepth(MAX_DEPTH);

            instance.Register<BookingStep, BookingStepDTO>()
                .MaxDepth(MAX_DEPTH);
            instance.Register<BookingStepDTO, BookingStep>()
                .Ignore(p => p.BookingStepConfig)
                .Ignore(p => p.BookingStepNext)
                .Ignore(p => p.BookingStepPrevious)
                .Ignore(p => p.Documents)
                .Ignore(p => p.MailTemplate)
                .MaxDepth(MAX_DEPTH);

            instance.Compile();
        }
    }
}