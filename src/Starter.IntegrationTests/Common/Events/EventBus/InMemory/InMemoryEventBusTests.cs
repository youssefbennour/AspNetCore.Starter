using Microsoft.Extensions.DependencyInjection;
using Starter.Common.Events.EventBus;
using Starter.IntegrationTests.Common.TestEngine.Configuration;

namespace Starter.IntegrationTests.Common.Events.EventBus.InMemory;


public sealed class InMemoryEventBusTests(
    WebApplicationFactory<Program> applicationInMemoryFactory,
    DatabaseContainer database) : IntegrationTest(database), IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _applicationInMemory = applicationInMemoryFactory
        .WithContainerDatabaseConfigured(database.ConnectionString!)
        .WithFakeConsumers();

    [Fact]
    internal async Task Given_valid_event_published_Then_event_should_be_consumed()
    {
        // Arrange
        using var scope = _applicationInMemory.Services.CreateScope();
        var eventBus = scope.ServiceProvider.GetRequiredService<IEventBus>();
        var fakeEvent = FakeEvent.Create();

        // Act
        await eventBus.PublishAsync(fakeEvent, CancellationToken.None);

        // Assert
        fakeEvent.Consumed.Should().BeTrue();
    }
}
