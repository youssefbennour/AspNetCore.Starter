
using Starter.Common.Telemetry.OpenTelemetry;

namespace Microsoft.Extensions.DependencyInjection {
    internal static class TelemetryModule {
        internal static IServiceCollection AddTelemetry(this WebApplicationBuilder builder) {
            return builder.AddOpenTelemetryModule();
        }

        internal static IApplicationBuilder UseTelemetry(this IApplicationBuilder app) {
            return app.UseOpenTelemetry();
        }
    }
}
