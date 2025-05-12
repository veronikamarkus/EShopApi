using Microsoft.Extensions.Caching.Memory;

namespace EShopApi.Cache 
{
    public class InMemoryCacheService : ICacheService
    {
        private readonly IMemoryCache _cache;

        public InMemoryCacheService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public Task<T?> GetAsync<T>(string key)
        {
            ValidateKey(key);
            _cache.TryGetValue(key, out T? value);
            return Task.FromResult(value);
        }

        public Task SetAsync<T>(string key, T value, TimeSpan duration)
        {
            ValidateKey(key);
            _cache.Set(key, value, duration);
            return Task.CompletedTask;
        }

        public Task RemoveAsync(string key)
        {
            ValidateKey(key);
            _cache.Remove(key);

            return Task.CompletedTask;
        }

        private void ValidateKey(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Cache key must not be null or empty.", nameof(key));
        }
    }
}