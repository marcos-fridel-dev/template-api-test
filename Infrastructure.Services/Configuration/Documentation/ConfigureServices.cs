using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Infrastructure.Services.Configuration.Documentation
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddDocumentation(this IServiceCollection services)
        {
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            services.AddSwaggerGen(opt =>
            {
                opt.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });

                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
               {
                    new OpenApiSecurityScheme
                    {
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                        Reference = new OpenApiReference
                        {
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme
                        }
                    },
                    new List<string>()
               }
                });
            });

            return services;
        }

        public static IApplicationBuilder UseDocumentation(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                var providerApiVersion = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

                app.UseSwagger(opt =>
                {
                    opt.RouteTemplate = "docs/{documentName}/swagger.json";
                });

                app.UseSwaggerUI(opt =>
                {
                    foreach (var apiVersion in providerApiVersion.ApiVersionDescriptions)
                    {
                        opt.SwaggerEndpoint($"/docs/{apiVersion.GroupName}/swagger.json", apiVersion.GroupName.ToUpperInvariant());
                    }
                    opt.RoutePrefix = $"docs";
                });
            }

            return app;
        }
    }
}