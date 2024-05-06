using Infrastructure.Services.Interfaces.Environment;

namespace Infrastructure.Services.Models.Environment
{
    public class SettingsEnvironmentSwaggerLicence: ISettingsEnvironmentSwaggerLicence
    {
        public string Name { get; init; }
        public string Url { get; init; }
        public Uri UrlUri => new Uri(Url);
    }
}
