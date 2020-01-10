using ComProvis.AV.Core;
using ComProvis.AV.Services.Companys;
using ComProvis.AV.Services.Implementation;
using ComProvis.AV.Services.Interfaces;
using ComProvis.AV.Services.Users;
using System.Threading.Tasks;

namespace ComProvis.AV.Services
{
    public class DataService : IDataService
    {
        private readonly IUnitOfWork _unitOfWork;

        private IApplicationLicencesService _applicationLicences;

        private IApplicationSubscriptionService _applicationSubscriptionService;

        private IProductService _productService;

        private IRolesService _rolesService;

        private ICompanyService _companyService;

        private IUserRoleService _userRoleService;

        private IUserService _userService;

        private ILogDataService _logDataService;

        public DataService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IApplicationLicencesService ApplicationLicencesService
        {
            get
            {
                if (_applicationLicences == null)
                {
                    _applicationLicences = new ApplicationLicencesService(_unitOfWork);
                    return _applicationLicences;
                }
                return _applicationLicences;
            }
        }

        public IApplicationSubscriptionService ApplicationSubscriptionService
        {
            get
            {
                if (_applicationSubscriptionService == null)
                {
                    _applicationSubscriptionService = new ApplicationSubscriptionService(_unitOfWork);
                    return _applicationSubscriptionService;
                }
                return _applicationSubscriptionService;
            }
        }

        public IProductService ProductService
        {
            get
            {
                if (_productService == null)
                {
                    _productService = new ProductService(_unitOfWork);
                    return _productService;
                }
                return _productService;
            }
        }

        public IRolesService RolesService
        {
            get
            {
                if (_rolesService == null)
                {
                    _rolesService = new RolesService(_unitOfWork);
                    return _rolesService;
                }
                return _rolesService;
            }
        }

        public ICompanyService CompanyService
        {
            get
            {
                if (_companyService == null)
                {
                    _companyService = new CompanyService(_unitOfWork);
                    return _companyService;
                }
                return _companyService;
            }
        }

        public IUserRoleService UserRoleService
        {
            get
            {
                if (_userRoleService == null)
                {
                    _userRoleService = new UserRoleService(_unitOfWork);
                    return _userRoleService;
                }
                return _userRoleService;
            }
        }

        public IUserService UserService
        {
            get
            {
                if (_userService == null)
                {
                    _userService = new UserService(_unitOfWork);
                    return _userService;
                }
                return _userService;
            }
        }
        public ILogDataService LogDataService
        {
            get
            {
                if (_logDataService == null)
                {
                    _logDataService = new LogDataService(_unitOfWork);
                    return _logDataService;
                }
                return _logDataService;
            }
        }

        public async Task SaveChangesAsync()
        {
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
