
using System.ComponentModel.DataAnnotations;

namespace Shared.DTOS.BasketDTOS
{
    public record BasketItemDTO
        
    {
        public int Id { get; init; }
        public string ProductName { get; init; } = string.Empty;
        public string PictureUrl { get; init; } = string.Empty;
        [Range(1, double.MaxValue)]
        public decimal Price { get; init; }
        [Range(1, 99)]
        public int Quantity { get; init; }
    }
 
}
