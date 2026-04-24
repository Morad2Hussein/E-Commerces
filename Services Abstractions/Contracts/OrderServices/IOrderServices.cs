

namespace Services_Abstractions.Contracts.OrderServices
{
    public interface IOrderServices
    {
        // Get OrdersBy Id  === > Take Guid ID and return OrderResult DTO   
        Task<OrderResult> GetOrderByIdAsync(Guid id);
        // Get Order By Email === > Take Email and return IEnumerable of OrderResult DTOs
        Task<IEnumerable<OrderResult>> GetOrdersByEmailAsync(string useremail);
        // Create Order === > Take CreateOrderDTO and return OrderResult DTO
        Task<OrderResult> CreateOrderAsync(OrderRequest order , string useremail);
        //Get Delivery Methods === > Return IEnumerable of DeliveryMethod DTOs
        Task<IEnumerable<DeliveryMethodResult>> GetDeliveryMethodsAsync();
    }
}
