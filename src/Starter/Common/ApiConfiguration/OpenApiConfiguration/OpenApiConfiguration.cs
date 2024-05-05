
using Starter.Common.ApiConfiguration.OpenApiConfiguration;
using Swashbuckle.AspNetCore.Filters;

namespace Microsoft.Extensions.DependencyInjection {
    internal static class OpenApiConfiguration {
        internal static IServiceCollection AddOpenApiConfiguration(this IServiceCollection services) {
            services.AddSwaggerGen(c => {
                c.OperationFilter<AcceptLanguageHeaderFilter>();
            });

            services.AddSwaggerExamplesFromAssemblyOf<Program>();
            return services;
        }

        internal static IApplicationBuilder UseSwagger(this IApplicationBuilder app) {
            SwaggerBuilderExtensions.UseSwagger(app);
            app.UseSwaggerUI();

            return app;
        }
    }
}
