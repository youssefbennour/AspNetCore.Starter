using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Softylines.Contably.Common.DataAccess.Orms.EfCore.DbContexts;
using Softylines.Contably.Common.Events;
using Softylines.Contably.Common.Events.Publisher;

namespace Softylines.Contably.Common.EventualConsistency.Outbox;

internal sealed class OutboxWorker(IServiceProvider serviceProvider, Assembly assembly)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = serviceProvider.CreateScope();
            var eventProcessor = scope.ServiceProvider.GetRequiredService<OutboxProcessor>();
            await eventProcessor.ProcessEventsAsync(assembly, CancellationToken.None);
            
            await Task.Delay(1000, stoppingToken);
        }
    }

   
}