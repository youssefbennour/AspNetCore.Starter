using Softylines.Contably.Common.DataAccess.Orms.EfCore.DbContexts;
using Softylines.Contably.Common.EventualConsistency.Outbox;

namespace Softylines.Contably.Common.Events.EventBus.Persistent;

public class PersistentEventBus(OutboxPersistence persistence) : IEventBus
{
    public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) 
        where TEvent : IIntegrationEvent => await PersistIntegrationEvent(@event);
    public async Task PublishManyAsync<TEvent>(List<TEvent> events, CancellationToken cancellationToken = default)
        where TEvent : IIntegrationEvent
    {
        foreach (var @event in events)
        {
            await PersistIntegrationEvent(@event);
        }

        await persistence.SaveChangesAsync(CancellationToken.None);
    }

    private async Task PersistIntegrationEvent<TEvent>(TEvent @event) where TEvent : IIntegrationEvent
    {
        var outBoxMessage = OutboxMessage.CreateFrom(@event);
        await persistence.Set<OutboxMessage>()
            .AddAsync(outBoxMessage);
    }
        
}
