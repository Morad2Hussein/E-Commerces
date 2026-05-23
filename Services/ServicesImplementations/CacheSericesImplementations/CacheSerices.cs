

using Services_Abstractions.Contracts.CacheSerices;

namespace Services.ServicesImplementations.CacheSericesImplementations
{
    public class CacheSerices(ICacheRepository _cacheRepository) : ICacheSerices
    {
        public async Task<string?> GetCachedValueAsync(string key)
        => await _cacheRepository.GetAsync(key);

        public Task SetCacheValueAsync(string key, object value, TimeSpan duration)
        => _cacheRepository.SetAsync(key, value, duration);
    }
}
