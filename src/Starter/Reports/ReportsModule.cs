using Starter.Reports.DataAccess;
using Starter.Reports.GenerateNewPassesRegistrationsPerMonthReport;

namespace Starter.Reports;

internal static class ReportsModule
{
    internal static IServiceCollection AddReports(this IServiceCollection services)
    {
        services.AddDataAccess();
        services.AddNewPassesRegistrationsPerMonthReport();

        return services;
    }

    internal static IApplicationBuilder UseReports(this IApplicationBuilder applicationBuilder) =>
        applicationBuilder;
}
