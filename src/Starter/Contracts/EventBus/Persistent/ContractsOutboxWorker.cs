using System.Reflection;
using Starter.Common.Events.Publisher;
using Starter.Common.EventualConsistency.Outbox;
using Starter.Contracts.Data.Database;

namespace Starter.Contracts.EventBus.Persistent;

internal sealed class ContractsOutboxWorker(IServiceProvider serviceProvider)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = serviceProvider.CreateScope();
            
            var contractsPersistence = scope.ServiceProvider.GetRequiredService<ContractsPersistence>();
            var eventPublisher = scope.ServiceProvider.GetRequiredService<IPublisher>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<OutboxProcessor>>();
            
            var eventProcessor = new OutboxProcessor(contractsPersistence, eventPublisher, logger);
            await eventProcessor.ProcessEventsAsync(Assembly.GetExecutingAssembly(), CancellationToken.None);
            
            await Task.Delay(1000, stoppingToken);
        }
    }
}