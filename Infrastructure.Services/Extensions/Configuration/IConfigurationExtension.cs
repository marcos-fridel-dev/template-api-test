using Infrastructure.Services.Models.Environment;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services.Extensions.Configuration
{
    public static class IConfigurationExtension
    {
        public static SettingsEnvironment GetEnvironmentSettings(this IConfiguration configuration)
        {
            return configuration
                .Get<SettingsEnvironment>() ?? new SettingsEnvironment();
        }
    }
}