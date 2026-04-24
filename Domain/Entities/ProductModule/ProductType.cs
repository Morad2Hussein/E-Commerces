
namespace Domain.Entities.ProductModule
{
    public class ProductType : BaseEntity<int>
    {
        #region Properties
         public string Name { get; set; } = default!;
        #endregion
      
    }
}
