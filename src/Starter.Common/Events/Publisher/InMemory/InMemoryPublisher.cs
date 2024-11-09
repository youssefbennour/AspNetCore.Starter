using MediatR;

namespace Starter.Common.Events.Publisher.InMemory;

internal sealed class InMemoryPublisher(IMediator mediator) : IPublisher
{
    public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : IIntegrationEvent =>
        await mediator.Publish(@event, cancellationToken);
}
