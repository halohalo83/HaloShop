using HaloShop.Data.Infrastructure;
using HaloShop.Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace HaloShop.Data.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<ProductCategory> GetByAlias(string alias);
    }

    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<ProductCategory> GetByAlias(string alias)
        {
            return this.DbContext.ProductCategories.Where(x => x.Alias == alias);
        }
    }
}