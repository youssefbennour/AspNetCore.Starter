namespace Starter.IntegrationTests.Common.Events.EventBus.InMemory;


internal sealed class TestEventConsumer : IIntegrationEventHandler<FakeEvent>
{
    public Task Handle(FakeEvent @event, CancellationToken cancellationToken)
    {
        @event.MarkAsConsumed();
        return Task.CompletedTask;
    }
}
