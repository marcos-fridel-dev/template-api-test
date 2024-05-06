using Infrastructure.Services.Interfaces.Environment;

namespace Infrastructure.Services.Models.Environment
{
    public class SettingsEnvironmentSwagger : ISettingsEnvironmentSwagger
    {
        public string Title { get; init; }
        public string Description { get; init; }
        public string UrlTermsOfService { get; init; }
        public Uri UrlTermsOfServiceUri => new Uri(UrlTermsOfService);
        public SettingsEnvironmentSwaggerContact Contact { get; init; }
        public SettingsEnvironmentSwaggerLicence License { get; init; }
    }
}
