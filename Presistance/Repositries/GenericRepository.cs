
using Domain.Contracts.GenericRepository;
using Domain.Contracts.Specifications;
using Presistance.Specifications;

namespace Presistance.Repositries
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly StoreDbContext _dbContext;
        public GenericRepository(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        #region Read Only GETALL , Get ByID 
        #region GETALL
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool asNoTracking = false)
          => asNoTracking ? await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync() :
              await _dbContext.Set<TEntity>().ToListAsync();
        #endregion

        #region GetById
        public async Task<TEntity?> GetByIdAsync(TKey id)
        => await _dbContext.Set<TEntity>().FindAsync(id);

        #endregion
        #endregion

        #region Specifications
        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, TKey> specifications)
        => await SpecificationEvaluator.CreateQuery(_dbContext.Set<TEntity>(), specifications).ToListAsync();

        public async Task<TEntity?> GetByIdAsync(TKey id, ISpecifications<TEntity, TKey> specifications)
            => await SpecificationEvaluator.CreateQuery(_dbContext.Set<TEntity>(), specifications).FirstOrDefaultAsync();
        public async Task<TEntity?> GetByIdAsync(ISpecifications<TEntity, TKey> specifications)
    => await SpecificationEvaluator.CreateQuery(_dbContext.Set<TEntity>(), specifications).FirstOrDefaultAsync();
        #endregion
        #region Add 
        public async Task AddAsync(TEntity entity)
     => await _dbContext.Set<TEntity>().AddAsync(entity); 
        #endregion

        #region Delete
        public void Delete(TEntity entity)
      => _dbContext.Set<TEntity>().Remove(entity);
        #endregion
        #region CounAsync
        public async Task<int> CountAsync(ISpecifications<TEntity, TKey> specifications)
        => await SpecificationEvaluator.CreateQuery(_dbContext.Set<TEntity>(), specifications).CountAsync();
        #endregion

        #region Update
        public void Update(TEntity entity)
       => _dbContext.Set<TEntity>().Update(entity);



        #endregion
    }
}
