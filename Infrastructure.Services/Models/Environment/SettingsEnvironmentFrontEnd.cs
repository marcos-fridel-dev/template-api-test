using Infrastructure.Services.Interfaces.Environment;

namespace Infrastructure.Services.Models.Environment
{
    public class SettingsEnvironmentFrontEnd : ISettingsEnvironmentFrontEnd
    {
        public string Http { get; init; }
        public string Https { get; init; }
    }
}
