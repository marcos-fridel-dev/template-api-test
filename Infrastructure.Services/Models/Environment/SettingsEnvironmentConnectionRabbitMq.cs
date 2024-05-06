using Infrastructure.Services.Constants;
using Infrastructure.Services.Interfaces.Environment;

namespace Infrastructure.Services.Models.Environment
{
    public class SettingsEnvironmentConnectionRabbitMq : ISettingsEnvironmentConnectionRabbitMq
    {
        public string Host { get; init; }
        public string Port { get; init; }
        public string User { get; init; }
        public string Password { get; init; }
        public string VirtualHost { get; init; }

        public string ConnectionString => String.Format(
            ConnectionsStrings.RabbitMq,
            this.Host,
            this.Port,
            this.User,
            this.Password,
            this.VirtualHost
        );
    }
}
