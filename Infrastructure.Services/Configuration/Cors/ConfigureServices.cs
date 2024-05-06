using Infrastructure.Services.Configuration.Cors;
using Infrastructure.Services.Models.Environment;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Services.Configuration.Cors
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddCors(this IServiceCollection services, SettingsEnvironment env)
        {
            string[] origins = (env.Cors.Origins ?? "*").Split(";");
            string[] methods = (env.Cors.Methods ?? "*").Split(";");
            string[] headers = (env.Cors.Headers ?? "*").Split(";");

            services.AddCors(cfg =>
                cfg.AddPolicy("CorsPolicy", b =>
                {
                    if (origins.FirstOrDefault() == "*")
                        b.AllowAnyOrigin();
                    else
                        b.WithOrigins(origins);

                    if (methods.FirstOrDefault() == "*")
                        b.AllowAnyMethod();
                    else
                        b.WithMethods(methods);

                    if (headers.FirstOrDefault() == "*")
                        b.AllowAnyHeader();
                    else
                        b.WithHeaders(headers);
                })
            );

            return services;
        }
        public static IApplicationBuilder UseCors(this WebApplication app, SettingsEnvironment env)
        {
            app.UseCors("CorsPolicy");

            return app;
        }
    }
}