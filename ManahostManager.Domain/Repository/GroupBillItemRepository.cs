using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManahostManager.Domain.Repository
{
    public interface IGroupBillItemRepository : IAbstractRepository<GroupBillItem>
    {
        GroupBillItem GroupBillItemById(int id, int clientId);
    }

    public class GroupBillItemRepository : AbstractRepository<GroupBillItem>, IGroupBillItemRepository
    {
        public GroupBillItemRepository(ManahostManagerDAL dal) : base(dal)
        {

        }

        public GroupBillItem GroupBillItemById(int id, int clientId)
        {
            return GetUniq(p => p.Id == id && p.Home.ClientId == clientId);
        }
    }
}
