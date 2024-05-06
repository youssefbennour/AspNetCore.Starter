using Microsoft.AspNetCore.HttpLogging;
using OpenTelemetry;
using OpenTelemetry.Instrumentation.AspNetCore;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Starter.Common.Telemetry.OpenTelemetry {
    internal static class OpenTelemetryModule {
        internal static IServiceCollection AddOpenTelemetryModule(this WebApplicationBuilder builder) {
            builder.Logging
                .AddOpenTelemetry(options => {
                    options.IncludeFormattedMessage = true;
                    options.IncludeScopes = true;

                    var resBuilder = ResourceBuilder.CreateDefault();
                    var serviceName = builder.Configuration["ServiceName"]!;
                    resBuilder.AddService(serviceName);
                    options.SetResourceBuilder(resBuilder);

                    options.AddOtlpExporter();
                });

            builder.Services.AddHttpLogging(o => o.LoggingFields = HttpLoggingFields.All);
            builder.Services.Configure<AspNetCoreTraceInstrumentationOptions>(options => {
                // Filter out instrumentation of the Prometheus scraping endpoint.
                options.Filter = ctx => ctx.Request.Path != "/metrics";
            });

            builder.Services.AddOpenTelemetry()
                .AddTracing(builder.Configuration)
                .AddMetrics(builder.Configuration);

            return builder.Services;
        }

        internal static IOpenTelemetryBuilder AddTracing(this IOpenTelemetryBuilder optlBuilder, IConfiguration configuration) {
            optlBuilder.WithTracing(b => b
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddEntityFrameworkCoreInstrumentation()
                .AddOtlpExporter());

            return optlBuilder;
        }

        internal static IOpenTelemetryBuilder AddMetrics(this IOpenTelemetryBuilder optlBuilder, IConfiguration configuration) {
            optlBuilder.WithMetrics(b => b
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddRuntimeInstrumentation()
                .AddProcessInstrumentation()
                .AddPrometheusExporter(o =>
                o.DisableTotalNameSuffixForCounters = true));

            return optlBuilder;
        }

        internal static IApplicationBuilder UseOpenTelemetry(this IApplicationBuilder app) {
            return app.UseOpenTelemetryPrometheusScrapingEndpoint();
        }
    }
}
