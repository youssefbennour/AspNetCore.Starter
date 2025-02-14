using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Starter.Common.DataAccess.Orms.EfCore.DbContexts;
using Starter.Common.Events;
using Starter.Common.Events.Publisher;

namespace Starter.Common.EventualConsistency.Outbox;

/// <summary>
/// 
/// </summary>
/// <param name="persistence"></param>
/// <param name="eventPublisher"></param>
/// <param name="logger"></param>
/// <remarks>You're responsible for implementing the retry mechanism that better suits your business for handling failed messages.</remarks>
/// <remarks>You're also responsible for implementing a mechanism for deleting processed messaged.</remarks>
public sealed class OutboxProcessor(
    OutboxPersistence persistence, 
    IPublisher eventPublisher,
    ILogger<OutboxProcessor> logger)
{
    public async Task ProcessEventsAsync(Assembly assembly, CancellationToken cancellationToken = default)
    {
        var outboxMessages = await persistence.Set<OutboxMessage>()
            .Where(x => x.ExecutedOn == null)
            .OrderBy(x => x.SavedOn)
            .Take(10)
            .ToListAsync(cancellationToken);

        foreach (var outboxMessage in outboxMessages)
        {
            await ProcessEventAsync(outboxMessage, 
                assembly,
                cancellationToken); 
        } 
    }
    
    private async Task ProcessEventAsync(
        OutboxMessage outBoxMessage, 
        Assembly assembly,
        CancellationToken stoppingToken)
    {
        try
        {
            var eventType = Assembly.Load(assembly.GetName()).GetType(outBoxMessage.Type);
            if (eventType is null)
            {
                logger.LogError("[Outbox] Event type : null");
                return;
            }

            var @event = JsonConvert.DeserializeObject(outBoxMessage.Message, eventType) as IIntegrationEvent;
            if (@event is null)
            {
                logger.LogError("[Outbox] Event cannot be deserialized: {Event}", @event);
                return;
            }

            await eventPublisher.PublishAsync(@event, stoppingToken);

            outBoxMessage.MarkAsExecuted(DateTimeOffset.UtcNow);
            await persistence.SaveChangesAsync(CancellationToken.None);

            logger.LogInformation("[Outbox] Message handled: {MessageId}", outBoxMessage.Id);

        }
        catch (Exception ex)
        {
            outBoxMessage.MarkAsFailed(ex.Message);
            await persistence.SaveChangesAsync(CancellationToken.None);
            logger.LogError(ex, "[Outbox] Exception when handling message. Message body: {Message}", outBoxMessage.Message);
        }
    }
}