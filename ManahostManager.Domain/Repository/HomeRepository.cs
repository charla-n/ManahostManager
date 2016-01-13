using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;
using System.Collections.Generic;
using System.Linq;

namespace ManahostManager.Domain.Repository
{
    public interface IHomeRepository : IAbstractRepository<Home>
    {
        Home GetHomeById(int id, int idClient);

        IEnumerable<Home> GetHomesForClient(int clientId);
    }

    public class HomeRepository : AbstractRepository<Home>, IHomeRepository
    {
        public HomeRepository(ManahostManagerDAL ctx)
            : base(ctx, true)
        {
        }

        public Home GetHomeById(int id, int idClient)
        {
            return GetUniq(p => p.Id == id && idClient == p.ClientId);
        }

        public IEnumerable<Home> GetHomesForClient(int clientId)
        {
            return GetList(p => p.ClientId == clientId).Take(100);
        }
    }
}