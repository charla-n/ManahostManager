using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;
using System.Collections.Generic;

namespace ManahostManager.Domain.Repository
{
    public interface IDinnerBookingRepository : IAbstractRepository<DinnerBooking>
    {
        DinnerBooking GetDinnerBookingById(int id, int clientId);

        IEnumerable<MealBooking> GetAllMealByDinnerBooking(int dinnerBookingId, int clientId);

        MealPrice GetMealPriceByPeopleCategoryId(int? peoplecategoryId, int mealId, int clientId);
    }

    public class DinnerBookingRepository : AbstractRepository<DinnerBooking>, IDinnerBookingRepository
    {
        public DinnerBookingRepository(ManahostManagerDAL ctx) : base(ctx)
        { }

        public DinnerBooking GetDinnerBookingById(int id, int clientId)
        {
            return GetUniq(p => p.Id == id && p.Home.ClientId == clientId);
        }

        public IEnumerable<MealBooking> GetAllMealByDinnerBooking(int dinnerBookingId, int clientId)
        {
            includes.Add("DinnerBooking");
            return GetList<MealBooking>(p => p.DinnerBooking.Id == dinnerBookingId && p.Home.ClientId == clientId);
        }

        public MealPrice GetMealPriceByPeopleCategoryId(int? peoplecategoryId, int mealId, int clientId)
        {
            includes.Add("PeopleCategory");
            includes.Add("Meal");
            return GetUniq<MealPrice>(p => p.PeopleCategory.Id == peoplecategoryId && p.Meal.Id == mealId && p.Home.ClientId == clientId);
        }
    }
}