using Application.Services.Sample.Queue.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Services.Sample.Queue.Configuration
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddSubscriptionServices(this IServiceCollection services)
        {
            services.AddScoped<QueueBasicService>();

            return services;
        }
    }
}