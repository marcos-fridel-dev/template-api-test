using Application.Services.Sample.Http.Pokemon.Configuration;
using Infrastructure.Services.Extensions.Configuration;
using Infrastructure.Services.Models.Environment;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Services.Configuration
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddHttpServices(this IServiceCollection services, IConfiguration configuration)
        {
            SettingsEnvironment env = configuration.GetEnvironmentSettings();

            services.AddHttpServices(env);
            //services.AddQueueServices();
            //services.AddSubscriptionServices();

            return services;
        }

        public static IServiceCollection AddHttpServices(this IServiceCollection services, SettingsEnvironment env)
        {
            services.AddPokemonHttpSampleService(env);

            services.AddHttpClient();

            return services;
        }

    }
}