namespace Microsoft.Extensions.DependencyInjection;

using Starter.Common.Events.EventBus;
using Starter.Common.Events.EventBus.InMemory;
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
