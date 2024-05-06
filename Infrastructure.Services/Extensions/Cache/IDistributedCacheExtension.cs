using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Text.Json;

namespace Infrastructure.Services.Extensions.Cache
{
    public static class IDistributedCacheExtension
    {
        public async static Task GetCacheAsync(this IDistributedCache cache, string key, Func<Task<object>> func)
            => await GetCacheAsync<object>(cache, key, func);

        public async static Task<T?> GetCacheAsync<T>(this IDistributedCache cache, string key, Func<Task<T>> func)
        {
            T? result;

            var redis = await cache.GetAsync(key);
            if (redis != null)
            {
                return JsonSerializer.Deserialize<T>(redis);
            }

            result = await func();

            if (result != null)
            {
                await SetCacheAsync(cache, key, result);
            }

            return result;
        }

        public async static Task<bool> SetCacheAsync<T>(this IDistributedCache cache, string key, T data)
        {
            try
            {

                var serialized = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(data));
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddHours(8))
                    .SetSlidingExpiration(TimeSpan.FromSeconds(60));
                await cache.SetAsync(key, serialized, options);

                return true;
            }
            catch
            {
                return false;
            };
        }

        public async static Task<T?> GetCacheAsync<T>(this IDistributedCache cache, string key)
        {
            byte[]? redis = await cache.GetAsync(key);
            return redis != null ? JsonSerializer.Deserialize<T>(redis) : default;
        }

        public async static Task<List<T>?> GetCacheToListAsync<T>(this IDistributedCache cache, string key)
        {
            byte[]? redis = await cache.GetAsync(key);
            return redis != null ? JsonSerializer.Deserialize<List<T>>(redis) : new List<T>();
        }
    }
}