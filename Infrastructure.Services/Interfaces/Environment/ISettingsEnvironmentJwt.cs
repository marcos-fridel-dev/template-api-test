namespace Infrastructure.Services.Interfaces.Environment
{
    public interface ISettingsEnvironmentJwt
    {
        public string SigningKey { get; init; } //=> EnvironmentVariables.JWT_SIGNING_KEY.GetEnvironment();
        public string Issuer { get; init; } //=> EnvironmentVariables.JWT_ISSUER.GetEnvironment();
        public string Audience { get; init; } //=> EnvironmentVariables.JWT_AUDIENCE.GetEnvironment();
        public int ExpiresMinutes { get; init; }
        public string Subject { get; init; } //=> EnvironmentVariables.JWT_SUBJECT.GetEnvironment();
        public bool ValidateIssuer { get; init; }
        public bool ValidateAudience { get; init; }
        public bool ValidateLifetime { get; init; }
        public bool ValidateIssuerSigningKey { get; init; }
        public bool RequireHttpsMetadata { get; init; }
        public bool SaveToken { get; init; }
    }
}
