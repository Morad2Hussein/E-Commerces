namespace Shared.Paginated
{ 
    public record PaginatedResult<TEntity>(int pageIndex, int pageSize, int totalCount, IEnumerable<TEntity> data);

}
