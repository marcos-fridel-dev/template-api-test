using Infrastructure.Services.Interfaces.Environment;

namespace Infrastructure.Services.Models.Environment
{
    public class SettingsEnvironmentMailSmtp : ISettingsEnvironmentMailSmtp
    {
        public string Host { get; init; }
        public int Port { get; init; }
        public string UserName { get; init; }
        public string Password { get; init; }
        public bool EnabledSsl { get; init; }
        public bool UseDomainUserNameToFrom { get; init; }
    }
}
