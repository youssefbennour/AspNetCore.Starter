
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Microsoft.Extensions.DependencyInjection.HealthChecks;

internal static class HealthCheckModule
{
    internal static IServiceCollection AddHealthCheckModule(this IServiceCollection services)
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

    internal static IEndpointConventionBuilder UseHealthCheckModule(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        return endpointRouteBuilder.MapHealthChecks("/health")
            .CacheOutput("HealthChecks")
            .WithRequestTimeout("HealthChecks");
    }
}