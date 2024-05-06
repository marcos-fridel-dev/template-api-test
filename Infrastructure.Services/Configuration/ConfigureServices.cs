using Application.Services.Configuration.Queue;
using Infrastructure.Persistence.Interfaces.Context;
using Infrastructure.Services.Configuration.Authentication;
using Infrastructure.Services.Configuration.Cache;
using Infrastructure.Services.Configuration.Cors;
using Infrastructure.Services.Configuration.Documentation;
using Infrastructure.Services.Configuration.Environment;
using Infrastructure.Services.Configuration.HealthCheck;
using Infrastructure.Services.Configuration.Mail;
using Infrastructure.Services.Configuration.RateLimiting;
using Infrastructure.Services.Configuration.Subscription;
using Infrastructure.Services.Configuration.Versioning;
using Infrastructure.Services.Extensions.Configuration;
using Infrastructure.Services.Models.Environment;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Services.Configuration
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddSettingsEnvironmentServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSettingsEnvironment(configuration);

            return services;
        }

        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, SettingsEnvironment env)
        {
            services.AddAuthenticationJwt(env);
            services.AddCache(env);
            services.AddCors(env);
            services.AddMail(env);
            services.AddRateLimit(env);
            services.AddVersioning(env);
            services.AddDocumentation();
            services.AddQueue(env);
            services.AddSubscription(env);

            services.AddHealthCheck(env);

            return services;
        }

        public static IApplicationBuilder UseInfrastructureServices(this WebApplication app, SettingsEnvironment env)
        {
            app.UseCors(env);
            app.UseDocumentation();
            app.UseHealthCheck(env);
            app.UseRateLimit();

            return app;
        }
    }
}