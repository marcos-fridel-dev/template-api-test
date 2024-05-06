using Microsoft.Extensions.DependencyInjection;
using Services.Api.Configuration.Middleware;

namespace Services.Api.Configuration
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInjections(this IServiceCollection services)
        {
            services.AddMiddlewares();

            return services;
        }
    }
}