namespace Services_Abstractions.Contracts.ProductServices
{
    public interface IProductServices
    {
        // Get All Products
        Task<PaginatedResult<ProductResultDto>> GetAllProductsAsync(ProductQueryParams queryParams);
        // Get Product By Id
        Task<ProductResultDto> GetProductByIdAsync(int id);
        // get All Brands 
        Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync();
        // get All Types
        Task<IEnumerable<TypeResultDto>> GetAllTypesAsync();
    }
}
