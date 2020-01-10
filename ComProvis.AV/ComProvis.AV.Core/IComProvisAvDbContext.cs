using ComProvis.AV.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ComProvis.AV.Core
{
    public interface IComProvisAvDbContext
    {
        Task SaveChangesAsync();

        DbSet<TModel> ModelSet<TModel>() where TModel : class;

        DbSet<SpGetLicenceCountByCompanyId> SpGetLicenceCountByCompanyIds { get; set; }

        Task<int> ExecuteSqlCommandAsync(string sql, params object[] parameters);
    }
}
