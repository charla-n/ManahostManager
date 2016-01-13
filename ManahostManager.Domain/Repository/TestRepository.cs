using System.Collections;

namespace ManahostManager.Domain.Repository
{
    public class TestRepository
    {
        public IEnumerable Test()
        {
            return null;
            //return new BookingRepository(new ManahostManagerDAL()).RetrieveContext().Set(typeof(RoomBooking)).Where("BookingId == @0 && HomeId == 1", 1).OrderBy("Booking.DateArrival").Skip(0).Take(100);
            //return new BookingRepository(new ManahostManagerDAL()).RetrieveContext().Set(typeof(RoomBooking)).OrderBy("Booking.DateArrival");
        }
    }
}