using ComProvis.AV.Core.Repositories;
using System.Threading.Tasks;

namespace ComProvis.AV.Core
{
    public interface IUnitOfWork
    {
        IRepository<TEntity> Repository<TEntity>() where TEntity : class, IPrimaryKey, new();
        IApplicationLicencesRepository ApplicationLicencesRepository { get; }
        IApplicationSubscriptionRepository ApplicationSubscriptionRepository { get; }
        ICompanyRepository CompanyRepository { get; }
        IOrderDemandRepository OrderDemandRepository { get; }
        IProductRepository ProductRepository { get; }
        IRolesRepository RolesRepository { get; }
        IUserRepository UserRepository { get; }
        IUserRoleRepository UserRoleRepository { get; }
        ILogDataRepository LogDataRepository { get; }
        Task SaveChangesAsync();
    }
}
