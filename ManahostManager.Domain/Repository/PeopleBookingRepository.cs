using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;

namespace ManahostManager.Domain.Repository
{
    public interface IPeopleBookingRepository : IAbstractRepository<PeopleBooking>
    {
        PeopleBooking GetPeopleBookingById(int id, int clientId);
    }

    public class PeopleBookingRepository : AbstractRepository<PeopleBooking>, IPeopleBookingRepository
    {
        public PeopleBookingRepository(ManahostManagerDAL ctx) : base(ctx)
        { }

        public PeopleBooking GetPeopleBookingById(int id, int clientId)
        {
            return GetUniq(p => p.Id == id && p.Home.ClientId == clientId);
        }
    }
}