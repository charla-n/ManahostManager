using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;

namespace ManahostManager.Domain.Repository
{
    public interface IHomeConfigRepository : IAbstractRepository<HomeConfig>
    {
        HomeConfig GetHomeConfigById(int id, int clientId);
    }

    public class HomeConfigRepository : AbstractRepository<HomeConfig>, IHomeConfigRepository
    {
        public HomeConfigRepository(ManahostManagerDAL ctx) : base(ctx)
        {
        }

        public HomeConfig GetHomeConfigById(int id, int clientId)
        {
            return GetUniq(p => p.Id == id && p.Home.ClientId == clientId);
        }

        public override void Delete(HomeConfig obj)
        {
            base.Delete(obj);
        }
    }
}