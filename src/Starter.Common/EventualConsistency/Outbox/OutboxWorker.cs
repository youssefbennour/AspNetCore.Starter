using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Starter.Common.EventualConsistency.Outbox;

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