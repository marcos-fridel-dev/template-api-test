using Infrastructure.Services.Interfaces.Environment;

namespace Infrastructure.Services.Models.Environment
{
    public class SettingsEnvironmentCors : ISettingsEnvironmentCors
    {
        public string Origins { get; init; } //=> $"{EnvironmentVariables.CORS_ORIGINS_HTTP.GetEnvironment()};{EnvironmentVariables.CORS_ORIGINS_HTTPS.GetEnvironment()}";
        public string Methods { get; init; }
        public string Headers { get; init; }
    }
}
