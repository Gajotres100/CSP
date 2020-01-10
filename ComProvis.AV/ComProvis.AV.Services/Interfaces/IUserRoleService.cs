using ComProvis.AV.Core.Entities;
using SaaSApi;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComProvis.AV.Services.Interfaces
{
    public interface IUserRoleService : IService<UserRole>
    {
        Task<Result> RemoveUserRole(int userId, int roleId);

        Task<List<SpGetLicenceCountByCompanyId>> GetLicenceCountByCompanyIdAsync(int companyId);
        Task<List<UserRole>> GetByUserIDAsync(int Id);
        Task<UserRole> GetByUserIdAndRoleAsync(int UserId, int RoleId);
    }
}
