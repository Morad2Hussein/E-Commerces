

using Shared.ProductsEnum;

namespace Shared.QueryParams
{
    public class ProductQueryParams
    {
        public int? TypeId { get; set; }
        public int? BrandId { get; set; }
        public ProductSortingOptions Sort { get; set; }
        public string? Search { get; set; }
        private const int MaxPageSize = 10;
        private const int defaultPageSize = 5;
        private int _pageSize = defaultPageSize;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
        public int PageIndex { get; set; } = 1;
    }
}
