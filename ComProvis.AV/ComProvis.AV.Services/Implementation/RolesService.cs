using ComProvis.AV.Core;
using ComProvis.AV.Services.Interfaces;

namespace ComProvis.AV.Services.Implementation
{
    public class RolesService : Service<Roles>, IRolesService
    {
        public RolesService(IUnitOfWork uow) : base(uow)
        {

        }
    }
}
