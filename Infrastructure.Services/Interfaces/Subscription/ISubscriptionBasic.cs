namespace Infrastructure.Services.Interfaces.Subscription
{
    public interface ISubscriptionBasic
    {
        public void Publish<TRequest>(string nameQueue, TRequest @event, bool? durable = false, bool? exclusive = false, bool? autoDelete = false);
    }
}