using Domain.Contracts.Specifications;

namespace Presistance.Specifications
{
    internal static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity,TKey>(IQueryable<TEntity> inputQuery, ISpecifications<TEntity,TKey> specifications) where TEntity : BaseEntity<TKey>
        {
            var query = inputQuery;
            if (specifications.Criteria != null)
                query = query.Where(specifications.Criteria);
            if (specifications.OrderBy != null)
                query = query.OrderBy(specifications.OrderBy);
                if (specifications.OrderByDescending != null)
                query = query.OrderByDescending(specifications.OrderByDescending);
            if (specifications.IncludeExpressions != null && specifications.IncludeExpressions.Count > 0)
            {
                query = specifications.IncludeExpressions.Aggregate(query, (current, includeExpression) 
                    => current.Include(includeExpression));
            }
            if (specifications.IsPaginationEnabled)
                query = query.Skip(specifications.Skip).Take(specifications.Take);

            return query;
        }
    }
}
