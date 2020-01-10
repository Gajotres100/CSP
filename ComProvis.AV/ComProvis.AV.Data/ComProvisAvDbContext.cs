using ComProvis.AV.Core;
using ComProvis.AV.Core.Entities;
using ComProvis.AV.Data.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ComProvis.AV.Data
{
    public class ComProvisAvDbContext : DbContext, IComProvisAvDbContext
    {
        public ComProvisAvDbContext()
        {
        }

        public ComProvisAvDbContext(DbContextOptions<ComProvisAvDbContext> options) : base(options)
        {
        }

        public async Task<int> ExecuteSqlCommandAsync(string sql, params object[] parameters) => await Database.ExecuteSqlCommandAsync(sql, parameters);

        public DbSet<SpGetLicenceCountByCompanyId> SpGetLicenceCountByCompanyIds { get; set; }

        public virtual DbSet<TModel> ModelSet<TModel>() where TModel : class => Set<TModel>();

        public new async Task SaveChangesAsync() => await base.SaveChangesAsync();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ApplicationLicencesConfiguration())
                .ApplyConfiguration(new ApplicationSubscriptionConfiguration())
                .ApplyConfiguration(new CompanyConfiguration())
                .ApplyConfiguration(new OrderDemandConfiguration())
                .ApplyConfiguration(new ProductConfiguration())
                .ApplyConfiguration(new RolesConfiguration())
                .ApplyConfiguration(new UserConfiguration())
                .ApplyConfiguration(new UserRoleConfiguration());
        }
    }
}
