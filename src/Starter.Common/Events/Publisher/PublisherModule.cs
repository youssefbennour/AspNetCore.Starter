using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Starter.Common.Events.Publisher.InMemory;

namespace Starter.Common.Events.Publisher;

public static class PublisherModule 
{
    public static IServiceCollection AddPublisher(this IServiceCollection services, Assembly assembly) =>
        services.AddInMemoryPublisher(assembly);
}