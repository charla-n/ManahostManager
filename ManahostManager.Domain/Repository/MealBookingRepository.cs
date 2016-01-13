using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;

namespace ManahostManager.Domain.Repository
{
    public interface IMealBookingRepository : IAbstractRepository<MealBooking>
    {
        MealBooking GetMealBookingById(int id, int clientId);
    }

    public class MealBookingRepository : AbstractRepository<MealBooking>, IMealBookingRepository
    {
        public MealBookingRepository(ManahostManagerDAL ctx) : base(ctx)
        { }

        public MealBooking GetMealBookingById(int id, int clientId)
        {
            return GetUniq(p => p.Id == id && p.Home.ClientId == clientId);
        }
    }
}