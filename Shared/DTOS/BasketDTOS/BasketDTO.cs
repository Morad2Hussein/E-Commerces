 

namespace Shared.DTOS.BasketDTOS
{
    public record BasketDTO
    {
      public    string Id { get; init; } = string.Empty;
      public  ICollection<BasketItemDTO> Items { get; init; } = [];  
         public string? PaymentIntentId { get; init; }
          public string? ClientSecret { get; init; }
          public decimal? ShippingPrice { get; init; }
          public int? DeliveryMethodId { get; init; }
    }
}
