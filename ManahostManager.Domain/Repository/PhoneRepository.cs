using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;

namespace ManahostManager.Domain.Repository
{
    public interface IPhoneRepository : IAbstractRepository<PhoneNumber>
    {
    }

    public class PhoneRepository : AbstractRepository<PhoneNumber>, IPhoneRepository
    {
        public PhoneRepository(ManahostManagerDAL ctx) : base(ctx, true)
        {
        }
    }
}