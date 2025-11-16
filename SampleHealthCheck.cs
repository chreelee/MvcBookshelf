using Microsoft.Extensions.Diagnostics.HealthChecks;

// redundant - custom health check not needed using AddDbContext for health check
namespace MvcBookshelf
{
    public class SampleHealthCheck : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            // suggested code to check health with boolean
            var isHealthy = true;

            if (isHealthy)
            {
                return Task.FromResult(HealthCheckResult.Healthy("A healthy result.")); // is healthy
            }
            return Task.FromResult(HealthCheckResult.Unhealthy("An unhealthy result.")); // otherwise isn't
        }
    }

}
