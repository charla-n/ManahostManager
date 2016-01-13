using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;

namespace ManahostManager.Domain.Repository
{
    public interface IBillItemCategoryRepository : IAbstractRepository<BillItemCategory>
    {
        BillItemCategory GetBillItemCategoryById(int id, int clientId);
    }

    public class BillItemCategoryRepository : AbstractRepository<BillItemCategory>, IBillItemCategoryRepository
    {
        public BillItemCategoryRepository(ManahostManagerDAL ctx)
            : base(ctx)
        { }

        public BillItemCategory GetBillItemCategoryById(int id, int clientId)
        {
            return GetUniq(p => p.Id == id && p.Home.ClientId == clientId);
        }
    }
}