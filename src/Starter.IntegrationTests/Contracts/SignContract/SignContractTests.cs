using System.Net.Http.Json;
using Starter.Common.ErrorHandling;
using Starter.Common.Events.EventBus;
using Starter.Contracts;
using Starter.Contracts.PrepareContract;
using Starter.Contracts.SignContract;
using Starter.Contracts.SignContract.Events;
using Starter.IntegrationTests.Common.TestEngine.Configuration;
using Starter.IntegrationTests.Contracts.PrepareContract;

namespace Starter.IntegrationTests.Contracts.SignContract;

public sealed class SignContractTests : IClassFixture<WebApplicationFactory<Program>>, IClassFixture<DatabaseContainer>
{
    private readonly HttpClient _applicationHttpClient;
    private readonly IEventBus _fakeEventBus = Substitute.For<IEventBus>();

    public SignContractTests(WebApplicationFactory<Program> applicationInMemoryFactory,
        DatabaseContainer database) =>
        _applicationHttpClient = applicationInMemoryFactory
            .WithFakeEventBus(_fakeEventBus)
            .WithContainerDatabaseConfigured(database.ConnectionString!)
            .CreateClient();

    [Fact]
    internal async Task Given_valid_contract_signature_request_Then_should_return_no_content_status_code()
    {
        // Arrange
        var preparedContractId = await PrepareContract();
        var requestParameters = SignContractRequestParameters.GetValid(preparedContractId);
        var signContractRequest = new SignContractRequest(requestParameters.SignedAt);

        // Act
        var signContractResponse =
            await _applicationHttpClient.PatchAsJsonAsync(requestParameters.Url, signContractRequest);

        // Assert
        signContractResponse.Should().HaveStatusCode(HttpStatusCode.OK);
    }

    [Fact]
    internal async Task Given_valid_contract_signature_request_Then_contract_signed_event_was_published()
    {
        // Arrange
        var preparedContractId = await PrepareContract();
        var requestParameters = SignContractRequestParameters.GetValid(preparedContractId);
        var signContractRequest = new SignContractRequest(requestParameters.SignedAt);

        // Act
        await _applicationHttpClient.PatchAsJsonAsync(requestParameters.Url, signContractRequest);

        // Assert
        EnsureThatContractSignedEventWasPublished();
    }

    private void EnsureThatContractSignedEventWasPublished() => _fakeEventBus.Received(1)
        .PublishAsync(Arg.Any<ContractSignedEvent>(), Arg.Any<CancellationToken>());

    [Fact]
    internal async Task Given_contract_signature_request_with_not_existing_id_Then_should_return_not_found()
    {
        // Arrange
        var requestParameters = SignContractRequestParameters.GetWithNotExistingContractId();
        var signContractRequest = new SignContractRequest(requestParameters.SignedAt);

        // Act
        var signContractResponse =
            await _applicationHttpClient.PatchAsJsonAsync(requestParameters.Url, signContractRequest);

        // Assert
        signContractResponse.Should().HaveStatusCode(HttpStatusCode.NotFound);
    }

    [Fact]
    internal async Task
        Given_contract_signature_request_with_invalid_signed_date_Then_should_return_unprocessable_entity_status_code()
    {
        // Arrange
        var preparedContractId = await PrepareContract();
        var requestParameters =
            SignContractRequestParameters.GetWithInvalidSignedAtDate(preparedContractId);
        var signContractRequest = new SignContractRequest(requestParameters.SignedAt);

        // Act
        var signContractResponse =
            await _applicationHttpClient.PatchAsJsonAsync(requestParameters.Url, signContractRequest);

        // Assert
        signContractResponse.Should().HaveStatusCode(HttpStatusCode.UnprocessableEntity);

        var responseMessage = await signContractResponse.Content.ReadFromJsonAsync<AppProblemDetails>();
        responseMessage?.Errors?.Count.Should().Be(1);
        var error = responseMessage?.Errors?.First();
        error?.Message.Should().Be("Contract can not be signed because more than 30 days have passed from the contract preparation");
    }

    private async Task<Guid> PrepareContract()
    {
        var requestParameters = PrepareContractRequestParameters.GetValid();
        PrepareContractRequest prepareContractRequest = new PrepareContractRequestFaker(requestParameters.MinAge,
            requestParameters.MaxAge, requestParameters.MinHeight, requestParameters.MaxHeight);
        var prepareContractResponse =
            await _applicationHttpClient.PostAsJsonAsync(ContractsApiPaths.Prepare, prepareContractRequest);
        var preparedContractId = await prepareContractResponse.Content.ReadFromJsonAsync<Guid>();

        return preparedContractId;
    }
}
