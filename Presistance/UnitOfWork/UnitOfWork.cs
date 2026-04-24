
using System.Collections.Concurrent;

namespace Presistance.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _dbcontext;
        private readonly ConcurrentDictionary<Type, object> _repositories;
        public UnitOfWork(StoreDbContext dbcontext)
        {
            _dbcontext = dbcontext;
            _repositories = new ConcurrentDictionary<Type, object>();
        }
        #region SaveChanges
        public async Task<int> SaveChangesAsync()
        
          => await _dbcontext.SaveChangesAsync();
        #endregion
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        
            => (IGenericRepository<TEntity, TKey>)_repositories.GetOrAdd(typeof(TEntity), (type) => new GenericRepository<TEntity, TKey>(_dbcontext));

    }
}
