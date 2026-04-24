



using Shared.ProductsEnum;

namespace Services.Specifications
{
    internal class ProductWithBreandAndTypeSpecifications : BaseSpecifications<Product, int>
    {
        #region to get all products without Criteria
        public ProductWithBreandAndTypeSpecifications(ProductQueryParams queryParams) : 
            base(SpecificationsProductHelper.GetProductCriteria(queryParams))
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
            switch (queryParams.Sort)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(p => p.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDescending(p => p.Name);
                    break;
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDescending(p => p.Price);
                    break;
                default:
                    AddOrderBy(p => p.Name);
                    break;
            }
            ApplyPagination(queryParams.PageSize, queryParams.PageIndex);
        }
        #endregion
        #region to get product by id 
        public ProductWithBreandAndTypeSpecifications(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
        #endregion
    }


}
