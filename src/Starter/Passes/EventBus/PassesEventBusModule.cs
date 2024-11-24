using Starter.Passes.EventBus.Persistent;

namespace Starter.Passes.EventBus;

internal static class PassesEventBusModule
{
    internal static IServiceCollection AddPassesEventBus(this IServiceCollection services)
    {
        services.AddPersistentPassesEventBus();

        return services;
    }
}