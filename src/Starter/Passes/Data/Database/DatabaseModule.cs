namespace Starter.Passes.Data.Database;

internal static class DatabaseModule
{
    private const string ConnectionStringName = "Passes";

    internal static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(ConnectionStringName);
        Console.WriteLine($"ConnectionString: {connectionString}");
        services.AddDbContext<PassesPersistence>(options => options.UseNpgsql(connectionString));

        return services;
    }

    internal static IApplicationBuilder UseDatabase(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseAutomaticMigrations();

        return applicationBuilder;
    }
}
