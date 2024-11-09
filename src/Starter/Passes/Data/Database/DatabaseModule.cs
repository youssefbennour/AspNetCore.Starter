using Starter.Common.DataAccess.Orms.EfCore.DbContexts;

namespace Starter.Passes.Data.Database;

internal static class DatabaseModule
{
    private const string ConnectionStringName = "Passes";

    internal static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(ConnectionStringName);
        Console.WriteLine($"ConnectionString: {connectionString}");
        services.AddDbContext<PassesPersistence>(options => options.UseNpgsql(connectionString));
        services.AddScoped<OutboxPersistence>(provider =>
            provider.GetRequiredService<PassesPersistence>());

        return services;
    }

    internal static IApplicationBuilder UseDatabase(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseAutomaticMigrations();

        return applicationBuilder;
    }
}
