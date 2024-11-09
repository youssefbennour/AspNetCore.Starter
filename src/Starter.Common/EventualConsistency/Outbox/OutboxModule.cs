using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Starter.Common.DataAccess.Orms.EfCore.DbContexts;

namespace Starter.Common.EventualConsistency.Outbox;

public static class OutboxModule
{
    public static IServiceCollection AddOutboxModule(this IServiceCollection services, Assembly assembly)
    {
        services.AddHostedService<OutboxWorker>(serviceProvider => new OutboxWorker(serviceProvider, assembly));
        services.AddScoped<OutboxProcessor>();
        
        return services;
    }
}