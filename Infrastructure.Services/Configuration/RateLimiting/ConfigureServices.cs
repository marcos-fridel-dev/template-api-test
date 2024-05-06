using Infrastructure.Services.Models.Environment;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Services.Configuration.RateLimiting
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddRateLimit(this IServiceCollection services, SettingsEnvironment env)
        {
            string defaultPolicyName = "default";
            services.AddRateLimiter(opt =>
            {
                opt.AddFixedWindowLimiter(
                    policyName: defaultPolicyName,
                    fixedWindow =>
                    {
                        fixedWindow.PermitLimit = env.RateLimit.PermitLimit;
                        fixedWindow.Window = TimeSpan.FromSeconds(env.RateLimit.Window);
                        fixedWindow.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
                        fixedWindow.QueueLimit = env.RateLimit.QueueLimit;
                    });
                opt.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            });

            return services;
        }

        public static IApplicationBuilder UseRateLimit(this WebApplication app)
        {
            app.UseRateLimiter();

            return app;
        }
    }
}