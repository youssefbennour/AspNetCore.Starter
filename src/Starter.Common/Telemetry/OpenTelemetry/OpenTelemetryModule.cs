using Microsoft.AspNetCore.HttpLogging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenTelemetry;
using OpenTelemetry.Instrumentation.AspNetCore;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Starter.Common.Telemetry.OpenTelemetry.Processors;

namespace Starter.Common.Telemetry.OpenTelemetry {
    public static class OpenTelemetryModule {
        public static IServiceCollection AddOpenTelemetryModule(this WebApplicationBuilder builder)
        {
            string serviceName = GetServiceName(builder);
            builder.AddOpenTelemetryLogging(serviceName);

            builder.Services.AddOpenTelemetry()
                .AddTracing(serviceName)
                .AddMetrics(serviceName);

            return builder.Services;
        }

        private static WebApplicationBuilder AddOpenTelemetryLogging(this WebApplicationBuilder builder, string serviceName)
        {
            builder.Logging.AddOpenTelemetry(options => {
                options.IncludeFormattedMessage = true;
                options.IncludeScopes = true;
                options.SetResourceBuilder(ResourceBuilder
                    .CreateDefault()
                    .AddService(serviceName))
                    .AddOtlpExporter();
            });
            
            builder.Services.AddHttpLogging(o => o.LoggingFields = HttpLoggingFields.All);
            builder.Services.Configure<AspNetCoreTraceInstrumentationOptions>(options =>
            {
                // Filter out instrumentation of the Prometheus scraping endpoint.
                options.Filter = ctx => 
                    ctx.Request.Path != "/metrics" || ctx.Request.Path != "/health";
            });


            return builder;
        }
        
        private static IOpenTelemetryBuilder AddTracing(this IOpenTelemetryBuilder optlBuilder, string serviceName) {
            optlBuilder.WithTracing(b =>
            {
                b.SetResourceBuilder(ResourceBuilder
                    .CreateDefault()
                    .AddService(serviceName));
                
                b.AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddEntityFrameworkCoreInstrumentation()
                    .AddProcessor<AutomatedEndpointsProcessor>()
                    .AddOtlpExporter();
            });

            return optlBuilder;
        }
        
        private static IOpenTelemetryBuilder AddMetrics(this IOpenTelemetryBuilder optlBuilder, string serviceName) {
            optlBuilder.WithMetrics(b =>
                {
                    b.SetResourceBuilder(ResourceBuilder
                        .CreateDefault()
                        .AddService(serviceName));
                    
                    b.AddAspNetCoreInstrumentation()
                        .AddHttpClientInstrumentation()
                        .AddRuntimeInstrumentation()
                        .AddProcessInstrumentation()
                        .AddPrometheusExporter(o =>
                            o.DisableTotalNameSuffixForCounters = true);
                }
                );

            return optlBuilder;
        }
        
        private static string GetServiceName(WebApplicationBuilder builder) => 
            builder.Configuration["ServiceName"] ?? "unknown-service";

        public static IApplicationBuilder UseOpenTelemetry(this IApplicationBuilder app) {
            return app.UseOpenTelemetryPrometheusScrapingEndpoint();
        }
    }
}
