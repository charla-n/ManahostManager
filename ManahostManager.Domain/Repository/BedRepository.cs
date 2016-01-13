using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;

namespace ManahostManager.Domain.Repository
{
    public interface IBedRepository : IAbstractRepository<Bed>
    {
        Bed GetBedById(int id, int clientId);
    }

    public class BedRepository : AbstractRepository<Bed>, IBedRepository
    {
        public BedRepository(ManahostManagerDAL ctx)
            : base(ctx)
        { }

        public Bed GetBedById(int id, int clientId)
        {
            return GetUniq(p => p.Id == id && p.Home.ClientId == clientId);
        }
    }
}