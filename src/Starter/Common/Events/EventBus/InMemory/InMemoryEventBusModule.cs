namespace Starter.Common.Events.EventBus.InMemory;

using Starter.Common.Events.EventBus;
using System.Reflection;

internal static class InMemoryEventBusModule
{
    internal static IServiceCollection AddInMemoryEventBus(this IServiceCollection services, Assembly assembly)
    {
        services.AddScoped<IEventBus, InMemoryEventBus>();
        services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(assembly));

        return services;
    }
}
