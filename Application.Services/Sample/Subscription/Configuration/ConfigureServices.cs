using Microsoft.Extensions.DependencyInjection;

namespace Application.Services.Sample.Subscription.Configuration
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddSubscriptionServices(this IServiceCollection services)
        {
            //services.AddScoped<QueueBasicSubscriptionService>();

            return services;
        }
    }
}