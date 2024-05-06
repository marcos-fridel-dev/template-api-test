using Infrastructure.Services.Interfaces.Subscription;
using RabbitMQ.Client;

namespace Infrastructure.Services.Models.Subscription
{
    public class SubscriptionBasic(IConnection _connection) : ISubscriptionBasic
    {
        public void Publish<TRequest>(string nameQueue, TRequest message, bool? durable = false, bool? exclusive = false, bool? autoDelete = false)
        {
            //    try 
            //    {
            //        IModel channel = _connection.CreateModel();

            //        channel.QueueDeclare(
            //            nameQueue,
            //            durable: durable ?? false,
            //            exclusive: exclusive ?? false,
            //            autoDelete: autoDelete ?? false
            //        );

            //        string json = System.Text.Json.JsonSerializer.Serialize(message);
            //        byte[] body = Encoding.UTF8.GetBytes(json);

            //        channel.BasicPublish(
            //            exchange: string.Empty, 
            //            routingKey: nameQueue, 
            //            body: body
            //        );
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(ex.ToString());
            //    } 
        }
    }
}