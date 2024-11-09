using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Starter.Common.Telemetry.HealthChecks;

public static class HealthCheckModule
{
    public static IServiceCollection AddHealthCheckModule(this IServiceCollection services)
    {
        services.AddRequestTimeouts(
            configure: static timeouts =>
                timeouts.AddPolicy("HealthChecks", TimeSpan.FromSeconds(5)));
        
        services.AddOutputCache(
            configureOptions: static caching =>
                caching.AddPolicy("HealthChecks",
                    build: static policy => policy.Expire(TimeSpan.FromSeconds(10))));

        services.AddHealthChecks();
        
        return services;
    }

    public static IEndpointConventionBuilder UseHealthCheckModule(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        return endpointRouteBuilder.MapHealthChecks("/health")
            .CacheOutput("HealthChecks")
            .WithRequestTimeout("HealthChecks");
    }
}