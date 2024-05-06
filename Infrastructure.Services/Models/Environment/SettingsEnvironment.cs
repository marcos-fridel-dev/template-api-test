﻿using Infrastructure.Services.Interfaces.Environment;

namespace Infrastructure.Services.Models.Environment
{
    public class SettingsEnvironment : ISettingsEnvironment
    {
        public SettingsEnvironmentConnectionDatabase Database { get; init; }
        public SettingsEnvironmentConnectionRedis Redis { get; init; }
        public SettingsEnvironmentConnectionRabbitMq RabbitMq { get; init; }
        public SettingsEnvironmentCors Cors { get; init; }
        public SettingsEnvironmentFrontEnd FrontEnd { get; init; }
        public SettingsEnvironmentHealthChecksUI HealthChecksUI { get; init; }
        public SettingsEnvironmentJwt Jwt { get; init; }
        public SettingsEnvironmentMail Mail { get; init; }
        public SettingsEnvironmentRateLimit RateLimit { get; init; }
        public SettingsEnvironmentSecurity Security { get; init; }
        public SettingsEnvironmentSwagger Swagger { get; init; }
        public SettingsEnvironmentUrls Urls { get; init; }
    }
}