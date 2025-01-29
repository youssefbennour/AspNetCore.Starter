using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Starter.Common.ApiConfiguration;

public static class OpenApiConfiguration {
    public static IServiceCollection AddOpenApiConfiguration<T>(this IServiceCollection services)
    {
        services.AddSwaggerDocument();
        services.AddSwaggerGen(options =>
        {
            options.CustomSchemaIds(type => type.ToString());
            
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer"
            });
            
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new List<string>()
                }
            });
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