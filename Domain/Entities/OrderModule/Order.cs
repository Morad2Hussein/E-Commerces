using ShippingAddress = Domain.Entities.OrderModule.Address;
namespace Domain.Entities.OrderModule
{
    public class Order : BaseEntity<Guid>
    {
        public Order() { }
        public Order(string userName, ShippingAddress shippingAddress, ICollection<OrderItem> orderItems, DeliveryMethod deliveryMethod, decimal subtotal)
        {
            Id = Guid.NewGuid();
            UserName = userName;
            ShippingAddress = shippingAddress;
            OrderItems = orderItems;
            DeliveryMethod = deliveryMethod;
            Subtotal = subtotal;
        }

        public string UserName { get; set; } = string.Empty;
        public ShippingAddress ShippingAddress { get; set; } 
        public ICollection<OrderItem> OrderItems { get; set; } =[];
         public OrderPaymentStatus PaymentStatus { get; set; } = OrderPaymentStatus.Pending;
         public DeliveryMethod DeliveryMethod { get; set; }
        public int? DeliveryMethodId { get; set; }
        public decimal Subtotal { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
        public string PaymentIntentId { get; set; } = string.Empty;
    }
}
