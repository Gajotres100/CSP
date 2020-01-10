using ComProvis.AV.Core;
using ComProvis.AV.Core.Repositories;
using ComProvis.AV.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComProvis.AV.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IComProvisAvDbContext _dbContext;
        private Dictionary<Type, object> cachedGenericRepo = new Dictionary<Type, object>();

        private IApplicationLicencesRepository _applicationLicencesRepository;
        private IApplicationSubscriptionRepository _applicationSubscriptionRepository;
        private ICompanyRepository _companyRepository;
        private IOrderDemandRepository _orderDemandRepository;
        private IProductRepository _productRepository;
        private IRolesRepository _rolesRepository;
        private IUserRepository _userRepository;
        private IUserRoleRepository _userRoleRepository;
        private ILogDataRepository _LogDataRepository;

        public UnitOfWork(IComProvisAvDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IApplicationLicencesRepository ApplicationLicencesRepository => _applicationLicencesRepository ??
                                                                               (_applicationLicencesRepository = new ApplicationLicencesRepository(_dbContext));

        public IApplicationSubscriptionRepository ApplicationSubscriptionRepository => _applicationSubscriptionRepository ?? (_applicationSubscriptionRepository =
                           new ApplicationSubscriptionRepository(_dbContext));

        public ICompanyRepository CompanyRepository => _companyRepository ?? (_companyRepository = new CompanyRepository(_dbContext));

        public IOrderDemandRepository OrderDemandRepository => _orderDemandRepository ?? (_orderDemandRepository = new OrderDemandRepository(_dbContext));

        public IProductRepository ProductRepository => _productRepository ?? (_productRepository = new ProductRepository(_dbContext));

        public IRolesRepository RolesRepository => _rolesRepository ?? (_rolesRepository = new RolesRepository(_dbContext));

        public IUserRepository UserRepository => _userRepository ?? (_userRepository = new UserRepository(_dbContext));

        public IUserRoleRepository UserRoleRepository => _userRoleRepository ?? (_userRoleRepository = new UserRoleRepository(_dbContext));

        public ILogDataRepository LogDataRepository => _LogDataRepository ?? (_LogDataRepository = new LogDataRepository(_dbContext));

        public virtual IRepository<TEntity> Repository<TEntity>() where TEntity : class, IPrimaryKey, new()
        {
            if (cachedGenericRepo.ContainsKey(typeof(TEntity))) return cachedGenericRepo[typeof(TEntity)] as IRepository<TEntity>;
            var repo = new Repository<TEntity>(_dbContext);
            cachedGenericRepo.Add(typeof(TEntity), repo);
            return repo;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
