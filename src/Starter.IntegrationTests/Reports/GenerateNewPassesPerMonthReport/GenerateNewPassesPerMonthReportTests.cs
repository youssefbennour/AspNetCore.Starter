using System.Net.Http.Json;
using Starter.Contracts.SignContract.Events;
using Starter.IntegrationTests.Common;
using Starter.IntegrationTests.Common.TestEngine.Configuration;
using Starter.IntegrationTests.Common.TestEngine.IntegrationEvents.Handlers;
using Starter.IntegrationTests.Passes.RegisterPass;
using Starter.IntegrationTests.Reports.GenerateNewPassesPerMonthReport.TestData;
using Starter.Reports;
using Starter.IntegrationTests.Common.TestEngine.Time;
using Starter.Reports.GenerateNewPassesRegistrationsPerMonthReport.Dtos;

namespace Starter.IntegrationTests.Reports.GenerateNewPassesPerMonthReport;

public sealed partial class GenerateNewPassesPerMonthReportTests : 
    IntegrationTest, IClassFixture<WebApplicationFactory<Program>>
{
    private static readonly FakeTimeProvider FakeTimeProvider = new(ReportTestCases.FakeNowDate);
    private readonly HttpClient _applicationHttpClient;
    private readonly WebApplicationFactory<Program> _applicationInMemoryFactory;

    public GenerateNewPassesPerMonthReportTests(WebApplicationFactory<Program> applicationInMemoryFactory,
        DatabaseContainer database) : base(database)
    {
        _applicationInMemoryFactory = applicationInMemoryFactory
            .WithContainerDatabaseConfigured(database.ConnectionString!)
            .WithTime(FakeTimeProvider);

        _applicationHttpClient = _applicationInMemoryFactory.CreateClient();
    }

    [Theory]
    [ClassData(typeof(ReportTestCases))]
    internal async Task Given_valid_generate_new_report_request_Then_should_return_correct_data(
        List<PassRegistrationDateRange> passRegistrationDateRanges)
    {
        // Arrange
        await RegisterPasses(passRegistrationDateRanges);

        // Act
        var getReportResult = await _applicationHttpClient.GetAsync(ReportsApiPaths.GenerateNewReport);

        // Assert
        getReportResult.Should().HaveStatusCode(HttpStatusCode.OK);
        var reportData = await getReportResult.Content.ReadFromJsonAsync<NewPassesRegistrationsPerMonthResponse>();
        await Verify(reportData);
    }

    private async Task RegisterPasses(List<PassRegistrationDateRange> reportTestData)
    {
        foreach (var passRegistration in reportTestData)
        {
            await RegisterPass(passRegistration.From, passRegistration.To);
        }
    }

    private async Task RegisterPass(DateTimeOffset from, DateTimeOffset to)
    {
        using var integrationEventHandlerScope =
            new IntegrationEventHandlerScope<ContractSignedEvent>(_applicationInMemoryFactory);
        var integrationEventHandler = integrationEventHandlerScope.IntegrationEventHandler;
        var @event = ContractSignedEventFaker.Create(from, to);
        await integrationEventHandler.Handle(@event, CancellationToken.None);
    }
}
