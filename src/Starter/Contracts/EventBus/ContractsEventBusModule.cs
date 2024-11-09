using Starter.Contracts.EventBus.Persistent;

namespace Starter.Contracts.EventBus;

internal static class ContractsEventBusModule
{
    internal static IServiceCollection AddContractsEventBus(this IServiceCollection services)
    {
        services.AddScoped<IContractsEventBus, PersistentContractsEventBus>();

        return services;
    }
}