namespace Softylines.Contably.Common.Events.Publisher;

public interface IPublisher
{
    Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
        where TEvent : IIntegrationEvent;
}