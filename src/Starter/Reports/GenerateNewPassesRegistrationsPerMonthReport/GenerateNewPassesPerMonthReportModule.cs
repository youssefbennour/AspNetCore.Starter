using Starter.Reports.DataAccess;
using Starter.Reports.GenerateNewPassesRegistrationsPerMonthReport.DataRetriever;

namespace Starter.Reports.GenerateNewPassesRegistrationsPerMonthReport;

internal static class GenerateNewPassesPerMonthReportModule
{
    internal static IServiceCollection AddNewPassesRegistrationsPerMonthReport(this IServiceCollection services)
    {
        services.AddSingleton<INewPassesRegistrationPerMonthReportDataRetriever, NewPassesRegistrationPerMonthReportDataRetriever>();
        services.AddSingleton<IDatabaseConnectionFactory, DatabaseConnectionFactory>();

        return services;
    }
}