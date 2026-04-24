


namespace Services.Specifications
{
    internal class ProductCountSpecifications : BaseSpecifications<Product, int>
    {
        public ProductCountSpecifications(ProductQueryParams productQuery) :
            base(SpecificationsProductHelper.GetProductCriteria(productQuery) )
        {
        }
    }
}
