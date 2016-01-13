using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManahostManager.Domain.Repository
{
    public interface IBillRepository : IAbstractRepository<Bill>
    {
        Bill GetBillById(int id, int clientId);
    }

    public class BillRepository : AbstractRepository<Bill>, IBillRepository
    {
        public BillRepository(ManahostManagerDAL dal) : base(dal)
        {

        }

        public Bill GetBillById(int id, int clientId)
        {
            return GetUniq(p => p.Id == id && p.Home.ClientId == clientId);
        }
    }
}
