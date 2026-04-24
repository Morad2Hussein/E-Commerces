

namespace Services.Specifications
{
    public abstract class BaseSpecifications<TEntity, TKey> : ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        protected BaseSpecifications(Expression<Func<TEntity, bool>>? criteria)
        {
            Criteria = criteria;
        }
        #region Criteria
        public Expression<Func<TEntity, bool>>? Criteria { get; private set; }

        #endregion
        #region Include
        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = [];

        #endregion
        #region order
        public Expression<Func<TEntity, object>>? OrderBy { get; private set; }

        public Expression<Func<TEntity, object>>? OrderByDescending { get; private set; }

        #endregion
        #region Paginations 
        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPaginationEnabled { get; private set; }
        #endregion

        protected void AddInclude(Expression<Func<TEntity, object>> includeExpression)
        {
            IncludeExpressions?.Add(includeExpression);
        }
        protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExpression)
         => OrderBy = orderByExpression;
        protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescendingExpression)
         => OrderByDescending = orderByDescendingExpression;
        protected void ApplyPagination(int pageSize, int pageIndex)
        {
            IsPaginationEnabled = true;
            Take = pageSize;
            Skip = (pageIndex - 1) * pageSize;
        }
    }
}
