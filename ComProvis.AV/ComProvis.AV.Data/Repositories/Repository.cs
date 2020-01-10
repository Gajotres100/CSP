using ComProvis.AV.Core;
using ComProvis.AV.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComProvis.AV.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IPrimaryKey, new()
    {
        protected readonly IComProvisAvDbContext _context;
        protected readonly DbSet<TEntity> _entitySet;

        public Repository(IComProvisAvDbContext context)
        {
            _context = context;
            _entitySet = _context.ModelSet<TEntity>();
        }

        public IQueryable<TEntity> Fetch() => _entitySet;

        public virtual void Add(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            _entitySet.Add(entity);
        }

        public virtual async Task<TEntity> GetAsync(int id) => await _entitySet.FirstOrDefaultAsync(x => x.Id == id);


        public virtual async Task<ICollection<TEntity>> GetAllAsync() => await _entitySet.AsNoTracking().ToArrayAsync();

        public void Delete(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            _entitySet.Remove(entity);
        }

        public virtual void Update(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            _entitySet.Attach(entity);

            _entitySet.Update(entity);

        }

        public async Task<bool> ExistsAsync(int id) => await _entitySet.AnyAsync(x => x.Id == id);

        public Task<TEntity> ExternalGetAsync(string ExternalId) => _entitySet.FirstOrDefaultAsync(x => x.ExternalId == ExternalId);
    }
}
