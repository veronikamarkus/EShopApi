using Microsoft.Extensions.Caching.Memory;

namespace EShopApi.Cache 
{
    public static class CacheFactory 
    {
        public static ICacheService CreateCache(string type, IServiceProvider services)
        {
            return type switch
            {
                "InMemory" => new InMemoryCacheService(services.GetRequiredService<IMemoryCache>()),
                "FakeRedis" => new FakeRedisCacheService(),
                _ => throw new ArgumentException("Invalid cache type")
            };
        }
    }

}
