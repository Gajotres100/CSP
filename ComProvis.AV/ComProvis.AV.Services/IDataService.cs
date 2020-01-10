using ComProvis.AV.Services.Companys;
using ComProvis.AV.Services.Interfaces;
using ComProvis.AV.Services.Users;
using System.Threading.Tasks;

namespace ComProvis.AV.Services
{
    public interface IDataService
    {
        IApplicationLicencesService ApplicationLicencesService { get; }

        IApplicationSubscriptionService ApplicationSubscriptionService { get; }

        ICompanyService CompanyService { get; }

        IProductService ProductService { get; }

        IRolesService RolesService { get; }

        IUserRoleService UserRoleService { get; }

        IUserService UserService { get; }

        ILogDataService LogDataService { get; }

        Task SaveChangesAsync();
    }
}
