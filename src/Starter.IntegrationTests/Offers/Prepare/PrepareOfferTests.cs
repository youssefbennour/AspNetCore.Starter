using Starter.Common.Events.EventBus;
using Starter.IntegrationTests.Common;
using Starter.IntegrationTests.Common.TestEngine.Configuration;
using Starter.IntegrationTests.Common.TestEngine.IntegrationEvents.Handlers;
using Starter.Offers.EventBus;
using Starter.Offers.Prepare;
using Starter.Passes.MarkPassAsExpired.Events;

namespace Starter.IntegrationTests.Offers.Prepare;

public sealed class PrepareOfferTests : IntegrationTest, IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _applicationInMemory;
    private readonly IOffersEventBus _fakeEventBus = Substitute.For<IOffersEventBus>();

    public PrepareOfferTests(WebApplicationFactory<Program> applicationInMemoryFactory,
        DatabaseContainer database) :  base(database)
    {
        _applicationInMemory = applicationInMemoryFactory
            .WithFakeOffersEventBus(_fakeEventBus)
            .WithContainerDatabaseConfigured(database.ConnectionString!);

        _applicationInMemory.CreateClient();
    }

    [Fact]
    internal async Task Given_pass_expired_event_published_Then_new_offer_should_be_prepared()
    {
        // Arrange
        using var integrationEventHandlerScope =
            new IntegrationEventHandlerScope<PassExpiredEvent>(_applicationInMemory);
        var integrationEventHandler = integrationEventHandlerScope.IntegrationEventHandler;
        var @event = PassExpiredEventFaker.CreateValid();

        // Act
        await integrationEventHandler.Handle(@event, CancellationToken.None);

        // Assert
        EnsureThatOfferPreparedEventWasPublished();
    }

    private void EnsureThatOfferPreparedEventWasPublished() => _fakeEventBus.Received(1)
        .PublishAsync(Arg.Any<OfferPrepareEvent>(), Arg.Any<CancellationToken>());
}