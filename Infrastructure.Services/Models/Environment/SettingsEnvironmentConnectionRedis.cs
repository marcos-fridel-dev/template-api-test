using Infrastructure.Services.Constants;
using Infrastructure.Services.Interfaces.Environment;

namespace Infrastructure.Services.Models.Environment
{
    public class SettingsEnvironmentConnectionRedis : ISettingsEnvironmentConnectionRedis
    {
        public string Server { get; init; }
        public string Password { get; init; }

        public string ConnectionString => String.Format(
            ConnectionsStrings.Redis,
            this.Server,
            this.Password
        );
    }
}
