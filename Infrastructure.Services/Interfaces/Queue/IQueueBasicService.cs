namespace Infrastructure.Services.Interfaces.Queue
{
    public interface IQueueBasicService
    {
        Task<bool> SendAsync<T>(string key, T item);
        Task<List<T>> GetAsync<T>(string key);
    }
}