using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Softylines.Contably.Common.DataAccess.Orms.EfCore.DbContexts;

namespace Softylines.Contably.Common.EventualConsistency.Outbox;

public static class OutboxModule
{
    public static IServiceCollection AddOutboxModule(this IServiceCollection services, IConfiguration configuration, Assembly assembly)
    {
        services.AddHostedService<OutboxWorker>(serviceProvider => new OutboxWorker(serviceProvider, assembly));
        services.AddScoped<OutboxProcessor>();
        
        return services;
    }
    
    public static IApplicationBuilder UseOutbox(this IApplicationBuilder applicationBuilder)
    {
        using var scope = applicationBuilder.ApplicationServices.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<OutboxPersistence>();
        context.Database.Migrate();
        return applicationBuilder;
    }
}