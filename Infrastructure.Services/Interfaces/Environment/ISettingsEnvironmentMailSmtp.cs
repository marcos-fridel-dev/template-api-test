namespace Infrastructure.Services.Interfaces.Environment
{
    public interface ISettingsEnvironmentMailSmtp
    {
        public string Host { get; init; }
        public int Port { get; init; }        
        public string UserName { get; init; }
        public string Password { get; init; }
        public bool EnabledSsl { get; init; }
    }
}
