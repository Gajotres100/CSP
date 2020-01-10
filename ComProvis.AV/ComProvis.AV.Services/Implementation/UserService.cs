using ComProvis.AV.Core;
using SaaSApi;
using System.Threading.Tasks;

namespace ComProvis.AV.Services.Users
{
    public class UserService : Service<User>, IUserService
    {
        public UserService(IUnitOfWork uow) : base(uow)
        {

        }

        public async Task<Result> AddUserAsync(User user)
        {
            UnitOfWork.UserRepository.Add(user);
            await UnitOfWork.SaveChangesAsync();
            return new Result(true, null);
        }
    }
}
