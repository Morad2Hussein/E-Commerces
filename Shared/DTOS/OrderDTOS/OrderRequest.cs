
namespace Shared.DTOS.OrderDTOS
{
    public record OrderRequest
    {
        public string BasketId { get; init; } = string.Empty;
        public AddressDtos ShippingAddress { get; init; }
        public int DeliveryMethodId { get; init; }

    }
}
