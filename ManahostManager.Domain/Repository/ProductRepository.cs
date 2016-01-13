using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;

namespace ManahostManager.Domain.Repository
{
    public interface IProductRepository : IAbstractRepository<Product>
    {
        Product GetProductById(int Id, int clientId);
    }

    public class ProductRepository : AbstractRepository<Product>, IProductRepository
    {
        public ProductRepository(ManahostManagerDAL ctx) : base(ctx)
        { }

        public Product GetProductById(int Id, int clientId)
        {
            return GetUniq(p => p.Id == Id && p.Home.ClientId == clientId);
        }

        public override void Delete(Product obj)
        {
            DeleteRange<ProductBooking>(GetList<ProductBooking>(p => p.Product.Id == obj.Id));
            base.Delete(obj);
        }
    }
}