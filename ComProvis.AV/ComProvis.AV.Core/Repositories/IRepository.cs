using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComProvis.AV.Core.Repositories
{
    public interface IRepository<TEntity> where TEntity : class, IPrimaryKey, new()
    {
        IQueryable<TEntity> Fetch();
        void Add(TEntity entity);
        Task<TEntity> GetAsync(int id);
        Task<TEntity> ExternalGetAsync(string ExternalId);
        Task<ICollection<TEntity>> GetAllAsync();
        void Delete(TEntity entity);
        void Update(TEntity entity);
        Task<bool> ExistsAsync(int id);
    }
}
