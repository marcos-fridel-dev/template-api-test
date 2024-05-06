using Infrastructure.Services.Models.Environment;

namespace Infrastructure.Services.Interfaces.Environment
{
    public interface ISettingsEnvironmentMail
    {
        public SettingsEnvironmentMailSmtp Smtp { get; init;  }
    }
}
