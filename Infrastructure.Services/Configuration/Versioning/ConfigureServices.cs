using Asp.Versioning;
using Infrastructure.Services.Models.Environment;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Services.Configuration.Versioning
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddVersioning(this IServiceCollection services, SettingsEnvironment env)
        {
            services.AddApiVersioning(o =>
            {
                o.DefaultApiVersion = new ApiVersion(1, 0);
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.ReportApiVersions = true;
                o.ApiVersionReader = new HeaderApiVersionReader("x-version");
                o.ApiVersionReader = new QueryStringApiVersionReader("version");
                o.ApiVersionReader = new UrlSegmentApiVersionReader();

            }).AddApiExplorer(options =>
            {
                //semantic versioning
                //first character is the principal or greater version
                //second character is the minor version
                //third character is the patch
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            return services;
        }
    }
}