using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Infrastructure.Services.Models.HealthCheck
{
    public class HealthCheckService
    {
        private readonly Random _random = new();

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var responseTime = _random.Next(1, 1000);
            if (responseTime < 100)
                return Task.FromResult(HealthCheckResult.Healthy("Result Healthy"));
            if (responseTime < 200)
                return Task.FromResult(HealthCheckResult.Degraded("Result Degraded"));

            return Task.FromResult(HealthCheckResult.Unhealthy("Result Unhealthy"));
        }
    }
}