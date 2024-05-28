using Starter.Reports.GenerateNewPassesRegistrationsPerMonthReport.Dtos;

namespace Starter.Reports.GenerateNewPassesRegistrationsPerMonthReport.DataRetriever;

internal interface INewPassesRegistrationPerMonthReportDataRetriever
{
    Task<IReadOnlyCollection<NewPassesRegistrationsPerMonthDto>> GetReportDataAsync(CancellationToken cancellationToken = default);
}
