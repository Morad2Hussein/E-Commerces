

namespace Services_Abstractions.Contracts.CacheSerices
{
    public interface ICacheSerices
    {
        // Get ==> I Ready have data [caching]  ==> return Data From Cache
        Task<string?> GetCachedValueAsync(string key);
        Task SetCacheValueAsync(string key, object value, TimeSpan duration);
    }
}
