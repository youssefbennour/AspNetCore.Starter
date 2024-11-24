using System.Reflection;
using Starter.Common.Events.Publisher;
using Starter.Common.EventualConsistency.Outbox;
using Starter.Contracts.Data.Database;
using Starter.Passes.Data.Database;

namespace Starter.Passes.EventBus.Persistent;

internal sealed class PassesOutboxWorker(IServiceProvider serviceProvider)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = serviceProvider.CreateScope();
            
            
            var passesPersistence = scope.ServiceProvider.GetRequiredService<PassesPersistence>();
            var eventPublisher = scope.ServiceProvider.GetRequiredService<IPublisher>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<OutboxProcessor>>();
            
            var eventProcessor = new OutboxProcessor(passesPersistence, eventPublisher, logger);
            await eventProcessor.ProcessEventsAsync(Assembly.GetExecutingAssembly(), CancellationToken.None);
            
            await Task.Delay(1000, stoppingToken);
        }
    }
}