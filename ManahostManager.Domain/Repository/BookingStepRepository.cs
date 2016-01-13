using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;
using System.Collections.Generic;

namespace ManahostManager.Domain.Repository
{
    public interface IBookingStepRepository : IAbstractRepository<BookingStep>
    {
        BookingStep GetBookingStepById(int id, int clientId);

        BookingStep GetFirstBookingStep(int stepconfigid, int clientId);
    }

    public class BookingStepRepository : AbstractRepository<BookingStep>, IBookingStepRepository
    {
        public BookingStepRepository(ManahostManagerDAL ctx) : base(ctx)
        {
        }

        public BookingStep GetBookingStepById(int id, int clientId)
        {
            return GetUniq(p => p.Id == id && p.Home.ClientId == clientId);
        }

        public BookingStep GetFirstBookingStep(int stepconfigId, int clientId)
        {
            return GetUniq(p => p.BookingStepPrevious == null && p.Home.ClientId == clientId && p.BookingStepConfig.Id == stepconfigId);
        }

        public override void Delete(BookingStep obj)
        {
            includes.Add("CurrentStep");
            IEnumerable<BookingStepBooking> listBookingStepBooking = GetList<BookingStepBooking>(p => p.CurrentStep.Id == obj.Id);
            includes.Add("BookingStepNext");
            includes.Add("BookingStepPrevious");
            IEnumerable<BookingStep> listBookingStep = GetList<BookingStep>(p => p.BookingStepNext.Id == obj.Id || p.BookingStepPrevious.Id == obj.Id);

            foreach (BookingStepBooking cur in listBookingStepBooking)
                cur.CurrentStep = null;
            foreach (BookingStep cur in listBookingStep)
            {
                if (cur.BookingStepNext != null && cur.BookingStepNext.Id == obj.Id)
                    cur.BookingStepNext = null;
                if (cur.BookingStepPrevious != null && cur.BookingStepPrevious.Id == obj.Id)
                    cur.BookingStepPrevious = null;
            }
            base.Delete(obj);
        }
    }
}