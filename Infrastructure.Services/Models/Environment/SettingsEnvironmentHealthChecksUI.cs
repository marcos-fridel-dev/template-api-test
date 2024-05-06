using Infrastructure.Services.Interfaces.Environment;

namespace Infrastructure.Services.Models.Environment
{
    public class SettingsEnvironmentHealthChecksUI : ISettingsEnvironmentHealthChecksUI
    {
        public string Title { get; init; }
        public string Uri { get; init; }
        public List<SettingsEnvironmentHealthChecks> HealthChecks { get; init; }
        public int EvaluationTimeInSeconds { get; init; }
    }
}
