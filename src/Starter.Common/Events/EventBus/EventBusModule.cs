namespace Microsoft.Extensions.DependencyInjection;

using System.Reflection;

internal static class EventBusModule
{
    internal static IServiceCollection AddEventBus(this IServiceCollection services) =>
        services.AddInMemoryEventBus(Assembly.GetExecutingAssembly());
}
