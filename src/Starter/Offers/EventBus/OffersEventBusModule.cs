using Starter.Offers.EventBus.Persistent;

namespace Starter.Offers.EventBus;

internal static class OffersEventBusModule
{
    internal static IServiceCollection AddOffersEventBus(this IServiceCollection services)
    {
        services.AddPersistentOffersEventBus();

        return services;
    }
}