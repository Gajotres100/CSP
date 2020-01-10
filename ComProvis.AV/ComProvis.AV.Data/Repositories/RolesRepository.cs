using ComProvis.AV.Core;
using ComProvis.AV.Core.Repositories;

namespace ComProvis.AV.Data.Repositories
{
    class RolesRepository : Repository<Roles>, IRolesRepository
    {
        public RolesRepository(IComProvisAvDbContext context) : base(context)
        {

        }
    }
}
