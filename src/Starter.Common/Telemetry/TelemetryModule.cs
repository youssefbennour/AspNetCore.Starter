using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Softylines.Contably.Common.Telemetry.HealthChecks;
using Softylines.Contably.Common.Telemetry.OpenTelemetry;

namespace Softylines.Contably.Common.Telemetry {
    public static class TelemetryModule {
        public static IServiceCollection AddTelemetry(this WebApplicationBuilder builder) {
            builder.Host.UseDefaultServiceProvider(options => 
                options.ValidateOnBuild = true);
            builder.AddOpenTelemetryModule();
            builder.Services.AddHealthChecks();

            return builder.Services;
        }

        public static IApplicationBuilder UseTelemetry(this WebApplication app) {
            app.UseOpenTelemetry();
            app.UseHealthCheckModule();
            
            return app;
        }
    }
}
