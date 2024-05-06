using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Shared.Common.Extensions.Core;
using Shared.Localization.Attributes;
using Shared.Localization.Enums;
using System;
using System.Linq;
using LanguageResource = Shared.Localization.Resources.LanguageResource;

namespace Shared.Localization.Configuration
{
    public static class ConfigureServices
    {
        //private const string ResourcesPath = "Resources";
        //private const string DefaultCulture = "en-US";

        public static IServiceCollection AddLocalizer(this IServiceCollection services)
        {
            //services
            //.AddLocalization(options =>
            //    options.ResourcesPath = ResourcesPath);

            services.AddLocalization();

            services.AddTransient<IStringLocalizer, StringLocalizer<LanguageResource>>();

            return services;
        }

        public static IApplicationBuilder UseLocalizer(this IApplicationBuilder app)
        {
            var supportedCultures =
                Enum.GetValues(typeof(Cultures))
                    .Cast<Cultures>()
                    .Select(v => v.GetAttribute<CultureAttribute>() ?? "")
                    .ToArray();

            var localizationOptions = new RequestLocalizationOptions()
                .SetDefaultCulture(supportedCultures[0])
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);

            //localizationOptions.ApplyCurrentCultureToResponseHeaders = true;

            app.UseRequestLocalization(localizationOptions);

            return app;
        }
    }
}