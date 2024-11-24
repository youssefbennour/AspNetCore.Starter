using System.Reflection;
using Starter.Common.Events.Publisher;
using Starter.Common.EventualConsistency.Outbox;
using Starter.Contracts.Data.Database;
using Starter.Offers.Data.Database;

namespace Starter.Offers.EventBus.Persistent;

internal sealed class OffersOutboxWorker(IServiceProvider serviceProvider)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = serviceProvider.CreateScope();
            
            var offersPersistence = scope.ServiceProvider.GetRequiredService<OffersPersistence>();
            var eventPublisher = scope.ServiceProvider.GetRequiredService<IPublisher>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<OutboxProcessor>>();
            
            var eventProcessor = new OutboxProcessor(offersPersistence, eventPublisher, logger);
            await eventProcessor.ProcessEventsAsync(Assembly.GetExecutingAssembly(), CancellationToken.None);
            
            await Task.Delay(1000, stoppingToken);
        }
    }

   
}