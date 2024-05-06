namespace Infrastructure.Services.Interfaces.Environment
{
    public interface ISettingsEnvironmentFrontEnd
    {
        public string Http { get; init; }
        public string Https { get; init; }
    }
}
