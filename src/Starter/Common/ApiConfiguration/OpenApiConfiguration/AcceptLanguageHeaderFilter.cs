using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Starter.Common.ApiConfiguration.OpenApiConfiguration {
    internal class AcceptLanguageHeaderFilter : IOperationFilter {
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
