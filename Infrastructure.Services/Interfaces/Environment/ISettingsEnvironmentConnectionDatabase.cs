namespace Infrastructure.Services.Interfaces.Environment
{
    public interface ISettingsEnvironmentConnectionDatabase
    {
        public string Server { get; init; }
        public string Database { get; init; }
        public string User { get; init; }
        public string Password { get; init; }
        public string ConnectionString { get; }
    }
}
