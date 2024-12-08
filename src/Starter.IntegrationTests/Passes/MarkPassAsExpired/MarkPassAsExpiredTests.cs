using System.Net.Http.Json;
using Microsoft.AspNetCore.WebUtilities;
using Starter.Common.Events.EventBus;
using Starter.Common.Responses.Models;
using Starter.Contracts.SignContract.Events;
using Starter.IntegrationTests.Common;
using Starter.IntegrationTests.Common.TestEngine.Configuration;
using Starter.IntegrationTests.Common.TestEngine.IntegrationEvents.Handlers;
using Starter.IntegrationTests.Passes.RegisterPass;
using Starter.Passes;
using Starter.Passes.EventBus;
using Starter.Passes.GetAllPasses;
using Starter.Passes.MarkPassAsExpired.Events;

namespace Starter.IntegrationTests.Passes.MarkPassAsExpired;

public sealed class MarkPassAsExpiredTests : IntegrationTest, IClassFixture<WebApplicationFactory<Program>>
{
    private static readonly StringContent EmptyContent = new(string.Empty);

    private readonly HttpClient _applicationHttpClient;
    private readonly WebApplicationFactory<Program> _applicationInMemoryFactory;
    private readonly IPassesEventBus _fakeEventBus = Substitute.For<IPassesEventBus>();

    public MarkPassAsExpiredTests(WebApplicationFactory<Program> applicationInMemoryFactory,
        DatabaseContainer database) : base(database)
    {
        _applicationInMemoryFactory = applicationInMemoryFactory
            .WithContainerDatabaseConfigured(database.ConnectionString!)
            .WithFakePassesEventBus(_fakeEventBus);
        _applicationHttpClient = _applicationInMemoryFactory.CreateClient();
    }

    [Fact]
    internal async Task Given_valid_mark_pass_as_expired_request_Then_should_return_no_content()
    {
        // Arrange
        var contractSigned = await RegisterPass();
        var registeredPassId = await GetCreatedPass(contractSigned.ContractCustomerId);
        var url = BuildUrl(registeredPassId);

        // Act
        await _applicationHttpClient.PatchAsJsonAsync(url, EmptyContent);

        // Assert
        EnsureThatPassExpiredEventWasPublished();
    }

    [Fact]
    internal async Task Given_valid_mark_pass_as_expired_request_Then_should_publish_pass_expired_event()
    {
        // Arrange
        var contractSigned = await RegisterPass();
        var registeredPassId = await GetCreatedPass(contractSigned.ContractCustomerId);
        var url = BuildUrl(registeredPassId);

        // Act
        var markAsExpiredResponse = await _applicationHttpClient.PatchAsJsonAsync(url, EmptyContent);

        // Assert
        markAsExpiredResponse.Should().HaveStatusCode(HttpStatusCode.OK);
    }

    [Fact]
    internal async Task Given_mark_pass_as_expired_request_with_not_existing_id_Then_should_return_not_found()
    {
        // Arrange
        var notExistingId = Guid.NewGuid();
        var url = BuildUrl(notExistingId);

        // Act
        var markAsExpiredResponse = await _applicationHttpClient.PatchAsJsonAsync(url, EmptyContent);

        // Assert
        markAsExpiredResponse.Should().HaveStatusCode(HttpStatusCode.NotFound);
    }

    private async Task<ContractSignedEvent> RegisterPass()
    {
        var @event = ContractSignedEventFaker.Create();
        using var integrationEventHandlerScope =
            new IntegrationEventHandlerScope<ContractSignedEvent>(_applicationInMemoryFactory);
        await integrationEventHandlerScope.Consume(@event);

        return @event;
    }

    private async Task<Guid> GetCreatedPass(Guid customerId)
    {
        var createdPass = await CreatedPass(customerId);
        createdPass.Should().NotBeNull();

        return createdPass!.Id;
    }

    private async Task<PassDto?> CreatedPass(Guid customerId)
    {
        var query = new Dictionary<string, string?>
        {
            ["page"] = "1",
            ["per_page"] = "20"
        }; 
        
        var getAllPassesResponse = await _applicationHttpClient.GetAsync(
            QueryHelpers.AddQueryString(PassesApiPaths.GetAll, query));
        
        var response = await getAllPassesResponse.Content.ReadFromJsonAsync<PaginatedList<PassDto>>();
        var createdPass = response!.Data.FirstOrDefault(pass => pass.CustomerId == customerId);
        return createdPass;
    }

    private static string BuildUrl(Guid id) => PassesApiPaths.MarkPassAsExpired.Replace("{id}", id.ToString());

    private void EnsureThatPassExpiredEventWasPublished() => _fakeEventBus.Received(1)
        .PublishAsync(Arg.Any<PassExpiredEvent>(), Arg.Any<CancellationToken>());
}
