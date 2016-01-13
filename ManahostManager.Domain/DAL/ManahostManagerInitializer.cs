using ManahostManager.Domain.Entity;
using ManahostManager.Domain.Tools;
using ManahostManager.Utils;
using Microsoft.AspNet.Identity;
using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;

namespace ManahostManager.Domain.DAL
{
    public class ManahostManagerInitializer : DropCreateDatabaseAlways<ManahostManagerDAL>
    {
        private ManahostManagerDAL context;

        #region ADDITIONALBOOKING

        private AdditionalBooking addbook;

        public void ServicesSeed()
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

        private void AdditionalBookingSeed()
        {
            context.AdditionnalBookingSet.Add(addbook = new AdditionalBooking()
            {
                BillItemCategory = bcat1,
                Booking = booking2,
                Home = home1,
                PriceHT = 330M,
                PriceTTC = 380M,
                Title = "Jalal de chambre"
            });
        }

        #endregion ADDITIONALBOOKING

        #region BED

        private void BedSeed()
        {
            Bed b1;
            Bed b2;
            Bed b3;
            Bed b4;
            Bed b5;

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

            context.BedSet.Add(b3 = new Bed()
            {
                Home = home1,
                NumberPeople = 2,
                Room = r2
            });
            context.BedSet.Add(b4 = new Bed()
            {
                Home = home1,
                NumberPeople = 2,
                Room = r3
            });
            context.BedSet.Add(b5 = new Bed()
            {
                Home = home1,
                NumberPeople = 1,
                Room = r4
            });
        }

        #endregion BED

        #region BILL

        private Bill bill1;
        private Bill bill2;

        private void BillSeed()
        {
            context.BillSet.Add(bill1 = new Bill()
            {
                CreationDate = DateTime.Now,
                Home = home1,
                IsPayed = false,
                Reference = "B14-0001",
                TotalTTC = -550M,
                TotalHT = -500M,
                Supplier = sup1
            });
            context.BillSet.Add(bill2 = new Bill()
            {
                CreationDate = DateTime.Now,
                Home = home1,
                IsPayed = true,
                Reference = "B14-0002",
                TotalTTC = 650M,
                TotalHT = 500M,
                Booking = booking1
            });
        }

        #endregion BILL

        #region BILLITEM

        private BillItem item1;
        private BillItem item2;
        private BillItem item3;

        private void BillItemSeed()
        {
            context.BillItemSet.Add(item1 = new BillItem()
            {
                Home = home1,
                Bill = bill2,
                BillItemCategory = bcat1,
                Description = "Réservation de 3 nuits, pas de casse ou de deterioration du materiel notable",
                GroupBillItem = gbillitem,
                Title = "Room1",
                PriceHT = 150M,
                Quantity = 3,
                PriceTTC = 180M
            });
            context.BillItemSet.Add(item2 = new BillItem()
            {
                Home = home1,
                Bill = bill2,
                BillItemCategory = bcat1,
                Description = "Réservation d'une nuit",
                GroupBillItem = gbillitem,
                Title = "Room2",
                PriceHT = 589M,
                Quantity = 30,
                PriceTTC = 35M
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
        }

        #endregion BILLITEM

        #region BILLITEMCATEGORY

        private BillItemCategory bcat1;
        private BillItemCategory bcat2;

        private void BillItemCategorySeed()
        {
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
        }

        #endregion BILLITEMCATEGORY

        #region BOOKING

        private Booking booking1;
        private Booking booking2;
        private Booking booking3;
        private Booking booking4;

        private void BookingSeed()
        {
            context.BookingSet.Add(booking1 = new Booking()
            {
                Comment = "Je viendrais le jour de la reservation, ma femme et mes enfants me rejoindront plus tard",
                DateArrival = DateTime.Now.AddDays(3),
                DateCreation = DateTime.Now.AddDays(1),
                DateDeparture = DateTime.Now.AddDays(5),
                DateDesiredPayment = DateTime.Now.AddDays(5),
                DateModification = DateTime.Now,
                DateValidation = DateTime.Now.AddDays(2),
                Home = home1,
                IsOnline = false,
                IsSatisfactionSended = false,
                People = people1,
                TotalPeople = 4
            });
            context.BookingSet.Add(booking2 = new Booking()
            {
                DateArrival = DateTime.Now.AddDays(2),
                DateCreation = DateTime.Now,
                DateDeparture = DateTime.Now.AddDays(4),
                DateDesiredPayment = DateTime.Now.AddDays(3),
                DateModification = DateTime.Now,
                DateValidation = DateTime.Now,
                Home = home1,
                IsOnline = false,
                IsSatisfactionSended = false,
                People = people2,
                TotalPeople = 4
            });
            context.BookingSet.Add(booking3 = new Booking()
            {
                DateArrival = DateTime.Now.AddDays(9),
                DateCreation = DateTime.Now,
                DateDeparture = DateTime.Now.AddDays(17),
                DateDesiredPayment = DateTime.Now.AddDays(17),
                DateModification = DateTime.Now,
                DateValidation = DateTime.Now.AddDays(3),
                Home = home1,
                IsOnline = false,
                IsSatisfactionSended = false,
                People = people3,
                TotalPeople = 4
            });
            context.BookingSet.Add(booking4 = new Booking()
            {
                Comment = "",
                DateArrival = DateTime.Now.AddDays(5),
                DateCreation = DateTime.Now,
                DateDeparture = DateTime.Now.AddDays(9),
                DateDesiredPayment = DateTime.Now.AddDays(9),
                DateModification = DateTime.Now,
                DateValidation = null,
                Home = home1,
                IsOnline = false,
                IsSatisfactionSended = false,
                People = people1,
                TotalPeople = 2
            });
        }

        #endregion BOOKING

        #region BOOKINGSTEP

        private BookingStep step1 = null;
        private BookingStep step2 = null;
        private BookingStep step3;
        private BookingStep step4;

        private void BookingStepSeed()
        {
            context.BookingStepSet.Add(step4 = new BookingStep()
            {
                Home = home1,
                BookingStepConfig = stepConfig,
                Title = "Réservation archivé",
                BookingValidated = true,
                BookingArchived = true
            });
            context.BookingStepSet.Add(step3 = new BookingStep()
            {
                Home = home1,
                BookingStepConfig = stepConfig,
                Title = "Réservation validé",
                BookingStepNext = step4,
                BookingValidated = true,
                BookingArchived = false
            });
            context.BookingStepSet.Add(step2 = new BookingStep()
            {
                Home = home1,
                BookingStepConfig = stepConfig,
                BookingStepNext = step3,
                Title = "Paiement en attente",
                BookingArchived = false,
                BookingValidated = false
            });
            context.BookingStepSet.Add(step1 = new BookingStep()
            {
                Home = home1,
                BookingStepConfig = stepConfig,
                BookingStepNext = step2,
                Title = "Réservation en attente",
                BookingArchived = false,
                BookingValidated = false,
                MailSubject = "Réservation mail waiting confirmation"
            });
        }

        private void BookingStepSetCompleteSeed()
        {
            step4.BookingStepPrevious = step3;
            step3.BookingStepPrevious = step2;
            step2.BookingStepPrevious = step1;
            step1.MailTemplate = mailTemplate;
        }

        #endregion BOOKINGSTEP

        #region BOOKINGSTEPBOOKING

        private BookingStepBooking stepbooking;
        private BookingStepBooking stepbooking2;
        private BookingStepBooking stepbooking3;

        private void BookingStepBookingSeed()
        {
            context.BookingStepBookingSet.Add(stepbooking2 = new BookingStepBooking()
            {
                Booking = booking1,
                BookingStepConfig = stepConfig,
                CurrentStep = step4,
                Home = home1,
                DateCurrentStepChanged = new DateTime(2015, 3, 19),
                MailSent = 0,
                MailLog = null,
                Canceled = false
            });
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
            context.BookingStepBookingSet.Add(stepbooking3 = new BookingStepBooking()
            {
                Booking = booking3,
                BookingStepConfig = stepConfig,
                CurrentStep = step1,
                Home = home1,
                DateCurrentStepChanged = DateTime.Now,
                MailSent = 0,
                MailLog = null,
                Canceled = false
            });
        }

        #endregion BOOKINGSTEPBOOKING

        #region BOOKINGSTEPCONFIG

        private BookingStepConfig stepConfig;
        private BookingStepConfig config3;

        private void BookingStepConfigSeed()
        {
            context.BookingStepConfigSet.Add(stepConfig = new BookingStepConfig()
            {
                Home = home1,
                Title = "Etapes de chambres"
            });
        }

        #endregion BOOKINGSTEPCONFIG

        #region DEPOSIT

        private Deposit deposit1;
        private Deposit deposit2;
        private Deposit deposit3;
        private Deposit deposit4;

        private void DepositSeed()
        {
            context.DepositSet.Add(deposit2 = new Deposit()
            {
                Booking = booking1,
                DateCreation = DateTime.Now,
                Home = home1,
                Price = 100M,
                ValueType = EValueType.PERCENT
            });
            context.DepositSet.Add(deposit1 = new Deposit()
            {
                Booking = booking2,
                DateCreation = DateTime.Now,
                Home = home1,
                Price = 56M,
                ValueType = EValueType.AMOUNT
            });
            context.DepositSet.Add(deposit3 = new Deposit()
            {
                Booking = booking3,
                DateCreation = DateTime.Now,
                Home = home1,
                Price = 30M,
                ValueType = EValueType.PERCENT
            });
            context.DepositSet.Add(deposit4 = new Deposit()
            {
                Booking = booking4,
                DateCreation = DateTime.Now,
                Home = home1,
                Price = 20M,
                ValueType = EValueType.PERCENT
            });
        }

        #endregion DEPOSIT

        #region DINNERBOOKING

        private DinnerBooking dbook1;
        private DinnerBooking dbook2;

        private void DinnerBookingSeed()
        {
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
        }

        #endregion DINNERBOOKING

        #region DOCUMENT

        private Document mailTemplate;
        private Document accessPlan;

        private void DocumentSeed()
        {
            context.DocumentSet.Add(mailTemplate = new Document()
            {
                Hide = false,
                Home = home1,
                IsPrivate = false,
                DocumentCategory = cat2,
                Title = "EtapeReservation_emailType1.tp",
                Url = "1\\Public\\MailTemplateTest.tp"
            });
            if (!Directory.Exists("C:\\inetpub\\UploadDev\\1\\Public"))
            {
                Directory.CreateDirectory("C:\\inetpub\\UploadDev\\1\\Public");
                Directory.CreateDirectory("C:\\inetpub\\UploadDev\\1\\Private");
            }
            File.WriteAllText("C:\\inetpub\\UploadDev\\1\\Public\\MailTemplateTest.tp", "<h3>Test Content</h3>");
            context.DocumentSet.Add(accessPlan = new Document()
            {
                Hide = false,
                Home = home1,
                IsPrivate = false,
                DocumentCategory = cat1,
                Title = "Access Plan.txt",
                Url = "1\\Public\\AccessPlan.txt"
            });
            File.WriteAllText("C:\\inetpub\\UploadDev\\1\\Public\\AccessPlan.txt", "1.<br/>2.<br/>3.<br/>4.<br/>");
        }

        #endregion DOCUMENT

        #region DOCUMENTCATEGORY

        private DocumentCategory cat1;
        private DocumentCategory cat2;

        private void DocumentCategorySeed()
        {
            context.DocumentCategorySet.Add(cat1 = new DocumentCategory()
            {
                Home = home1,
                Title = "Informations"
            });
            context.DocumentCategorySet.Add(cat2 = new DocumentCategory()
            {
                Home = home1,
                Title = "Document Manahost"
            });
        }

        #endregion DOCUMENTCATEGORY

        #region DOCUMENTLOG

        private DocumentLog Documentlog1;

        private void DocumentLogSeed()
        {
            context.DocumentLogSet.Add(Documentlog1 = new DocumentLog()
            {
                Client = VIP,
                CurrentSize = 0,
                ResourceConfig = config1,
                BuySize = 0
            });
        }

        #endregion DOCUMENTLOG

        #region CLIENT

        private Client VIP;
        private Client VIP2;
        private Client DISABLE;
        private Client Admin;

        private void ClientSeed()
        {
            PhoneNumber phone = new PhoneNumber()
            {
                Phone = "0689761612",
                PhoneType = PhoneType.HOME
            };
            context.PhoneNumberSet.Add(phone);
            VIP = new Entity.Client()
            {
                IsManager = true,
                Email = "contact@manahost.fr",
                EmailConfirmed = true,
                AcceptMailing = false,
                Civility = "Mr",
                Country = "France",
                DateBirth = new DateTime(1990, 05, 15),
                DateCreation = new DateTime(2014, 09, 11),
                FirstName = "Cyril",
                LastName = "Oberlin",
                DefaultHomeId = 1,
                Locale = "fr-FR",
                Timezone = -3,
                InitManager = true,
                TutorialManager = true,
                PrincipalPhone = phone
            };
            phone = new PhoneNumber()
            {
                Phone = "0689761612",
                PhoneType = PhoneType.HOME
            };
            context.PhoneNumberSet.Add(phone);
            DISABLE = new Entity.Client()
            {
                IsManager = true,
                Email = "GeraldM@hotmail.fr",
                EmailConfirmed = true,
                AcceptMailing = false,
                Civility = "Mr",
                Country = "France",
                DateBirth = new DateTime(1950, 10, 05),
                DateCreation = new DateTime(2014, 08, 21),
                FirstName = "Gerald",
                LastName = "Maçonnier",
                DefaultHomeId = 3,
                Locale = "fr-FR",
                Timezone = -3,
                InitManager = true,
                TutorialManager = true,
                PrincipalPhone = phone
            };
            phone = new PhoneNumber()
            {
                Phone = "0689761612",
                PhoneType = PhoneType.HOME
            };
            context.PhoneNumberSet.Add(phone);
            VIP2 = new Entity.Client()
            {
                IsManager = true,
                Email = "BrigMon@outlook.fr",
                EmailConfirmed = true,
                AcceptMailing = false,
                Civility = "Mme",
                Country = "France",
                DateBirth = new DateTime(1955, 06, 11),
                DateCreation = new DateTime(2014, 09, 13),
                FirstName = "Brigitte",
                LastName = "Monvent",
                Locale = "fr-FR",
                Timezone = -3,
                InitManager = true,
                TutorialManager = true,
                PrincipalPhone = phone
            };
            phone = new PhoneNumber()
            {
                Phone = "0689761612",
                PhoneType = PhoneType.HOME
            };
            context.PhoneNumberSet.Add(phone);
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
                Timezone = -3,
                InitManager = true,
                TutorialManager = true,
                PrincipalPhone = phone
            };
            //var manager = ClientUserManager.Create(null, context);
            var manager = new ClientUserManager(new CustomUserStore(context));
            manager.Create(Admin, "TOTOTITi88$$");
            manager.Create(VIP, "TOTOTITi88$$");
            manager.Create(VIP2, "TOTOTITi88$$");
            manager.Create(DISABLE, "TOTOTITi88$$");
            //RoleManager<CustomRole, int> managerGroup = ClientRoleManager.Create(null, context);
            ClientRoleManager managerGroup = new ClientRoleManager(new CustomRoleStore(context));
            managerGroup.Create(new CustomRole(GenericNames.ADMINISTRATOR));
            managerGroup.Create(new CustomRole(GenericNames.REGISTERED_VIP));
            managerGroup.Create(new CustomRole(GenericNames.VIP));
            managerGroup.Create(new CustomRole(GenericNames.DISABLED));
            managerGroup.Create(new CustomRole(GenericNames.MANAGER));
            manager.AddToRoles(VIP.Id, new string[] { GenericNames.REGISTERED_VIP, GenericNames.VIP, GenericNames.MANAGER });
        }

        #endregion CLIENT

        #region GROUPBILLITEM

        private GroupBillItem gbillitem;

        private void GroupBillItemSeed()
        {
            context.GroupBillItemSet.Add(gbillitem = new GroupBillItem()
            {
                Home = home1,
                Discount = 30M,
                ValueType = EValueType.AMOUNT
            });
        }

        #endregion GROUPBILLITEM

        #region FieldGroup

        private FieldGroup gp1;

        private void FieldGroupSeed()
        {
            context.FieldGroupSet.Add(gp1 = new FieldGroup()
            {
                Home = home1,
                Title = "MyFieldGroup"
            });
        }

        #endregion FieldGroup

        #region HOME

        private Home home2;
        private Home home1;

        private void HomeSeed()
        {
            context.HomeSet.Add(home1 = new Home()
            {
                Client = VIP,
                EstablishmentType = EEstablishmentType.BB,
                Title = "La Corderie",
                EncryptionPassword = "loliProutoutouRito",
                isDefault = true
            });
            context.HomeSet.Add(home2 = new Home()
            {
                Client = VIP,
                EstablishmentType = EEstablishmentType.BB,
                Title = "La Taverne",
                EncryptionPassword = "EveryoneLoveNicolas",
                isDefault = false
            });
        }

        #endregion HOME

        #region HOMECONFIG

        private HomeConfig config;

        private void HomeConfigSeed()
        {
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
                Devise = "€",
                DefaultMailConfig = mailconfig2,
                HourFormat24 = true,
                DefaultHourCheckIn = 14 * 60,
                DefaultHourCheckOut = 12 * 60
            });
        }

        #endregion HOMECONFIG

        #region KEYGENERATOR

        private KeyGenerator key1;
        private KeyGenerator key2;
        private KeyGenerator key3;
        private KeyGenerator key4;

        private void KeyGeneratorSeed()
        {
            context.KeyGeneratorSet.Add(key1 = new KeyGenerator()
            {
                Key = Guid.NewGuid().ToString(),
                KeyType = EKeyType.BETA,
                DateExp = DateTime.Now.AddMonths(6)
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
        }

        #endregion KEYGENERATOR

        #region MAILCONFIG

        private MailConfig mailconfig2;
        private MailConfig mailconfig1;

        private void MailConfigSeed()
        {
            context.MailConfigSet.Add(mailconfig1 = new MailConfig()
            {
                Email = "contact@manahost.fr",
                Home = home1,
                Title = "myemail",
                SmtpPort = 443,
                Smtp = "nosmtp_go_see_mailconfig2",
                IsSSL = true
            });
            context.MailConfigSet.Add(mailconfig2 = new MailConfig()
            {
                Email = "manahost_test@yahoo.com",
                Home = home1,
                IsSSL = true,
                Smtp = "smtp.mail.yahoo.com",
                SmtpPort = 25,
                Title = "Yahoo"
            });
        }

        #endregion MAILCONFIG

        #region MAILLOG

        private MailLog maillog;

        private void MailLogSeed()
        {
            context.MailLogSet.Add(maillog = new MailLog()
            {
                Home = home1,
                Successful = true,
                To = "contact@manahost.fr,contact2@manahost.fr",
                DateSended = DateTime.Now
            });
        }

        #endregion MAILLOG

        #region MEAL

        private Meal meal1;
        private Meal meal2;
        private Meal meal3;

        private void MealSeed()
        {
            context.MealSet.Add(meal1 = new Meal()
            {
                Description = "Croissant fait-maison à la française",
                Home = home1,
                Title = "Croissant",
                MealCategory = mc1,
                RefHide = true,
                Hide = false
            });
            context.MealSet.Add(meal1 = new Meal()
            {
                Description = "Café à la milanaise avec de la crème chantille",
                Home = home1,
                Title = "Café de Milan",
                MealCategory = mc4,
                RefHide = true,
                Hide = false
            });
            context.MealSet.Add(meal2 = new Meal()
            {
                Description = "Boeuf avec sauce forestiere et des spaghettis faites maison",
                Home = home1,
                Title = "Boeuf bourgignon",
                MealCategory = mc2,
                RefHide = true,
                Hide = false
            });
            context.MealSet.Add(meal2 = new Meal()
            {
                Description = "Cordon bleu à la dinde avec son coulis de bleu",
                Home = home1,
                Title = "Cordon bleu",
                MealCategory = mc2,
                RefHide = true,
                Hide = false
            });
            context.MealSet.Add(meal3 = new Meal()
            {
                Description = "Profitrolle au chocolat, sauce chocolat alsacien",
                Home = home1,
                Title = "Profitrolle",
                MealCategory = mc3,
                RefHide = true,
                Hide = false
            });
            context.MealSet.Add(meal3 = new Meal()
            {
                Description = "Boule de glace avec coulis de fraise ou chantille",
                Home = home1,
                Title = "Glace",
                MealCategory = mc3,
                RefHide = true,
                Hide = false
            });
        }

        #endregion MEAL

        #region MEALBOOKING

        private MealBooking mealb1;
        private MealBooking mealb2;
        private MealBooking mealb3;

        private void MealBookingSeed()
        {
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
        }

        #endregion MEALBOOKING

        #region MEALCATEGORY

        private MealCategory mc1;
        private MealCategory mc2;
        private MealCategory mc3;
        private MealCategory mc4;

        private void MealCategorySeed()
        {
            context.MealCategorySet.Add(mc1 = new MealCategory()
            {
                Home = home1,
                Label = "Petit déjeuner"
            });
            context.MealCategorySet.Add(mc2 = new MealCategory()
            {
                Home = home1,
                Label = "Repas"
            });
            context.MealCategorySet.Add(mc3 = new MealCategory()
            {
                Home = home1,
                Label = "Dessert"
            });
            context.MealCategorySet.Add(mc4 = new MealCategory()
            {
                Home = home1,
                Label = "Café"
            });
        }

        #endregion MEALCATEGORY

        #region MEALPRICE

        private MealPrice mp1;
        private MealPrice mp2;
        private MealPrice mp3;

        private void MealPriceSeed()
        {
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
        }

        #endregion MEALPRICE

        #region PAYMENTMETHOD

        private PaymentMethod met1;
        private PaymentMethod met2;

        private void PaymentMethodSeed()
        {
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
        }

        #endregion PAYMENTMETHOD

        #region PAYMENTTYPE

        private PaymentType pt1;
        private PaymentType pt2;

        private void PaymentTypeSeed()
        {
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
        }

        #endregion PAYMENTTYPE

        #region PEOPLE

        private People people1;
        private People people2;
        private People people3;
        private People people4;
        private People people5;
        private People people6;
        private People people7;
        private People people8;

        private void PeopleSeed()
        {
            context.PeopleSet.Add(people1 = new People()
            {
                AcceptMailing = true,
                Addr = "1 rue du dome",
                City = "Strasbourg",
                Civility = "Mr",
                Comment = "Client regulier et respectueux",
                Country = "France",
                DateBirth = new DateTime(1982, 12, 2),
                DateCreation = new DateTime(2011, 07, 20),
                Email = "manahost_test@yahoo.com",
                Firstname = "Frédéric",
                Home = home1,
                Lastname = "Forestier",
                Mark = null,
                Phone1 = "0640306050",
                Phone2 = "0389545451",
                State = null,
                ZipCode = "67000",
                Hide = false
            });
            context.PeopleSet.Add(people3 = new People()
            {
                AcceptMailing = true,
                Addr = "26 rue de Normandie",
                City = "Illzach",
                Civility = "Mme",
                Comment = null,
                Country = "France",
                DateBirth = new DateTime(1980, 11, 12),
                DateCreation = new DateTime(2011, 04, 22),
                Email = "carole.astier@hotmail.fr",
                Firstname = "Carole",
                Home = home1,
                Lastname = "Astier",
                Mark = null,
                Phone1 = "0680804242",
                Phone2 = null,
                State = null,
                ZipCode = "68110",
                Hide = true
            });
            context.PeopleSet.Add(people2 = new People()
            {
                AcceptMailing = true,
                Addr = "30 rue de la marchande",
                City = "Strasbourg",
                Civility = "Mr",
                Comment = "Client difficile",
                Country = "France",
                DateBirth = new DateTime(1982, 12, 2),
                DateCreation = new DateTime(2011, 07, 20),
                Email = "manahostnotifier@gmail.com",
                Firstname = "Jalal",
                Home = home1,
                Lastname = "Chaabane",
                Mark = 0,
                Phone1 = "0650258250",
                Phone2 = null,
                State = null,
                ZipCode = "67000",
                Hide = false
            });
            context.PeopleSet.Add(people4 = new People()
            {
                AcceptMailing = true,
                Addr = "2 rue des musclés",
                City = "Strasbourg",
                Civility = "Mr",
                Comment = "",
                Country = "France",
                DateBirth = new DateTime(1982, 12, 2),
                DateCreation = new DateTime(2011, 07, 20),
                Email = "jeanonchelemuclé@gmail.com",
                Firstname = "Jean",
                Home = home1,
                Lastname = "Onche",
                Mark = 0,
                Phone1 = "0650258255",
                Phone2 = "0362625132",
                State = null,
                ZipCode = "67000",
                Hide = false
            });
            context.PeopleSet.Add(people5 = new People()
            {
                AcceptMailing = true,
                Addr = "03 rue de la liberté",
                City = "Mulhouse",
                Civility = "Mme",
                Comment = "",
                Country = "France",
                DateBirth = new DateTime(1947, 08, 12),
                DateCreation = new DateTime(2011, 07, 20),
                Email = "BernadetteWurt@gmail.com",
                Firstname = "Bernadette",
                Home = home1,
                Lastname = "Wurt",
                Mark = 0,
                Phone1 = "0650258250",
                Phone2 = null,
                State = null,
                ZipCode = "68000",
                Hide = false
            });
            context.PeopleSet.Add(people6 = new People()
            {
                AcceptMailing = true,
                Addr = "Budapester Straße 23",
                City = "Ebertshausen",
                Civility = "Mme",
                DateBirth = new DateTime(1988, 07, 2),
                DateCreation = new DateTime(2011, 07, 20),
                Comment = "",
                Country = "Allemagne",
                Email = "PatriciaAWashington@dayrep.com",
                Firstname = "Patricia",
                Home = home1,
                Lastname = "Washington",
                Mark = null,
                Phone1 = "06486448503",
                Phone2 = "",
                State = null,
                ZipCode = "56370",
                Hide = false
            });
            context.PeopleSet.Add(people7 = new People()
            {
                AcceptMailing = true,
                Addr = "5 rue de l'esplanade",
                City = "Paris",
                Civility = "Mme",
                DateBirth = new DateTime(1988, 07, 2),
                DateCreation = new DateTime(2011, 07, 20),
                Comment = "Masseuse",
                Country = "France",
                Email = "RennaSheering@dayrep.com",
                Firstname = "Renna",
                Home = home1,
                Lastname = "Sheering",
                Mark = null,
                Phone1 = "06486448503",
                Phone2 = "",
                State = null,
                ZipCode = "87100",
                Hide = false
            });
            context.PeopleSet.Add(people8 = new People()
            {
                AcceptMailing = true,
                Addr = "8 rue de l'Espagne",
                City = "Paris",
                Civility = "Mme",
                DateBirth = new DateTime(1988, 07, 2),
                DateCreation = new DateTime(2011, 07, 20),
                Comment = "Femme de ménage",
                Country = "France",
                Email = "AmeliaNoure@dayrep.com",
                Firstname = "Amelia",
                Home = home1,
                Lastname = "Noure",
                Mark = null,
                Phone1 = "06486448503",
                Phone2 = "",
                State = null,
                ZipCode = "87100",
                Hide = false
            });
        }

        #endregion PEOPLE

        #region PEOPLEBOOKING

        private PeopleBooking pebook1;
        private PeopleBooking pebook2;
        private PeopleBooking pebook3;

        private void PeopleBookingSeed()
        {
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
        }

        #endregion PEOPLEBOOKING

        #region PEOPLECATEGORY

        private PeopleCategory peoplecat1;
        private PeopleCategory peoplecat2;
        private PeopleCategory peoplecat3;

        private void PeopleCategorySeed()
        {
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
            context.PeopleCategorySet.Add(peoplecat3 = new PeopleCategory()
            {
                Home = home1,
                Label = "Seniors",
                Tax = tax1
            });
        }

        #endregion PEOPLECATEGORY

        #region PEOPLEFIELD

        private void PeopleFieldSeed()
        {
            PeopleField pe1;
            PeopleField pe2;
            PeopleField pe3;
            PeopleField pe4;
            context.PeopleFieldSet.Add(pe1 = new PeopleField()
            {
                FieldGroup = gp1,
                Key = "MyPersonnalField1",
                People = null,
                Value = "MyPersonnalField1",
                Home = home1
            });
            context.PeopleFieldSet.Add(pe2 = new PeopleField()
            {
                FieldGroup = gp1,
                Key = "MyPersonnalField2",
                Value = "MyPersonnalField2",
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
                FieldGroup = null,
                Key = "MyPersonnalField4",
                People = people1,
                Value = "MyPersonnalValue4",
                Home = home1
            });
        }

        #endregion PEOPLEFIELD

        #region PERIOD

        private Period period0;
        private Period period1;
        private Period period4;
        private Period period2;
        private Period period3;
        private Period period5;
        private Period period6;
        private Period period7;

        private void PeriodSeed()
        {
            context.PeriodSet.Add(period0 = new Period()
            {
                Home = home1,
                Begin = new DateTime(1970, 1, 1),
                End = new DateTime(1970, 4, 1),
                IsClosed = false,
                Title = "Haute saison",
                Days = 1 | 2 | 4 | 8 | 16 | 32 | 64
            });
            context.PeriodSet.Add(period6 = new Period()
            {
                Home = home1,
                Begin = new DateTime(2015, 3, 2, 0, 0, 0),
                End = new DateTime(2015, 12, 31, 23, 59, 59),
                IsClosed = false,
                Title = "Reste de l'année",
                Days = 1 | 2 | 4 | 8 | 16 | 32 | 64
            });
            context.PeriodSet.Add(period1 = new Period()
            {
                Home = home1,
                Begin = new DateTime(2015, 1, 1, 0, 0, 0),
                End = new DateTime(2015, 3, 1, 23, 59, 59),
                IsClosed = false,
                Title = "Saison hivernal",
                Days = 1 | 2 | 4 | 8 | 16 | 32 | 64
            });
            context.PeriodSet.Add(period2 = new Period()
            {
                Home = home1,
                Begin = new DateTime(2015, 3, 2, 0, 0, 0),
                End = new DateTime(2015, 3, 8, 23, 59, 59),
                IsClosed = true,
                Title = "Vacances d'été !",
                Days = 1 | 2 | 4 | 8 | 16 | 32 | 64
            });
            context.PeriodSet.Add(period3 = new Period()
            {
                Begin = new DateTime(2015, 3, 9, 0, 0, 0),
                End = new DateTime(2015, 12, 31, 23, 59, 59),
                Days = 1 | 2 | 4 | 8 | 16,
                IsClosed = false,
                Title = "Temps restant",
                Home = home1
            });
            context.PeriodSet.Add(period4 = new Period()
            {
                Begin = new DateTime(2015, 3, 9, 0, 0, 0),
                End = new DateTime(2015, 9, 30, 23, 59, 59),
                Days = 32 | 64,
                IsClosed = false,
                Title = "week-ends restant",
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
            context.PeriodSet.Add(period7 = new Period()
            {
                Begin = new DateTime(2016, 1, 1, 0, 0, 0),
                End = new DateTime(2016, 12, 31, 23, 59, 59),
                Days = 1 | 2 | 4 | 8 | 16 | 32 | 64,
                IsClosed = false,
                Title = "Année 2016",
                Home = home1
            });
        }

        #endregion PERIOD

        #region PRODUCT

        private Product prod1;
        private Product prod2;
        private Product prod3;
        private Product prod4;
        private Product prod5;
        private Product prod6;

        private void ProductSeed()
        {
            context.ProductSet.Add(prod1 = new Product()
            {
                Home = home1,
                Title = "Huile de massage",
                Supplier = sup2,
                PriceHT = 5M,
                ProductCategory = p1,
                Stock = 50,
                Tax = tax2,
                RefHide = true,
                Hide = false,
                IsUnderThreshold = false
            });
            context.ProductSet.Add(prod2 = new Product()
            {
                Home = home1,
                Title = "Vin d'Alsace",
                Supplier = sup1,
                PriceHT = 80M,
                ProductCategory = p1,
                Stock = 2,
                Tax = tax1,
                Threshold = 3,
                RefHide = true,
                Hide = false,
                IsUnderThreshold = true
            });
            context.ProductSet.Add(prod5 = new Product()
            {
                Home = home1,
                Title = "Bouteil 1L Coca Cola",
                Supplier = sup1,
                PriceHT = 4M,
                ProductCategory = p1,
                Stock = 8,
                Tax = tax1,
                Threshold = 10,
                RefHide = true,
                Hide = false,
                IsUnderThreshold = true
            });
            context.ProductSet.Add(prod6 = new Product()
            {
                Home = home1,
                Title = "Bouteil 1L Ice Tea",
                Supplier = sup1,
                PriceHT = 3.80M,
                ProductCategory = p1,
                Stock = 40,
                Tax = tax1,
                Threshold = 10,
                RefHide = true,
                Hide = false,
                IsUnderThreshold = false
            });
            context.ProductSet.Add(prod3 = new Product()
            {
                Home = home1,
                Title = "Massage",
                Supplier = supplier10,
                PriceHT = 18M,
                ProductCategory = p2,
                Tax = tax3,
                Duration = 1800,
                RefHide = true,
                Hide = false,
                IsUnderThreshold = false,
                IsService = true,
            });
            context.ProductSet.Add(prod4 = new Product()
            {
                Home = home1,
                Title = "Ménage",
                Supplier = supplier11,
                PriceHT = 15M,
                ProductCategory = p2,
                Tax = tax3,
                Duration = 3600,
                RefHide = true,
                Hide = false,
                IsUnderThreshold = false,
                IsService = true
            });
        }

        #endregion PRODUCT

        #region PRODUCTBOOKING

        private ProductBooking pbook1;
        private ProductBooking pbook2;
        private ProductBooking pbook3;

        private void ProductBookingSeed()
        {
            context.ProductBookingSet.Add(pbook1 = new ProductBooking()
            {
                Booking = booking1,
                Date = DateTime.Now.AddDays(4),
                Home = home1,
                PriceHT = 36M,
                PriceTTC = 40M,
                Product = prod3,
                Quantity = 1,
                Duration = 777600000 // 1h
            });
            context.ProductBookingSet.Add(pbook3 = new ProductBooking()
            {
                Booking = booking1,
                Home = home1,
                PriceHT = 3.80M,
                PriceTTC = 3.80M,
                Product = prod6,
                Quantity = 2,
            });
            context.ProductBookingSet.Add(pbook2 = new ProductBooking()
            {
                Booking = booking2,
                Date = DateTime.Now.AddDays(3),
                PriceHT = 15M,
                PriceTTC = 18M,
                Product = prod4,
                Quantity = 1,
                Duration = 777600000, // 1h
                Home = home1
            });
        }

        #endregion PRODUCTBOOKING

        #region PRODUCTCATEGORY

        private ProductCategory p1;
        private ProductCategory p2;

        private void ProductCategorySeed()
        {
            context.ProductCategorySet.Add(p1 = new ProductCategory()
            {
                Home = home1,
                Title = "Produit",
                RefHide = false
            });
            context.ProductCategorySet.Add(p2 = new ProductCategory()
            {
                Home = home1,
                Title = "Service",
                RefHide = false
            });
        }

        #endregion PRODUCTCATEGORY

        #region PRICEPERPERSON

        private void PricePerPersonSeed()
        {
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
            PricePerPerson ppp13;
            PricePerPerson ppp14;
            PricePerPerson ppp15;

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
            context.PricePerPersonSet.Add(ppp13 = new PricePerPerson()
            {
                Home = home1,
                Title = "Prix unique room1 haute saison",
                Period = period1,
                Room = r1,
                PriceHT = 50M,
                Tax = tax1
            });
            context.PricePerPersonSet.Add(ppp15 = new PricePerPerson()
            {
                Home = home1,
                Title = "Prix unique room3 haute saison",
                Period = period1,
                Room = r3,
                PriceHT = 50M,
                Tax = tax1
            });
            context.PricePerPersonSet.Add(ppp14 = new PricePerPerson()
            {
                Home = home1,
                Title = "Prix unique room1 reste de l'année",
                Period = period6,
                Room = r1,
                PriceHT = 39M,
                Tax = tax1
            });
            context.PricePerPersonSet.Add(ppp14 = new PricePerPerson()
            {
                Home = home1,
                Title = "Prix unique room3 reste de l'année",
                Period = period6,
                Room = r3,
                PriceHT = 39M,
                Tax = tax1
            });
        }

        #endregion PRICEPERPERSON

        #region RESOURCECONFIG

        private ResourceConfig config1;

        private void ResourceConfigSeed()
        {
            context.ResourceConfigSet.Add(config1 = new ResourceConfig()
            {
                LimitBase = 1073741824
            });
        }

        #endregion RESOURCECONFIG

        #region ROOM

        private Room r1;
        private Room r2;
        private Room r3;
        private Room r4;

        private void RoomSeed()
        {
            context.RoomSet.Add(r1 = new Room()
            {
                Home = home1,
                Caution = 500M,
                Classification = "3 épis",
                Color = 0x9b59b6,
                Description = "Chambre avec vue sur le jardin de la residence, un lit double et un lit double simple, style chalet avec mobilier en bois. salle de bain privée avec douche.",
                ShortDescription = "Belle vue, salle de bain équipée",
                IsClosed = false,
                Title = "Pin doré",
                RoomCategory = rcat1,
                RoomState = "Craquelures sur le carelage de la salle de bain",
                Size = 42,
                Capacity = 4,
                Hide = false,
                RefHide = true
            });
            context.RoomSet.Add(r2 = new Room()
            {
                Home = home1,
                Classification = "2 épis",
                Color = 0x27ae60,
                Description = "Chambres style chalet, un lit double, mobilier en bois, wc privé",
                IsClosed = false,
                Title = "Pin argenté",
                RoomCategory = rcat2,
                RoomState = "Rien à signaler",
                Capacity = 2,
                Hide = false,
                RefHide = true
            });
            context.RoomSet.Add(r3 = new Room()
            {
                Home = home1,
                Classification = "1 épis",
                Color = 0xe74c3c,
                Description = "Chambre classique style chalet, lit double",
                IsClosed = false,
                Title = "Pin de bronze",
                RoomCategory = rcat2,
                RoomState = "Rien à signaler",
                Capacity = 2,
                Hide = false,
                RefHide = true
            });
            context.RoomSet.Add(r4 = new Room()
            {
                Home = home1,
                Classification = "1 épis",
                Color = 0x95a5a6,
                Description = "Chambre classique style chalet, lit simple",
                IsClosed = false,
                Title = "Courrone de bronze",
                RoomCategory = rcat3,
                RoomState = "Rien à signaler",
                Capacity = 1,
                Hide = false,
                RefHide = true
            });
        }

        #endregion ROOM

        #region ROOMBOOKING

        private RoomBooking rbook1;
        private RoomBooking rbook2;
        private RoomBooking rbook3;
        private RoomBooking rbook4;
        private RoomBooking rbook5;

        private void RoomBookingSeed()
        {
            context.RoomBookingSet.Add(rbook1 = new RoomBooking()
            {
                Booking = booking1,
                Home = home1,
                PriceHT = 77M,
                PriceTTC = 83M,
                Room = r1,
                NbNights = 4,
                DateBegin = DateTime.Now.AddDays(3),
                DateEnd = DateTime.Now.AddDays(5)
            });
            context.RoomBookingSet.Add(rbook2 = new RoomBooking()
            {
                Booking = booking2,
                Home = home1,
                PriceHT = 99M,
                PriceTTC = 107M,
                Room = r2,
                NbNights = 3,
                DateBegin = DateTime.Now.AddDays(2),
                DateEnd = DateTime.Now.AddDays(4)
            });
            context.RoomBookingSet.Add(rbook3 = new RoomBooking()
            {
                Booking = booking2,
                Home = home1,
                PriceHT = 77M,
                PriceTTC = 83M,
                Room = r3,
                NbNights = 3,
                DateBegin = DateTime.Now.AddDays(3),
                DateEnd = DateTime.Now.AddDays(4)
            });
            context.RoomBookingSet.Add(rbook4 = new RoomBooking()
            {
                Booking = booking3,
                Home = home1,
                PriceHT = 99M,
                PriceTTC = 107M,
                Room = r1,
                NbNights = 1,
                DateBegin = DateTime.Now.AddDays(9),
                DateEnd = DateTime.Now.AddDays(17),
            });
        }

        #endregion ROOMBOOKING

        #region ROOMCATEGORY

        private RoomCategory rcat1;
        private RoomCategory rcat2;
        private RoomCategory rcat3;

        private void RoomCategorySeed()
        {
            context.RoomCategorySet.Add(rcat1 = new RoomCategory()
            {
                Home = home1,
                Title = "Haut de gamme",
                RefHide = true
            });
            context.RoomCategorySet.Add(rcat2 = new RoomCategory()
            {
                Home = home1,
                Title = "Modeste",
                RefHide = false
            });
            context.RoomCategorySet.Add(rcat3 = new RoomCategory()
            {
                Home = home1,
                Title = "Bas de gamme",
                RefHide = false
            });
        }

        #endregion ROOMCATEGORY

        #region ROOMSUPPLEMENT

        private RoomSupplement rs1;
        private RoomSupplement rs2;
        private RoomSupplement rs3;

        private void RoomSupplementSeed()
        {
            context.RoomSupplementSet.Add(rs1 = new RoomSupplement()
            {
                Home = home1,
                Title = "Chevalet de dessin",
                PriceHT = 10M,
                Tax = tax1,
                Hide = false
            });
        }

        #endregion ROOMSUPPLEMENT

        #region SATISFACTIONCLIENT

        private SatisfactionClient sclient;
        private SatisfactionClient sclient2;

        private void SatisfactionClientSeed()
        {
            context.SatisfactionClientSet.Add(sclient = new SatisfactionClient()
            {
                Booking = booking2,
                PeopleDest = people1,
                Code = "000000",
                DateAnswered = DateTime.Now,
                Home = home1
            });
            context.SatisfactionClientSet.Add(sclient2 = new SatisfactionClient()
            {
                Booking = booking3,
                PeopleDest = people1,
                Code = "000001",
                DateAnswered = DateTime.Now,
                Home = home1
            });
        }

        #endregion SATISFACTIONCLIENT

        #region SATISFACTIONCLIENTANSWER

        private SatisfactionClientAnswer sanswer1;
        private SatisfactionClientAnswer sanswer2;
        private SatisfactionClientAnswer sanswer3;
        private SatisfactionClientAnswer sanswer4;

        private void SatisfactionClientAnswerSeed()
        {
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
                Answer = "Y",
                SatisfactionClient = sclient
            });
            context.SatisfactionClientAnsweredSet.Add(sanswer4 = new SatisfactionClientAnswer()
            {
                Home = home1,
                Question = "Des détails ?",
                AnswerType = EAnswerType.DESC,
                Answer = "Un parking serait un énorme plus",
                SatisfactionClient = sclient2
            });

            context.SatisfactionClientAnsweredSet.Add(sanswer1 = new SatisfactionClientAnswer()
            {
                Home = home1,
                Question = "Le séjour vous a t-il plu ?",
                AnswerType = EAnswerType.RADIO,
                Answer = "N",
                SatisfactionClient = sclient2
            });
            context.SatisfactionClientAnsweredSet.Add(sanswer2 = new SatisfactionClientAnswer()
            {
                Home = home1,
                Question = "Notez sur 5 votre satisfaction",
                AnswerType = EAnswerType.NUMBER,
                Answer = "1",
                SatisfactionClient = sclient2
            });
            context.SatisfactionClientAnsweredSet.Add(sanswer3 = new SatisfactionClientAnswer()
            {
                Home = home1,
                Question = "Conseillerez-vous à vos proche notre établissement ?",
                AnswerType = EAnswerType.RADIO,
                Answer = "N",
                SatisfactionClient = sclient2
            });
            context.SatisfactionClientAnsweredSet.Add(sanswer4 = new SatisfactionClientAnswer()
            {
                Home = home1,
                Question = "Des détails ?",
                AnswerType = EAnswerType.DESC,
                Answer = "Pas de parking, accueil innexistante, je ne recommanderais pas cette endroit",
                SatisfactionClient = sclient2
            });
        }

        #endregion SATISFACTIONCLIENTANSWER

        #region SATISFACTIONCONFIG

        private SatisfactionConfig sconf1;

        private void SatisfactionConfigSeed()
        {
            context.SatisfactionConfigSet.Add(sconf1 = new SatisfactionConfig()
            {
                Description = "Veuillez trouver ici présent un petit questionnaire<br/>Merci d'y répondre :<br/>",
                Home = home1,
                Title = "La Corderie / Enquête de satisfaction"
            });
        }

        #endregion SATISFACTIONCONFIG

        #region SATISFACTIONCONFIGQUESTION

        private SatisfactionConfigQuestion squestion1;
        private SatisfactionConfigQuestion squestion2;
        private SatisfactionConfigQuestion squestion3;
        private SatisfactionConfigQuestion squestion4;

        private void SatisfactionConfigQuestionSeed()
        {
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
        }

        #endregion SATISFACTIONCONFIGQUESTION

        #region STATISTICS

        private MStatistics stat1;
        private MStatistics stat2;
        private MStatistics stat3;
        private MStatistics stat4;
        private MStatistics stat5;
        private MStatistics stat6;
        private MStatistics stat7;

        private void StatisticsSeed()
        {
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
        }

        #endregion STATISTICS

        #region SUPPLEMENTROOMBOOKING

        private SupplementRoomBooking supprbook1;

        private void SupplementRoomBookingSeed()
        {
            context.SupplementRoomBookingSet.Add(supprbook1 = new SupplementRoomBooking()
            {
                Home = home1,
                PriceHT = 10M,
                PriceTTC = 12M,
                RoomSupplement = rs1,
                RoomBooking = rbook1
            });
        }

        #endregion SUPPLEMENTROOMBOOKING

        #region SUPPLIER

        private Supplier sup1;
        private Supplier sup2;
        private Supplier sup3;
        private Supplier supplier10;
        private Supplier supplier11;

        private void SupplierSeed()
        {
            context.SupplierSet.Add(sup1 = new Supplier()
            {
                SocietyName = "Les Grands Blancs",
                DateCreation = new DateTime(2011, 10, 25),
                Email = "vignobledalsace@hotmail.fr",
                Home = home1,
                Hide = false
            });

            context.SupplierSet.Add(sup2 = new Supplier()
            {
                SocietyName = "ChinaComfort",
                DateCreation = new DateTime(2012, 08, 20),
                Email = "confortdechine@hotmail.fr",
                Home = home1,
                Hide = false
            });

            context.SupplierSet.Add(sup2 = new Supplier()
            {
                SocietyName = "METRO",
                DateCreation = new DateTime(2011, 05, 05),
                Email = "MetroAlimentation@hotmail.fr",
                Home = home1,
                Hide = false
            });
            context.SupplierSet.Add(supplier10 = new Supplier()
            {
                SocietyName = "Mme Peirera",
                Phone1 = "0648713589",
                Email = "peirera@live.fr",
                DateCreation = DateTime.Now.AddMonths(-5),
                Home = home1
            });
            context.SupplierSet.Add(supplier11 = new Supplier()
            {
                SocietyName = "Nettoietout",
                Phone1 = "0648713589",
                Email = "nettoietout@live.fr",
                DateCreation = DateTime.Now.AddMonths(-2),
                Contact = "shirley@hotmail.com",
                Home = home1
            });
        }

        #endregion SUPPLIER

        #region TAX

        private Tax tax1;
        private Tax tax2;
        private Tax tax3;
        private Tax tax4;

        private void TaxSeed()
        {
            context.TaxSet.Add(tax2 = new Tax()
            {
                Home = home1,
                Title = "TVA 20%",
                Price = 20M,
                ValueType = EValueType.PERCENT
            });
            context.TaxSet.Add(tax1 = new Tax()
            {
                Home = home1,
                Title = "TVA 10%",
                Price = 10M,
                ValueType = EValueType.PERCENT
            });
            context.TaxSet.Add(tax3 = new Tax()
            {
                Home = home1,
                Title = "TVA 5.5%",
                Price = 5.5M,
                ValueType = EValueType.PERCENT
            });
            context.TaxSet.Add(tax4 = new Tax()
            {
                Home = home1,
                Title = "TVA 0",
                Price = 0M,
                ValueType = EValueType.PERCENT
            });
        }

        #endregion TAX

        private void SaveChanges()
        {
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
            catch (UpdateException e)
            {
                throw new Exception(e.Message, e.InnerException);
            }
            catch (DataException e)
            {
                throw new Exception(e.Message, e.InnerException);
            }
        }

        protected override void Seed(ManahostManagerDAL context)
        {
            this.context = context;

            ServicesSeed();
            ClientSeed();
            HomeSeed();
            PeopleSeed();
            DocumentCategorySeed();
            MailConfigSeed();
            HomeConfigSeed();
            TaxSeed();
            SupplierSeed();
            RoomCategorySeed();
            RoomSeed();
            ProductCategorySeed();
            ProductSeed();
            BedSeed();
            RoomSupplementSeed();
            BookingSeed();
            BillSeed();
            BillItemCategorySeed();
            GroupBillItemSeed();
            BillItemSeed();
            BookingStepConfigSeed();
            BookingStepSeed();
            MailLogSeed();
            BookingStepBookingSeed();
            KeyGeneratorSeed();
            MealCategorySeed();
            MealSeed();
            PaymentTypeSeed();
            PaymentMethodSeed();
            PeopleCategorySeed();
            PeriodSeed();
            PricePerPersonSeed();
            MealPriceSeed();
            StatisticsSeed();
            FieldGroupSeed();
            PeopleFieldSeed();
            SatisfactionConfigSeed();
            SatisfactionConfigQuestionSeed();
            SatisfactionClientSeed();
            SatisfactionClientAnswerSeed();
            AdditionalBookingSeed();
            DepositSeed();
            DinnerBookingSeed();
            MealBookingSeed();
            ProductBookingSeed();
            RoomBookingSeed();
            PeopleBookingSeed();
            SupplementRoomBookingSeed();
            DocumentSeed();
            ResourceConfigSeed();
            DocumentLogSeed();
            SaveChanges();
            BookingStepSetCompleteSeed();
            SaveChanges();
            base.Seed(context);
        }
    }
}