using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace Softylines.Contably.Common.Localizations
{
    public static class LocalizationModule {

        public static IServiceCollection AddRequestBasedLocalization(this IServiceCollection services)
        {
            services.AddLocalization()
                .AddRequestLocalization(opts =>
                {
                    var supportedCultures = new List<CultureInfo>
                    {
                        new CultureInfo("en"),
                        new CultureInfo("fr"),
                        new CultureInfo("ar"),
                        new CultureInfo("es")
                    };

                    opts.DefaultRequestCulture = new RequestCulture(supportedCultures[0]);
                    // Formatting numbers, dates, etc.
                    opts.SupportedCultures = supportedCultures;
                    // UI strings that we have localized.
                    opts.SupportedUICultures = supportedCultures;

                });

            return services;
        }

        public static IApplicationBuilder UseRequestBasedLocalization(this WebApplication applicationBuilder)
        {
            using var scope = applicationBuilder.Services.CreateScope();
            IOptions<RequestLocalizationOptions> options =
                scope.ServiceProvider.GetService<IOptions<RequestLocalizationOptions>>()
                ?? throw new InternalServerException();
            applicationBuilder.UseRequestLocalization(options.Value);
            
            return applicationBuilder;
        }

        /// <summary>
        /// Maps an endpoint with 'GET' method, for localization testing purposes.
        /// </summary>
        /// <param name="app">injected instance of <see cref="IEndpointRouteBuilder"/></param>
        public static IEndpointConventionBuilder MapLocalizationSampleEndpoint(this IEndpointRouteBuilder app)
        {
            return app.MapGet("/localization-tests", ([FromServices]IStringLocalizer<ILocalizationSample> localizer) 
                => localizer["Hello"]);
        }
    }
}
