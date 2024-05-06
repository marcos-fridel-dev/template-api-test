using Infrastructure.Services.Constants;
using Infrastructure.Services.Interfaces.Environment;

namespace Infrastructure.Services.Models.Environment
{
    public class SettingsEnvironmentConnectionDatabase : ISettingsEnvironmentConnectionDatabase
    {
        public string Server { get; init; }
        public string Database { get; init; }
        public string User { get; init; }
        public string Password { get; init; }

        public string ConnectionString => String.Format(
            ConnectionsStrings.SqlServer,
            this.Server,
            this.Database,
            this.User,
            this.Password
        );

    }
}
