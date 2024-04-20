using NSwag;
using NSwag.Generation.Processors;
using NSwag.Generation.Processors.Contexts;

namespace Starter.Common.ApiConfiguration
{
    internal static class OpenApiConfiguration
    {
        internal static IServiceCollection AddOpenApiConfiguration(this IServiceCollection services)
        {
            services.AddOpenApiDocument(opts =>
            {
                opts.Title = "API name";
                opts.OperationProcessors.Add(new AcceptLanguageHeaderParameter());
            });

            return services;
        }
    }

    internal class AcceptLanguageHeaderParameter : IOperationProcessor
    {
        public bool Process(OperationProcessorContext context)
        {
            var parameter = new OpenApiParameter
            {
                Name = "Accept-Language",
                Kind = OpenApiParameterKind.Header,
                Description = "Language preference for the response.",
                IsRequired = true,
                IsNullableRaw = true,
                Default = "en-US",
                Schema = new NJsonSchema.JsonSchema()
                {
                    Type = NJsonSchema.JsonObjectType.String,
                    Item = new NJsonSchema.JsonSchema() { Type = NJsonSchema.JsonObjectType.String },
                },
            };
            parameter.Schema.Enumeration.Add("en-US");
            parameter.Schema.Enumeration.Add("fr-FR");
            parameter.Schema.Enumeration.Add("ar-TN)");
            context.OperationDescription.Operation.Parameters.Add(parameter);
            return true;
        }
    }
}
