using Infrastructure.Services.Interfaces.Environment;

namespace Infrastructure.Services.Models.Environment
{
    public class SettingsEnvironmentRateLimit : ISettingsEnvironmentRateLimit
    {
        public int PermitLimit { get; init; }
        public int Window { get; init; }
        public int QueueLimit { get; init; }
    }
}
