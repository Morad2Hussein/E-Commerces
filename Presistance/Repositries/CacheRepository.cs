using StackExchange.Redis;
using System.Text.Json;

namespace Presistance.Repositries
{
    public class CacheRepository(IConnectionMultiplexer _connection) : ICacheRepository
    {
        private readonly IDatabase _database = _connection.GetDatabase();
        public async Task<string?> GetAsync(string key)
        {
         var value = await _database.StringGetAsync(key);
            return  value.IsNullOrEmpty ? default : value;
        }

        public Task SetAsync(string key, object value, TimeSpan duration)
        {
          var Serializeobj = JsonSerializer.Serialize(value);
            return _database.StringSetAsync(key, Serializeobj, duration);
        }
    }
}
