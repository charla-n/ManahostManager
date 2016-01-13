using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;
using System.Collections.Generic;

namespace ManahostManager.Domain.Repository
{
    public interface IBookingRepository : IAbstractRepository<Booking>
    {
        Booking GetBookingById(int id, int clientId);

        IEnumerable<ProductBooking> GetProductBookingFromBookingId(int bookingId, int clientId);
    }

    public class BookingRepository : AbstractRepository<Booking>, IBookingRepository
    {
        public BookingRepository(ManahostManagerDAL ctx) : base(ctx)
        { }

        public Booking GetBookingById(int id, int clientId)
        {
            return GetUniq(p => p.Id == id && p.Home.ClientId == clientId);
        }

        public IEnumerable<ProductBooking> GetProductBookingFromBookingId(int bookingId, int clientId)
        {
            return GetList<ProductBooking>(p => p.Booking.Id == bookingId && p.Home.ClientId == clientId);
        }

        public override void Delete(Booking obj)
        {
            includes.Add("Booking");
            IEnumerable<Bill> listBills = GetList<Bill>(p => p.Booking.Id == obj.Id);
            includes.Add("Booking");
            IEnumerable<SatisfactionClient> listSatisfactionClient = GetList<SatisfactionClient>(p => p.Booking.Id == obj.Id);

            foreach (Bill cur in listBills)
                cur.Booking = null;
            foreach (SatisfactionClient cur in listSatisfactionClient)
                cur.Booking = null;
            base.Delete(obj);
        }
    }
}