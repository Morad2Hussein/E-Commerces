namespace Domain.Contracts.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();

        IGenericRepository<TEntity, TKey> GetRepository<TEntity,TKey>() where TEntity : BaseEntity<TKey>;
    }
}
