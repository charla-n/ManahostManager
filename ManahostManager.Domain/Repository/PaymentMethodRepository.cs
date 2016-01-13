using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManahostManager.Domain.Repository
{
    public interface IPaymentMethodRepository : IAbstractRepository<PaymentMethod>
    {
        PaymentMethod GetPaymentMethodById(int id, int clientid);
    }

    public class PaymentMethodRepository : AbstractRepository<PaymentMethod>, IPaymentMethodRepository
    {
        public PaymentMethodRepository(ManahostManagerDAL ctx) : base(ctx)
        { }

        public PaymentMethod GetPaymentMethodById(int id, int clientid)
        {
            return GetUniq(p => p.Id == id && p.Home.ClientId == clientid);
        }
    }
}
