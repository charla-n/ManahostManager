namespace ManahostManager.Domain.Migrations
{
    using ManahostManager.Domain.DAL;
    using ManahostManager.Domain.Entity;
    using ManahostManager.Domain.Tools;
    using ManahostManager.Utils;
    using Microsoft.AspNet.Identity;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<ManahostManager.Domain.DAL.ManahostManagerDAL>
    {
        //Sql("CREATE TRIGGER TRG_DOCUMENT_BOOKING_DEL ON dbo.Document FOR DELETE AS " +
        //    "DELETE FROM dbo.BookingDocument where dbo.BookingDocument.DocumentId = Id");

        //Sql("CREATE TRIGGER TRG_MEAL_BOOKING_DEL ON dbo.Meal FOR DELETE AS " +
        //    "DELETE FROM dbo.MealBooking where dbo.MealBooking.MealId = Id");

        //Sql("CREATE TRIGGER TRG_PRODUCT_BOOKING_DEL ON dbo.Product FOR DELETE AS " +
        //    "DELETE FROM dbo.ProductBooking where dbo.ProductBooking.ProductId = Id");

        //Sql("CREATE TRIGGER TRG_ROOM_BOOKING_DEL ON dbo.Room FOR DELETE AS " +
        //    "DELETE FROM dbo.RoomBooking where dbo.RoomBooking.RoomId = Id");

        //Sql("CREATE TRIGGER TRG_PEOPLE_BOOKING_DEL on dbo.PeopleCategory FOR DELETE AS " +
        //    "DELETE FROM dbo.PeopleBooking where dbo.PeopleBooking.PeopleCategoryId = Id");

        //Sql("CREATE TRIGGER TRG_SUPPLEMENT_ROOM_BOOKING_DEL on dbo.RoomSupplement FOR DELETE AS " +
        //    "DELETE FROM dbo.SupplementRoomBooking where dbo.SupplementRoomBooking.RoomSupplementId = Id");

        //Sql("CREATE TRIGGER TRG_PEOPLE_DEL on dbo.People FOR DELETE AS " +
        //    "DELETE FROM dbo.PeopleField where dbo.PeopleField.PeopleId = Id");

        //Sql("DROP TRIGGER TRG_DOCUMENT_BOOKING_DEL");
        //Sql("DROP TRIGGER TRG_MEAL_BOOKING_DEL");
        //Sql("DROP TRIGGER TRG_PRODUCT_BOOKING_DEL");
        //Sql("DROP TRIGGER TRG_ROOM_BOOKING_DEL");
        //Sql("DROP TRIGGER TRG_PEOPLE_BOOKING_DEL");
        //Sql("DROP TRIGGER TRG_SUPPLEMENT_ROOM_BOOKING_DEL");
        //Sql("DROP TRIGGER TRG_PEOPLE_DEL");

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

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
        }

        public void GroupsSeed(ManahostManagerDAL context)
        {
            RoleManager<CustomRole, int> managerGroup = new ClientRoleManager(new CustomRoleStore(context));
            if (managerGroup.FindByName(GenericNames.ADMINISTRATOR) == null)
                managerGroup.Create(new CustomRole(GenericNames.ADMINISTRATOR));
            if (managerGroup.FindByName(GenericNames.REGISTERED_VIP) == null)
                managerGroup.Create(new CustomRole(GenericNames.REGISTERED_VIP));
            if (managerGroup.FindByName(GenericNames.VIP) == null)
                managerGroup.Create(new CustomRole(GenericNames.ADMINISTRATOR));
            if (managerGroup.FindByName(GenericNames.DISABLED) == null)
                managerGroup.Create(new CustomRole(GenericNames.DISABLED));
            if (managerGroup.FindByName(GenericNames.MANAGER) == null)
                managerGroup.Create(new CustomRole(GenericNames.MANAGER));
        }

        protected override void Seed(ManahostManagerDAL context)
        {
            ServicesSeed(context);
            GroupsSeed(context);
        }
    }
}