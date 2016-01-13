using ManahostManager.Domain.Entity;
using ManahostManager.Domain.Tools;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;

namespace ManahostManager.Domain.DAL
{
    public class ManahostManagerDAL : IdentityDbContext<Client, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public ManahostManagerDAL()
            : base("ManahostEntities")
        {
            Configuration.LazyLoadingEnabled = false;
            /*
#if (PROD == false)
            Database.SetInitializer(new ManahostManagerInitializer());
#endif*/
        }

        /*        public ManahostManagerDAL(IDatabaseInitializer<ManahostManagerDAL> init)
            : base("ManahostEntities")
        {
            Configuration.LazyLoadingEnabled = false;
#if (PROD == false)
            Database.SetInitializer<ManahostManagerDAL>(init);
#endif
                }*/

        public static ManahostManagerDAL Create()
        {
            return new ManahostManagerDAL();
        }

        public DbSet<Test> TestSet { get; set; }

        public DbSet<Service> ServiceTest { get; set; }

        public DbSet<RefreshToken> RefreshToken { get; set; }

        public DbSet<PhoneNumber> PhoneNumberSet { get; set; }

        public DbSet<People> PeopleSet { get; set; }

        public DbSet<Home> HomeSet { get; set; }

        public DbSet<HomeConfig> HomeConfigSet { get; set; }

        public DbSet<Payment> PaymentSet { get; set; }

        public DbSet<Subscription> SubscriptionSet { get; set; }

        public DbSet<MailConfig> MailConfigSet { get; set; }

        public DbSet<Document> DocumentSet { get; set; }

        public DbSet<DocumentCategory> DocumentCategorySet { get; set; }

        public DbSet<Tax> TaxSet { get; set; }

        public DbSet<Supplier> SupplierSet { get; set; }

        public DbSet<RoomCategory> RoomCategorySet { get; set; }

        public DbSet<Room> RoomSet { get; set; }

        public DbSet<ProductCategory> ProductCategorySet { get; set; }

        public DbSet<Product> ProductSet { get; set; }

        public DbSet<Bed> BedSet { get; set; }

        public DbSet<FieldGroup> FieldGroupSet { get; set; }

        public DbSet<PeopleField> PeopleFieldSet { get; set; }

        public DbSet<Meal> MealSet { get; set; }

        public DbSet<MStatistics> StatisticsSet { get; set; }

        public DbSet<PricePerPerson> PricePerPersonSet { get; set; }

        public DbSet<Period> PeriodSet { get; set; }

        public DbSet<PeopleCategory> PeopleCategorySet { get; set; }

        public DbSet<PaymentType> PaymentTypeSet { get; set; }

        public DbSet<PaymentMethod> PaymentMethodSet { get; set; }

        public DbSet<MealPrice> MealPriceSet { get; set; }

        public DbSet<MealCategory> MealCategorySet { get; set; }

        public DbSet<KeyGenerator> KeyGeneratorSet { get; set; }

        public DbSet<GroupBillItem> GroupBillItemSet { get; set; }

        public DbSet<BillItemCategory> BillItemCategorySet { get; set; }

        public DbSet<BillItem> BillItemSet { get; set; }

        public DbSet<Bill> BillSet { get; set; }

        public DbSet<RoomSupplement> RoomSupplementSet { get; set; }

        public DbSet<BookingStepConfig> BookingStepConfigSet { get; set; }

        public DbSet<BookingStep> BookingStepSet { get; set; }

        public DbSet<Booking> BookingSet { get; set; }

        public DbSet<SatisfactionConfig> SatisfactionConfigSet { get; set; }

        public DbSet<SatisfactionConfigQuestion> SatisfactionConfigQuestionSet { get; set; }

        public DbSet<SatisfactionClient> SatisfactionClientSet { get; set; }

        public DbSet<SatisfactionClientAnswer> SatisfactionClientAnsweredSet { get; set; }

        public DbSet<AdditionalBooking> AdditionnalBookingSet { get; set; }

        public DbSet<BookingDocument> BookingDocumentSet { get; set; }

        public DbSet<Deposit> DepositSet { get; set; }

        public DbSet<DinnerBooking> DinnerBookingSet { get; set; }

        public DbSet<MealBooking> MealBookingSet { get; set; }

        public DbSet<ProductBooking> ProductBookingSet { get; set; }

        public DbSet<RoomBooking> RoomBookingSet { get; set; }

        public DbSet<PeopleBooking> PeopleBookingSet { get; set; }

        public DbSet<SupplementRoomBooking> SupplementRoomBookingSet { get; set; }

        public DbSet<BookingStepBooking> BookingStepBookingSet { get; set; }

        public DbSet<MailLog> MailLogSet { get; set; }

        public DbSet<ResourceConfig> ResourceConfigSet { get; set; }

        public DbSet<DocumentLog> DocumentLogSet { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<DateTime>().Configure(c => c.HasColumnType("datetime2"));

            #region RELATIONSHIPS

            modelBuilder.Entity<Document>().HasRequired(x => x.Home).WithMany().HasForeignKey(x => x.HomeId).WillCascadeOnDelete();
            modelBuilder.Entity<Document>().HasOptional(x => x.DocumentCategory).WithMany().WillCascadeOnDelete(false);

            modelBuilder.Entity<DocumentCategory>().HasRequired(x => x.Home).WithMany().HasForeignKey(x => x.HomeId).WillCascadeOnDelete();

            modelBuilder.Entity<Payment>().HasRequired(x => x.Subscription).WithMany(x => x.Payments).HasForeignKey(x => x.SubscriptionId).WillCascadeOnDelete();

            modelBuilder.Entity<Subscription>().HasRequired(x => x.Client).WithOptional().WillCascadeOnDelete();

            modelBuilder.Entity<People>().HasRequired(x => x.Home).WithMany().HasForeignKey(x => x.HomeId).WillCascadeOnDelete();

            modelBuilder.Entity<Home>().HasRequired(x => x.Client).WithMany().HasForeignKey(x => x.ClientId).WillCascadeOnDelete();

            modelBuilder.Entity<MailConfig>().HasRequired(x => x.Home).WithMany().HasForeignKey(x => x.HomeId).WillCascadeOnDelete();

            modelBuilder.Entity<HomeConfig>().HasRequired(x => x.Home).WithRequiredDependent().WillCascadeOnDelete();
            modelBuilder.Entity<HomeConfig>().HasOptional(x => x.BookingCanceledMailTemplate).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<HomeConfig>().HasOptional(x => x.DefaultMailConfig).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<HomeConfig>().HasOptional(x => x.DefaultBillItemCategoryMeal).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<HomeConfig>().HasOptional(x => x.DefaultBillItemCategoryProduct).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<HomeConfig>().HasOptional(x => x.DefaultBillItemCategoryRoom).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<HomeConfig>().HasOptional(x => x.BookingCanceledMailTemplate).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<HomeConfig>().HasOptional(x => x.DefaultMailConfig).WithMany().WillCascadeOnDelete(false);

            modelBuilder.Entity<MailLog>().HasRequired(x => x.Home).WithMany().HasForeignKey(x => x.HomeId).WillCascadeOnDelete();

            modelBuilder.Entity<Tax>().HasRequired(x => x.Home).WithMany().HasForeignKey(x => x.HomeId).WillCascadeOnDelete();

            modelBuilder.Entity<Supplier>().HasRequired(x => x.Home).WithMany().HasForeignKey(x => x.HomeId).WillCascadeOnDelete();

            modelBuilder.Entity<RoomCategory>().HasRequired(x => x.Home).WithMany().HasForeignKey(x => x.HomeId).WillCascadeOnDelete();

            modelBuilder.Entity<Room>().HasRequired(x => x.Home).WithMany().HasForeignKey(x => x.HomeId).WillCascadeOnDelete();
            modelBuilder.Entity<Room>().HasOptional(x => x.RoomCategory).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Room>().HasOptional(x => x.BillItemCategory).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Room>().HasMany(x => x.Beds).WithOptional(x => x.Room).WillCascadeOnDelete(false);
            modelBuilder.Entity<Room>().HasMany(x => x.Documents).WithOptional().WillCascadeOnDelete(false);

            modelBuilder.Entity<ProductCategory>().HasRequired(x => x.Home).WithMany().HasForeignKey(x => x.HomeId).WillCascadeOnDelete();

            modelBuilder.Entity<Product>().HasRequired(x => x.Home).WithMany().HasForeignKey(x => x.HomeId).WillCascadeOnDelete();
            modelBuilder.Entity<Product>().HasOptional(x => x.ProductCategory).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Product>().HasOptional(x => x.Tax).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Product>().HasOptional(x => x.Supplier).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Product>().HasOptional(x => x.BillItemCategory).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Product>().HasMany(x => x.Documents).WithOptional().WillCascadeOnDelete(false);

            modelBuilder.Entity<Bed>().HasRequired(x => x.Home).WithMany().HasForeignKey(x => x.HomeId).WillCascadeOnDelete();

            modelBuilder.Entity<FieldGroup>().HasRequired(x => x.Home).WithMany().HasForeignKey(x => x.HomeId).WillCascadeOnDelete();

            modelBuilder.Entity<PeopleField>().HasOptional(x => x.People).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<PeopleField>().HasOptional(x => x.FieldGroup).WithMany(x => x.PeopleFields).WillCascadeOnDelete();
            modelBuilder.Entity<PeopleField>().HasRequired(x => x.Home).WithMany().HasForeignKey(x => x.HomeId).WillCascadeOnDelete(false);

            modelBuilder.Entity<Meal>().HasOptional(x => x.MealCategory).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Meal>().HasRequired(x => x.Home).WithMany().HasForeignKey(x => x.HomeId).WillCascadeOnDelete();
            modelBuilder.Entity<Meal>().HasOptional(x => x.BillItemCategory).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Meal>().HasMany(x => x.Documents).WithOptional().WillCascadeOnDelete(false);

            modelBuilder.Entity<MStatistics>().HasRequired(x => x.Home).WithMany().HasForeignKey(x => x.HomeId).WillCascadeOnDelete();

            modelBuilder.Entity<PricePerPerson>().HasRequired(x => x.Home).WithMany().HasForeignKey(x => x.HomeId).WillCascadeOnDelete();
            modelBuilder.Entity<PricePerPerson>().HasOptional(x => x.Tax).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<PricePerPerson>().HasRequired(x => x.Room).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<PricePerPerson>().HasRequired(x => x.Period).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<PricePerPerson>().HasOptional(x => x.PeopleCategory).WithMany().WillCascadeOnDelete(false);

            modelBuilder.Entity<Period>().HasRequired(x => x.Home).WithMany().HasForeignKey(x => x.HomeId).WillCascadeOnDelete();

            modelBuilder.Entity<PeopleCategory>().HasRequired(x => x.Home).WithMany().HasForeignKey(x => x.HomeId).WillCascadeOnDelete();
            modelBuilder.Entity<PeopleCategory>().HasOptional(x => x.Tax).WithMany().WillCascadeOnDelete(false);

            modelBuilder.Entity<PaymentMethod>().HasRequired(x => x.PaymentType).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<PaymentMethod>().HasRequired(x => x.Bill).WithMany(x => x.PaymentMethods).WillCascadeOnDelete();
            modelBuilder.Entity<PaymentMethod>().HasRequired(x => x.Home).WithMany().HasForeignKey(x => x.HomeId).WillCascadeOnDelete(false);

            modelBuilder.Entity<MealPrice>().HasOptional(x => x.Tax).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<MealPrice>().HasRequired(x => x.Meal).WithMany(x => x.MealPrices).WillCascadeOnDelete();
            modelBuilder.Entity<MealPrice>().HasOptional(x => x.PeopleCategory).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<MealPrice>().HasRequired(x => x.Home).WithMany().HasForeignKey(x => x.HomeId).WillCascadeOnDelete(false);

            modelBuilder.Entity<MealCategory>().HasRequired(x => x.Home).WithMany().HasForeignKey(x => x.HomeId).WillCascadeOnDelete();

            modelBuilder.Entity<KeyGenerator>().HasOptional(x => x.Home).WithMany().HasForeignKey(x => x.HomeId).WillCascadeOnDelete();

            modelBuilder.Entity<BillItemCategory>().HasRequired(x => x.Home).WithMany().HasForeignKey(x => x.HomeId).WillCascadeOnDelete();

            modelBuilder.Entity<GroupBillItem>().HasRequired(x => x.Home).WithMany().HasForeignKey(x => x.HomeId).WillCascadeOnDelete();

            modelBuilder.Entity<BillItem>().HasRequired(x => x.Bill).WithMany(x => x.BillItems).WillCascadeOnDelete();
            modelBuilder.Entity<BillItem>().HasOptional(x => x.BillItemCategory).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<BillItem>().HasOptional(x => x.GroupBillItem).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<BillItem>().HasRequired(x => x.Home).WithMany().HasForeignKey(x => x.HomeId).WillCascadeOnDelete(false);

            modelBuilder.Entity<Bill>().HasRequired(x => x.Home).WithMany().HasForeignKey(x => x.HomeId).WillCascadeOnDelete();
            modelBuilder.Entity<Bill>().HasOptional(x => x.Supplier).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Bill>().HasOptional(x => x.Booking).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Bill>().HasOptional(x => x.Document).WithMany().WillCascadeOnDelete(false);

            modelBuilder.Entity<RoomSupplement>().HasRequired(x => x.Home).WithMany().HasForeignKey(x => x.HomeId).WillCascadeOnDelete();
            modelBuilder.Entity<RoomSupplement>().HasOptional(x => x.Tax).WithMany().WillCascadeOnDelete(false);

            modelBuilder.Entity<BookingStepConfig>().HasRequired(x => x.Home).WithMany().HasForeignKey(x => x.HomeId).WillCascadeOnDelete();

            modelBuilder.Entity<BookingStep>().HasMany(x => x.Documents).WithOptional().WillCascadeOnDelete(false);
            modelBuilder.Entity<BookingStep>().HasRequired(x => x.BookingStepConfig).WithMany(x => x.BookingSteps).WillCascadeOnDelete();
            modelBuilder.Entity<BookingStep>().HasOptional(x => x.BookingStepNext).WithMany().HasForeignKey(x => x.BookingStepIdNext).WillCascadeOnDelete(false);
            modelBuilder.Entity<BookingStep>().HasOptional(x => x.BookingStepPrevious).WithMany().HasForeignKey(x => x.BookingStepIdPrevious).WillCascadeOnDelete(false);
            modelBuilder.Entity<BookingStep>().HasRequired(x => x.Home).WithMany().HasForeignKey(x => x.HomeId).WillCascadeOnDelete(false);

            modelBuilder.Entity<Booking>().HasRequired(x => x.Home).WithMany().HasForeignKey(x => x.HomeId).WillCascadeOnDelete();
            modelBuilder.Entity<Booking>().HasRequired(x => x.People).WithMany().WillCascadeOnDelete(false);

            modelBuilder.Entity<BookingStepBooking>().HasRequired(x => x.Booking).WithRequiredDependent(x => x.BookingStepBooking).WillCascadeOnDelete();
            modelBuilder.Entity<BookingStepBooking>().HasRequired(x => x.Home).WithMany().HasForeignKey(x => x.HomeId).WillCascadeOnDelete(false);
            modelBuilder.Entity<BookingStepBooking>().HasRequired(x => x.BookingStepConfig).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<BookingStepBooking>().HasOptional(x => x.CurrentStep).WithMany().WillCascadeOnDelete(false);

            modelBuilder.Entity<SatisfactionConfig>().HasRequired(x => x.Home).WithRequiredDependent().WillCascadeOnDelete();

            modelBuilder.Entity<SatisfactionConfigQuestion>().HasRequired(x => x.SatisfactionConfig).WithMany(x => x.SatisfactionConfigQuestions).WillCascadeOnDelete();
            modelBuilder.Entity<SatisfactionConfigQuestion>().HasRequired(x => x.Home).WithMany().HasForeignKey(x => x.HomeId).WillCascadeOnDelete(false);

            modelBuilder.Entity<SatisfactionClient>().HasOptional(x => x.Booking).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<SatisfactionClient>().HasRequired(x => x.Home).WithMany().HasForeignKey(x => x.HomeId).WillCascadeOnDelete();
            modelBuilder.Entity<SatisfactionClient>().HasOptional(x => x.ClientDest).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<SatisfactionClient>().HasOptional(x => x.PeopleDest).WithMany().WillCascadeOnDelete(false);

            modelBuilder.Entity<SatisfactionClientAnswer>().HasRequired(x => x.SatisfactionClient).WithMany(x => x.SatisfactionClientAnswers).WillCascadeOnDelete();
            modelBuilder.Entity<SatisfactionClientAnswer>().HasRequired(x => x.Home).WithMany().HasForeignKey(x => x.HomeId).WillCascadeOnDelete(false);

            modelBuilder.Entity<AdditionalBooking>().HasRequired(x => x.Booking).WithMany(x => x.AdditionalBookings).WillCascadeOnDelete();
            modelBuilder.Entity<AdditionalBooking>().HasOptional(x => x.BillItemCategory).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<AdditionalBooking>().HasOptional(x => x.Tax).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<AdditionalBooking>().HasRequired(x => x.Home).WithMany().HasForeignKey(x => x.HomeId).WillCascadeOnDelete(false);

            //TODO: Prévenir lors de la destruction d'un document/meal/room/... que cela détruira aussi les documentbooking/mealbooking/roombooking/... donc demander au gérant si il ne préfère pas cacher les éléments plutôt que de les supprimer

            modelBuilder.Entity<BookingDocument>().HasRequired(x => x.Booking).WithMany(x => x.BookingDocuments).WillCascadeOnDelete();
            modelBuilder.Entity<BookingDocument>().HasRequired(x => x.Document).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<BookingDocument>().HasRequired(x => x.Home).WithMany().HasForeignKey(x => x.HomeId).WillCascadeOnDelete(false);

            modelBuilder.Entity<Deposit>().HasRequired(x => x.Booking).WithMany(x => x.Deposits).WillCascadeOnDelete();
            modelBuilder.Entity<Deposit>().HasRequired(x => x.Home).WithMany().HasForeignKey(x => x.HomeId).WillCascadeOnDelete(false);

            modelBuilder.Entity<DinnerBooking>().HasRequired(x => x.Booking).WithMany(x => x.DinnerBookings).WillCascadeOnDelete();
            modelBuilder.Entity<DinnerBooking>().HasRequired(x => x.Home).WithMany().HasForeignKey(x => x.HomeId).WillCascadeOnDelete(false);

            modelBuilder.Entity<MealBooking>().HasRequired(x => x.DinnerBooking).WithMany(x => x.MealBookings).WillCascadeOnDelete();
            modelBuilder.Entity<MealBooking>().HasRequired(x => x.Meal).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<MealBooking>().HasRequired(x => x.Home).WithMany().HasForeignKey(x => x.HomeId).WillCascadeOnDelete(false);
            modelBuilder.Entity<MealBooking>().HasOptional(x => x.PeopleCategory).WithMany().WillCascadeOnDelete(false);

            modelBuilder.Entity<ProductBooking>().HasRequired(x => x.Booking).WithMany(x => x.ProductBookings).WillCascadeOnDelete();
            modelBuilder.Entity<ProductBooking>().HasRequired(x => x.Product).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<ProductBooking>().HasRequired(x => x.Home).WithMany().HasForeignKey(x => x.HomeId).WillCascadeOnDelete(false);

            modelBuilder.Entity<RoomBooking>().HasRequired(x => x.Booking).WithMany(x => x.RoomBookings).WillCascadeOnDelete();
            modelBuilder.Entity<RoomBooking>().HasRequired(x => x.Room).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<RoomBooking>().HasRequired(x => x.Home).WithMany().HasForeignKey(x => x.HomeId).WillCascadeOnDelete(false);

            modelBuilder.Entity<PeopleBooking>().HasRequired(x => x.RoomBooking).WithMany(x => x.PeopleBookings).WillCascadeOnDelete();
            modelBuilder.Entity<PeopleBooking>().HasRequired(x => x.PeopleCategory).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<PeopleBooking>().HasRequired(x => x.Home).WithMany().HasForeignKey(x => x.HomeId).WillCascadeOnDelete(false);

            modelBuilder.Entity<SupplementRoomBooking>().HasRequired(x => x.RoomBooking).WithMany(x => x.SupplementRoomBookings).WillCascadeOnDelete();
            modelBuilder.Entity<SupplementRoomBooking>().HasRequired(x => x.RoomSupplement).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<SupplementRoomBooking>().HasRequired(x => x.Home).WithMany().HasForeignKey(x => x.HomeId).WillCascadeOnDelete(false);

            modelBuilder.Entity<PaymentType>().HasRequired(x => x.Home).WithMany().HasForeignKey(x => x.HomeId).WillCascadeOnDelete();

            modelBuilder.Entity<DocumentLog>().HasRequired(x => x.Client).WithRequiredDependent().WillCascadeOnDelete();
            modelBuilder.Entity<DocumentLog>().HasRequired(x => x.ResourceConfig).WithMany().WillCascadeOnDelete(false);

            modelBuilder.Entity<RefreshToken>().HasRequired(x => x.Client).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<RefreshToken>().HasRequired(x => x.Service).WithMany().WillCascadeOnDelete(false);

            #endregion RELATIONSHIPS

            base.OnModelCreating(modelBuilder);
        }
    }
}