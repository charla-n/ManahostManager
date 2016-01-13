using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;
using System.Collections.Generic;
using System.Linq;

namespace ManahostManager.Domain.Repository
{
    public interface IPeopleRepository : IAbstractRepository<People>
    {
        People GetPeopleById(int Id, int clientId);
    }

    public class PeopleRepository : AbstractRepository<People>, IPeopleRepository
    {
        public PeopleRepository(ManahostManagerDAL ctx)
            : base(ctx)
        { }

        public People GetPeopleById(int Id, int clientId)
        {
            return GetUniq(p => p.Id == Id && p.Home.ClientId == clientId);
        }

        public override void Delete(People obj)
        {
            List<Booking> listBookings = GetList<Booking>(p => p.People.Id == obj.Id).ToList();

            foreach (Booking curBooking in listBookings)
            {
                IEnumerable<Bill> listBills = GetList<Bill>(p => p.Booking.Id == curBooking.Id);

                foreach (Bill curBill in listBills)
                {
                    curBill.Booking = null;
                    Update<Bill>(curBill);
                }
            }

            DeleteRange<PeopleField>(GetList<PeopleField>(p => p.People.Id == obj.Id));
            DeleteRange<SatisfactionClient>(GetList<SatisfactionClient>(p => p.PeopleDest.Id == obj.Id));
            DeleteRange<Booking>(listBookings);
            base.Delete(obj);
        }
    }
}