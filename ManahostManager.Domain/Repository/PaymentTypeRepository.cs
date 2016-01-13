using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;

namespace ManahostManager.Domain.Repository
{
    public interface IPaymentTypeRepository : IAbstractRepository<PaymentType>
    {
        PaymentType GetPaymentTypeById(int id, int clientId);
    }

    public class PaymentTypeRepository : AbstractRepository<PaymentType>, IPaymentTypeRepository
    {
        public PaymentTypeRepository(ManahostManagerDAL ctx) : base(ctx)
        {
        }

        public PaymentType GetPaymentTypeById(int id, int clientId)
        {
            return GetUniq(p => p.Id == id && p.Home.ClientId == clientId);
        }

        public override void Delete(PaymentType obj)
        {
            includes.Add("PaymentType");
            DeleteRange<PaymentMethod>(GetList<PaymentMethod>(p => p.PaymentType.Id == obj.Id));
            base.Delete(obj);
        }
    }
}