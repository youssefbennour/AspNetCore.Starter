using System.Reflection;
using Starter.Common.EventualConsistency.Outbox;
using Starter.Passes.Data.Database;
using Starter.Passes.EventBus;
using Starter.Passes.EventBus.Persistent;

namespace Starter.Passes;

internal static class PassesModule
{
    internal static IServiceCollection AddPasses(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase(configuration);
        services.AddPassesEventBus();
        services.AddOutboxModule(Assembly.GetExecutingAssembly());

        return services;
    }

    internal static IApplicationBuilder UsePasses(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseDatabase();
        
        return applicationBuilder;
    }
}
