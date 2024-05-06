using Infrastructure.Services.Models.Environment;

namespace Infrastructure.Services.Interfaces.Environment
{
    public interface ISettingsEnvironmentHealthChecksUI
    {
        public string Title { get; init; }
        public string Uri { get; init; }
        public List<SettingsEnvironmentHealthChecks> HealthChecks { get; init; }
        public int EvaluationTimeInSeconds { get; init; }
    }
}
