namespace Infrastructure.Services.Interfaces.Environment
{
    public interface ISettingsEnvironmentConnectionRedis
    {
        public string Server { get; init; }
        public string Password { get; init; }

        public string ConnectionString { get; }
    }
}
