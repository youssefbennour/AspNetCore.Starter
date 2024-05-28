using Starter.Reports.GenerateNewPassesRegistrationsPerMonthReport;

namespace Starter.Reports;

internal static class ReportsEndpoints
{
    internal static void MapReports(this IEndpointRouteBuilder app) =>
        app.MapGenerateNewPassesRegistrationsPerMonthReport();
}
