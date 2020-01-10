using SaaSApi;
using System.Threading.Tasks;

namespace ComProvis.AV.Services.Users
{
    public interface IUserService : IService<User>
    {
        Task<Result> AddUserAsync(User user);
    }
}
