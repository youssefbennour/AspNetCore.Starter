using System.Net.Http.Json;
using Starter.Common.ErrorHandling;
using Starter.Contracts;
using Starter.Contracts.PrepareContract;
using Starter.IntegrationTests.Common;
using Starter.IntegrationTests.Common.TestEngine.Configuration;

namespace Starter.IntegrationTests.Contracts.PrepareContract;

public sealed class PrepareContractTests(
    WebApplicationFactory<Program> applicationInMemoryFactory,
    DatabaseContainer database) : IntegrationTest(database), IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _applicationHttpClient = applicationInMemoryFactory
        .WithContainerDatabaseConfigured(database.ConnectionString!)
        .CreateClient();

    [Fact]
    internal async Task Given_valid_contract_preparation_request_Then_should_return_created_status_code()
    {
        // Arrange
        var requestParameters = PrepareContractRequestParameters.GetValid();

        // Act
        var prepareContractResponse = await PrepareCorrectContract(requestParameters);

        // Assert
        prepareContractResponse.Should().HaveStatusCode(HttpStatusCode.Created);
    }

    [Fact]
    internal async Task Given_contract_preparation_request_with_invalid_age_Then_should_return_unprocessable_entity_status_code()
    {
        // Arrange
        var requestParameters = PrepareContractRequestParameters.GetWithInvalidAge();

        PrepareContractRequest prepareContractRequest = new PrepareContractRequestFaker(requestParameters.MinAge,
            requestParameters.MaxAge, requestParameters.MinHeight, requestParameters.MaxHeight);

        // Act
        var prepareContractResponse =
            await _applicationHttpClient.PostAsJsonAsync(ContractsApiPaths.Prepare, prepareContractRequest);

        // Assert
        prepareContractResponse.Should().HaveStatusCode(HttpStatusCode.UnprocessableEntity);

        var responseMessage = await prepareContractResponse.Content.ReadFromJsonAsync<AppProblemDetails>();
        responseMessage?.Errors?.Count.Should().Be(1);
        var error = responseMessage?.Errors?.First();
        error?.Message.Should().Be("Contract can not be prepared for a person who is not adult");
    }

    [Fact]
    internal async Task Given_contract_preparation_request_with_invalid_height_Then_should_return_unprocessable_entity_status_code()
    {
        // Arrange
        var requestParameters = PrepareContractRequestParameters.GetWithInvalidHeight();

        PrepareContractRequest prepareContractRequest = new PrepareContractRequestFaker(requestParameters.MinAge,
            requestParameters.MaxAge, requestParameters.MinHeight, requestParameters.MaxHeight);

        // Act
        var prepareContractResponse =
            await _applicationHttpClient.PostAsJsonAsync(ContractsApiPaths.Prepare, prepareContractRequest);

        // Assert
        prepareContractResponse.Should().HaveStatusCode(HttpStatusCode.UnprocessableEntity);

        var responseMessage = await prepareContractResponse.Content.ReadFromJsonAsync<AppProblemDetails>();
        responseMessage?.Errors?.Count.Should().Be(1);
        var error = responseMessage?.Errors?.First();
        error?.Message.Should().Be("Customer height must fit maximum limit for gym instruments");
    }

    [Fact]
    internal async Task Given_contract_preparation_request_When_contract_for_customer_was_prepared_earlier_and_was_not_signed_yet_Then_should_return_unprocessable_entity_status_code()
    {
        // Arrange
        var requestParameters = PrepareContractRequestParameters.GetValid();
        var customerId = Guid.NewGuid();
        await PrepareCorrectContract(requestParameters, customerId);
        
        //Act
        var prepareContractResponse = await PrepareCorrectContract(requestParameters, customerId);

        // Assert
        prepareContractResponse.Should().HaveStatusCode(HttpStatusCode.UnprocessableEntity);
        var responseMessage = await prepareContractResponse.Content.ReadFromJsonAsync<AppProblemDetails>();
        responseMessage?.Errors?.Count.Should().Be(1);
        var error = responseMessage?.Errors?.First();
        error?.Message.Should().Be("Previous contract must be signed by the customer");
    }

    private async Task<HttpResponseMessage> PrepareCorrectContract(PrepareContractRequestParameters requestParameters, Guid? customerId = null)
    {
        PrepareContractRequest prepareContractRequest = new PrepareContractRequestFaker(requestParameters.MinAge,
            requestParameters.MaxAge, requestParameters.MinHeight, requestParameters.MaxHeight, customerId);
        var prepareContractResponse =
            await _applicationHttpClient.PostAsJsonAsync(ContractsApiPaths.Prepare, prepareContractRequest);
        return prepareContractResponse;
    }
}
