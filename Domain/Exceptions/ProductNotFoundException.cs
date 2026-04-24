
namespace Domain.Exceptions
{
    public sealed class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException(int  id) : base($"The Product With Id :{id}  not Found")
        {
        }
    }
}
