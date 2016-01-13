namespace ManahostManager.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigrations : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdditionalBooking",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        PriceHT = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PriceTTC = c.Decimal(precision: 18, scale: 2),
                        HomeId = c.Int(nullable: false),
                        DateModification = c.DateTime(precision: 7, storeType: "datetime2"),
                        BillItemCategory_Id = c.Int(),
                        Booking_Id = c.Int(nullable: false),
                        Tax_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BillItemCategory", t => t.BillItemCategory_Id)
                .ForeignKey("dbo.Booking", t => t.Booking_Id, cascadeDelete: true)
                .ForeignKey("dbo.Home", t => t.HomeId)
                .ForeignKey("dbo.Tax", t => t.Tax_Id)
                .Index(t => t.HomeId)
                .Index(t => t.BillItemCategory_Id)
                .Index(t => t.Booking_Id)
                .Index(t => t.Tax_Id);
            
            CreateTable(
                "dbo.BillItemCategory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HomeId = c.Int(nullable: false),
                        Title = c.String(),
                        DateModification = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Home", t => t.HomeId, cascadeDelete: true)
                .Index(t => t.HomeId);
            
            CreateTable(
                "dbo.Home",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EstablishmentType = c.Int(nullable: false),
                        Title = c.String(),
                        ClientId = c.Int(nullable: false),
                        EncryptionPassword = c.String(),
                        Address = c.String(),
                        DateModification = c.DateTime(precision: 7, storeType: "datetime2"),
                        isDefault = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ClientId, cascadeDelete: true)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DefaultHomeId = c.Int(),
                        UserName = c.String(nullable: false, maxLength: 256),
                        PhoneNumber = c.String(),
                        IsManager = c.Boolean(nullable: false),
                        AcceptMailing = c.Boolean(nullable: false),
                        Civility = c.String(),
                        Country = c.String(),
                        DateBirth = c.DateTime(precision: 7, storeType: "datetime2"),
                        DateCreation = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Locale = c.String(),
                        Timezone = c.Double(nullable: false),
                        DateModification = c.DateTime(precision: 7, storeType: "datetime2"),
                        InitManager = c.Boolean(nullable: false),
                        TutorialManager = c.Boolean(nullable: false),
                        LastAttemptConnexion = c.DateTime(precision: 7, storeType: "datetime2"),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(precision: 7, storeType: "datetime2"),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        PrincipalPhone_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PhoneNumbers", t => t.PrincipalPhone_Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.PrincipalPhone_Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.PhoneNumbers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PhoneType = c.Int(nullable: false),
                        Phone = c.String(nullable: false),
                        Client_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Client_Id)
                .Index(t => t.Client_Id);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Booking",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Comment = c.String(),
                        DateArrival = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DateCreation = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DateDeparture = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DateDesiredPayment = c.DateTime(precision: 7, storeType: "datetime2"),
                        DateModification = c.DateTime(precision: 7, storeType: "datetime2"),
                        DateValidation = c.DateTime(precision: 7, storeType: "datetime2"),
                        HomeId = c.Int(nullable: false),
                        IsOnline = c.Boolean(nullable: false),
                        IsSatisfactionSended = c.Boolean(nullable: false),
                        TotalPeople = c.Int(nullable: false),
                        People_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Home", t => t.HomeId, cascadeDelete: true)
                .ForeignKey("dbo.People", t => t.People_Id)
                .Index(t => t.HomeId)
                .Index(t => t.People_Id);
            
            CreateTable(
                "dbo.BookingDocument",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HomeId = c.Int(nullable: false),
                        DateModification = c.DateTime(precision: 7, storeType: "datetime2"),
                        Booking_Id = c.Int(nullable: false),
                        Document_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Booking", t => t.Booking_Id, cascadeDelete: true)
                .ForeignKey("dbo.Document", t => t.Document_Id)
                .ForeignKey("dbo.Home", t => t.HomeId)
                .Index(t => t.HomeId)
                .Index(t => t.Booking_Id)
                .Index(t => t.Document_Id);
            
            CreateTable(
                "dbo.Document",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateUpload = c.DateTime(precision: 7, storeType: "datetime2"),
                        SizeDocument = c.Long(nullable: false),
                        IsPrivate = c.Boolean(nullable: false),
                        Title = c.String(),
                        Url = c.String(),
                        HomeId = c.Int(nullable: false),
                        Hide = c.Boolean(nullable: false),
                        MimeType = c.String(),
                        DateModification = c.DateTime(precision: 7, storeType: "datetime2"),
                        DocumentCategory_Id = c.Int(),
                        BookingStep_Id = c.Int(),
                        Meal_Id = c.Int(),
                        Product_Id = c.Int(),
                        Room_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DocumentCategory", t => t.DocumentCategory_Id)
                .ForeignKey("dbo.Home", t => t.HomeId, cascadeDelete: true)
                .ForeignKey("dbo.BookingStep", t => t.BookingStep_Id)
                .ForeignKey("dbo.Meal", t => t.Meal_Id)
                .ForeignKey("dbo.Product", t => t.Product_Id)
                .ForeignKey("dbo.Room", t => t.Room_Id)
                .Index(t => t.HomeId)
                .Index(t => t.DocumentCategory_Id)
                .Index(t => t.BookingStep_Id)
                .Index(t => t.Meal_Id)
                .Index(t => t.Product_Id)
                .Index(t => t.Room_Id);
            
            CreateTable(
                "dbo.DocumentCategory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        HomeId = c.Int(nullable: false),
                        DateModification = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Home", t => t.HomeId, cascadeDelete: true)
                .Index(t => t.HomeId);
            
            CreateTable(
                "dbo.BookingStepBooking",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        MailSent = c.Int(nullable: false),
                        HomeId = c.Int(nullable: false),
                        DateCurrentStepChanged = c.DateTime(precision: 7, storeType: "datetime2"),
                        DateModification = c.DateTime(precision: 7, storeType: "datetime2"),
                        Canceled = c.Boolean(nullable: false),
                        BookingStepConfig_Id = c.Int(nullable: false),
                        CurrentStep_Id = c.Int(),
                        MailLog_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Booking", t => t.Id, cascadeDelete: true)
                .ForeignKey("dbo.BookingStepConfig", t => t.BookingStepConfig_Id)
                .ForeignKey("dbo.BookingStep", t => t.CurrentStep_Id)
                .ForeignKey("dbo.Home", t => t.HomeId)
                .ForeignKey("dbo.MailLog", t => t.MailLog_Id)
                .Index(t => t.Id)
                .Index(t => t.HomeId)
                .Index(t => t.BookingStepConfig_Id)
                .Index(t => t.CurrentStep_Id)
                .Index(t => t.MailLog_Id);
            
            CreateTable(
                "dbo.BookingStepConfig",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        HomeId = c.Int(nullable: false),
                        DateModification = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Home", t => t.HomeId, cascadeDelete: true)
                .Index(t => t.HomeId);
            
            CreateTable(
                "dbo.BookingStep",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        BookingStepIdNext = c.Int(),
                        BookingStepIdPrevious = c.Int(),
                        HomeId = c.Int(nullable: false),
                        BookingValidated = c.Boolean(nullable: false),
                        BookingArchived = c.Boolean(nullable: false),
                        MailSubject = c.String(),
                        DateModification = c.DateTime(precision: 7, storeType: "datetime2"),
                        BookingStepConfig_Id = c.Int(nullable: false),
                        MailTemplate_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BookingStepConfig", t => t.BookingStepConfig_Id, cascadeDelete: true)
                .ForeignKey("dbo.BookingStep", t => t.BookingStepIdNext)
                .ForeignKey("dbo.BookingStep", t => t.BookingStepIdPrevious)
                .ForeignKey("dbo.Home", t => t.HomeId)
                .ForeignKey("dbo.Document", t => t.MailTemplate_Id)
                .Index(t => t.BookingStepIdNext)
                .Index(t => t.BookingStepIdPrevious)
                .Index(t => t.HomeId)
                .Index(t => t.BookingStepConfig_Id)
                .Index(t => t.MailTemplate_Id);
            
            CreateTable(
                "dbo.MailLog",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Successful = c.Boolean(nullable: false),
                        Reason = c.String(),
                        DateModification = c.DateTime(precision: 7, storeType: "datetime2"),
                        To = c.String(),
                        DateSended = c.DateTime(precision: 7, storeType: "datetime2"),
                        HomeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Home", t => t.HomeId, cascadeDelete: true)
                .Index(t => t.HomeId);
            
            CreateTable(
                "dbo.Deposit",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateCreation = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ValueType = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ComputedPrice = c.Decimal(precision: 18, scale: 2),
                        HomeId = c.Int(nullable: false),
                        DateModification = c.DateTime(precision: 7, storeType: "datetime2"),
                        Booking_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Booking", t => t.Booking_Id, cascadeDelete: true)
                .ForeignKey("dbo.Home", t => t.HomeId)
                .Index(t => t.HomeId)
                .Index(t => t.Booking_Id);
            
            CreateTable(
                "dbo.DinnerBooking",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        NumberOfPeople = c.Int(),
                        PriceHT = c.Decimal(precision: 18, scale: 2),
                        PriceTTC = c.Decimal(precision: 18, scale: 2),
                        HomeId = c.Int(nullable: false),
                        DateModification = c.DateTime(precision: 7, storeType: "datetime2"),
                        Booking_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Booking", t => t.Booking_Id, cascadeDelete: true)
                .ForeignKey("dbo.Home", t => t.HomeId)
                .Index(t => t.HomeId)
                .Index(t => t.Booking_Id);
            
            CreateTable(
                "dbo.MealBooking",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HomeId = c.Int(nullable: false),
                        DateModification = c.DateTime(precision: 7, storeType: "datetime2"),
                        NumberOfPeople = c.Int(),
                        DinnerBooking_Id = c.Int(nullable: false),
                        Meal_Id = c.Int(nullable: false),
                        PeopleCategory_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DinnerBooking", t => t.DinnerBooking_Id, cascadeDelete: true)
                .ForeignKey("dbo.Home", t => t.HomeId)
                .ForeignKey("dbo.Meal", t => t.Meal_Id)
                .ForeignKey("dbo.PeopleCategory", t => t.PeopleCategory_Id)
                .Index(t => t.HomeId)
                .Index(t => t.DinnerBooking_Id)
                .Index(t => t.Meal_Id)
                .Index(t => t.PeopleCategory_Id);
            
            CreateTable(
                "dbo.Meal",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        ShortDescription = c.String(),
                        HomeId = c.Int(nullable: false),
                        Title = c.String(),
                        RefHide = c.Boolean(),
                        Hide = c.Boolean(nullable: false),
                        DateModification = c.DateTime(precision: 7, storeType: "datetime2"),
                        BillItemCategory_Id = c.Int(),
                        MealCategory_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BillItemCategory", t => t.BillItemCategory_Id)
                .ForeignKey("dbo.Home", t => t.HomeId, cascadeDelete: true)
                .ForeignKey("dbo.MealCategory", t => t.MealCategory_Id)
                .Index(t => t.HomeId)
                .Index(t => t.BillItemCategory_Id)
                .Index(t => t.MealCategory_Id);
            
            CreateTable(
                "dbo.MealCategory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HomeId = c.Int(nullable: false),
                        Label = c.String(),
                        RefHide = c.Boolean(),
                        DateModification = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Home", t => t.HomeId, cascadeDelete: true)
                .Index(t => t.HomeId);
            
            CreateTable(
                "dbo.MealPrice",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PriceHT = c.Decimal(nullable: false, precision: 18, scale: 2),
                        HomeId = c.Int(nullable: false),
                        DateModification = c.DateTime(precision: 7, storeType: "datetime2"),
                        Meal_Id = c.Int(nullable: false),
                        PeopleCategory_Id = c.Int(),
                        Tax_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Home", t => t.HomeId)
                .ForeignKey("dbo.Meal", t => t.Meal_Id, cascadeDelete: true)
                .ForeignKey("dbo.PeopleCategory", t => t.PeopleCategory_Id)
                .ForeignKey("dbo.Tax", t => t.Tax_Id)
                .Index(t => t.HomeId)
                .Index(t => t.Meal_Id)
                .Index(t => t.PeopleCategory_Id)
                .Index(t => t.Tax_Id);
            
            CreateTable(
                "dbo.PeopleCategory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HomeId = c.Int(nullable: false),
                        Label = c.String(),
                        DateModification = c.DateTime(precision: 7, storeType: "datetime2"),
                        Tax_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Home", t => t.HomeId, cascadeDelete: true)
                .ForeignKey("dbo.Tax", t => t.Tax_Id)
                .Index(t => t.HomeId)
                .Index(t => t.Tax_Id);
            
            CreateTable(
                "dbo.Tax",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ValueType = c.Int(nullable: false),
                        HomeId = c.Int(nullable: false),
                        Title = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DateModification = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Home", t => t.HomeId, cascadeDelete: true)
                .Index(t => t.HomeId);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AcceptMailing = c.Boolean(nullable: false),
                        Addr = c.String(),
                        City = c.String(),
                        Civility = c.String(),
                        Comment = c.String(),
                        Country = c.String(),
                        DateBirth = c.DateTime(precision: 7, storeType: "datetime2"),
                        DateCreation = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Email = c.String(),
                        Firstname = c.String(),
                        Lastname = c.String(),
                        Mark = c.Int(),
                        Phone1 = c.String(),
                        Phone2 = c.String(),
                        State = c.String(),
                        ZipCode = c.String(),
                        HomeId = c.Int(nullable: false),
                        Hide = c.Boolean(nullable: false),
                        DateModification = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Home", t => t.HomeId, cascadeDelete: true)
                .Index(t => t.HomeId);
            
            CreateTable(
                "dbo.ProductBooking",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(precision: 7, storeType: "datetime2"),
                        Duration = c.Long(),
                        PriceHT = c.Decimal(precision: 18, scale: 2),
                        PriceTTC = c.Decimal(precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                        HomeId = c.Int(nullable: false),
                        DateModification = c.DateTime(precision: 7, storeType: "datetime2"),
                        Booking_Id = c.Int(nullable: false),
                        Product_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Booking", t => t.Booking_Id, cascadeDelete: true)
                .ForeignKey("dbo.Home", t => t.HomeId)
                .ForeignKey("dbo.Product", t => t.Product_Id)
                .Index(t => t.HomeId)
                .Index(t => t.Booking_Id)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HomeId = c.Int(nullable: false),
                        Title = c.String(),
                        ShortDescription = c.String(),
                        PriceHT = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RefHide = c.Boolean(),
                        Stock = c.Int(),
                        Threshold = c.Int(),
                        Duration = c.Long(),
                        Hide = c.Boolean(nullable: false),
                        DateModification = c.DateTime(precision: 7, storeType: "datetime2"),
                        IsUnderThreshold = c.Boolean(nullable: false),
                        BillItemCategory_Id = c.Int(),
                        ProductCategory_Id = c.Int(),
                        Supplier_Id = c.Int(),
                        Tax_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BillItemCategory", t => t.BillItemCategory_Id)
                .ForeignKey("dbo.Home", t => t.HomeId, cascadeDelete: true)
                .ForeignKey("dbo.ProductCategory", t => t.ProductCategory_Id)
                .ForeignKey("dbo.Supplier", t => t.Supplier_Id)
                .ForeignKey("dbo.Tax", t => t.Tax_Id)
                .Index(t => t.HomeId)
                .Index(t => t.BillItemCategory_Id)
                .Index(t => t.ProductCategory_Id)
                .Index(t => t.Supplier_Id)
                .Index(t => t.Tax_Id);
            
            CreateTable(
                "dbo.ProductCategory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        HomeId = c.Int(nullable: false),
                        RefHide = c.Boolean(),
                        DateModification = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Home", t => t.HomeId, cascadeDelete: true)
                .Index(t => t.HomeId);
            
            CreateTable(
                "dbo.Supplier",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SocietyName = c.String(),
                        Addr = c.String(),
                        Contact = c.String(),
                        Comment = c.String(),
                        DateCreation = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Email = c.String(),
                        Phone1 = c.String(),
                        Phone2 = c.String(),
                        HomeId = c.Int(nullable: false),
                        Hide = c.Boolean(nullable: false),
                        DateModification = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Home", t => t.HomeId, cascadeDelete: true)
                .Index(t => t.HomeId);
            
            CreateTable(
                "dbo.RoomBooking",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PriceHT = c.Decimal(precision: 18, scale: 2),
                        PriceTTC = c.Decimal(precision: 18, scale: 2),
                        DateBegin = c.DateTime(precision: 7, storeType: "datetime2"),
                        DateEnd = c.DateTime(precision: 7, storeType: "datetime2"),
                        NbNights = c.Int(nullable: false),
                        HomeId = c.Int(nullable: false),
                        DateModification = c.DateTime(precision: 7, storeType: "datetime2"),
                        Booking_Id = c.Int(nullable: false),
                        Room_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Booking", t => t.Booking_Id, cascadeDelete: true)
                .ForeignKey("dbo.Home", t => t.HomeId)
                .ForeignKey("dbo.Room", t => t.Room_Id)
                .Index(t => t.HomeId)
                .Index(t => t.Booking_Id)
                .Index(t => t.Room_Id);
            
            CreateTable(
                "dbo.PeopleBooking",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NumberOfPeople = c.Int(nullable: false),
                        DateBegin = c.DateTime(precision: 7, storeType: "datetime2"),
                        DateEnd = c.DateTime(precision: 7, storeType: "datetime2"),
                        HomeId = c.Int(nullable: false),
                        DateModification = c.DateTime(precision: 7, storeType: "datetime2"),
                        PeopleCategory_Id = c.Int(nullable: false),
                        RoomBooking_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Home", t => t.HomeId)
                .ForeignKey("dbo.PeopleCategory", t => t.PeopleCategory_Id)
                .ForeignKey("dbo.RoomBooking", t => t.RoomBooking_Id, cascadeDelete: true)
                .Index(t => t.HomeId)
                .Index(t => t.PeopleCategory_Id)
                .Index(t => t.RoomBooking_Id);
            
            CreateTable(
                "dbo.Room",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Caution = c.Decimal(precision: 18, scale: 2),
                        Classification = c.String(),
                        Color = c.Int(),
                        Description = c.String(),
                        ShortDescription = c.String(),
                        HomeId = c.Int(nullable: false),
                        IsClosed = c.Boolean(nullable: false),
                        Title = c.String(),
                        RefHide = c.Boolean(),
                        RoomState = c.String(),
                        Size = c.Int(),
                        Hide = c.Boolean(nullable: false),
                        Capacity = c.Int(nullable: false),
                        DateModification = c.DateTime(precision: 7, storeType: "datetime2"),
                        BillItemCategory_Id = c.Int(),
                        RoomCategory_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BillItemCategory", t => t.BillItemCategory_Id)
                .ForeignKey("dbo.Home", t => t.HomeId, cascadeDelete: true)
                .ForeignKey("dbo.RoomCategory", t => t.RoomCategory_Id)
                .Index(t => t.HomeId)
                .Index(t => t.BillItemCategory_Id)
                .Index(t => t.RoomCategory_Id);
            
            CreateTable(
                "dbo.Bed",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        NumberPeople = c.Int(),
                        HomeId = c.Int(nullable: false),
                        DateModification = c.DateTime(precision: 7, storeType: "datetime2"),
                        Room_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Home", t => t.HomeId, cascadeDelete: true)
                .ForeignKey("dbo.Room", t => t.Room_Id)
                .Index(t => t.HomeId)
                .Index(t => t.Room_Id);
            
            CreateTable(
                "dbo.RoomCategory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HomeId = c.Int(nullable: false),
                        Title = c.String(),
                        RefHide = c.Boolean(),
                        DateModification = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Home", t => t.HomeId, cascadeDelete: true)
                .Index(t => t.HomeId);
            
            CreateTable(
                "dbo.SupplementRoomBooking",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PriceHT = c.Decimal(precision: 18, scale: 2),
                        PriceTTC = c.Decimal(precision: 18, scale: 2),
                        HomeId = c.Int(nullable: false),
                        DateModification = c.DateTime(precision: 7, storeType: "datetime2"),
                        RoomBooking_Id = c.Int(nullable: false),
                        RoomSupplement_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Home", t => t.HomeId)
                .ForeignKey("dbo.RoomBooking", t => t.RoomBooking_Id, cascadeDelete: true)
                .ForeignKey("dbo.RoomSupplement", t => t.RoomSupplement_Id)
                .Index(t => t.HomeId)
                .Index(t => t.RoomBooking_Id)
                .Index(t => t.RoomSupplement_Id);
            
            CreateTable(
                "dbo.RoomSupplement",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HomeId = c.Int(nullable: false),
                        Title = c.String(),
                        PriceHT = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Hide = c.Boolean(nullable: false),
                        DateModification = c.DateTime(precision: 7, storeType: "datetime2"),
                        Tax_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Home", t => t.HomeId, cascadeDelete: true)
                .ForeignKey("dbo.Tax", t => t.Tax_Id)
                .Index(t => t.HomeId)
                .Index(t => t.Tax_Id);
            
            CreateTable(
                "dbo.BillItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Title = c.String(),
                        PriceHT = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                        PriceTTC = c.Decimal(nullable: false, precision: 18, scale: 2),
                        HomeId = c.Int(nullable: false),
                        DateModification = c.DateTime(precision: 7, storeType: "datetime2"),
                        Bill_Id = c.Int(nullable: false),
                        BillItemCategory_Id = c.Int(),
                        GroupBillItem_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bill", t => t.Bill_Id, cascadeDelete: true)
                .ForeignKey("dbo.BillItemCategory", t => t.BillItemCategory_Id)
                .ForeignKey("dbo.GroupBillItem", t => t.GroupBillItem_Id)
                .ForeignKey("dbo.Home", t => t.HomeId)
                .Index(t => t.HomeId)
                .Index(t => t.Bill_Id)
                .Index(t => t.BillItemCategory_Id)
                .Index(t => t.GroupBillItem_Id);
            
            CreateTable(
                "dbo.Bill",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreationDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        HomeId = c.Int(nullable: false),
                        IsPayed = c.Boolean(nullable: false),
                        Reference = c.String(maxLength: 64),
                        TotalTTC = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalHT = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DateModification = c.DateTime(precision: 7, storeType: "datetime2"),
                        Booking_Id = c.Int(),
                        Document_Id = c.Int(),
                        Supplier_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Booking", t => t.Booking_Id)
                .ForeignKey("dbo.Document", t => t.Document_Id)
                .ForeignKey("dbo.Home", t => t.HomeId, cascadeDelete: true)
                .ForeignKey("dbo.Supplier", t => t.Supplier_Id)
                .Index(t => t.HomeId)
                .Index(t => t.Booking_Id)
                .Index(t => t.Document_Id)
                .Index(t => t.Supplier_Id);
            
            CreateTable(
                "dbo.PaymentMethod",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        HomeId = c.Int(nullable: false),
                        DateModification = c.DateTime(precision: 7, storeType: "datetime2"),
                        Bill_Id = c.Int(nullable: false),
                        PaymentType_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bill", t => t.Bill_Id, cascadeDelete: true)
                .ForeignKey("dbo.Home", t => t.HomeId)
                .ForeignKey("dbo.PaymentType", t => t.PaymentType_Id)
                .Index(t => t.HomeId)
                .Index(t => t.Bill_Id)
                .Index(t => t.PaymentType_Id);
            
            CreateTable(
                "dbo.PaymentType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        HomeId = c.Int(nullable: false),
                        DateModification = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Home", t => t.HomeId, cascadeDelete: true)
                .Index(t => t.HomeId);
            
            CreateTable(
                "dbo.GroupBillItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ValueType = c.Int(nullable: false),
                        Discount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        HomeId = c.Int(nullable: false),
                        DateModification = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Home", t => t.HomeId, cascadeDelete: true)
                .Index(t => t.HomeId);
            
            CreateTable(
                "dbo.DocumentLog",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        CurrentSize = c.Long(nullable: false),
                        BuySize = c.Long(nullable: false),
                        DateModification = c.DateTime(precision: 7, storeType: "datetime2"),
                        ResourceConfig_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Id, cascadeDelete: true)
                .ForeignKey("dbo.ResourceConfig", t => t.ResourceConfig_Id)
                .Index(t => t.Id)
                .Index(t => t.ResourceConfig_Id);
            
            CreateTable(
                "dbo.ResourceConfig",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LimitBase = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FieldGroup",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HomeId = c.Int(nullable: false),
                        Title = c.String(),
                        DateModification = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Home", t => t.HomeId, cascadeDelete: true)
                .Index(t => t.HomeId);
            
            CreateTable(
                "dbo.PeopleField",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Key = c.String(),
                        Value = c.String(),
                        HomeId = c.Int(nullable: false),
                        DateModification = c.DateTime(precision: 7, storeType: "datetime2"),
                        FieldGroup_Id = c.Int(),
                        People_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FieldGroup", t => t.FieldGroup_Id, cascadeDelete: true)
                .ForeignKey("dbo.Home", t => t.HomeId)
                .ForeignKey("dbo.People", t => t.People_Id)
                .Index(t => t.HomeId)
                .Index(t => t.FieldGroup_Id)
                .Index(t => t.People_Id);
            
            CreateTable(
                "dbo.HomeConfig",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        AutoSendSatisfactionEmail = c.Boolean(nullable: false),
                        DefaultCaution = c.Decimal(precision: 18, scale: 2),
                        DepositNotifEnabled = c.Boolean(nullable: false),
                        DinnerCapacity = c.Int(),
                        EnableDisplayActivities = c.Boolean(nullable: false),
                        EnableDisplayMeals = c.Boolean(nullable: false),
                        EnableDisplayProducts = c.Boolean(nullable: false),
                        EnableDisplayRooms = c.Boolean(nullable: false),
                        EnableReferencing = c.Boolean(nullable: false),
                        DefaultValueType = c.Int(),
                        EnableDinner = c.Boolean(),
                        FollowStockEnable = c.Boolean(nullable: false),
                        Devise = c.String(maxLength: 256),
                        DateModification = c.DateTime(precision: 7, storeType: "datetime2"),
                        DefaultHourCheckIn = c.Long(),
                        DefaultHourCheckOut = c.Long(),
                        HourFormat24 = c.Boolean(),
                        BookingCanceledMailTemplate_Id = c.Int(),
                        DefaultBillItemCategoryMeal_Id = c.Int(),
                        DefaultBillItemCategoryProduct_Id = c.Int(),
                        DefaultBillItemCategoryRoom_Id = c.Int(),
                        DefaultMailConfig_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Document", t => t.BookingCanceledMailTemplate_Id)
                .ForeignKey("dbo.BillItemCategory", t => t.DefaultBillItemCategoryMeal_Id)
                .ForeignKey("dbo.BillItemCategory", t => t.DefaultBillItemCategoryProduct_Id)
                .ForeignKey("dbo.BillItemCategory", t => t.DefaultBillItemCategoryRoom_Id)
                .ForeignKey("dbo.MailConfig", t => t.DefaultMailConfig_Id)
                .ForeignKey("dbo.Home", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.BookingCanceledMailTemplate_Id)
                .Index(t => t.DefaultBillItemCategoryMeal_Id)
                .Index(t => t.DefaultBillItemCategoryProduct_Id)
                .Index(t => t.DefaultBillItemCategoryRoom_Id)
                .Index(t => t.DefaultMailConfig_Id);
            
            CreateTable(
                "dbo.MailConfig",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        HomeId = c.Int(nullable: false),
                        Title = c.String(),
                        Smtp = c.String(),
                        SmtpPort = c.Int(nullable: false),
                        IsSSL = c.Boolean(),
                        DateModification = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Home", t => t.HomeId, cascadeDelete: true)
                .Index(t => t.HomeId);
            
            CreateTable(
                "dbo.KeyGenerator",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateExp = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        KeyType = c.Int(nullable: false),
                        ValueType = c.Int(nullable: false),
                        Key = c.String(),
                        Price = c.Decimal(precision: 18, scale: 2),
                        Description = c.String(),
                        HomeId = c.Int(),
                        DateModification = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Home", t => t.HomeId, cascadeDelete: true)
                .Index(t => t.HomeId);
            
            CreateTable(
                "dbo.Payment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccountName = c.String(maxLength: 256),
                        Label = c.String(maxLength: 256),
                        RemoteIP = c.String(),
                        Price = c.Decimal(precision: 18, scale: 2),
                        SubscriptionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Subscription", t => t.SubscriptionId, cascadeDelete: true)
                .Index(t => t.SubscriptionId);
            
            CreateTable(
                "dbo.Subscription",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        ExpirationDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        NumberOfRoom = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Period",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Begin = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        End = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Days = c.Int(nullable: false),
                        Description = c.String(),
                        HomeId = c.Int(nullable: false),
                        IsClosed = c.Boolean(nullable: false),
                        Title = c.String(),
                        DateModification = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Home", t => t.HomeId, cascadeDelete: true)
                .Index(t => t.HomeId);
            
            CreateTable(
                "dbo.PricePerPerson",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        PriceHT = c.Decimal(nullable: false, precision: 18, scale: 2),
                        HomeId = c.Int(nullable: false),
                        DateModification = c.DateTime(precision: 7, storeType: "datetime2"),
                        PeopleCategory_Id = c.Int(),
                        Period_Id = c.Int(nullable: false),
                        Room_Id = c.Int(nullable: false),
                        Tax_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Home", t => t.HomeId, cascadeDelete: true)
                .ForeignKey("dbo.PeopleCategory", t => t.PeopleCategory_Id)
                .ForeignKey("dbo.Period", t => t.Period_Id)
                .ForeignKey("dbo.Room", t => t.Room_Id)
                .ForeignKey("dbo.Tax", t => t.Tax_Id)
                .Index(t => t.HomeId)
                .Index(t => t.PeopleCategory_Id)
                .Index(t => t.Period_Id)
                .Index(t => t.Room_Id)
                .Index(t => t.Tax_Id);
            
            CreateTable(
                "dbo.RefreshTokens",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ProtectedTicket = c.String(nullable: false),
                        IssuedUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ExpiresUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Client_Id = c.Int(nullable: false),
                        Service_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Client_Id)
                .ForeignKey("dbo.Services", t => t.Service_Id)
                .Index(t => t.Client_Id)
                .Index(t => t.Service_Id);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Secret = c.String(nullable: false),
                        Name = c.String(nullable: false, maxLength: 256),
                        Active = c.Boolean(nullable: false),
                        AllowedOrigin = c.String(maxLength: 256),
                        ApplicationType = c.Int(nullable: false),
                        RefreshTokenLifeTime = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.SatisfactionClientAnswer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Answer = c.String(),
                        AnswerType = c.Int(nullable: false),
                        Question = c.String(),
                        HomeId = c.Int(nullable: false),
                        DateModification = c.DateTime(precision: 7, storeType: "datetime2"),
                        SatisfactionClient_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Home", t => t.HomeId)
                .ForeignKey("dbo.SatisfactionClient", t => t.SatisfactionClient_Id, cascadeDelete: true)
                .Index(t => t.HomeId)
                .Index(t => t.SatisfactionClient_Id);
            
            CreateTable(
                "dbo.SatisfactionClient",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        DateAnswered = c.DateTime(precision: 7, storeType: "datetime2"),
                        HomeId = c.Int(nullable: false),
                        DateModification = c.DateTime(precision: 7, storeType: "datetime2"),
                        Booking_Id = c.Int(),
                        ClientDest_Id = c.Int(),
                        PeopleDest_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Booking", t => t.Booking_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ClientDest_Id)
                .ForeignKey("dbo.Home", t => t.HomeId, cascadeDelete: true)
                .ForeignKey("dbo.People", t => t.PeopleDest_Id)
                .Index(t => t.HomeId)
                .Index(t => t.Booking_Id)
                .Index(t => t.ClientDest_Id)
                .Index(t => t.PeopleDest_Id);
            
            CreateTable(
                "dbo.SatisfactionConfigQuestion",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AnswerType = c.Int(nullable: false),
                        Question = c.String(),
                        HomeId = c.Int(nullable: false),
                        DateModification = c.DateTime(precision: 7, storeType: "datetime2"),
                        SatisfactionConfig_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Home", t => t.HomeId)
                .ForeignKey("dbo.SatisfactionConfig", t => t.SatisfactionConfig_Id, cascadeDelete: true)
                .Index(t => t.HomeId)
                .Index(t => t.SatisfactionConfig_Id);
            
            CreateTable(
                "dbo.SatisfactionConfig",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        AdditionalInfo = c.String(),
                        Description = c.String(),
                        Title = c.String(),
                        DateModification = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Home", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.MStatistics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Data = c.String(),
                        Date = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        HomeId = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        DateModification = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Home", t => t.HomeId, cascadeDelete: true)
                .Index(t => t.HomeId);
            
            CreateTable(
                "dbo.Tests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TestStr = c.String(),
                        CannotSet = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MStatistics", "HomeId", "dbo.Home");
            DropForeignKey("dbo.SatisfactionConfigQuestion", "SatisfactionConfig_Id", "dbo.SatisfactionConfig");
            DropForeignKey("dbo.SatisfactionConfig", "Id", "dbo.Home");
            DropForeignKey("dbo.SatisfactionConfigQuestion", "HomeId", "dbo.Home");
            DropForeignKey("dbo.SatisfactionClientAnswer", "SatisfactionClient_Id", "dbo.SatisfactionClient");
            DropForeignKey("dbo.SatisfactionClient", "PeopleDest_Id", "dbo.People");
            DropForeignKey("dbo.SatisfactionClient", "HomeId", "dbo.Home");
            DropForeignKey("dbo.SatisfactionClient", "ClientDest_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.SatisfactionClient", "Booking_Id", "dbo.Booking");
            DropForeignKey("dbo.SatisfactionClientAnswer", "HomeId", "dbo.Home");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.RefreshTokens", "Service_Id", "dbo.Services");
            DropForeignKey("dbo.RefreshTokens", "Client_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.PricePerPerson", "Tax_Id", "dbo.Tax");
            DropForeignKey("dbo.PricePerPerson", "Room_Id", "dbo.Room");
            DropForeignKey("dbo.PricePerPerson", "Period_Id", "dbo.Period");
            DropForeignKey("dbo.PricePerPerson", "PeopleCategory_Id", "dbo.PeopleCategory");
            DropForeignKey("dbo.PricePerPerson", "HomeId", "dbo.Home");
            DropForeignKey("dbo.Period", "HomeId", "dbo.Home");
            DropForeignKey("dbo.Payment", "SubscriptionId", "dbo.Subscription");
            DropForeignKey("dbo.Subscription", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.KeyGenerator", "HomeId", "dbo.Home");
            DropForeignKey("dbo.HomeConfig", "Id", "dbo.Home");
            DropForeignKey("dbo.HomeConfig", "DefaultMailConfig_Id", "dbo.MailConfig");
            DropForeignKey("dbo.MailConfig", "HomeId", "dbo.Home");
            DropForeignKey("dbo.HomeConfig", "DefaultBillItemCategoryRoom_Id", "dbo.BillItemCategory");
            DropForeignKey("dbo.HomeConfig", "DefaultBillItemCategoryProduct_Id", "dbo.BillItemCategory");
            DropForeignKey("dbo.HomeConfig", "DefaultBillItemCategoryMeal_Id", "dbo.BillItemCategory");
            DropForeignKey("dbo.HomeConfig", "BookingCanceledMailTemplate_Id", "dbo.Document");
            DropForeignKey("dbo.PeopleField", "People_Id", "dbo.People");
            DropForeignKey("dbo.PeopleField", "HomeId", "dbo.Home");
            DropForeignKey("dbo.PeopleField", "FieldGroup_Id", "dbo.FieldGroup");
            DropForeignKey("dbo.FieldGroup", "HomeId", "dbo.Home");
            DropForeignKey("dbo.DocumentLog", "ResourceConfig_Id", "dbo.ResourceConfig");
            DropForeignKey("dbo.DocumentLog", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.BillItem", "HomeId", "dbo.Home");
            DropForeignKey("dbo.BillItem", "GroupBillItem_Id", "dbo.GroupBillItem");
            DropForeignKey("dbo.GroupBillItem", "HomeId", "dbo.Home");
            DropForeignKey("dbo.BillItem", "BillItemCategory_Id", "dbo.BillItemCategory");
            DropForeignKey("dbo.BillItem", "Bill_Id", "dbo.Bill");
            DropForeignKey("dbo.Bill", "Supplier_Id", "dbo.Supplier");
            DropForeignKey("dbo.PaymentMethod", "PaymentType_Id", "dbo.PaymentType");
            DropForeignKey("dbo.PaymentType", "HomeId", "dbo.Home");
            DropForeignKey("dbo.PaymentMethod", "HomeId", "dbo.Home");
            DropForeignKey("dbo.PaymentMethod", "Bill_Id", "dbo.Bill");
            DropForeignKey("dbo.Bill", "HomeId", "dbo.Home");
            DropForeignKey("dbo.Bill", "Document_Id", "dbo.Document");
            DropForeignKey("dbo.Bill", "Booking_Id", "dbo.Booking");
            DropForeignKey("dbo.AdditionalBooking", "Tax_Id", "dbo.Tax");
            DropForeignKey("dbo.AdditionalBooking", "HomeId", "dbo.Home");
            DropForeignKey("dbo.AdditionalBooking", "Booking_Id", "dbo.Booking");
            DropForeignKey("dbo.SupplementRoomBooking", "RoomSupplement_Id", "dbo.RoomSupplement");
            DropForeignKey("dbo.RoomSupplement", "Tax_Id", "dbo.Tax");
            DropForeignKey("dbo.RoomSupplement", "HomeId", "dbo.Home");
            DropForeignKey("dbo.SupplementRoomBooking", "RoomBooking_Id", "dbo.RoomBooking");
            DropForeignKey("dbo.SupplementRoomBooking", "HomeId", "dbo.Home");
            DropForeignKey("dbo.RoomBooking", "Room_Id", "dbo.Room");
            DropForeignKey("dbo.Room", "RoomCategory_Id", "dbo.RoomCategory");
            DropForeignKey("dbo.RoomCategory", "HomeId", "dbo.Home");
            DropForeignKey("dbo.Room", "HomeId", "dbo.Home");
            DropForeignKey("dbo.Document", "Room_Id", "dbo.Room");
            DropForeignKey("dbo.Room", "BillItemCategory_Id", "dbo.BillItemCategory");
            DropForeignKey("dbo.Bed", "Room_Id", "dbo.Room");
            DropForeignKey("dbo.Bed", "HomeId", "dbo.Home");
            DropForeignKey("dbo.PeopleBooking", "RoomBooking_Id", "dbo.RoomBooking");
            DropForeignKey("dbo.PeopleBooking", "PeopleCategory_Id", "dbo.PeopleCategory");
            DropForeignKey("dbo.PeopleBooking", "HomeId", "dbo.Home");
            DropForeignKey("dbo.RoomBooking", "HomeId", "dbo.Home");
            DropForeignKey("dbo.RoomBooking", "Booking_Id", "dbo.Booking");
            DropForeignKey("dbo.ProductBooking", "Product_Id", "dbo.Product");
            DropForeignKey("dbo.Product", "Tax_Id", "dbo.Tax");
            DropForeignKey("dbo.Product", "Supplier_Id", "dbo.Supplier");
            DropForeignKey("dbo.Supplier", "HomeId", "dbo.Home");
            DropForeignKey("dbo.Product", "ProductCategory_Id", "dbo.ProductCategory");
            DropForeignKey("dbo.ProductCategory", "HomeId", "dbo.Home");
            DropForeignKey("dbo.Product", "HomeId", "dbo.Home");
            DropForeignKey("dbo.Document", "Product_Id", "dbo.Product");
            DropForeignKey("dbo.Product", "BillItemCategory_Id", "dbo.BillItemCategory");
            DropForeignKey("dbo.ProductBooking", "HomeId", "dbo.Home");
            DropForeignKey("dbo.ProductBooking", "Booking_Id", "dbo.Booking");
            DropForeignKey("dbo.Booking", "People_Id", "dbo.People");
            DropForeignKey("dbo.People", "HomeId", "dbo.Home");
            DropForeignKey("dbo.Booking", "HomeId", "dbo.Home");
            DropForeignKey("dbo.MealBooking", "PeopleCategory_Id", "dbo.PeopleCategory");
            DropForeignKey("dbo.MealBooking", "Meal_Id", "dbo.Meal");
            DropForeignKey("dbo.MealPrice", "Tax_Id", "dbo.Tax");
            DropForeignKey("dbo.MealPrice", "PeopleCategory_Id", "dbo.PeopleCategory");
            DropForeignKey("dbo.PeopleCategory", "Tax_Id", "dbo.Tax");
            DropForeignKey("dbo.Tax", "HomeId", "dbo.Home");
            DropForeignKey("dbo.PeopleCategory", "HomeId", "dbo.Home");
            DropForeignKey("dbo.MealPrice", "Meal_Id", "dbo.Meal");
            DropForeignKey("dbo.MealPrice", "HomeId", "dbo.Home");
            DropForeignKey("dbo.Meal", "MealCategory_Id", "dbo.MealCategory");
            DropForeignKey("dbo.MealCategory", "HomeId", "dbo.Home");
            DropForeignKey("dbo.Meal", "HomeId", "dbo.Home");
            DropForeignKey("dbo.Document", "Meal_Id", "dbo.Meal");
            DropForeignKey("dbo.Meal", "BillItemCategory_Id", "dbo.BillItemCategory");
            DropForeignKey("dbo.MealBooking", "HomeId", "dbo.Home");
            DropForeignKey("dbo.MealBooking", "DinnerBooking_Id", "dbo.DinnerBooking");
            DropForeignKey("dbo.DinnerBooking", "HomeId", "dbo.Home");
            DropForeignKey("dbo.DinnerBooking", "Booking_Id", "dbo.Booking");
            DropForeignKey("dbo.Deposit", "HomeId", "dbo.Home");
            DropForeignKey("dbo.Deposit", "Booking_Id", "dbo.Booking");
            DropForeignKey("dbo.BookingStepBooking", "MailLog_Id", "dbo.MailLog");
            DropForeignKey("dbo.MailLog", "HomeId", "dbo.Home");
            DropForeignKey("dbo.BookingStepBooking", "HomeId", "dbo.Home");
            DropForeignKey("dbo.BookingStepBooking", "CurrentStep_Id", "dbo.BookingStep");
            DropForeignKey("dbo.BookingStepBooking", "BookingStepConfig_Id", "dbo.BookingStepConfig");
            DropForeignKey("dbo.BookingStepConfig", "HomeId", "dbo.Home");
            DropForeignKey("dbo.BookingStep", "MailTemplate_Id", "dbo.Document");
            DropForeignKey("dbo.BookingStep", "HomeId", "dbo.Home");
            DropForeignKey("dbo.Document", "BookingStep_Id", "dbo.BookingStep");
            DropForeignKey("dbo.BookingStep", "BookingStepIdPrevious", "dbo.BookingStep");
            DropForeignKey("dbo.BookingStep", "BookingStepIdNext", "dbo.BookingStep");
            DropForeignKey("dbo.BookingStep", "BookingStepConfig_Id", "dbo.BookingStepConfig");
            DropForeignKey("dbo.BookingStepBooking", "Id", "dbo.Booking");
            DropForeignKey("dbo.BookingDocument", "HomeId", "dbo.Home");
            DropForeignKey("dbo.BookingDocument", "Document_Id", "dbo.Document");
            DropForeignKey("dbo.Document", "HomeId", "dbo.Home");
            DropForeignKey("dbo.Document", "DocumentCategory_Id", "dbo.DocumentCategory");
            DropForeignKey("dbo.DocumentCategory", "HomeId", "dbo.Home");
            DropForeignKey("dbo.BookingDocument", "Booking_Id", "dbo.Booking");
            DropForeignKey("dbo.AdditionalBooking", "BillItemCategory_Id", "dbo.BillItemCategory");
            DropForeignKey("dbo.BillItemCategory", "HomeId", "dbo.Home");
            DropForeignKey("dbo.Home", "ClientId", "dbo.AspNetUsers");
            DropForeignKey("dbo.PhoneNumbers", "Client_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "PrincipalPhone_Id", "dbo.PhoneNumbers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.MStatistics", new[] { "HomeId" });
            DropIndex("dbo.SatisfactionConfig", new[] { "Id" });
            DropIndex("dbo.SatisfactionConfigQuestion", new[] { "SatisfactionConfig_Id" });
            DropIndex("dbo.SatisfactionConfigQuestion", new[] { "HomeId" });
            DropIndex("dbo.SatisfactionClient", new[] { "PeopleDest_Id" });
            DropIndex("dbo.SatisfactionClient", new[] { "ClientDest_Id" });
            DropIndex("dbo.SatisfactionClient", new[] { "Booking_Id" });
            DropIndex("dbo.SatisfactionClient", new[] { "HomeId" });
            DropIndex("dbo.SatisfactionClientAnswer", new[] { "SatisfactionClient_Id" });
            DropIndex("dbo.SatisfactionClientAnswer", new[] { "HomeId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.RefreshTokens", new[] { "Service_Id" });
            DropIndex("dbo.RefreshTokens", new[] { "Client_Id" });
            DropIndex("dbo.PricePerPerson", new[] { "Tax_Id" });
            DropIndex("dbo.PricePerPerson", new[] { "Room_Id" });
            DropIndex("dbo.PricePerPerson", new[] { "Period_Id" });
            DropIndex("dbo.PricePerPerson", new[] { "PeopleCategory_Id" });
            DropIndex("dbo.PricePerPerson", new[] { "HomeId" });
            DropIndex("dbo.Period", new[] { "HomeId" });
            DropIndex("dbo.Subscription", new[] { "Id" });
            DropIndex("dbo.Payment", new[] { "SubscriptionId" });
            DropIndex("dbo.KeyGenerator", new[] { "HomeId" });
            DropIndex("dbo.MailConfig", new[] { "HomeId" });
            DropIndex("dbo.HomeConfig", new[] { "DefaultMailConfig_Id" });
            DropIndex("dbo.HomeConfig", new[] { "DefaultBillItemCategoryRoom_Id" });
            DropIndex("dbo.HomeConfig", new[] { "DefaultBillItemCategoryProduct_Id" });
            DropIndex("dbo.HomeConfig", new[] { "DefaultBillItemCategoryMeal_Id" });
            DropIndex("dbo.HomeConfig", new[] { "BookingCanceledMailTemplate_Id" });
            DropIndex("dbo.HomeConfig", new[] { "Id" });
            DropIndex("dbo.PeopleField", new[] { "People_Id" });
            DropIndex("dbo.PeopleField", new[] { "FieldGroup_Id" });
            DropIndex("dbo.PeopleField", new[] { "HomeId" });
            DropIndex("dbo.FieldGroup", new[] { "HomeId" });
            DropIndex("dbo.DocumentLog", new[] { "ResourceConfig_Id" });
            DropIndex("dbo.DocumentLog", new[] { "Id" });
            DropIndex("dbo.GroupBillItem", new[] { "HomeId" });
            DropIndex("dbo.PaymentType", new[] { "HomeId" });
            DropIndex("dbo.PaymentMethod", new[] { "PaymentType_Id" });
            DropIndex("dbo.PaymentMethod", new[] { "Bill_Id" });
            DropIndex("dbo.PaymentMethod", new[] { "HomeId" });
            DropIndex("dbo.Bill", new[] { "Supplier_Id" });
            DropIndex("dbo.Bill", new[] { "Document_Id" });
            DropIndex("dbo.Bill", new[] { "Booking_Id" });
            DropIndex("dbo.Bill", new[] { "HomeId" });
            DropIndex("dbo.BillItem", new[] { "GroupBillItem_Id" });
            DropIndex("dbo.BillItem", new[] { "BillItemCategory_Id" });
            DropIndex("dbo.BillItem", new[] { "Bill_Id" });
            DropIndex("dbo.BillItem", new[] { "HomeId" });
            DropIndex("dbo.RoomSupplement", new[] { "Tax_Id" });
            DropIndex("dbo.RoomSupplement", new[] { "HomeId" });
            DropIndex("dbo.SupplementRoomBooking", new[] { "RoomSupplement_Id" });
            DropIndex("dbo.SupplementRoomBooking", new[] { "RoomBooking_Id" });
            DropIndex("dbo.SupplementRoomBooking", new[] { "HomeId" });
            DropIndex("dbo.RoomCategory", new[] { "HomeId" });
            DropIndex("dbo.Bed", new[] { "Room_Id" });
            DropIndex("dbo.Bed", new[] { "HomeId" });
            DropIndex("dbo.Room", new[] { "RoomCategory_Id" });
            DropIndex("dbo.Room", new[] { "BillItemCategory_Id" });
            DropIndex("dbo.Room", new[] { "HomeId" });
            DropIndex("dbo.PeopleBooking", new[] { "RoomBooking_Id" });
            DropIndex("dbo.PeopleBooking", new[] { "PeopleCategory_Id" });
            DropIndex("dbo.PeopleBooking", new[] { "HomeId" });
            DropIndex("dbo.RoomBooking", new[] { "Room_Id" });
            DropIndex("dbo.RoomBooking", new[] { "Booking_Id" });
            DropIndex("dbo.RoomBooking", new[] { "HomeId" });
            DropIndex("dbo.Supplier", new[] { "HomeId" });
            DropIndex("dbo.ProductCategory", new[] { "HomeId" });
            DropIndex("dbo.Product", new[] { "Tax_Id" });
            DropIndex("dbo.Product", new[] { "Supplier_Id" });
            DropIndex("dbo.Product", new[] { "ProductCategory_Id" });
            DropIndex("dbo.Product", new[] { "BillItemCategory_Id" });
            DropIndex("dbo.Product", new[] { "HomeId" });
            DropIndex("dbo.ProductBooking", new[] { "Product_Id" });
            DropIndex("dbo.ProductBooking", new[] { "Booking_Id" });
            DropIndex("dbo.ProductBooking", new[] { "HomeId" });
            DropIndex("dbo.People", new[] { "HomeId" });
            DropIndex("dbo.Tax", new[] { "HomeId" });
            DropIndex("dbo.PeopleCategory", new[] { "Tax_Id" });
            DropIndex("dbo.PeopleCategory", new[] { "HomeId" });
            DropIndex("dbo.MealPrice", new[] { "Tax_Id" });
            DropIndex("dbo.MealPrice", new[] { "PeopleCategory_Id" });
            DropIndex("dbo.MealPrice", new[] { "Meal_Id" });
            DropIndex("dbo.MealPrice", new[] { "HomeId" });
            DropIndex("dbo.MealCategory", new[] { "HomeId" });
            DropIndex("dbo.Meal", new[] { "MealCategory_Id" });
            DropIndex("dbo.Meal", new[] { "BillItemCategory_Id" });
            DropIndex("dbo.Meal", new[] { "HomeId" });
            DropIndex("dbo.MealBooking", new[] { "PeopleCategory_Id" });
            DropIndex("dbo.MealBooking", new[] { "Meal_Id" });
            DropIndex("dbo.MealBooking", new[] { "DinnerBooking_Id" });
            DropIndex("dbo.MealBooking", new[] { "HomeId" });
            DropIndex("dbo.DinnerBooking", new[] { "Booking_Id" });
            DropIndex("dbo.DinnerBooking", new[] { "HomeId" });
            DropIndex("dbo.Deposit", new[] { "Booking_Id" });
            DropIndex("dbo.Deposit", new[] { "HomeId" });
            DropIndex("dbo.MailLog", new[] { "HomeId" });
            DropIndex("dbo.BookingStep", new[] { "MailTemplate_Id" });
            DropIndex("dbo.BookingStep", new[] { "BookingStepConfig_Id" });
            DropIndex("dbo.BookingStep", new[] { "HomeId" });
            DropIndex("dbo.BookingStep", new[] { "BookingStepIdPrevious" });
            DropIndex("dbo.BookingStep", new[] { "BookingStepIdNext" });
            DropIndex("dbo.BookingStepConfig", new[] { "HomeId" });
            DropIndex("dbo.BookingStepBooking", new[] { "MailLog_Id" });
            DropIndex("dbo.BookingStepBooking", new[] { "CurrentStep_Id" });
            DropIndex("dbo.BookingStepBooking", new[] { "BookingStepConfig_Id" });
            DropIndex("dbo.BookingStepBooking", new[] { "HomeId" });
            DropIndex("dbo.BookingStepBooking", new[] { "Id" });
            DropIndex("dbo.DocumentCategory", new[] { "HomeId" });
            DropIndex("dbo.Document", new[] { "Room_Id" });
            DropIndex("dbo.Document", new[] { "Product_Id" });
            DropIndex("dbo.Document", new[] { "Meal_Id" });
            DropIndex("dbo.Document", new[] { "BookingStep_Id" });
            DropIndex("dbo.Document", new[] { "DocumentCategory_Id" });
            DropIndex("dbo.Document", new[] { "HomeId" });
            DropIndex("dbo.BookingDocument", new[] { "Document_Id" });
            DropIndex("dbo.BookingDocument", new[] { "Booking_Id" });
            DropIndex("dbo.BookingDocument", new[] { "HomeId" });
            DropIndex("dbo.Booking", new[] { "People_Id" });
            DropIndex("dbo.Booking", new[] { "HomeId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.PhoneNumbers", new[] { "Client_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "PrincipalPhone_Id" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Home", new[] { "ClientId" });
            DropIndex("dbo.BillItemCategory", new[] { "HomeId" });
            DropIndex("dbo.AdditionalBooking", new[] { "Tax_Id" });
            DropIndex("dbo.AdditionalBooking", new[] { "Booking_Id" });
            DropIndex("dbo.AdditionalBooking", new[] { "BillItemCategory_Id" });
            DropIndex("dbo.AdditionalBooking", new[] { "HomeId" });
            DropTable("dbo.Tests");
            DropTable("dbo.MStatistics");
            DropTable("dbo.SatisfactionConfig");
            DropTable("dbo.SatisfactionConfigQuestion");
            DropTable("dbo.SatisfactionClient");
            DropTable("dbo.SatisfactionClientAnswer");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Services");
            DropTable("dbo.RefreshTokens");
            DropTable("dbo.PricePerPerson");
            DropTable("dbo.Period");
            DropTable("dbo.Subscription");
            DropTable("dbo.Payment");
            DropTable("dbo.KeyGenerator");
            DropTable("dbo.MailConfig");
            DropTable("dbo.HomeConfig");
            DropTable("dbo.PeopleField");
            DropTable("dbo.FieldGroup");
            DropTable("dbo.ResourceConfig");
            DropTable("dbo.DocumentLog");
            DropTable("dbo.GroupBillItem");
            DropTable("dbo.PaymentType");
            DropTable("dbo.PaymentMethod");
            DropTable("dbo.Bill");
            DropTable("dbo.BillItem");
            DropTable("dbo.RoomSupplement");
            DropTable("dbo.SupplementRoomBooking");
            DropTable("dbo.RoomCategory");
            DropTable("dbo.Bed");
            DropTable("dbo.Room");
            DropTable("dbo.PeopleBooking");
            DropTable("dbo.RoomBooking");
            DropTable("dbo.Supplier");
            DropTable("dbo.ProductCategory");
            DropTable("dbo.Product");
            DropTable("dbo.ProductBooking");
            DropTable("dbo.People");
            DropTable("dbo.Tax");
            DropTable("dbo.PeopleCategory");
            DropTable("dbo.MealPrice");
            DropTable("dbo.MealCategory");
            DropTable("dbo.Meal");
            DropTable("dbo.MealBooking");
            DropTable("dbo.DinnerBooking");
            DropTable("dbo.Deposit");
            DropTable("dbo.MailLog");
            DropTable("dbo.BookingStep");
            DropTable("dbo.BookingStepConfig");
            DropTable("dbo.BookingStepBooking");
            DropTable("dbo.DocumentCategory");
            DropTable("dbo.Document");
            DropTable("dbo.BookingDocument");
            DropTable("dbo.Booking");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.PhoneNumbers");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Home");
            DropTable("dbo.BillItemCategory");
            DropTable("dbo.AdditionalBooking");
        }
    }
}
