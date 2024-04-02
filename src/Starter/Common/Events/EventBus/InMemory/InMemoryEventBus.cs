namespace Starter.Common.Events.EventBus.InMemory;

using MediatR;
using Starter.Common.Events;
using Starter.Common.Events.EventBus;

internal sealed class InMemoryEventBus(IMediator mediator) : IEventBus
{
    public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : IIntegrationEvent =>
        await mediator.Publish(@event, cancellationToken);
}
