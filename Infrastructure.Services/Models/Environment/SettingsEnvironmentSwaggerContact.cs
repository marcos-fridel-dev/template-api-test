using Infrastructure.Services.Interfaces.Environment;

namespace Infrastructure.Services.Models.Environment
{
    public class SettingsEnvironmentSwaggerContact : ISettingsEnvironmentSwaggerContact
    {
        public string Name { get; init; }
        public string Email { get; init; }
        public string Url { get; init; }
        public Uri UrlUri => new Uri(Url);
    }
}
