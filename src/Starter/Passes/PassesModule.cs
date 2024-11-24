using Starter.Passes.Data.Database;
using Starter.Passes.EventBus;

namespace Starter.Passes;

internal static class PassesModule
{
    internal static IServiceCollection AddPasses(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase(configuration);
        services.AddPassesEventBus();

        return services;
    }

    internal static IApplicationBuilder UsePasses(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseDatabase();
        
        return applicationBuilder;
    }
}
