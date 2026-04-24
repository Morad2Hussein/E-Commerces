

using StackExchange.Redis;
using System.Text.Json;

namespace Presistance.Repositries
{
    public class BasketRepository(IConnectionMultiplexer _connection) : IBasketRepository
    {
        private readonly IDatabase _database = _connection.GetDatabase();
        #region Get By ID
        public async Task<CustomerBasket?> GetBasketAsync(string basketId)
        {
            var result = await _database.StringGetAsync(basketId);
            return result.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(result!);
        }
        #endregion

        #region Create or Update
        public async Task<CustomerBasket?> UpdateOrCreateBasketAsync(CustomerBasket basket, TimeSpan? timeToLive = null)
        {
            var serializedBasket = JsonSerializer.Serialize(basket);
            var expiry = timeToLive ?? TimeSpan.FromDays(7);
            var result = await _database.StringSetAsync(basket.Id, serializedBasket, expiry);
            return result ? await GetBasketAsync(basket.Id) : null;
        } 
        #endregion
        public async Task<bool> DeleteBasketAsync(string basketId) 
        => await _database.KeyDeleteAsync(basketId);
    }
}
