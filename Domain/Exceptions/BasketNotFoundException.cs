

namespace Domain.Exceptions
{
    public sealed class BasketNotFoundException : NotFoundException
    {
        public BasketNotFoundException(string id) : base($"Basket With Id: {id} Not Found")
        {
        }
    }
}
