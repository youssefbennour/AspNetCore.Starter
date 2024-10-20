
using Microsoft.AspNetCore.Builder;
using Starter.Common.ApiConfiguration.OpenApiConfiguration;
using Swashbuckle.AspNetCore.Filters;

namespace Microsoft.Extensions.DependencyInjection {
    internal static class OpenApiConfiguration {
        internal static IServiceCollection AddOpenApiConfiguration<T>(this IServiceCollection services) where T: class{
            services.AddSwaggerGen(c => {
                c.OperationFilter<AcceptLanguageHeaderFilter>();
            });

            services.AddSwaggerExamplesFromAssemblyOf<T>();
            return services;
        }

        internal static IApplicationBuilder UseSwagger(this IApplicationBuilder app) {
            SwaggerBuilderExtensions.UseSwagger(app);
            app.UseSwaggerUI();

            return app;
        }
    }
}
