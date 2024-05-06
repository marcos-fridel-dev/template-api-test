using Infrastructure.Services.Extensions.Cache;
using Infrastructure.Services.Interfaces.Queue;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Text.Json;

namespace Infrastructure.Services.Services.Queue
{
    internal class QueueBasicService(IDistributedCache _cache) : IQueueBasicService
    {
        public async Task<List<T>?> GetAsync<T>(string key)
        {
            return await _cache.GetCacheToListAsync<T>(key);
        }

        public async Task<bool> SendAsync<T>(string key, T item)
        {
            List<T> list = new List<T>();

            var redis = await _cache.GetAsync(key);
            if (redis != null)
            {
                list = JsonSerializer.Deserialize<List<T>>(redis);
            }

            list.Add(item);

            var serialized = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(list));
            var options = new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(DateTime.Now.AddHours(8))
                .SetSlidingExpiration(TimeSpan.FromSeconds(60));
            _cache.Set(key, serialized, options);

            return true;
        }
    }
}