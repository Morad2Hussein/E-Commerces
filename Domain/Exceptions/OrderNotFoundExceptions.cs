

using Domain.Entities.OrderModule;

namespace Domain.Exceptions
{
    public sealed class OrderNotFoundExceptions : NotFoundException
    {
        public OrderNotFoundExceptions(Guid id) : base($"Order with id {id} not found")
        {
        }
    }
}
