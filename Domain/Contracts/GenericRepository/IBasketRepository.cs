
using Domain.Entities.BasketModule;

namespace Domain.Contracts.GenericRepository
{
    public interface IBasketRepository
    {
        // Get Basket By Id
        public Task<CustomerBasket?> GetBasketAsync(string basketId);
        // Update Or Create Basket
        public Task<CustomerBasket?> UpdateOrCreateBasketAsync(CustomerBasket basket, TimeSpan? timeToLive = null);
        // Delete Basket
        Task<bool> DeleteBasketAsync(string basketId);


    }
}
