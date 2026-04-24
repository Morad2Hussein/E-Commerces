


namespace Services_Abstractions.Contracts.BasketServices
{
    public interface IBasketServices
    {
        // Get Basket BY ID
        Task<BasketDTO> GetBasketAsync(string id);
        // Created OR Updated Basket
        Task<BasketDTO> UpdateOrCreateBasketAsync(BasketDTO basketDTO);
        // delete Basket By Id 
        Task<bool> DeleteAsync(string id);
    }
}
