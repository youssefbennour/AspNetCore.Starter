using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Softylines.Contably.Common.Events.Publisher.InMemory;

namespace Softylines.Contably.Common.Events.Publisher;

public static class PublisherModule 
{
    public static IServiceCollection AddPublisher(this IServiceCollection services, Assembly assembly) =>
        services.AddInMemoryPublisher(assembly);
}