using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;

namespace ManahostManager.Domain.Repository
{
    public interface ISatisfactionConfigQuestionRepository : IAbstractRepository<SatisfactionConfigQuestion>
    {
        SatisfactionConfigQuestion GetSatisfactionConfigQuestionByClientId(int id, int idClient);

        SatisfactionConfigQuestion GetSatisfactionConfigQuestionByHomeId(int id, int idClient);
    }

    public class SatisfactionConfigQuestionRepository : AbstractRepository<SatisfactionConfigQuestion>, ISatisfactionConfigQuestionRepository
    {
        public SatisfactionConfigQuestionRepository(ManahostManagerDAL ctx) : base(ctx)
        {
        }

        public SatisfactionConfigQuestion GetSatisfactionConfigQuestionByClientId(int id, int idClient)
        {
            return GetUniq(p => p.Id == id && p.Home.ClientId == idClient);
        }

        public SatisfactionConfigQuestion GetSatisfactionConfigQuestionByHomeId(int id, int idHome)
        {
            return GetUniq(p => p.Id == id && p.Home.Id == idHome);
        }
    }
}