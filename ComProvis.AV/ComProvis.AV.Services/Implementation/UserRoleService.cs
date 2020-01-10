using ComProvis.AV.Core;
using ComProvis.AV.Core.Entities;
using ComProvis.AV.Services.Interfaces;
using SaaSApi;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComProvis.AV.Services.Implementation
{
    public class UserRoleService : Service<UserRole>, IUserRoleService
    {
        public UserRoleService(IUnitOfWork uow) : base(uow)
        {

        }

        public async Task<Result> RemoveUserRole(int userId, int roleId)
        {
            await UnitOfWork.UserRoleRepository.RemoveUserRoleAsync(userId, roleId);
            await UnitOfWork.SaveChangesAsync();
            return new Result(true, null);
        }

        public Task<List<SpGetLicenceCountByCompanyId>> GetLicenceCountByCompanyIdAsync(int companyId) => UnitOfWork.UserRoleRepository.GetLicenceCountByCompanyIdAsync(companyId);

        public async Task<List<UserRole>> GetByUserIDAsync(int Id) => await UnitOfWork.UserRoleRepository.GetByUserIdAsync(Id);

        public async Task<UserRole> GetByUserIdAndRoleAsync(int UserId, int RoleId) => await UnitOfWork.UserRoleRepository.GetByUserIdAndRoleAsync(UserId, RoleId);
    }
}
