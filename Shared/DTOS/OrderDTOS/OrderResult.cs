

namespace Shared.DTOS.OrderDTOS
{
    public record   OrderResult
    {
        public Guid Id { get; init; }
        public string UserName { get; init; } = string.Empty;
        public AddressDtos ShippingAddress { get; init; }
        public ICollection<OrderItemDTO> OrderItems { get; init; } = [];
        public string PaymentStatus { get; init; } = string.Empty;
        public string DeliveryMethod { get; init; } = string.Empty;
        public int? DeliveryMethodId { get; init; }
        public decimal Subtotal { get; init; }
        public decimal Total { get; init; }   // Total = Subtotal + DeliveryPrice
        public DateTimeOffset OrderDate { get; init; } = DateTimeOffset.UtcNow;
        public string PaymentIntentId { get; init; } = string.Empty;
    }
}
