using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;
using System.Collections.Generic;

namespace ManahostManager.Domain.Repository
{
    public interface ISupplierRepository : IAbstractRepository<Supplier>
    {
        Supplier GetSupplierById(int Id, int clientId);
    }

    public class SupplierRepository : AbstractRepository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(ManahostManagerDAL ctx) : base(ctx)
        { }

        public Supplier GetSupplierById(int Id, int clientId)
        {
            return GetUniq(p => p.Id == Id && p.Home.ClientId == clientId);
        }

        public override void Delete(Supplier obj)
        {
            IEnumerable<Bill> listBills = GetList<Bill>(p => p.Supplier.Id == obj.Id);
            IEnumerable<Product> listProducts = GetList<Product>(p => p.Supplier.Id == obj.Id);

            foreach (Bill cur in listBills)
            {
                cur.Supplier = null;
                Update<Bill>(cur);
            }
            foreach (Product cur in listProducts)
            {
                cur.Supplier = null;
                Update<Product>(cur);
            }
            base.Delete(obj);
        }
    }
}