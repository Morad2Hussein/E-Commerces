 

namespace Shared.DTOS.BasketDTOS
{
    public record BasketDTO
    {
      public    string Id { get; init; } = string.Empty;
      public  ICollection<BasketItemDTO> BasketItemDTO { get; init; } = [];  
    }
}
