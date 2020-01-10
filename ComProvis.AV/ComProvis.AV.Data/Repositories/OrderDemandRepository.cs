using ComProvis.AV.Core;
using ComProvis.AV.Core.Repositories;

namespace ComProvis.AV.Data.Repositories
{
    class OrderDemandRepository : Repository<OrderDemand>, IOrderDemandRepository
    {
        public OrderDemandRepository(IComProvisAvDbContext context) : base(context)
        {

        }
    }
}
