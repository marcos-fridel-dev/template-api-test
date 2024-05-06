using Infrastructure.Services.Interfaces.Environment;

namespace Infrastructure.Services.Models.Environment
{
    public class SettingsEnvironmentHealthChecks : ISettingsEnvironmentHealthChecks
    {
        public string Name { get; init; }
        public string Uri { get; init; }
    }
}
