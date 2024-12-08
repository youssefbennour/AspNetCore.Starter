using Starter.Common.Events.EventBus;
using Starter.Contracts.SignContract.Events;
using Starter.IntegrationTests.Common;
using Starter.IntegrationTests.Common.TestEngine.Configuration;
using Starter.IntegrationTests.Common.TestEngine.IntegrationEvents.Handlers;
using Starter.Passes.EventBus;
using Starter.Passes.RegisterPass.Events;

namespace Starter.IntegrationTests.Passes.RegisterPass;

public sealed class RegisterPassTests : IntegrationTest, IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _applicationInMemory;
    private readonly IPassesEventBus _fakeEventBus = Substitute.For<IPassesEventBus>();

    public RegisterPassTests(WebApplicationFactory<Program> applicationInMemoryFactory,
        DatabaseContainer database) : base(database)
    {
        _applicationInMemory = applicationInMemoryFactory
                .WithContainerDatabaseConfigured(database.ConnectionString!)
                .WithFakePassesEventBus(_fakeEventBus);
        _applicationInMemory.CreateClient();
    }

    [Fact]
    internal async Task Given_contract_signed_event_Then_should_register_pass()
    {
        // Arrange
        using var integrationEventHandlerScope =
            new IntegrationEventHandlerScope<ContractSignedEvent>(_applicationInMemory);
        var @event = ContractSignedEventFaker.Create();

        // Act
        await integrationEventHandlerScope.Consume(@event);

        // Assert
        EnsureThatPassRegisteredEventWasPublished();
    }

    private void EnsureThatPassRegisteredEventWasPublished() => _fakeEventBus.Received(1)
        .PublishAsync(Arg.Any<PassRegisteredEvent>(), Arg.Any<CancellationToken>());
}
