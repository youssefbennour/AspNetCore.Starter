using Starter.Offers.Data.Database;

namespace Starter.Offers;

internal static class OffersModule
{
    internal static IServiceCollection AddOffers(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase(configuration);

        return services;
    }

    internal static IApplicationBuilder UseOffers(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseDatabase();

        return applicationBuilder;
    }
}
