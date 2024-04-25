using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Starter.Common.ApiConfiguration {
    internal static class OpenApiConfiguration {
        internal static IServiceCollection AddOpenApiConfiguration(this IServiceCollection services) {
            services.AddSwaggerGen(c => {
                c.OperationFilter<AcceptLanguageHeaderParameter>();
            });

            return services;
        }
    }

    internal class AcceptLanguageHeaderParameter : IOperationFilter {
        public void Apply(OpenApiOperation operation, OperationFilterContext context) {
            operation.Parameters.Add(new OpenApiParameter() {
                Name = "Accept-Language",
                In = ParameterLocation.Header,
                Required = false,
                Description = "Language preference for the response.",
            });

        }
    }
}
