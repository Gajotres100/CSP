using ComProvis.AV.Core;
using ComProvis.AV.Core.Entities;
using ComProvis.AV.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComProvis.AV.Data.Repositories
{
    class UserRoleRepository : Repository<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(IComProvisAvDbContext context) : base(context)
        {

        }

        public async Task RemoveUserRoleAsync(int userId, int roleId)
        {
            var userRole = await Fetch().FirstOrDefaultAsync(l => l.RoleId == roleId && l.UserId == userId);
            Delete(userRole);
        }

        public Task<List<SpGetLicenceCountByCompanyId>> GetLicenceCountByCompanyIdAsync(int companyId) => _context.SpGetLicenceCountByCompanyIds.FromSql("EXECUTE [dbo].[GetLicenceCountByCompanyId] @p0", companyId).ToListAsync();

        public async Task<List<UserRole>> GetByUserIdAsync(int Id) => await Fetch().Where(u => u.UserId == Id).ToListAsync();

        public async Task<UserRole> GetByUserIdAndRoleAsync(int userId, int roleId) => await Fetch().FirstOrDefaultAsync(u => u.UserId == userId && u.RoleId == roleId);
    }
}
