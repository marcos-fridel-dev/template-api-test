namespace Infrastructure.Services.Interfaces.Environment
{
    public interface ISettingsEnvironmentSwaggerContact
    {
        public string Name { get; init; }
        public string Email { get; init; }
        public string Url { get; init; }
        public Uri UrlUri => new Uri(Url);
    }
}
