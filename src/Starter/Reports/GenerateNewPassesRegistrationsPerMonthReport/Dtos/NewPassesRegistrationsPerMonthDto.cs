namespace Starter.Reports.GenerateNewPassesRegistrationsPerMonthReport.Dtos;

public sealed record NewPassesRegistrationsPerMonthDto(int MonthOrder, string MonthName, long RegisteredPasses);