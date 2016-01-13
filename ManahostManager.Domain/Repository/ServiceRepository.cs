using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;

namespace ManahostManager.Domain.Repository
{
    public interface IServiceRepository : IAbstractRepository<Service>
    {
    }

    public class ServiceRepository : AbstractRepository<Service>, IServiceRepository
    {
        public ServiceRepository(ManahostManagerDAL ctx) : base(ctx, true)
        {
        }
    }
}