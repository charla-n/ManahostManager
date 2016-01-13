using ManahostManager.Domain.DAL;
using System.Data.Entity;

namespace ManahostManager.Tests.EFTests
{
    public class EFContext
    {
        public static ManahostManagerDAL CreateContext()
        {
            Database.SetInitializer(new ManahostManagerInitializerTest());
            using (ManahostManagerDAL prectx = new ManahostManagerDAL())
            {
                prectx.Database.Delete();
            }

            ManahostManagerDAL ctx = new ManahostManagerDAL();

            ctx.Database.CommandTimeout = 60;
            return ctx;
        }
    }
}