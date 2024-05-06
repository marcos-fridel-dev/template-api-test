namespace Infrastructure.Services.Models.Queue
{
    //public class QueueBasic(IDistributedCache _cache) : IQueueBasic
    //{
    //    public async Task<bool> Send<T>(T item)
    //    {
    //        List<T> list = new List<T>();

    //        var redis = await _cache.GetAsync("message");
    //        if (redis != null)
    //        {
    //            list = JsonSerializer.Deserialize<List<T>>(redis);
    //        }

    //        list.Add(item);

    //        var serialized = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(list));
    //        var options = new DistributedCacheEntryOptions()
    //            .SetAbsoluteExpiration(DateTime.Now.AddHours(8))
    //            .SetSlidingExpiration(TimeSpan.FromSeconds(60));
    //        _cache.Set("message", serialized, options);

    //        return true;
    //    }
    //}
}