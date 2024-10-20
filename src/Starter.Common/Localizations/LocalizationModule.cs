using Starter.Common.Localizations;
using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Starter.Common.ErrorHandling.Exceptions;

namespace Microsoft.Extensions.DependencyInjection
{
    internal static class LocalizationModule {

        internal static IServiceCollection AddRequestBasedLocalization(this IServiceCollection services)
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

        internal static IApplicationBuilder UseRequestBasedLocalization(this IApplicationBuilder applicationBuilder) {
            IOptions<RequestLocalizationOptions> options =
                applicationBuilder.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>()
                ?? throw new InternalServerException();

            applicationBuilder.UseRequestLocalization(options.Value);
            return applicationBuilder;
        }

        /// <summary>
        /// Maps an endpoint with 'GET' method, for localization testing purposes.
        /// </summary>
        /// <param name="app">injected instance of <see cref="IEndpointRouteBuilder"/></param>
        internal static void MapLocalizationSampleEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapGet("api/localization-tests", (IStringLocalizer<ILocalizationSample> localizer) => {
                return localizer["Hello"];
            }).RequireAuthorization(options => options.RequireAuthenticatedUser());
        }
    }
}
