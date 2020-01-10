using ComProvis.AV.Core;
using ComProvis.AV.Core.Repositories;

namespace ComProvis.AV.Data.Repositories
{
    class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(IComProvisAvDbContext context) : base(context)
        {

        }
    }
}
