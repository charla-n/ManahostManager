using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;
using System.Collections.Generic;

namespace ManahostManager.Domain.Repository
{
    public interface IMealCategoryRepository : IAbstractRepository<MealCategory>
    {
        MealCategory GetMealCategoryById(int Id, int clientId);
    }

    public class MealCategoryRepository : AbstractRepository<MealCategory>, IMealCategoryRepository
    {
        public MealCategoryRepository(ManahostManagerDAL ctx) : base(ctx)
        { }

        public MealCategory GetMealCategoryById(int Id, int clientId)
        {
            return GetUniq(p => p.Id == Id && p.Home.ClientId == clientId);
        }

        public override void Delete(MealCategory obj)
        {
            includes.Add("MealCategory");
            IEnumerable<Meal> listMeal = GetList<Meal>(p => p.MealCategory.Id == obj.Id);

            foreach (Meal cur in listMeal)
            {
                cur.MealCategory = null;
                Update<Meal>(cur);
            }
            base.Delete(obj);
        }
    }
}