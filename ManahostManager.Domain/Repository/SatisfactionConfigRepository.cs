using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;

namespace ManahostManager.Domain.Repository
{
    public interface ISatisfactionConfigRepository : IAbstractRepository<SatisfactionConfig>
    {
        SatisfactionConfig GetSatisfactionConfigById(int id, int clientId);
    }

    public class SatisfactionConfigRepository : AbstractRepository<SatisfactionConfig>, ISatisfactionConfigRepository
    {
        public SatisfactionConfigRepository(ManahostManagerDAL ctx) : base(ctx)
        { }

        public SatisfactionConfig GetSatisfactionConfigById(int id, int clientId)
        {
            return GetUniq(p => p.Id == id && p.Home.ClientId == clientId);
        }
    }
}