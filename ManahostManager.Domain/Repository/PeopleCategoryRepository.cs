using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;

namespace ManahostManager.Domain.Repository
{
    public interface IPeopleCategoryRepository : IAbstractRepository<PeopleCategory>
    {
        PeopleCategory GetPeopleCategoryById(int Id, int clientId);
    }

    public class PeopleCategoryRepository : AbstractRepository<PeopleCategory>, IPeopleCategoryRepository
    {
        public PeopleCategoryRepository(ManahostManagerDAL ctx) : base(ctx)
        { }

        public PeopleCategory GetPeopleCategoryById(int Id, int clientId)
        {
            return GetUniq(p => p.Id == Id && p.Home.ClientId == clientId);
        }

        public override void Delete(PeopleCategory obj)
        {
            includes.Add("PeopleCategory");
            DeleteRange<MealPrice>(GetList<MealPrice>(p => p.PeopleCategory.Id == obj.Id));
            includes.Add("PeopleCategory");
            DeleteRange<PricePerPerson>(GetList<PricePerPerson>(p => p.PeopleCategory.Id == obj.Id));
            includes.Add("PeopleCategory");
            DeleteRange<PeopleBooking>(GetList<PeopleBooking>(p => p.PeopleCategory.Id == obj.Id));
            base.Delete(obj);
        }
    }
}