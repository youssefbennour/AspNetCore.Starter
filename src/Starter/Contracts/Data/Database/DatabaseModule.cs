using Starter.Common.DataAccess.Orms.EfCore.DbContexts;

namespace Starter.Contracts.Data.Database;

internal static class DatabaseModule
{
    private const string ConnectionStringName = "Contracts";

    internal static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(ConnectionStringName);
        services.AddDbContext<ContractsPersistence>(options => options.UseNpgsql(connectionString));
        services.AddScoped<OutboxPersistence>(provider =>
            provider.GetRequiredService<ContractsPersistence>());

        return services;
    }

    internal static IApplicationBuilder UseDatabase(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseAutomaticMigrations();

        return applicationBuilder;
    }
}
