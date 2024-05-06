namespace Infrastructure.Services.Interfaces.Environment
{
    public interface ISettingsEnvironmentSwaggerLicence
    {
        public string Name { get; init; }
        public string Url { get; init; }
        public Uri UrlUri => new Uri(Url);
    }
}
