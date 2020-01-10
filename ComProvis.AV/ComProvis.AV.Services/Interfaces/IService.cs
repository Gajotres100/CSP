using ComProvis.AV.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComProvis.AV.Services
{
    public interface IService<TEntity> where TEntity : class, IPrimaryKey, new()
    {
        void Add(TEntity entity);
        Task<TEntity> GetAsync(int id);
        Task<TEntity> ExternalGetAsync(string externalId);
        Task<ICollection<TEntity>> GetAllAsync();
        void Delete(TEntity entity);
        void Update(TEntity entity);
        Task<bool> ExistsAsync(int id);
    }
}
