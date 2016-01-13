using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;

namespace ManahostManager.Domain.Repository
{
    public interface ISupplementRoomBookingRepository : IAbstractRepository<SupplementRoomBooking>
    {
        SupplementRoomBooking GetSupplementRoomBookingById(int id, int clientId);
    }

    public class SupplementRoomBookingRepository : AbstractRepository<SupplementRoomBooking>, ISupplementRoomBookingRepository
    {
        public SupplementRoomBookingRepository(ManahostManagerDAL ctx) : base(ctx)
        { }

        public SupplementRoomBooking GetSupplementRoomBookingById(int id, int clientId)
        {
            return GetUniq(p => p.Id == id && p.Home.ClientId == clientId);
        }
    }
}