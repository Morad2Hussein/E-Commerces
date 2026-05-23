
namespace Shared.DTOS.OrderDTOS
{
    public record OrderRequest
    {
        public string BasketId { get; init; } = string.Empty;
        public AddressDtos ShipToAddress { get; init; }
        public int DeliveryMethodId { get; init; }

    }
}
