using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;

namespace ManahostManager.Domain.Repository
{
    public interface IFieldGroupRepository : IAbstractRepository<FieldGroup>
    {
        FieldGroup GetFieldGroupById(int id, int clientId);
    }

    public class FieldGroupRepository : AbstractRepository<FieldGroup>, IFieldGroupRepository
    {
        public FieldGroupRepository(ManahostManagerDAL ctx)
            : base(ctx)
        { }

        public FieldGroup GetFieldGroupById(int id, int clientId)
        {
            return GetUniq(p => p.Id == id && p.Home.ClientId == clientId);
        }
    }
}