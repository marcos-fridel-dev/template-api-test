namespace Infrastructure.Services.Interfaces.Environment
{
    public interface ISettingsEnvironmentHealthChecks
    {
        public string Name { get; init; }
        public string Uri { get; init; }
    }
}
