using Asp.Versioning.ApiExplorer;
using Infrastructure.Services.Extensions.Configuration;
using Infrastructure.Services.Models.Environment;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Infrastructure.Services.Configuration.Documentation
{
    public class ConfigureSwaggerOptions(IApiVersionDescriptionProvider _provider, IConfiguration _configuration) : IConfigureOptions<SwaggerGenOptions>
    {
        public static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description, SettingsEnvironment env)
        {
            OpenApiInfo info = new OpenApiInfo
            {
                Version = description.ApiVersion.ToString(),
                Title = env.Swagger.Title,
                Description = env.Swagger.Description,
                TermsOfService = env.Swagger.UrlTermsOfServiceUri,
                Contact = new OpenApiContact
                {
                    Name = env.Swagger.Contact.Name,
                    Email = env.Swagger.Contact.Email,
                    Url = env.Swagger.Contact.UrlUri
                },
                License = new OpenApiLicense
                {
                    Name = env.Swagger.License.Name,
                    Url = env.Swagger.License.UrlUri
                }
            };

            if (description.IsDeprecated)
            {
                info.Description = "This API has become obsolete";
            }

            return info;
        }

        public void Configure(SwaggerGenOptions options)
        {
            SettingsEnvironment env = _configuration.GetEnvironmentSettings();

            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description, env));
            }
        }
    }
}