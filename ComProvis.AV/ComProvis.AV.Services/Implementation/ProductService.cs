using ComProvis.AV.Core;
using ComProvis.AV.Services.Interfaces;

namespace ComProvis.AV.Services.Implementation
{
    public class ProductService : Service<Product>, IProductService
    {
        public ProductService(IUnitOfWork uow) : base(uow)
        {

        }
    }
}
