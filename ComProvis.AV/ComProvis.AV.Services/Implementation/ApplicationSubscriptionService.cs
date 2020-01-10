using ComProvis.AV.Core;
using ComProvis.AV.Services.Interfaces;

namespace ComProvis.AV.Services.Implementation
{
    public class ApplicationSubscriptionService : Service<ApplicationSubscription>, IApplicationSubscriptionService
    {
        public ApplicationSubscriptionService(IUnitOfWork uow) : base(uow)
        {

        }
    }
}
