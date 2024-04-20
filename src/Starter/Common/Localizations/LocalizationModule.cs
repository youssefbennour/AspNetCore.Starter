using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using System.Globalization;

namespace Starter.Common.Localizations
{
    internal static class LocalizationModule
    {

        internal static IServiceCollection AddRequestBasedLocalization(this IServiceCollection services)
        {
            services.AddLocalization()
                .AddRequestLocalization(opts =>
                {
                    var supportedCultures = new List<CultureInfo>
                    {
                        new CultureInfo("en"),
                        new CultureInfo("fr"),
                        new CultureInfo("ar")
                    };

                    opts.DefaultRequestCulture = new RequestCulture(supportedCultures[0]);
                    // Formatting numbers, dates, etc.
                    opts.SupportedCultures = supportedCultures;
                    // UI strings that we have localized.
                    opts.SupportedUICultures = supportedCultures;

                });

            return services;
        }

        internal static IApplicationBuilder UserRequestBasedLocalization(this WebApplication app)
        {
            RequestLocalizationOptions requestLocalizationOptions =
                app.Services.GetService<IOptions<RequestLocalizationOptions>>()?.Value
                ?? throw new ArgumentNullException(nameof(RequestLocalizationOptions));

            return app.UseRequestLocalization(requestLocalizationOptions);
        }
    }
}
