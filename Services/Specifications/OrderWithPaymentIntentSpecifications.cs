


namespace Services.Specifications
{
    public class OrderWithPaymentIntentSpecifications : BaseSpecifications<Order, Guid>
    {
        public OrderWithPaymentIntentSpecifications(string paymentId) : base(o => o.PaymentIntentId == paymentId)
        {
        }
    }
}
