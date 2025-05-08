namespace EShopApi.Cache
{
    public class FakeRedisCacheService : ICacheService
    {
       
        private readonly Dictionary<string, (object value, DateTime expiry)> _store = new();

        public async Task<T?> GetAsync<T>(string key)
        {
            Console.WriteLine($"[FakeRedis] GET {key}");
            await Task.Delay(100);

            if (_store.TryGetValue(key, out var entry))
            {
                if (DateTime.UtcNow < entry.expiry)
                {
                    Console.WriteLine($"[FakeRedis] HIT {key}");
                    return (T?)entry.value;
                }

                Console.WriteLine($"[FakeRedis] EXPIRED {key}");
                _store.Remove(key); 
            }

            Console.WriteLine($"[FakeRedis] MISS {key}");
            return default;
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan duration)
        {
            Console.WriteLine($"[FakeRedis] SET {key} (expires in {duration.TotalSeconds} seconds)");
            await Task.Delay(100);

            var expiryTime = DateTime.UtcNow.Add(duration);
            _store[key] = (value!, expiryTime);
        }

        public async Task RemoveAsync(string key)
        {
            Console.WriteLine($"[FakeRedis] DELETE {key}");
            await Task.Delay(100);

            _store.Remove(key);
        }
    }
}
