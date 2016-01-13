using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;

namespace ManahostManager.Domain.Repository
{
    public interface IMealPriceRepository : IAbstractRepository<MealPrice>
    {
        MealPrice GetMealPriceById(int Id, int clientId);
    }

    public class MealPriceRepository : AbstractRepository<MealPrice>, IMealPriceRepository
    {
        public MealPriceRepository(ManahostManagerDAL ctx) : base(ctx)
        { }

        public MealPrice GetMealPriceById(int Id, int clientId)
        {
            return GetUniq(p => p.Id == Id && p.Home.ClientId == clientId);
        }
    }
}