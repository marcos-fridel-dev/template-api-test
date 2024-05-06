using Infrastructure.Services.Models.Environment;
using Infrastructure.Services.Services.Queue;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Services.Configuration.Queue
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddQueue(this IServiceCollection services, SettingsEnvironment env)
        {
            services.AddScoped<QueueBasicService>();

            return services;
        }
    }
}