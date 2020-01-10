using ComProvis.AV.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComProvis.AV.Core.Repositories
{
    public interface IUserRoleRepository : IRepository<UserRole>
    {
        Task RemoveUserRoleAsync(int userId, int roleId);
        Task<List<SpGetLicenceCountByCompanyId>> GetLicenceCountByCompanyIdAsync(int companyId);
        Task<List<UserRole>> GetByUserIdAsync(int Id);
        Task<UserRole> GetByUserIdAndRoleAsync(int userId, int roleId);
    }
}
