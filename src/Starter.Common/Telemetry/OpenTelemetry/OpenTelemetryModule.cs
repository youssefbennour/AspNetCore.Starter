using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Npgsql;
using OpenTelemetry;
using OpenTelemetry.Instrumentation.AspNetCore;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using StackExchange.Redis;
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
                options.Filter = ctx =>
                    ctx.Request.Path is not { Value: "/metrics" or "/health" };
            });


            return builder;
        }
        
        private static IOpenTelemetryBuilder AddTracing(this IOpenTelemetryBuilder optlBuilder, string serviceName) {
            optlBuilder.WithTracing(b =>
            {
                b.SetResourceBuilder(ResourceBuilder
                    .CreateDefault()
                    .AddService(serviceName))
                    .SetSampler(new AlwaysOnSampler());
                
                b.AddAspNetCoreInstrumentation()
                    .AddNpgsql()
                    .AddHttpClientInstrumentation()
                    .AddEntityFrameworkCoreInstrumentation(o 
                        => o.SetDbStatementForText = true)
                    .AddSource("Microsoft.AspNetCore.Hosting")
                    .AddSource("Microsoft.AspNetCore.Server.Kestrel")
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
                        .AddMeter("Microsoft.AspNetCore.Hosting")
                        .AddMeter("Microsoft.AspNetCore.Server.Kestrel")
                        .AddPrometheusExporter(o =>
                            o.DisableTotalNameSuffixForCounters = true);
                });

            return optlBuilder;
        }
        
        private static string GetServiceName(WebApplicationBuilder builder) => 
            builder.Configuration["ServiceName"] ?? "unknown-service";

        public static WebApplication UseOpenTelemetry(this WebApplication app)
        {
            app.Use(async (context, next) =>
            {
                var metricsFeature = context.Features.Get<IHttpMetricsTagsFeature>();
                if (metricsFeature != null &&
                    context.Request.Path is {Value: "/metrics" or "/health"})
                {
                    metricsFeature.MetricsDisabled = true;
                }

                await next(context);
            });
            app.MapPrometheusScrapingEndpoint()
                .AllowAnonymous();
            
            return app;
        }

    }
}
