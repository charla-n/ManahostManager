using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;

namespace ManahostManager.Domain.Repository
{
    public interface IAdditionalBookingRepository : IAbstractRepository<AdditionalBooking>
    {
        AdditionalBooking GetAdditionalBookingById(int id, int clientId);
    }

    public class AdditionalBookingRepository : AbstractRepository<AdditionalBooking>, IAdditionalBookingRepository
    {
        public AdditionalBookingRepository(ManahostManagerDAL ctx) : base(ctx)
        { }

        public AdditionalBooking GetAdditionalBookingById(int id, int clientId)
        {
            return GetUniq(p => p.Id == id && p.Home.ClientId == clientId);
        }
    }
}