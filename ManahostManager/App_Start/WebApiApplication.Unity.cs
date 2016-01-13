using ManahostManager.Domain.DAL;
using ManahostManager.Domain.DTOs;
using ManahostManager.Domain.Entity;
using ManahostManager.Domain.Repository;
using ManahostManager.Domain.Tools;
using ManahostManager.Model;
using ManahostManager.Services;
using ManahostManager.Utils;
using ManahostManager.Utils.API.Localisation.Country;
using ManahostManager.Utils.CaptchaUtils;
using ManahostManager.Utils.Mapper;
using ManahostManager.Validation;
using Microsoft.AspNet.Identity;
using Microsoft.Practices.Unity;
using System.Web.Http.Dependencies;

namespace ManahostManager.App_Start
{
    public partial class WebApiApplication
    {
        private static IUnityContainer SetupUnity()
        {
            IUnityContainer container = new UnityContainer();
            // IoC

            //DB IOC
            container.RegisterType<ManahostManagerDAL>(new HierarchicalLifetimeManager());

            container.RegisterType<UnityRegisterScope>();

            //Identity IOC
            container.RegisterType<IUserStore<Client, int>, CustomUserStore>(new HierarchicalLifetimeManager());
            container.RegisterType<IRoleStore<CustomRole, int>, CustomRoleStore>(new HierarchicalLifetimeManager());
            container.RegisterType<ClientUserManager>(new HierarchicalLifetimeManager());
            container.RegisterType<ClientRoleManager>(new HierarchicalLifetimeManager());

            container.RegisterType<ICaptcha, reCAPTCHA>();

            container.RegisterType<IMapper, ManahostMapster>(new ContainerControlledLifetimeManager());
            container.RegisterType<APICountryAutoComplete>();

            container.RegisterType<IClientRepository, ClientRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IHomeRepository, HomeRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ISearchRepository, SearchRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IRoomCategoryRepository, RoomCategoryRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IBedRepository, BedRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IPeopleRepository, PeopleRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IRoomRepository, RoomRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IBillItemCategoryRepository, BillItemCategoryRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IHomeConfigRepository, HomeConfigRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IMailConfigRepository, MailConfigRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ITaxRepository, TaxRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IProductRepository, ProductRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IProductCategoryRepository, ProductCategoryRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ISupplierRepository, SupplierRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IMealRepository, MealRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IMealCategoryRepository, MealCategoryRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IMealPriceRepository, MealPriceRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IRoomSupplementRepository, RoomSupplementRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IPaymentTypeRepository, PaymentTypeRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IBookingStepConfigRepository, BookingStepConfigRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IBookingStepRepository, BookingStepRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IBookingRepository, BookingRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IBookingStepBookingRepository, BookingStepBookingRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IDepositRepository, DepositRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IAdditionalBookingRepository, AdditionalBookingRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IProductBookingRepository, ProductBookingRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IMailLogRepository, MailLogRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IRoomBookingRepository, RoomBookingRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IDocumentRepository, DocumentRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IDocumentCategoryRepository, DocumentCategoryRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IPeriodRepository, PeriodRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ISupplementRoomBookingRepository, SupplementRoomBookingRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IPeopleBookingRepository, PeopleBookingRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IPricePerPersonRepository, PricePerPersonRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IPeopleCategoryRepository, PeopleCategoryRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IDinnerBookingRepository, DinnerBookingRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IMealBookingRepository, MealBookingRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ISatisfactionConfigRepository, SatisfactionConfigRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ISatisfactionConfigQuestionRepository, SatisfactionConfigQuestionRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IKeyGeneratorRepository, KeyGeneratorRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IFieldGroupRepository, FieldGroupRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IPeopleFieldRepository, PeopleFieldRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhoneRepository, PhoneRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IBillItemRepository, BillItemRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IBillRepository, BillRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IRefreshTokenRepository, RefreshTokenRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IGroupBillItemRepository, GroupBillItemRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IPaymentMethodRepository, PaymentMethodRepository>(new HierarchicalLifetimeManager());

            // Service Interceptor

            container.RegisterType<ISearchService, SearchService>();
            container.RegisterType<IAdvancedSearch, AdvancedSearch>();
            container.RegisterType<BookingStepBookingService>();
            container.RegisterType<AdditionalBookingService>();
            container.RegisterType<ProductBookingService>();
            container.RegisterType<RoomBookingService>();
            container.RegisterType<SupplementRoomBookingService>();
            container.RegisterType<MailService>();
            container.RegisterType<HomeService>();
            container.RegisterType<IAbstractService<RoomCategory, RoomCategoryDTO>, RoomCategoryService>();
            container.RegisterType<IAbstractService<Bed, BedDTO>, BedService>();
            container.RegisterType<IAbstractService<Home, HomeDTO>, HomeService>();
            container.RegisterType<IAbstractService<People, PeopleDTO>, PeopleService>();
            container.RegisterType<IAbstractService<Room, RoomDTO>, RoomService>();
            container.RegisterType<IAbstractService<HomeConfig, HomeConfigDTO>, HomeConfigService>();
            container.RegisterType<IAbstractService<MailConfig, MailConfigDTO>, MailConfigService>();
            container.RegisterType<IAbstractService<Tax, TaxDTO>, TaxService>();
            container.RegisterType<IAbstractService<Product, ProductDTO>, ProductService>();
            container.RegisterType<IAbstractService<ProductCategory, ProductCategoryDTO>, ProductCategoryService>();
            container.RegisterType<IAbstractService<Supplier, SupplierDTO>, SupplierService>();
            container.RegisterType<IAbstractService<RoomSupplement, RoomSupplementDTO>, RoomSupplementService>();
            container.RegisterType<IAbstractService<PaymentType, PaymentTypeDTO>, PaymentTypeService>();
            container.RegisterType<IAbstractService<BookingStep, BookingStepDTO>, BookingStepService>();
            container.RegisterType<IAbstractService<BookingStepConfig, BookingStepConfigDTO>, BookingStepConfigService>();
            container.RegisterType<IAbstractService<Meal, MealDTO>, MealService>();
            container.RegisterType<IAbstractService<MealCategory, MealCategoryDTO>, MealCategoryService>();
            container.RegisterType<IAbstractService<MealPrice, MealPriceDTO>, MealPriceService>();
            container.RegisterType<IAbstractService<Booking, BookingDTO>, BookingService>();
            container.RegisterType<IAbstractService<BookingStepBooking, BookingStepBookingDTO>, BookingStepBookingService>();
            container.RegisterType<IAbstractService<Deposit, DepositDTO>, DepositService>();
            container.RegisterType<IAbstractService<AdditionalBooking, AdditionalBookingDTO>, AdditionalBookingService>();
            container.RegisterType<IAbstractService<ProductBooking, ProductBookingDTO>, ProductBookingService>();
            container.RegisterType<IAdditionnalDocumentMethod, DocumentService>();
            container.RegisterType<IAbstractService<DocumentCategory, DocumentCategoryDTO>, DocumentCategoryService>();
            container.RegisterType<IAbstractService<RoomBooking, RoomBookingDTO>, RoomBookingService>();
            container.RegisterType<IAbstractService<SupplementRoomBooking, SupplementRoomBookingDTO>, SupplementRoomBookingService>();
            container.RegisterType<IAbstractService<PeopleBooking, PeopleBookingDTO>, PeopleBookingService>();
            container.RegisterType<IAbstractService<PricePerPerson, PricePerPersonDTO>, PricePerPersonService>();
            container.RegisterType<IAbstractService<PeopleCategory, PeopleCategoryDTO>, PeopleCategoryService>();
            container.RegisterType<IAbstractService<Period, PeriodDTO>, PeriodService>();
            container.RegisterType<IAbstractService<DinnerBooking, DinnerBookingDTO>, DinnerBookingService>();
            container.RegisterType<IAbstractService<MealBooking, MealBookingDTO>, MealBookingService>();
            container.RegisterType<IAbstractService<KeyGenerator, KeyGeneratorDTO>, KeyGeneratorService>();
            container.RegisterType<IAbstractService<SatisfactionConfig, SatisfactionConfigDTO>, SatisfactionConfigService>();
            container.RegisterType<IAbstractService<SatisfactionConfigQuestion, SatisfactionConfigQuestionDTO>, SatisfactionConfigQuestionService>();
            container.RegisterType<IAbstractService<FieldGroup, FieldGroupDTO>, FieldGroupService>();
            container.RegisterType<IAbstractService<PeopleField, PeopleFieldDTO>, PeopleFieldService>();
            container.RegisterType<IAbstractService<BillItem, BillItemDTO>, BillItemService>();
            container.RegisterType<IAbstractService<Bill, BillDTO>, BillService>();
            container.RegisterType<IAbstractService<BillItemCategory, BillItemCategoryDTO>, BillItemCategoryService>();
            container.RegisterType<IAbstractService<GroupBillItem, GroupBillItemDTO>, GroupBillItemService>();
            container.RegisterType<IAbstractService<PaymentMethod, PaymentMethodDTO>, PaymentMethodService>();

            // Validation register

            container.RegisterType<IValidation<BillItemCategory>, AbstractValidation<BillItemCategory>>();
            container.RegisterType<IValidation<Bed>, AbstractValidation<Bed>>();
            container.RegisterType<IValidation<Home>, AbstractValidation<Home>>();
            container.RegisterType<IValidation<People>, AbstractValidation<People>>();
            container.RegisterType<IValidation<RoomCategory>, AbstractValidation<RoomCategory>>();
            container.RegisterType<IValidation<Room>, AbstractValidation<Room>>();
            container.RegisterType<IValidation<HomeConfig>, HomeConfigValidation>();
            container.RegisterType<IValidation<MailConfig>, AbstractValidation<MailConfig>>();
            container.RegisterType<IValidation<Tax>, AbstractValidation<Tax>>();
            container.RegisterType<IValidation<Product>, AbstractValidation<Product>>();
            container.RegisterType<IValidation<ProductCategory>, AbstractValidation<ProductCategory>>();
            container.RegisterType<IValidation<Supplier>, AbstractValidation<Supplier>>();
            container.RegisterType<IValidation<RoomSupplement>, AbstractValidation<RoomSupplement>>();
            container.RegisterType<IValidation<PaymentType>, AbstractValidation<PaymentType>>();
            container.RegisterType<IValidation<MailModel>, MailValidation>();
            container.RegisterType<IValidation<BookingStep>, BookingStepValidation>();
            container.RegisterType<IValidation<BookingStepConfig>, AbstractValidation<BookingStepConfig>>();
            container.RegisterType<IValidation<Meal>, AbstractValidation<Meal>>();
            container.RegisterType<IValidation<MealCategory>, AbstractValidation<MealCategory>>();
            container.RegisterType<IValidation<MealPrice>, AbstractValidation<MealPrice>>();
            container.RegisterType<IValidation<Booking>, BookingValidation>();
            container.RegisterType<IValidation<BookingStepBooking>, BookingStepBookingValidation>();
            container.RegisterType<IValidation<Deposit>, AbstractValidation<Deposit>>();
            container.RegisterType<IValidation<AdditionalBooking>, AbstractValidation<AdditionalBooking>>();
            container.RegisterType<IValidation<ProductBooking>, ProductBookingValidation>();
            container.RegisterType<IValidation<RoomBooking>, RoomBookingValidation>();
            container.RegisterType<IValidation<Document>, DocumentValidation>();
            container.RegisterType<IValidation<DocumentCategory>, AbstractValidation<DocumentCategory>>();
            container.RegisterType<IValidation<SupplementRoomBooking>, AbstractValidation<SupplementRoomBooking>>();
            container.RegisterType<IValidation<PeopleBooking>, PeopleBookingValidation>();
            container.RegisterType<IValidation<PricePerPerson>, PricePerPersonValidation>();
            container.RegisterType<IValidation<Period>, PeriodValidation>();
            container.RegisterType<IValidation<PeopleCategory>, AbstractValidation<PeopleCategory>>();
            container.RegisterType<IValidation<DinnerBooking>, AbstractValidation<DinnerBooking>>();
            container.RegisterType<IValidation<MealBooking>, AbstractValidation<MealBooking>>();
            container.RegisterType<IValidation<KeyGenerator>, KeyGeneratorValidation>();
            container.RegisterType<IValidation<SatisfactionConfig>, AbstractValidation<SatisfactionConfig>>();
            container.RegisterType<IValidation<SatisfactionConfigQuestion>, AbstractValidation<SatisfactionConfigQuestion>>();
            container.RegisterType<IValidation<FieldGroup>, AbstractValidation<FieldGroup>>();
            container.RegisterType<IValidation<PeopleField>, PeopleFieldValidation>();
            container.RegisterType<IValidation<BillItem>, AbstractValidation<BillItem>>();
            container.RegisterType<IValidation<Bill>, AbstractValidation<Bill>>();
            container.RegisterType<IValidation<GroupBillItem>, AbstractValidation<GroupBillItem>>();
            container.RegisterType<IValidation<PaymentMethod>, AbstractValidation<PaymentMethod>>();

            return container;
        }
    }
}