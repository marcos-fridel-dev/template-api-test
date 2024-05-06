﻿using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Infrastructure.Services.Models.Swagger
{
    public class SwaggerOptionsService(IApiVersionDescriptionProvider _provider) : IConfigureOptions<SwaggerGenOptions>
    {
        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }
        }

        static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new OpenApiInfo
            {
                Version = description.ApiVersion.ToString(),
                Title = "Net Core Web Api Versioning",
                Description = "Example on how to versioning a Web Api in Net Core",
                Contact = new OpenApiContact
                {
                    Name = "Edson Martinez",
                    Email = "emz19.com@gmail.com",
                    Url = new Uri("https://www.linkedin.com/in/edsonmz/")
                },
                License = new OpenApiLicense
                {
                    Name = "Use under LICX"
                }
            };
            if (description.IsDeprecated)
            {
                info.Description += "This API version has been deprecated.";
            }
            return info;
        }
    }
}