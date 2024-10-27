
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Softylines.Contably.Common.ApiConfiguration.Filters;
using Swashbuckle.AspNetCore.Filters;

namespace Softylines.Contably.Common.ApiConfiguration;

public static class OpenApiConfiguration {
    public static IServiceCollection AddOpenApiConfiguration<T>(this IServiceCollection services)
    {
        services.AddSwaggerDocument();
        services.AddSwaggerGen(options =>
        {
            options.CustomSchemaIds(type => type.ToString());
            
            options.AddSecurityDefinition("Tenant", new OpenApiSecurityScheme
            {
                Name = "Tenant",
                In = ParameterLocation.Header,
                Description = "Tenant ID required for multi-tenancy",
                Type = SecuritySchemeType.ApiKey
            });
            
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer"
            });
            
            options.OperationFilter<AuthorizationHeaderParameterOperationFilter>();
        });
        
        services.AddSwaggerExamplesFromAssemblyOf<T>();
        return services;
    }

    public static IApplicationBuilder UseOpenApiConfiguration(this IApplicationBuilder app) {
        app.UseSwagger();
        app.UseSwaggerUi(s =>
        {
            s.PersistAuthorization = true;
        });

        return app;
    }
}