
using Asp.Versioning;
using Microsoft.Extensions.DependencyInjection;

namespace Softylines.Contably.Common.ApiConfiguration
{
    public static class ApiVersioningConfiguration
    {
        public static IServiceCollection AddApiVersioningConfiguration(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1);
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new UrlSegmentApiVersionReader(),
                    new HeaderApiVersionReader("X-Api-Version"));
            });

            return services;
        }
    }
}
