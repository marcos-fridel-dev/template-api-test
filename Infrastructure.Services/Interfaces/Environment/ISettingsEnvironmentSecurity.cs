namespace Infrastructure.Services.Interfaces.Environment
{
    public interface ISettingsEnvironmentSecurity
    {
        public string RoleDefault { get; init; }
        public string UserDefault { get; init; }
        public string FirstNameDefault { get; init; }
        public string LastNameDefault { get; init; }
        public string PasswordDefault { get; init; }
        public string EMailDefault { get; init; }
    }
}
