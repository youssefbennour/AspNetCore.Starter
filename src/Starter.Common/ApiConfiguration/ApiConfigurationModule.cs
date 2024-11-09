using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Starter.Common.ApiConfiguration.Cors;
using Starter.Common.Validation.Requests;

namespace Starter.Common.ApiConfiguration;

public static class ApiConfigurationModule
{
    public static IServiceCollection AddApiConfiguration<T>(this IServiceCollection services) where T : class
    {
        services.AddControllers()
            .AddNewtonsoftJson()
            .AddJsonOptions(o =>
            {
                o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

        services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
            options.SerializerOptions.Converters.Add(new JsonStringEnumConverter()));
        services.AddRequestsValidations<T>();
        services.AddEndpointsApiExplorer();
        services.AddOpenApiConfiguration<T>();
        services.AddApiVersioningConfiguration();
        services.AddCorsPolicies();
        return services;
    }

    public static IApplicationBuilder UseApiConfiguration(this IApplicationBuilder app)
    {
        app.UseCorsPolicies();
        app.UseOpenApiConfiguration();
        return app;
    }
}