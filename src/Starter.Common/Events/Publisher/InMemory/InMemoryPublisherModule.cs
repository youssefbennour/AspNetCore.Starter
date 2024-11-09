using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Starter.Common.Events.Publisher.InMemory;

internal static class InMemoryPublisherModule
{
    public static IServiceCollection AddInMemoryPublisher(this IServiceCollection services, Assembly assembly)
    {
        services.AddScoped<IPublisher, InMemoryPublisher>();
        services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(assembly));

        return services;
    }
}
