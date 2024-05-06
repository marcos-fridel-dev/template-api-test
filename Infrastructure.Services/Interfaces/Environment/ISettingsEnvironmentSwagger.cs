using Infrastructure.Services.Models.Environment;

namespace Infrastructure.Services.Interfaces.Environment
{
    public interface ISettingsEnvironmentSwagger
    {
        public string Title { get; init; }
        public string Description { get; init; }
        public string UrlTermsOfService { get; init; }
        public Uri UrlTermsOfServiceUri { get; }
        public SettingsEnvironmentSwaggerContact Contact { get; init; }
        public SettingsEnvironmentSwaggerLicence License { get; init; }
    }
}
