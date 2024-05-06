using Infrastructure.Services.Models.Environment;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Services.Configuration.Cache
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddCache(this IServiceCollection services, SettingsEnvironment settingsEnvironment)
        {
            services.AddStackExchangeRedisCache(opt =>
            {
                opt.Configuration = settingsEnvironment.Redis.ConnectionString;
            });

            return services;
        }
    }
}