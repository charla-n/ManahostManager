using ManahostManager.Domain.Entity;
using ManahostManager.Domain.Tools;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace ManahostManager.Domain.DAL
{
    public class ManahostManagerInitializerTest : DropCreateDatabaseAlways<ManahostManagerDAL>
    {
        public void ServicesSeed(ManahostManagerDAL context)
        {
            if (context.ServiceTest.Find("ngAuthApp") == null)
            {
                context.ServiceTest.Add(new Service()
                {
                    Id = "ngAuthApp",
                    Name = "Manager AngularJS",
                    Active = false,
                    AllowedOrigin = "https://manager.manahost.fr",
                    ApplicationType = ApplicationTypes.JAVASCRIPT,
                    RefreshTokenLifeTime = 7400,
                    Secret = new BcryptPasswordHasher().HashPassword("OSEF")
                });
            }
            if (context.ServiceTest.Find("ngAuthApp-DEV") == null)
            {
                context.ServiceTest.Add(new Service()
                {
                    Id = "ngAuthApp-DEV",
                    Name = "Manager AngularJS DEV",
                    Active = true,
                    AllowedOrigin = "http://homedev.manahost.fr",
                    ApplicationType = ApplicationTypes.JAVASCRIPT,
                    RefreshTokenLifeTime = 7400,
                    Secret = new BcryptPasswordHasher().HashPassword("OSEF")
                });
            }
            if (context.ServiceTest.Find("IOS") == null)
            {
                context.ServiceTest.Add(new Service()
                {
                    Id = "IOS",
                    Name = "Client native IOS",
                    Active = true,
                    AllowedOrigin = "*",
                    ApplicationType = ApplicationTypes.NATIVE_CLIENT,
                    RefreshTokenLifeTime = 7400,
                    Secret = new BcryptPasswordHasher().HashPassword("b4c6bb78b2099a5bf526625e7c3055e848544ed3a2254169ed45f831b5aa62d3")
                });
            }
            if (context.ServiceTest.Find("ANDROID") == null)
            {
                context.ServiceTest.Add(new Service()
                {
                    Id = "ANDROID",
                    Name = "Client native ANDROID",
                    Active = true,
                    AllowedOrigin = "*",
                    ApplicationType = ApplicationTypes.NATIVE_CLIENT,
                    RefreshTokenLifeTime = 7400,
                    Secret = new BcryptPasswordHasher().HashPassword("6666d2e1cb99c685b2eeeb25670791cb1e35481470b705cef73c8ae0da5d1ec0")
                });
            }
            if (context.ServiceTest.Find("WINDOWSPHONE") == null)
            {
                context.ServiceTest.Add(new Service()
                {
                    Id = "WINDOWSPHONE",
                    Name = "Client native Windows Phone",
                    Active = false,
                    AllowedOrigin = "*",
                    ApplicationType = ApplicationTypes.NATIVE_CLIENT,
                    RefreshTokenLifeTime = 7400,
                    Secret = new BcryptPasswordHasher().HashPassword("e25db8c8a431abf79c029b344241eb47d183bb26e39911b87b3da0bc39e25b60")
                });
            }
            if (context.ServiceTest.Find("UNITTEST") == null)
            {
                context.ServiceTest.Add(new Service()
                {
                    Id = "UNITTEST",
                    Name = "Client native Windows Phone",
                    Active = true,
                    AllowedOrigin = "*",
                    ApplicationType = ApplicationTypes.NATIVE_CLIENT,
                    RefreshTokenLifeTime = 7400,
                    Secret = new BcryptPasswordHasher().HashPassword("BLAHBLAHCAR")
                });
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals")]
        protected override void Seed(ManahostManagerDAL context)
        {
            context.TestSet.Add(new Entity.Test() { TestStr = "TADA !! C'est ok" });

            ServicesSeed(context);
            Client VIP;
            Client VIP2;
            Client DISABLE;
            Client Admin;
            var manager = new ClientUserManager(new CustomUserStore(context));
            PhoneNumber phone1 = new PhoneNumber()
            {
                Phone = "0689761611",
                PhoneType = PhoneType.HOME
            };
            context.PhoneNumberSet.Add(phone1);
            VIP = new Entity.Client()
            {
                IsManager = true,
                Email = "contact@manahost.fr",
                AcceptMailing = false,
                Civility = "Mr",
                Country = "Faguo",
                DateBirth = DateTime.MinValue,
                DateCreation = DateTime.MinValue,
                FirstName = "Wang",
                LastName = "Linlin",
                DefaultHomeId = 1,
                Locale = "fr-FR",
                Timezone = 8,
                InitManager = true,
                TutorialManager = true,
                PrincipalPhone = phone1
            };
            manager.Create(VIP, "TOTOTITi88$$");
            PhoneNumber phone2 = new PhoneNumber()
            {
                Phone = "0689761612",
                PhoneType = PhoneType.HOME
            };
            context.PhoneNumberSet.Add(phone2);
            DISABLE = new Entity.Client()
            {
                IsManager = true,
                Email = "contact4@manahost.fr",
                AcceptMailing = false,
                Civility = "Mr",
                Country = "Faguo",
                DateBirth = DateTime.MinValue,
                DateCreation = DateTime.MinValue,
                FirstName = "Wang",
                LastName = "Linlin",
                DefaultHomeId = 3,
                Locale = "fr-FR",
                Timezone = 8,
                InitManager = true,
                TutorialManager = true,
                PrincipalPhone = phone2
            };
            manager.Create(DISABLE, "TOTOTITi88$$");
            Home home2;
            context.HomeSet.Add(home2 = new Home()
            {
                Client = VIP,
                EstablishmentType = EEstablishmentType.BB,
                Title = "La Taverne",
                isDefault = true
            });
            Home home1;
            context.HomeSet.Add(home1 = new Home()
            {
                Client = VIP,
                EstablishmentType = EEstablishmentType.BB,
                Title = "LaCorderie",
                isDefault = false
            });
            PhoneNumber phone3 = new PhoneNumber()
            {
                Phone = "0689761613",
                PhoneType = PhoneType.HOME
            };
            context.PhoneNumberSet.Add(phone3);
            VIP2 = new Entity.Client()
            {
                IsManager = true,
                Email = "contact2@manahost.fr",
                AcceptMailing = false,
                Civility = "Mr",
                Country = "Faguo",
                DateBirth = DateTime.MinValue,
                DateCreation = DateTime.MinValue,
                FirstName = "Wang",
                LastName = "Linlin",
                Locale = "fr-FR",
                Timezone = 8,
                InitManager = true,
                TutorialManager = true,
                PrincipalPhone = phone3
            };
            manager.Create(VIP2, "TOTOTITi88$$");
            PhoneNumber phone4 = new PhoneNumber()
            {
                Phone = "0689761614",
                PhoneType = PhoneType.HOME
            };
            context.PhoneNumberSet.Add(phone4);
            Admin = new Entity.Client()
            {
                IsManager = true,
                Email = "contact3@manahost.fr",
                AcceptMailing = false,
                Civility = "Mr",
                Country = "Faguo",
                DateBirth = DateTime.MinValue,
                DateCreation = DateTime.MinValue,
                FirstName = "Wang",
                LastName = "Linlin",
                Locale = "fr-FR",
                Timezone = 8,
                InitManager = true,
                TutorialManager = true,
                PrincipalPhone = phone4
            };
            manager.Create(Admin, "TOTOTITi88$$");
            List<Payment> payments = new List<Payment>();
            Payment payment1;
            Payment payment2;
            context.PaymentSet.Add(payment1 = new Payment()
            {
                AccountName = "MyAccountName",
                Label = "Payment1",
                Price = 65.99M,
                RemoteIP = "127.0.0.1",
            });
            context.PaymentSet.Add(payment2 = new Payment()
            {
                AccountName = "MyAccountName",
                Label = "Payment2",
                Price = 65.99M,
                RemoteIP = "127.0.0.1",
            });
            payments.Add(payment1);
            payments.Add(payment2);
            context.SubscriptionSet.Add(new Subscription()
            {
                Client = VIP,
                ExpirationDate = DateTime.Now.AddYears(4),
                NumberOfRoom = 5,
                Payments = payments
            });
            People people1;
            People people2;
            People people3;
            context.PeopleSet.Add(people1 = new People()
            {
                AcceptMailing = true,
                Addr = "1 rue du dome",
                City = "Strasbourg",
                Civility = "Mme",
                Comment = null,
                Country = "FRANCE",
                DateBirth = DateTime.Now,
                DateCreation = DateTime.Now,
                Email = "contact@manahost.fr",
                Firstname = "TROLOLO",
                Home = home1,
                Lastname = "Frédéric",
                Mark = null,
                Phone1 = "0600000000",
                Phone2 = null,
                State = null,
                ZipCode = "67000",
                Hide = false
            });
            context.PeopleSet.Add(people3 = new People()
            {
                AcceptMailing = true,
                Addr = "3 rue austerlitz",
                City = "Strasbourg",
                Civility = "Mr",
                Comment = null,
                Country = "FRANCE",
                DateBirth = DateTime.Now,
                DateCreation = DateTime.Now,
                Email = "mass@mass.org",
                Firstname = "Planchin",
                Home = home1,
                Lastname = "Jacques",
                Mark = null,
                Phone1 = "0600000000",
                Phone2 = null,
                State = null,
                ZipCode = "67000",
                Hide = true
            });
            context.PeopleSet.Add(people2 = new People()
            {
                AcceptMailing = true,
                Addr = "4 place kleber",
                City = "Strasbourg",
                Civility = "Mr",
                Comment = "A mis le feu à la chambre",
                Country = "FRANCE",
                DateBirth = DateTime.Now,
                DateCreation = DateTime.Now,
                Email = "contact@manahost.fr",
                Firstname = "CHAABANE",
                Home = home1,
                Lastname = "Jalal",
                Mark = 0,
                Phone1 = "0600000000",
                Phone2 = null,
                State = null,
                ZipCode = "67000",
                Hide = false
            });
            DocumentCategory cat1;
            DocumentCategory cat2;
            context.DocumentCategorySet.Add(cat1 = new DocumentCategory()
            {
                Home = home1,
                Title = "Cat1"
            });
            context.DocumentCategorySet.Add(cat2 = new DocumentCategory()
            {
                Home = home1,
                Title = "Cat2"
            });
            List<Document> documents = new List<Document>();
            List<Document> documents2 = new List<Document>();
            Document doc1;
            Document doc2;
            Document doc3;
            Document doc4;
            context.DocumentSet.Add(doc1 = new Document()
            {
                DateUpload = DateTime.Now,
                DocumentCategory = cat1,
                Home = home1,
                IsPrivate = false,
                Title = "Doc1",
                Url = "http://blabla.com"
            });
            context.DocumentSet.Add(doc2 = new Document()
            {
                DateUpload = DateTime.MinValue,
                DocumentCategory = cat1,
                Home = home1,
                IsPrivate = false,
                Title = "Doc2",
                Url = "http://blabla2.com"
            });
            context.DocumentSet.Add(doc3 = new Document()
            {
                DateUpload = DateTime.Now,
                DocumentCategory = cat2,
                Home = home1,
                IsPrivate = true,
                Title = "Doc3",
                Url = "http://blabla3.com",
            });
            context.DocumentSet.Add(doc4 = new Document()
            {
                DateUpload = DateTime.Now,
                DocumentCategory = null,
                Home = home1,
                IsPrivate = true,
                Title = "Doc4",
                Url = "http://blabla3.com",
            });
            documents.Add(doc1);
            documents.Add(doc2);
            documents2.Add(doc2);
            MailConfig mailconfig2;
            context.MailConfigSet.Add(mailconfig2 = new MailConfig()
            {
                Email = "manahost_test@yahoo.com",
                Home = home1,
                IsSSL = true,
                Smtp = "smtp.mail.yahoo.com",
                SmtpPort = 25,
                Title = "Yahoo"
            });
            HomeConfig config;
            context.HomeConfigSet.Add(config = new HomeConfig()
            {
                AutoSendSatisfactionEmail = true,
                DefaultCaution = 500M,
                DefaultValueType = EValueType.AMOUNT,
                DepositNotifEnabled = true,
                DinnerCapacity = 15,
                EnableDinner = true,
                EnableDisplayActivities = true,
                EnableDisplayMeals = true,
                EnableDisplayProducts = true,
                EnableDisplayRooms = true,
                EnableReferencing = true,
                FollowStockEnable = true,
                Home = home1,
                Devise = "$",
                DefaultMailConfig = mailconfig2,
                HourFormat24 = true,
                DefaultHourCheckIn = 14 * 60,
                DefaultHourCheckOut = 12 * 60
            });
            MailConfig mailconfig1;
            context.MailConfigSet.Add(mailconfig1 = new MailConfig()
            {
                Email = "contact@manahost.fr",
                Home = home1,
                Title = "myemail",
                SmtpPort = 443,
                Smtp = "http://blablamail.com",
                IsSSL = true
            });
            Tax tax1;
            Tax tax2;
            context.TaxSet.Add(tax1 = new Tax()
            {
                Home = home1,
                Title = "Tax1",
                Price = 30M,
                ValueType = EValueType.PERCENT
            });
            context.TaxSet.Add(tax2 = new Tax()
            {
                Home = home1,
                Title = "Tax2",
                Price = 30M,
                ValueType = EValueType.AMOUNT
            });
            Supplier sup1;
            context.SupplierSet.Add(sup1 = new Supplier()
            {
                SocietyName = "Nongfu",
                DateCreation = DateTime.Now,
                Email = "nongfu@nongfu.cn",
                Home = home1,
                Hide = false
            });
            RoomCategory rcat1;
            RoomCategory rcat2;
            context.RoomCategorySet.Add(rcat1 = new RoomCategory()
            {
                Home = home1,
                Title = "Cat1",
                RefHide = true
            });
            context.RoomCategorySet.Add(rcat2 = new RoomCategory()
            {
                Home = home1,
                Title = "Cat2",
                RefHide = false
            });
            Room r1;
            Room r2;
            context.RoomSet.Add(r1 = new Room()
            {
                Home = home1,
                Caution = 500M,
                Classification = "3 epis",
                Color = 0x00FFFF,
                Description = "I'm a description",
                IsClosed = false,
                Title = "Melanie",
                RoomCategory = rcat1,
                RoomState = "RAS",
                Size = 42,
                Capacity = 3,
                Hide = false,
                RefHide = true
            });
            context.RoomSet.Add(r2 = new Room()
            {
                Home = home1,
                Classification = "",
                Color = 0xFF0000,
                Description = "I'm a description",
                IsClosed = false,
                Title = "Georges",
                RoomCategory = rcat2,
                RoomState = "RAS",
                Capacity = 2,
                Hide = false,
                RefHide = true
            });
            Document dr1;
            context.DocumentSet.Add(dr1 = new Document()
            {
                DateUpload = DateTime.Now,
                Hide = false,
                Home = home1,
                IsPrivate = false,
                Title = "PictureR1_1",
                Url = "http://myurl.fr"
            });
            ProductCategory p1;
            ProductCategory p2;
            context.ProductCategorySet.Add(p1 = new ProductCategory()
            {
                Home = home1,
                Title = "Category1",
                RefHide = false
            });
            context.ProductCategorySet.Add(p2 = new ProductCategory()
            {
                Home = home1,
                Title = "Category2",
                RefHide = false
            });
            Product prod1;
            Product prod2;
            context.ProductSet.Add(prod1 = new Product()
            {
                Home = home1,
                Title = "<span style='background-color:red'>Massage</span>",
                PriceHT = 300M,
                ProductCategory = p1,
                Stock = 1,
                Tax = tax1,
                RefHide = false,
                Hide = false,
                IsUnderThreshold = false
            });
            context.ProductSet.Add(prod2 = new Product()
            {
                Home = home1,
                Title = "Vin",
                Supplier = sup1,
                PriceHT = 300M,
                ProductCategory = p2,
                Stock = 10,
                Tax = tax2,
                Threshold = 3,
                RefHide = false,
                Hide = false,
                IsUnderThreshold = false
            });
            Bed b1;
            Bed b2;
            context.BedSet.Add(b1 = new Bed()
            {
                Home = home1,
                NumberPeople = 2,
                Room = r1
            });
            context.BedSet.Add(b2 = new Bed()
            {
                Home = home1,
                NumberPeople = 1,
                Room = r1
            });
            RoomSupplement rs1;
            RoomSupplement rs2;
            context.RoomSupplementSet.Add(rs1 = new RoomSupplement()
            {
                Home = home1,
                Title = "supp1",
                PriceHT = 78M,
                Tax = tax1,
                Hide = false
            });
            context.RoomSupplementSet.Add(rs2 = new RoomSupplement()
            {
                Home = home1,
                Title = "supp2",
                PriceHT = 199800M,
                Tax = tax2,
                Hide = false
            });
            Bill bill1;
            Bill bill2;
            context.BillSet.Add(bill1 = new Bill()
            {
                CreationDate = DateTime.Now,
                Document = doc1,
                Home = home1,
                IsPayed = false,
                Reference = "B14-0001",
                TotalTTC = -550M,
                TotalHT = -500M,
                Supplier = sup1
            });
            Booking booking1;
            context.BookingSet.Add(booking1 = new Booking()
            {
                Comment = "I am a comment",
                DateArrival = DateTime.Now.AddYears(1),
                DateCreation = DateTime.Now,
                DateDeparture = DateTime.Now.AddYears(1).AddDays(4),
                DateDesiredPayment = DateTime.Now.AddYears(1).AddMonths(1),
                DateModification = DateTime.Now,
                DateValidation = DateTime.Now.AddMonths(4),
                Home = home1,
                IsOnline = false,
                IsSatisfactionSended = false,
                People = people1,
                TotalPeople = 4
            });
            context.BillSet.Add(bill2 = new Bill()
            {
                CreationDate = DateTime.Now,
                Document = doc2,
                Home = home1,
                IsPayed = true,
                Reference = "B14-0002",
                TotalTTC = 650M,
                TotalHT = 500M,
                Booking = booking1
            });
            BillItemCategory bcat1;
            BillItemCategory bcat2;
            context.BillItemCategorySet.Add(bcat1 = new BillItemCategory()
            {
                Home = home1,
                Title = "Room"
            });
            context.BillItemCategorySet.Add(bcat2 = new BillItemCategory()
            {
                Home = home1,
                Title = "Product"
            });
            GroupBillItem gbillitem;
            context.GroupBillItemSet.Add(gbillitem = new GroupBillItem()
            {
                Home = home1,
                Discount = 30M,
                ValueType = EValueType.AMOUNT
            });
            BillItem item1;
            BillItem item2;
            BillItem item3;
            context.BillItemSet.Add(item1 = new BillItem()
            {
                Home = home1,
                Bill = bill2,
                BillItemCategory = bcat1,
                Description = "Booking room1 for 1001 nights",
                GroupBillItem = gbillitem,
                Title = "Room1",
                PriceHT = 999M,
                Quantity = 1001,
                PriceTTC = 1009M
            });
            context.BillItemSet.Add(item2 = new BillItem()
            {
                Home = home1,
                Bill = bill2,
                BillItemCategory = bcat1,
                Description = "Booking room2 for 1001 nights",
                GroupBillItem = gbillitem,
                Title = "Room2",
                PriceHT = 589M,
                Quantity = 1001,
                PriceTTC = 600M
            });
            context.BillItemSet.Add(item3 = new BillItem()
            {
                Home = home1,
                Bill = bill2,
                BillItemCategory = bcat2,
                Description = "Alcool de riz",
                GroupBillItem = gbillitem,
                Title = "Alcool de clodo",
                PriceHT = 74158M,
                Quantity = 3,
                PriceTTC = 8000M
            });
            BookingStepConfig stepConfig;
            context.BookingStepConfigSet.Add(stepConfig = new BookingStepConfig()
            {
                Home = home1,
                Title = "Room Steps"
            });
            BookingStep step1 = null;
            BookingStep step2 = null;
            BookingStep step3;
            context.BookingStepSet.Add(step3 = new BookingStep()
            {
                Home = home1,
                BookingStepConfig = stepConfig,
                Title = "VALIDATE",
                BookingValidated = true,
                BookingArchived = false
            });
            context.BookingStepSet.Add(step2 = new BookingStep()
            {
                Home = home1,
                BookingStepConfig = stepConfig,
                BookingStepNext = step3,
                Title = "WAITING_PAYMENT",
                BookingArchived = false,
                BookingValidated = false
            });
            Document docTemplate = new Document()
            {
                DateUpload = DateTime.Now,
                Hide = false,
                Home = home1,
                IsPrivate = false,
                Title = "MailTemplate",
                Url = "myurl"
            };
            context.DocumentSet.Add(docTemplate);
            context.BookingStepSet.Add(step1 = new BookingStep()
            {
                Home = home1,
                BookingStepConfig = stepConfig,
                BookingStepNext = step2,
                Title = "WAITING_CONFIRMATION",
                BookingArchived = false,
                BookingValidated = false,
                MailTemplate = docTemplate
            });
            Booking booking2;
            context.BookingSet.Add(booking2 = new Booking()
            {
                Comment = "I am a comment",
                DateArrival = DateTime.Now.AddYears(4),
                DateCreation = DateTime.Now,
                DateDeparture = DateTime.Now.AddYears(8),
                DateDesiredPayment = DateTime.Now.AddYears(1).AddMonths(10),
                DateModification = DateTime.Now,
                DateValidation = DateTime.Now.AddMonths(18),
                Home = home1,
                IsOnline = false,
                IsSatisfactionSended = false,
                People = people1,
                TotalPeople = 4
            });
            MailLog maillog;
            context.MailLogSet.Add(maillog = new MailLog()
            {
                Home = home1,
                Successful = true,
            });
            BookingStepBooking stepbooking;
            context.BookingStepBookingSet.Add(stepbooking = new BookingStepBooking()
            {
                Booking = booking2,
                BookingStepConfig = stepConfig,
                CurrentStep = step1,
                Home = home1,
                DateCurrentStepChanged = DateTime.Now,
                MailSent = 0,
                MailLog = maillog,
                Canceled = false
            });
            KeyGenerator key1;
            KeyGenerator key2;
            KeyGenerator key3;
            KeyGenerator key4;
            context.KeyGeneratorSet.Add(key1 = new KeyGenerator()
            {
                Key = Guid.NewGuid().ToString(),
                KeyType = EKeyType.BETA,
                DateExp = DateTime.Now,
            });
            context.KeyGeneratorSet.Add(key2 = new KeyGenerator()
            {
                KeyType = EKeyType.CLIENT,
                Home = home1,
                Key = Guid.NewGuid().ToString(),
                DateExp = DateTime.Now.AddMonths(4),
                Price = -15M,
                ValueType = EValueType.PERCENT
            });
            context.KeyGeneratorSet.Add(key3 = new KeyGenerator()
            {
                KeyType = EKeyType.CLIENT,
                Home = home1,
                Key = Guid.NewGuid().ToString(),
                DateExp = DateTime.Now.AddMonths(2),
                Price = -30M,
                ValueType = EValueType.AMOUNT
            });
            context.KeyGeneratorSet.Add(key4 = new KeyGenerator()
            {
                KeyType = EKeyType.MANAHOST,
                Key = Guid.NewGuid().ToString(),
                DateExp = DateTime.Now.AddDays(14),
                Price = -5M,
                ValueType = EValueType.PERCENT
            });
            MealCategory mc1;
            MealCategory mc2;
            context.MealCategorySet.Add(mc1 = new MealCategory()
            {
                Home = home1,
                Label = "meal category 1"
            });
            context.MealCategorySet.Add(mc2 = new MealCategory()
            {
                Home = home1,
                Label = "meal category 2"
            });
            Meal meal1;
            Meal meal2;
            Meal meal3;
            context.MealSet.Add(meal1 = new Meal()
            {
                Description = "Description of meal <p>Meal1</p>",
                Home = home1,
                Title = "Meal1",
                MealCategory = mc1
            });
            context.MealSet.Add(meal2 = new Meal()
            {
                Description = "Description of meal <p>Meal2</p>",
                Home = home1,
                Title = "Meal2",
                MealCategory = mc2
            });
            context.MealSet.Add(meal3 = new Meal()
            {
                Description = "Description of meal <p>Meal3</p>",
                Home = home1,
                Title = "Meal3",
                MealCategory = mc1
            });
            PaymentType pt1;
            PaymentType pt2;
            context.PaymentTypeSet.Add(pt1 = new PaymentType()
            {
                Home = home1,
                Title = "CASH"
            });
            context.PaymentTypeSet.Add(pt2 = new PaymentType()
            {
                Home = home1,
                Title = "CB"
            });
            PaymentMethod met1;
            PaymentMethod met2;
            context.PaymentMethodSet.Add(met1 = new PaymentMethod()
            {
                Home = home1,
                Bill = bill2,
                PaymentType = pt1,
                Price = 400M
            });
            context.PaymentMethodSet.Add(met2 = new PaymentMethod()
            {
                Home = home1,
                Bill = bill2,
                PaymentType = pt2,
                Price = 250M
            });
            PeopleCategory peoplecat1;
            PeopleCategory peoplecat2;
            context.PeopleCategorySet.Add(peoplecat1 = new PeopleCategory()
            {
                Home = home1,
                Label = "Adultes",
                Tax = tax1
            });
            context.PeopleCategorySet.Add(peoplecat2 = new PeopleCategory()
            {
                Home = home1,
                Label = "Enfants",
                Tax = tax2
            });
            Period period0;
            context.PeriodSet.Add(period0 = new Period()
            {
                Home = home1,
                Begin = new DateTime(1970, 1, 1),
                End = new DateTime(1970, 4, 1),
                IsClosed = false,
                Title = "Haute saison",
                Days = 1 | 2 | 4 | 8 | 16 | 32 | 64
            });
            Period period1;
            Period period4;
            Period period2;
            Period period3;
            Period period5;
            context.PeriodSet.Add(period1 = new Period()
            {
                Home = home1,
                Begin = new DateTime(2015, 1, 1, 0, 0, 0),
                End = new DateTime(2015, 3, 1, 23, 59, 59),
                IsClosed = false,
                Title = "Haute saison",
                Days = 1 | 2 | 4 | 8 | 16 | 32 | 64
            });
            context.PeriodSet.Add(period2 = new Period()
            {
                Home = home1,
                Begin = new DateTime(2015, 3, 2, 0, 0, 0),
                End = new DateTime(2015, 3, 8, 23, 59, 59),
                IsClosed = true,
                Title = "Vacances !",
                Days = 1 | 2 | 4 | 8 | 16 | 32 | 64
            });
            context.PeriodSet.Add(period3 = new Period()
            {
                Begin = new DateTime(2015, 3, 9, 0, 0, 0),
                End = new DateTime(2015, 12, 31, 23, 59, 59),
                Days = 1 | 2 | 4 | 8 | 16,
                IsClosed = false,
                Title = "Remaining",
                Home = home1
            });
            context.PeriodSet.Add(period4 = new Period()
            {
                Begin = new DateTime(2015, 3, 9, 0, 0, 0),
                End = new DateTime(2015, 9, 30, 23, 59, 59),
                Days = 32 | 64,
                IsClosed = false,
                Title = "Remaining week-ends",
                Home = home1
            });
            context.PeriodSet.Add(period5 = new Period()
            {
                Begin = new DateTime(2015, 10, 1, 0, 0, 0),
                End = new DateTime(2015, 12, 31, 23, 59, 59),
                Days = 32 | 64,
                IsClosed = true,
                Title = "Week-end vacances !",
                Home = home1
            });
            PricePerPerson ppp1;
            PricePerPerson ppp2;
            PricePerPerson ppp3;
            PricePerPerson ppp4;
            PricePerPerson ppp5;
            PricePerPerson ppp6;
            PricePerPerson ppp7;
            PricePerPerson ppp8;
            PricePerPerson ppp9;
            PricePerPerson ppp10;
            PricePerPerson ppp11;
            PricePerPerson ppp12;
            context.PricePerPersonSet.Add(ppp1 = new PricePerPerson()
            {
                Home = home1,
                Title = "Prix adulte room1 haute saison",
                PeopleCategory = peoplecat1,
                Period = period1,
                Room = r1,
                PriceHT = 80M,
                Tax = tax1
            });
            context.PricePerPersonSet.Add(ppp2 = new PricePerPerson()
            {
                Home = home1,
                Title = "Prix adulte room2 haute saison",
                PeopleCategory = peoplecat1,
                Period = period1,
                Room = r2,
                PriceHT = 160M,
                Tax = tax1
            });
            context.PricePerPersonSet.Add(ppp3 = new PricePerPerson()
            {
                Home = home1,
                Title = "Prix enfant room2 haute saison",
                PeopleCategory = peoplecat2,
                Period = period1,
                Room = r2,
                PriceHT = 110M,
                Tax = tax1
            });
            context.PricePerPersonSet.Add(ppp4 = new PricePerPerson()
            {
                Home = home1,
                Title = "Prix enfant room1 haute saison",
                PeopleCategory = peoplecat2,
                Period = period1,
                Room = r1,
                PriceHT = 60M,
                Tax = tax1
            });
            context.PricePerPersonSet.Add(ppp5 = new PricePerPerson()
            {
                Home = home1,
                Title = "Prix enfant room1 reste de l'année",
                PeopleCategory = peoplecat2,
                Period = period3,
                Room = r1,
                PriceHT = 40M,
                Tax = tax1
            });
            context.PricePerPersonSet.Add(ppp6 = new PricePerPerson()
            {
                Home = home1,
                Title = "Prix enfant room2 reste de l'année",
                PeopleCategory = peoplecat2,
                Period = period3,
                Room = r2,
                PriceHT = 55M,
                Tax = tax1
            });
            context.PricePerPersonSet.Add(ppp7 = new PricePerPerson()
            {
                Home = home1,
                Title = "Prix adulte room2 reste de l'année",
                PeopleCategory = peoplecat1,
                Period = period3,
                Room = r2,
                PriceHT = 69.9M,
                Tax = tax1
            });
            context.PricePerPersonSet.Add(ppp8 = new PricePerPerson()
            {
                Home = home1,
                Title = "Prix adulte room1 reste de l'année",
                PeopleCategory = peoplecat1,
                Period = period3,
                Room = r1,
                PriceHT = 59.78M,
                Tax = tax1
            });
            context.PricePerPersonSet.Add(ppp9 = new PricePerPerson()
            {
                Home = home1,
                Title = "Prix adulte room2 reste de l'année week-end",
                PeopleCategory = peoplecat1,
                Period = period4,
                Room = r2,
                PriceHT = 89.9M,
                Tax = tax1
            });
            context.PricePerPersonSet.Add(ppp10 = new PricePerPerson()
            {
                Home = home1,
                Title = "Prix adulte room1 reste de l'année week-end",
                PeopleCategory = peoplecat1,
                Period = period4,
                Room = r1,
                PriceHT = 79.78M,
                Tax = tax1
            });
            context.PricePerPersonSet.Add(ppp11 = new PricePerPerson()
            {
                Home = home1,
                Title = "Prix enfant room1 reste de l'année week-end",
                PeopleCategory = peoplecat2,
                Period = period4,
                Room = r1,
                PriceHT = 40M,
                Tax = tax1
            });
            context.PricePerPersonSet.Add(ppp12 = new PricePerPerson()
            {
                Home = home1,
                Title = "Prix enfant room2 reste de l'année week-end",
                PeopleCategory = peoplecat2,
                Period = period4,
                Room = r2,
                PriceHT = 55M,
                Tax = tax1
            });
            MealPrice mp1;
            MealPrice mp2;
            MealPrice mp3;
            context.MealPriceSet.Add(mp1 = new MealPrice()
            {
                Meal = meal1,
                PeopleCategory = peoplecat1,
                PriceHT = 15M,
                Tax = tax1,
                Home = home1
            });
            context.MealPriceSet.Add(mp2 = new MealPrice()
            {
                Meal = meal1,
                PeopleCategory = peoplecat2,
                PriceHT = 11M,
                Tax = tax1,
                Home = home1
            });
            context.MealPriceSet.Add(mp3 = new MealPrice()
            {
                Meal = meal2,
                PeopleCategory = peoplecat2,
                PriceHT = 6M,
                Tax = tax1,
                Home = home1
            });
            MStatistics stat1;
            MStatistics stat2;
            MStatistics stat3;
            MStatistics stat4;
            MStatistics stat5;
            MStatistics stat6;
            MStatistics stat7;
            context.StatisticsSet.Add(stat1 = new MStatistics()
            {
                Date = DateTime.Now,
                Home = home1,
                Type = StatisticsTypes.SATISFACTION,
                Data = "{\"Q\":[\"Le séjour vous a t-il plu ?\",\"Notez sur 5 votre satisfaction\",\"Conseillerez-vous à vos proche notre établissement ?\"],\"A\":[[3],[0,1,1,0,2],[2]],\"T\":4}"
            });
            context.StatisticsSet.Add(stat2 = new MStatistics()
            {
                Date = DateTime.Now.AddMonths(1),
                Home = home1,
                Type = StatisticsTypes.SATISFACTION,
                Data = "{\"Q\":[\"Le séjour vous a t-il plu ?\",\"Notez sur 5 votre satisfaction\",\"Conseillerez-vous à vos proche notre établissement ?\"],\"A\":[[7],[0,3,0,6,2],[5]],\"T\":11}"
            });
            context.StatisticsSet.Add(stat3 = new MStatistics()
            {
                Date = DateTime.Now.AddMonths(2),
                Home = home1,
                Type = StatisticsTypes.SATISFACTION,
                Data = "{\"Q\":[\"Le séjour vous a t-il plu ?\",\"Notez sur 5 votre satisfaction\",\"Conseillerez-vous à vos proche notre établissement ?\"],\"A\":[[5],[1,0,0,2,3],[5]],\"T\":6}"
            });
            context.StatisticsSet.Add(stat4 = new MStatistics()
            {
                Date = DateTime.Now,
                Home = home1,
                Type = StatisticsTypes.BOOKING,
                Data = "{\"NumberCancelled\": 0, \"Online\": 0, \"TotalNight\": 58, \"TotalRes\": 25, \"TotalPeople\": 44, " +
                    "\"PeopleCategories\": [{\"Title\" : \"Enfants\", \"Number\": 10} , {\"Title\" : \"Adultes\", \"Number\": 34}]," +
                    "\"Products\": [{\"Title\": \"Massage\", \"Number\": 5}, {\"Title\": \"Choucroute\", \"Number\": 9}], \"TotalProducts\": 14," +
                    "\"Rooms\": [{\"Title\": \"Annabelle\", \"Days\": [4, 4, 4, 4, 4, 4, 4]}, {\"Title\": \"Corinne\", \"Days\": [4, 4, 2, 2, 1, 4, 4]}, {\"Title\": \"Cédric\", \"Days\": [1, 0, 0, 0, 0, 4, 4]}]" +
                    ", \"TotalRooms\": 86, \"Dinners\": [[3, 6, 3, 3, 1, 10, 10], [1, 3, 4, 0, 3, 10, 12], [3, 6, 7, 10, 15, 30, 28]], \"TotalDinners\": 168}"
            });
            context.StatisticsSet.Add(stat5 = new MStatistics()
            {
                Date = DateTime.Now.AddMonths(1),
                Home = home1,
                Type = StatisticsTypes.BOOKING,
                Data = "{\"NumberCancelled\": 3, \"Online\": 0, \"TotalNight\": 10, \"TotalRes\": 5, \"TotalPeople\": 9, " +
                        "\"PeopleCategories\": [{\"Title\" : \"Enfants\", \"Number\": 1} , {\"Title\" : \"Adultes\", \"Number\": 8}]," +
                        "\"Products\": [{\"Title\": \"Massage\", \"Number\": 5}, {\"Title\": \"Choucroute\", \"Number\": 9}], \"TotalProducts\": 14," +
                        "\"Rooms\": [{\"Title\": \"Annabelle\", \"Days\": [0, 0, 0, 0, 0, 2, 2]}, {\"Title\": \"Corinne\", \"Days\": [0, 0, 0, 0, 0, 3, 3]}, {\"Title\": \"Cédric\", \"Days\": [0, 0, 0, 0, 0, 0, 0]}]" +
                        ", \"TotalRooms\": 10, \"Dinners\": [[0, 0, 0, 0, 0, 3, 4], [0, 0, 0, 0, 0, 0, 0], [0, 0, 0, 0, 0, 0, 0]], \"TotalDinners\": 7}"
            });
            context.StatisticsSet.Add(stat6 = new MStatistics()
            {
                Date = DateTime.Now,
                Home = home1,
                Type = StatisticsTypes.ACCOUNTING,
                Data = "{\"Income\":3500, \"Outcome\": 1080," +
                    "Rooms : [{\"Title\": \"Annabelle\", \"Income\": 2800}, {\"Title\": \"Cédric\", \"Income\": 500}, {\"Title\": \"Cédric\", \"Income\": 2950}]," +
                    "Products : []," +
                    "Dinners : [0,0,0], Other: 0}"
            });
            context.StatisticsSet.Add(stat7 = new MStatistics()
            {
                Date = DateTime.Now.AddMonths(1),
                Home = home1,
                Type = StatisticsTypes.ACCOUNTING,
                Data = "{\"Income\":5500, \"Outcome\": 1080," +
                        "Rooms : [{\"Title\": \"Annabelle\", \"Income\": 2800}, {\"Title\": \"Cédric\", \"Income\": 500}, {\"Title\": \"Cédric\", \"Income\": 200}]," +
                        "Products : [{\"Title\": \"Massage\", \"Income\": 250}]," +
                        "Dinners : [750,253,599], Other: 250}"
            });
            FieldGroup gp1;
            context.FieldGroupSet.Add(gp1 = new FieldGroup()
            {
                Home = home1,
                Title = "MyFieldGroup"
            });
            PeopleField pe1;
            PeopleField pe2;
            PeopleField pe3;
            PeopleField pe4;
            context.PeopleFieldSet.Add(pe1 = new PeopleField()
            {
                FieldGroup = gp1,
                Key = "MyPersonnalField1",
                Value = "MyPersonnalValue1",
                People = null,
                Home = home1
            });
            context.PeopleFieldSet.Add(pe2 = new PeopleField()
            {
                FieldGroup = gp1,
                Key = "MyPersonnalField2",
                Value = "MyPersonnalValue2",
                People = null,
                Home = home1
            });
            context.PeopleFieldSet.Add(pe3 = new PeopleField()
            {
                FieldGroup = gp1,
                Key = "MyPersonnalField3",
                People = people1,
                Value = "MyPersonnalValue3",
                Home = home1
            });
            context.PeopleFieldSet.Add(pe4 = new PeopleField()
            {
                FieldGroup = gp1,
                Key = "MyPersonnalField4",
                People = people1,
                Value = "MyPersonnalValue4",
                Home = home1
            });
            SatisfactionConfig sconf1;
            context.SatisfactionConfigSet.Add(sconf1 = new SatisfactionConfig()
            {
                Description = "Veuillez trouver ici présent un petit questionnaire<br/>Merci d'y répondre :<br/>",
                Home = home1,
                Title = "LaCorderie / Enquête de satisfaction"
            });
            SatisfactionConfigQuestion squestion1;
            SatisfactionConfigQuestion squestion2;
            SatisfactionConfigQuestion squestion3;
            SatisfactionConfigQuestion squestion4;
            context.SatisfactionConfigQuestionSet.Add(squestion1 = new SatisfactionConfigQuestion()
            {
                AnswerType = EAnswerType.RADIO,
                Home = home1,
                Question = "Le séjour vous a t-il plu ?",
                SatisfactionConfig = sconf1
            });
            context.SatisfactionConfigQuestionSet.Add(squestion2 = new SatisfactionConfigQuestion()
            {
                AnswerType = EAnswerType.NUMBER,
                Home = home1,
                Question = "Notez sur 5 votre satisfaction",
                SatisfactionConfig = sconf1
            });
            context.SatisfactionConfigQuestionSet.Add(squestion3 = new SatisfactionConfigQuestion()
            {
                AnswerType = EAnswerType.RADIO,
                Home = home1,
                Question = "Conseillerez-vous à vos proche notre établissement ?",
                SatisfactionConfig = sconf1
            });
            context.SatisfactionConfigQuestionSet.Add(squestion4 = new SatisfactionConfigQuestion()
            {
                AnswerType = EAnswerType.DESC,
                Home = home1,
                Question = "Des détails ?",
                SatisfactionConfig = sconf1
            });
            SatisfactionClient sclient;
            context.SatisfactionClientSet.Add(sclient = new SatisfactionClient()
            {
                Booking = booking2,
                PeopleDest = people1,
                Code = "000000",
                DateAnswered = DateTime.Now,
                Home = home1
            });
            SatisfactionClientAnswer sanswer1;
            SatisfactionClientAnswer sanswer2;
            SatisfactionClientAnswer sanswer3;
            SatisfactionClientAnswer sanswer4;
            context.SatisfactionClientAnsweredSet.Add(sanswer1 = new SatisfactionClientAnswer()
            {
                Home = home1,
                Question = "Le séjour vous a t-il plu ?",
                AnswerType = EAnswerType.RADIO,
                Answer = "Y",
                SatisfactionClient = sclient
            });
            context.SatisfactionClientAnsweredSet.Add(sanswer2 = new SatisfactionClientAnswer()
            {
                Home = home1,
                Question = "Notez sur 5 votre satisfaction",
                AnswerType = EAnswerType.NUMBER,
                Answer = "4",
                SatisfactionClient = sclient
            });
            context.SatisfactionClientAnsweredSet.Add(sanswer3 = new SatisfactionClientAnswer()
            {
                Home = home1,
                Question = "Conseillerez-vous à vos proche notre établissement ?",
                AnswerType = EAnswerType.RADIO,
                Answer = "N",
                SatisfactionClient = sclient
            });
            context.SatisfactionClientAnsweredSet.Add(sanswer4 = new SatisfactionClientAnswer()
            {
                Home = home1,
                Question = "Des détails ?",
                AnswerType = EAnswerType.DESC,
                Answer = "<i>Mais oui bien sûr !</i>",
                SatisfactionClient = sclient
            });
            AdditionalBooking addbook;
            context.AdditionnalBookingSet.Add(addbook = new AdditionalBooking()
            {
                BillItemCategory = bcat1,
                Booking = booking2,
                Home = home1,
                PriceHT = 330M,
                PriceTTC = 380M,
                Title = "Jalal de chambre"
            });
            Document dbook;
            context.DocumentSet.Add(dbook = new Document()
            {
                IsPrivate = false,
                DateUpload = DateTime.Now,
                Hide = false,
                Url = "http://blablabooking.com",
                Title = "Contrat de location",
                Home = home1,
            });
            BookingDocument bdoc;
            context.BookingDocumentSet.Add(bdoc = new BookingDocument()
            {
                Booking = booking2,
                Document = dbook,
                Home = home1
            });
            Deposit deposit1;
            Deposit deposit2;
            context.DepositSet.Add(deposit1 = new Deposit()
            {
                Booking = booking2,
                DateCreation = DateTime.Now,
                Home = home1,
                Price = 147M,
                ValueType = EValueType.AMOUNT
            });
            context.DepositSet.Add(deposit2 = new Deposit()
            {
                Booking = booking2,
                DateCreation = DateTime.Now,
                Home = home1,
                Price = 10M,
                ValueType = EValueType.PERCENT
            });
            DinnerBooking dbook1;
            DinnerBooking dbook2;
            context.DinnerBookingSet.Add(dbook1 = new DinnerBooking()
            {
                Booking = booking2,
                Date = DateTime.Now.AddYears(4).AddHours(10),
                Home = home1,
                NumberOfPeople = 3,
                PriceHT = 35M,
                PriceTTC = 48M
            });
            context.DinnerBookingSet.Add(dbook2 = new DinnerBooking()
            {
                Booking = booking2,
                Date = DateTime.Now.AddYears(4).AddHours(18),
                Home = home1,
                NumberOfPeople = 2,
                PriceHT = 18M,
                PriceTTC = 27M
            });
            MealBooking mealb1;
            MealBooking mealb2;
            MealBooking mealb3;
            context.MealBookingSet.Add(mealb1 = new MealBooking()
            {
                DinnerBooking = dbook1,
                Home = home1,
                Meal = meal1,
                NumberOfPeople = 1,
            });
            context.MealBookingSet.Add(mealb2 = new MealBooking()
            {
                DinnerBooking = dbook1,
                Home = home1,
                Meal = meal2,
                NumberOfPeople = 1,
            });
            context.MealBookingSet.Add(mealb3 = new MealBooking()
            {
                DinnerBooking = dbook2,
                Home = home1,
                Meal = meal3,
                NumberOfPeople = 1,
            });
            ProductBooking pbook1;
            ProductBooking pbook2;
            context.ProductBookingSet.Add(pbook1 = new ProductBooking()
            {
                Booking = booking2,
                Date = DateTime.Now.AddYears(4).AddHours(1),
                Home = home1,
                PriceHT = 140M,
                PriceTTC = 158M,
                Product = prod1,
                Quantity = 2
            });
            pbook1.Duration = 3000;
            context.ProductBookingSet.Add(pbook2 = new ProductBooking()
            {
                Booking = booking2,
                Date = DateTime.Now.AddYears(4).AddHours(90),
                PriceHT = 22M,
                PriceTTC = 26M,
                Product = prod2,
                Quantity = 1,
                Home = home1
            });
            RoomBooking rbook1;
            RoomBooking rbook2;
            RoomBooking rbook3;
            RoomBooking rbook4;
            context.RoomBookingSet.Add(rbook1 = new RoomBooking()
            {
                Booking = booking2,
                Home = home1,
                PriceHT = 77M,
                PriceTTC = 83M,
                Room = r1,
                DateBegin = DateTime.Now.AddYears(4),
                DateEnd = DateTime.Now.AddYears(8),
                NbNights = 2
            });
            context.RoomBookingSet.Add(rbook2 = new RoomBooking()
            {
                Booking = booking2,
                Home = home1,
                PriceHT = 99M,
                PriceTTC = 107M,
                Room = r2,
                DateBegin = DateTime.Now.AddYears(4),
                DateEnd = DateTime.Now.AddYears(8),
                NbNights = 1
            });
            context.RoomBookingSet.Add(rbook3 = new RoomBooking()
            {
                Booking = booking1,
                Home = home1,
                PriceHT = 77M,
                PriceTTC = 83M,
                Room = r1,
                DateBegin = DateTime.Now.AddYears(1),
                DateEnd = DateTime.Now.AddYears(3),
                NbNights = 2
            });
            context.RoomBookingSet.Add(rbook4 = new RoomBooking()
            {
                Booking = booking1,
                Home = home1,
                PriceHT = 99M,
                PriceTTC = 107M,
                Room = r2,
                NbNights = 1,
                DateBegin = DateTime.Now.AddYears(1),
                DateEnd = DateTime.Now.AddYears(3),
            });
            PeopleBooking pebook1;
            PeopleBooking pebook2;
            PeopleBooking pebook3;
            context.PeopleBookingSet.Add(pebook1 = new PeopleBooking()
            {
                Home = home1,
                NumberOfPeople = 1,
                PeopleCategory = peoplecat2,
                RoomBooking = rbook1
            });
            context.PeopleBookingSet.Add(pebook2 = new PeopleBooking()
            {
                Home = home1,
                NumberOfPeople = 2,
                PeopleCategory = peoplecat1,
                RoomBooking = rbook2
            });
            context.PeopleBookingSet.Add(pebook3 = new PeopleBooking()
            {
                Home = home1,
                NumberOfPeople = 1,
                PeopleCategory = peoplecat1,
                RoomBooking = rbook1
            });
            SupplementRoomBooking supprbook1;
            context.SupplementRoomBookingSet.Add(supprbook1 = new SupplementRoomBooking()
            {
                Home = home1,
                PriceHT = 78M,
                PriceTTC = 79M,
                RoomSupplement = rs1,
                RoomBooking = rbook1
            });
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);
                var fullErrorMessage = string.Join("; ", errorMessages);
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
            step3.BookingStepPrevious = step2;
            step2.BookingStepPrevious = step1;
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);
                var fullErrorMessage = string.Join("; ", errorMessages);
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }
    }
}