using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Services.Api.Middlewares;

namespace Services.Api.Configuration.Middleware
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddMiddlewares(this IServiceCollection services)
        {
            services.AddTransient<ResponseHandlerMiddleware>();
            services.AddTransient<ExceptionHandlerMiddleware>();

            return services;
        }

        public static IApplicationBuilder UseMiddlewares(this IApplicationBuilder app)
        {
            app.UseMiddleware<ResponseHandlerMiddleware>();
            app.UseMiddleware<ExceptionHandlerMiddleware>();

            return app;
        }
    }
}