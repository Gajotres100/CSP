using ComProvis.AV.Core;
using ComProvis.AV.Core.Repositories;

namespace ComProvis.AV.Data.Repositories
{
    class ApplicationSubscriptionRepository : Repository<ApplicationSubscription>, IApplicationSubscriptionRepository
    {
        public ApplicationSubscriptionRepository(IComProvisAvDbContext context) : base(context)
        {

        }
    }
}
