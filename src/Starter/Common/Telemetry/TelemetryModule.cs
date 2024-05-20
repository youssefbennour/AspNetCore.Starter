
using Microsoft.Extensions.DependencyInjection.HealthChecks;
using Starter.Common.Telemetry.OpenTelemetry;

namespace Microsoft.Extensions.DependencyInjection {
    internal static class TelemetryModule {
        internal static IServiceCollection AddTelemetry(this WebApplicationBuilder builder) {
            builder.AddOpenTelemetryModule();
            builder.Services.AddHealthChecks();

            return builder.Services;
        }

        internal static IApplicationBuilder UseTelemetry(this WebApplication app) {
            app.UseOpenTelemetry();
            app.UseHealthCheckModule();
            
            return app;
        }
    }
}
