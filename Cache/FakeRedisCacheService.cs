namespace EShopApi.Cache 
{
    public class FakeRedisCacheService : ICacheService
    {
        private readonly Dictionary<string, object> _store = new();

        public async Task<T?> GetAsync<T>(string key)
        {
            Console.WriteLine($"[FakeRedis] GET {key}");
            await Task.Delay(100);
            return _store.TryGetValue(key, out var value) ? (T?)value : default;
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan duration)
        {
            Console.WriteLine($"[FakeRedis] SET {key}");
            await Task.Delay(100);
            _store[key] = value!;
        }
    }
}
