using Infrastructure.Services.Models.Environment;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Services.Configuration.Subscription
{
    public static class SubscriptioExtension
    {
        public static IServiceCollection AddSubscription(this IServiceCollection services, SettingsEnvironment env)
        {
            //services.AddScoped<IQueueBasic, QueueBasic>();

            //ENABLE RABBITMQ
            //services.AddSingleton<IConnection>(sp =>
            //{
            //    ConnectionFactory factory = new ConnectionFactory
            //    {
            //        HostName = env.RabbitMQ.Host,
            //        Port = env.RabbitMQ.Port,
            //        UserName = env.RabbitMQ.User,
            //        Password = env.RabbitMQ.Password,
            //        VirtualHost = env.RabbitMQ.VirtualHost,
            //        AutomaticRecoveryEnabled = true
            //    };
            //    return factory.CreateConnection();
            //});

            return services;
        }
    }
}