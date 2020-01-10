using ComProvis.AV.Core;
using ComProvis.AV.Core.Repositories;

namespace ComProvis.AV.Data.Repositories
{
    class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(IComProvisAvDbContext context) : base(context)
        {

        }
    }
}
