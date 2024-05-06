namespace Infrastructure.Services.Interfaces.Environment
{
    public interface ISettingsEnvironmentRateLimit
    {
        public int PermitLimit { get; init; }
        public int Window { get; init; }
        public int QueueLimit { get; init; }
    }
}
