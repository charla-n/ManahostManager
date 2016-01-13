using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;
using System.Collections.Generic;

namespace ManahostManager.Domain.Repository
{
    public interface IProductCategoryRepository : IAbstractRepository<ProductCategory>
    {
        ProductCategory GetProductCategoryById(int Id, int clientId);
    }

    public class ProductCategoryRepository : AbstractRepository<ProductCategory>, IProductCategoryRepository
    {
        public ProductCategoryRepository(ManahostManagerDAL ctx) : base(ctx)
        { }

        public ProductCategory GetProductCategoryById(int Id, int clientId)
        {
            return GetUniq(p => p.Id == Id && p.Home.ClientId == clientId);
        }

        public override void Delete(ProductCategory obj)
        {
            IEnumerable<Product> productList = GetList<Product>(p => p.ProductCategory.Id == obj.Id);

            foreach (Product cur in productList)
            {
                cur.ProductCategory = null;
                Update<Product>(cur);
            }
            base.Delete(obj);
        }
    }
}