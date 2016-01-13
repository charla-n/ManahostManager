using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;
using System.Collections.Generic;

namespace ManahostManager.Domain.Repository
{
    public interface ITaxRepository : IAbstractRepository<Tax>
    {
        Tax GetTaxById(int Id, int clientId);
    }

    public class TaxRepository : AbstractRepository<Tax>, ITaxRepository
    {
        public TaxRepository(ManahostManagerDAL ctx) : base(ctx)
        { }

        public Tax GetTaxById(int Id, int clientId)
        {
            return GetUniq(p => p.Id == Id && p.Home.ClientId == clientId);
        }

        public override void Delete(Tax obj)
        {
            IEnumerable<Product> listProducts = GetList<Product>(p => p.Tax.Id == obj.Id);
            IEnumerable<MealPrice> listMealPrice = GetList<MealPrice>(p => p.Tax.Id == obj.Id);
            IEnumerable<PeopleCategory> listPeopleCategory = GetList<PeopleCategory>(p => p.Tax.Id == obj.Id);
            IEnumerable<PricePerPerson> listPricePerPerson = GetList<PricePerPerson>(p => p.Tax.Id == obj.Id);
            IEnumerable<RoomSupplement> listRoomSupplement = GetList<RoomSupplement>(p => p.Tax.Id == obj.Id);

            foreach (Product cur in listProducts)
                cur.Tax = null;
            foreach (MealPrice cur in listMealPrice)
                cur.Tax = null;
            foreach (PeopleCategory cur in listPeopleCategory)
                cur.Tax = null;
            foreach (PricePerPerson cur in listPricePerPerson)
                cur.Tax = null;
            foreach (RoomSupplement cur in listRoomSupplement)
                cur.Tax = null;
            base.Delete(obj);
        }
    }
}