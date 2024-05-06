using HealthChecks.UI.Client;
using Infrastructure.Services.Models.Environment;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Services.Configuration.HealthCheck
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddHealthCheck(this IServiceCollection services, SettingsEnvironment env)
        {
            services
                .AddHealthChecks()
                //.AddRabbitMQ(env.RabbitMQ.Host)
                .AddSqlServer(env.Database.ConnectionString ?? "", name: "Sql Server", tags: new[] { "database" })
                .AddRedis(env.Redis.ConnectionString ?? "", name: "Redis", tags: new[] { "cache" });
            //ENABLED RABBITMQ
            //.AddRabbitMQ(name: "RabbitMQ", tags: new[] { "queue" });

            services
                .AddHealthChecksUI(opt =>
                {
                    opt.AddHealthCheckEndpoint("UrlPublicHttp", $"{env.Urls.Http}{env.HealthChecksUI.Uri}");
                    opt.SetHeaderText(env.HealthChecksUI.Title ?? "Health Checks");
                    opt.MaximumHistoryEntriesPerEndpoint(50);
                })
                .AddInMemoryStorage();

            return services;
        }

        public static IApplicationBuilder UseHealthCheck(this WebApplication app, SettingsEnvironment env)
        {
            string uri = env.HealthChecksUI.Uri;//  env.HealthChecksUI.HealthChecks.FirstOrDefault()?.Uri;

            app.MapHealthChecksUI(opt =>
            {
                opt.UIPath = $"{uri}-ui";
                opt.ApiPath = $"{uri}-ui-api";
                opt.PageTitle = env.HealthChecksUI.Title;
            });

            app.MapHealthChecks(uri, new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            return app;
        }
    }
}