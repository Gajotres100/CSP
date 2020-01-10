using ComProvis.AV.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComProvis.AV.Services
{
    public class Service<TEntity> : IService<TEntity> where TEntity : class, IPrimaryKey, new()
    {
        private readonly IUnitOfWork _unitOfWork;

        protected IUnitOfWork UnitOfWork
        {
            get { return _unitOfWork; }
        }
        public Service(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Add(TEntity entity)
        {
            UnitOfWork.Repository<TEntity>().Add(entity);
        }

        public void Delete(TEntity entity)
        {
            UnitOfWork.Repository<TEntity>().Delete(entity);
        }

        public async Task<bool> ExistsAsync(int id) => await UnitOfWork.Repository<TEntity>().ExistsAsync(id);

        public async Task<TEntity> GetAsync(int id) => await UnitOfWork.Repository<TEntity>().GetAsync(id);

        public async Task<TEntity> ExternalGetAsync(string externalId)
        {
            return await UnitOfWork.Repository<TEntity>().ExternalGetAsync(externalId);
        }

        public virtual async Task<ICollection<TEntity>> GetAllAsync() => await UnitOfWork.Repository<TEntity>().GetAllAsync();

        public void Update(TEntity entity)
        {
            UnitOfWork.Repository<TEntity>().Update(entity);
        }
    }
}
