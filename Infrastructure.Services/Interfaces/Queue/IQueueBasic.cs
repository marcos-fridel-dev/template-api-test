namespace Infrastructure.Services.Interfaces.Queue
{
    public interface IQueueBasic
    {
        Task<bool> Send<T>(string queueName, T item);
    }
}