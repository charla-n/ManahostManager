using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;

namespace ManahostManager.Domain.Repository
{
    public interface IBookingStepBookingRepository : IAbstractRepository<BookingStepBooking>
    {
        BookingStepBooking GetBookingStepBookingById(int id, int clientId);
    }

    public class BookingStepBookingRepository : AbstractRepository<BookingStepBooking>, IBookingStepBookingRepository
    {
        public BookingStepBookingRepository(ManahostManagerDAL ctx) : base(ctx)
        { }

        public BookingStepBooking GetBookingStepBookingById(int id, int clientId)
        {
            return GetUniq(p => p.Id == id && p.Home.ClientId == clientId);
        }
    }
}