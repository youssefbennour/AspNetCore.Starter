using Starter.Contracts.Data.Database;
using Starter.Contracts.EventBus;

namespace Starter.Contracts;

internal static class ContractsModule
{
    internal static IServiceCollection AddContracts(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase(configuration);
        services.AddContractsEventBus();
        return services;
    }

    internal static IApplicationBuilder UseContracts(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseDatabase();

        return applicationBuilder;
    }
}
