using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;

namespace ManahostManager.Domain.Repository
{
    public interface IProductBookingRepository : IAbstractRepository<ProductBooking>
    {
        ProductBooking GetProductBookingById(int id, int clientId);
    }

    public class ProductBookingRepository : AbstractRepository<ProductBooking>, IProductBookingRepository
    {
        public ProductBookingRepository(ManahostManagerDAL ctx) : base(ctx)
        { }

        public ProductBooking GetProductBookingById(int id, int clientId)
        {
            return GetUniq(p => p.Id == id && p.Home.ClientId == clientId);
        }
    }
}