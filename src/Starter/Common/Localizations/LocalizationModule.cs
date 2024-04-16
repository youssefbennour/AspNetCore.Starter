using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using System.Globalization;

namespace Starter.Common.Localizations {
    internal static class LocalizationModule {

        internal static IServiceCollection AddRequestBasedLocalization(this IServiceCollection services) {
            services.AddLocalization();
            services.Configure<RequestLocalizationOptions>(
                opts => {
                    var supportedCultures = new List<CultureInfo>
                    {
                        new CultureInfo("en"),
                        new CultureInfo("fr"),
                        new CultureInfo("ar")
                    };

                    opts.DefaultRequestCulture = new RequestCulture("en", "en");
                    // Formatting numbers, dates, etc.
                    opts.SupportedCultures = supportedCultures;
                    // UI strings that we have localized.
                    opts.SupportedUICultures = supportedCultures;
                });

            return services;
        }

        internal static IApplicationBuilder UseRequestBasedLocalization(this IApplicationBuilder applicationBuilder) {
            IOptions<RequestLocalizationOptions>? options =
                applicationBuilder.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();

            Guard.Against.Null(options);
            applicationBuilder.UseRequestLocalization(options.Value);

            return applicationBuilder;
        }
    }
}
