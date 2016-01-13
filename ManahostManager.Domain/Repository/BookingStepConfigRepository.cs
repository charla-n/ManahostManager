using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;

namespace ManahostManager.Domain.Repository
{
    public interface IBookingStepConfigRepository : IAbstractRepository<BookingStepConfig>
    {
        BookingStepConfig GetBookingStepConfigById(int id, int clientId);
    }

    public class BookingStepConfigRepository : AbstractRepository<BookingStepConfig>, IBookingStepConfigRepository
    {
        public BookingStepConfigRepository(ManahostManagerDAL ctx) : base(ctx)
        {
        }

        public BookingStepConfig GetBookingStepConfigById(int id, int clientId)
        {
            return GetUniq(p => p.Id == id && p.Home.ClientId == clientId);
        }

        public override void Delete(BookingStepConfig obj)
        {
            includes.Add("BookingStepConfig");
            DeleteRange<BookingStepBooking>(GetList<BookingStepBooking>(p => p.BookingStepConfig.Id == obj.Id));
            base.Delete(obj);
        }
    }
}