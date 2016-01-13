using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;

namespace ManahostManager.Domain.Repository
{
    public interface IMealRepository : IAbstractRepository<Meal>
    {
        Meal GetMealById(int Id, int clientId);
    }

    public class MealRepository : AbstractRepository<Meal>, IMealRepository
    {
        public MealRepository(ManahostManagerDAL ctx) : base(ctx)
        { }

        public Meal GetMealById(int Id, int clientId)
        {
            return GetUniq(p => p.Id == Id && p.Home.ClientId == clientId);
        }

        public override void Delete(Meal obj)
        {
            includes.Add("Meal");
            DeleteRange<MealBooking>(GetList<MealBooking>(p => p.Meal.Id == obj.Id));
            base.Delete(obj);
        }
    }
}