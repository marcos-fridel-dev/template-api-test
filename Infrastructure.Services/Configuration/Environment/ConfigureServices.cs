using Infrastructure.Services.Extensions.Configuration;
using Infrastructure.Services.Models.Environment;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Services.Configuration.Environment
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddSettingsEnvironment(this IServiceCollection services, IConfiguration configuration)
        {
            SettingsEnvironment settings = configuration.GetEnvironmentSettings();

            services.AddSingleton(settings);

            return services;
        }
    }
}
