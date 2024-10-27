using Softylines.Contably.Common.Events.Publisher;

namespace Softylines.Contably.Common.Events.EventBus.InMemory;

public class InMemoryEventBus(IPublisher eventPublisher) : IEventBus
{
    
    public async Task PublishManyAsync<TEvent>(List<TEvent> events, CancellationToken cancellationToken = default)
        where TEvent : IIntegrationEvent
    {
        foreach (var @event in events)
        {
            await PublishAsync(@event, cancellationToken);
        }
    }
        
    public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) 
        where TEvent : IIntegrationEvent => await eventPublisher.PublishAsync(@event, cancellationToken);
}