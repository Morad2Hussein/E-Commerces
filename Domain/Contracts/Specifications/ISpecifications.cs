using System.Linq.Expressions;

namespace Domain.Contracts.Specifications
{
    public interface ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        #region Criteria
        public Expression<Func<TEntity, bool>>? Criteria { get; }

        #endregion
        #region Include
        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; }
        #endregion
        #region Order By [Asc and Desc]
        public Expression<Func<TEntity, object>>? OrderBy { get; }
        public Expression<Func<TEntity, object>>? OrderByDescending { get; }
        #endregion
        #region Paginations
        public int Take { get; }
        public int Skip { get; }
        public bool IsPaginationEnabled { get; }
        #endregion
    }
}
