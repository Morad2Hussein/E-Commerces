
namespace Domain.Entities.ProductModule
{
    public class Product : BaseEntity<int>
    {
        #region Properties
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string PictureUrl { get; set; } = default!;
        public decimal Price { get; set; }

        #endregion
        #region Relationships
        public int TypeId { get; set; }
        public ProductType ProductType { get; set; }  = default!;

        public int BrandId { get; set; }
        public ProductBrand ProductBrand { get; set; }  = default!;

        #endregion
    }
}
