﻿using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManahostManager.Domain.Repository
{
    public interface IBillItemRepository : IAbstractRepository<BillItem>
    {
        BillItem GetBillItemById(int id, int clientId);
    }

    public class BillItemRepository : AbstractRepository<BillItem>, IBillItemRepository
    {
        public BillItemRepository(ManahostManagerDAL dal) : base(dal)
        {

        }

        public BillItem GetBillItemById(int id, int clientId)
        {
            return GetUniq(p => p.Id == id && p.Home.ClientId == clientId);
        }
    }
}
