using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;
using System.Threading.Tasks;

namespace ManahostManager.Domain.Tools
{
    public class ManahostInitData
    {
        public static async Task Seed(ManahostManagerDAL context, Client currentClient)
        {
            await context.SaveChangesAsync();
        }
    }
}