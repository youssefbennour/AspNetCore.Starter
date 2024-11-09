using Starter.Common.DataAccess.Orms.EfCore.DbContexts;

namespace Starter.Offers.Data.Database;

internal static class DatabaseModule
{
    private const string ConnectionStringName = "Offers";

    internal static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(ConnectionStringName);
        services.AddDbContext<OffersPersistence>(options => options.UseNpgsql(connectionString));
        services.AddScoped<OutboxPersistence>(provider =>
            provider.GetRequiredService<OffersPersistence>());

        return services;
    }

    internal static IApplicationBuilder UseDatabase(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseAutomaticMigrations();

        return applicationBuilder;
    }
}