using Starter.Contracts.EventBus;
using Starter.Contracts.EventBus.Persistent;
using Starter.Passes.EventBus.Persistent;

namespace Starter.Passes.EventBus;

internal static class PassesEventBusModule
{
    internal static IServiceCollection AddPassesEventBus(this IServiceCollection services)
    {
        services.AddScoped<IPassesEventBus, PersistentPassesEventBus>();

        return services;
    }
}