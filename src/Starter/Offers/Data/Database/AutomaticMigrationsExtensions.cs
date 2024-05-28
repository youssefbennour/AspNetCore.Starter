using Microsoft.EntityFrameworkCore;

namespace Starter.Offers.Data.Database;

internal static class AutomaticMigrationsExtensions
{
    internal static IApplicationBuilder UseAutomaticMigrations(this IApplicationBuilder applicationBuilder)
    {
        using var scope = applicationBuilder.ApplicationServices.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<OffersPersistence>();
        context.Database.Migrate();

        return applicationBuilder;
    }
}