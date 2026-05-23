
namespace Domain.Contracts.GenericRepository
{
    public interface ICacheRepository
    {
        // Get ==> I Ready have data [caching]  ==> return Data From Cache
        Task<string?> GetAsync (string key);
        // Set ==> I have data [caching]  ==> Set Data In Cache
        Task SetAsync (string key , object value , TimeSpan duration);

    }
}
