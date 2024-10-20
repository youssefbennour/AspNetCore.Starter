using MediatR;

namespace Starter.Common.Events.EventBus.InMemory;

using Starter.Common.Events;
using Starter.Common.Events.EventBus;

internal sealed class InMemoryEventBus(IMediator mediator) : IEventBus
{
    public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : IIntegrationEvent =>
        await mediator.Publish(@event, cancellationToken);
}
