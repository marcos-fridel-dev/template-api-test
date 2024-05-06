using Infrastructure.Services.Interfaces.Environment;

namespace Infrastructure.Services.Models.Environment
{
    public class SettingsEnvironmentMail : ISettingsEnvironmentMail
    {
        public SettingsEnvironmentMailSmtp Smtp { get; init; }
    }
}
