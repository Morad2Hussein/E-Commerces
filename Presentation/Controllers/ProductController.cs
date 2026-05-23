

using Presentation.Attributes;

namespace Presentation.Controllers
{
    
    public class ProductsController(IServicesManager _servicesManager) : ApiController
    {
        #region Get All Products
        [RedisCache]
        
        [HttpGet]
        public async Task<ActionResult<PaginatedResult<ProductResultDto>>>  GetAllProducts([FromQuery]ProductQueryParams queryParams)
        {
            var products = await _servicesManager.ProductServices.GetAllProductsAsync(queryParams);
            return Ok(products);
        }
        #endregion
        #region get Product By ID
        [ProducesResponseType(typeof(ProductResultDto), StatusCodes.Status200OK)]
       
       
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductResultDto>> GetProductById(int id)
        {
            var product = await _servicesManager.ProductServices.GetProductByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        #endregion
        #region Get All Brands
        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<BrandResultDto>>> GetAllBrands()
        {
            var brands = await _servicesManager.ProductServices.GetAllBrandsAsync();
            return Ok(brands);
        }

        #endregion
        #region Get All Types
        [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<TypeResultDto>>> GetAllTypes()
        {
            var types = await _servicesManager.ProductServices.GetAllTypesAsync();
            return Ok(types);
        }
        #endregion
    }
}
