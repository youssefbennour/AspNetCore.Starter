using OpenTelemetry;
using OpenTelemetry.Exporter;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System.Runtime.CompilerServices;

namespace Microsoft.Extensions.DependencyInjection
{
    internal static class OpenTelemetryModule
    {
        internal static IServiceCollection AddOpenTelemetryModule(this IServiceCollection services)
        {
            services.AddOpenTelemetry()
                .AddTracing()
                .AddMetrics();
            return services;
        }

        internal static IOpenTelemetryBuilder AddTracing(this IOpenTelemetryBuilder optlBuilder)
        {
            optlBuilder.WithTracing(
                (builder) => builder.SetResourceBuilder(ResourceBuilder.CreateDefault()
                    .AddService("Starter")
                    .AddAttributes(new Dictionary<string, object>
                    {
                        ["deployment.environment"] = "development"
                    }))
                    .AddOtlpExporter()
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation());

            return optlBuilder;
        }

        internal static IOpenTelemetryBuilder AddMetrics(this IOpenTelemetryBuilder optlBuilder)
        {
            optlBuilder.WithMetrics(
                (builder) => builder.SetResourceBuilder(ResourceBuilder.CreateDefault()
                    .AddService("Starter")
                    .AddAttributes(new Dictionary<string, object>
                    {
                        ["deployment.environment"] = "development"
                    }))
                    .AddOtlpExporter()
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation());

            return optlBuilder;
        }
    }
}
