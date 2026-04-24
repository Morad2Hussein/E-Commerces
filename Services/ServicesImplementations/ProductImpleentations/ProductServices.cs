
namespace Services.ServicesImplementations.ProductImpleentations
{
    public class ProductServices(IUnitOfWork _unitOfWork, IMapper _mapper) : IProductServices
    {


        #region Get[All- ById ] Product
        public async Task<IEnumerable<TypeResultDto>> GetAllTypesAsync()
        {

            var Product = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            var ProductDto = _mapper.Map<IEnumerable<TypeResultDto>>(Product);
            return ProductDto;
        }

        #endregion
        #region Get All Brands 
        public async Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync()
        {
            var ProductBrands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            var ProductBrandDto = _mapper.Map<IEnumerable<BrandResultDto>>(ProductBrands);
            return ProductBrandDto;
        }
        #endregion

        #region Get All Products and Get Product By Id

        public async Task<PaginatedResult<ProductResultDto>> GetAllProductsAsync(ProductQueryParams queryParams)
        {
            var spec = new ProductWithBreandAndTypeSpecifications(queryParams);
            var ProductTypes = await _unitOfWork.GetRepository<Product, int>().GetAllAsync(spec);
            var ProductTypeDto = _mapper.Map<IEnumerable<ProductResultDto>>(ProductTypes);
            var CountSpec = new ProductCountSpecifications(queryParams);
            var totaCounts= await _unitOfWork.GetRepository<Product, int>().CountAsync(CountSpec);
            var PageSize = ProductTypeDto.Count();
            return new PaginatedResult<ProductResultDto>(queryParams.PageIndex, PageSize, totaCounts, ProductTypeDto);

        }
        public async Task<ProductResultDto> GetProductByIdAsync(int id)
        {
            var spec = new ProductWithBreandAndTypeSpecifications(id);
            var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(id, spec);
            if (product is null)
                throw new ProductNotFoundException(id);

            return _mapper.Map<ProductResultDto>(product);
        }
        #endregion

    }
}
