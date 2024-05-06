namespace Infrastructure.Services.Interfaces.Environment
{
    public interface ISettingsEnvironmentConnectionRabbitMq
    {
        public string Host { get; init; }
        public string Port { get; init; }
        public string User { get; init; }
        public string Password { get; init; }
        public string VirtualHost { get; init; }

        public string ConnectionString { get; }
    }
}
