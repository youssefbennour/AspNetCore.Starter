using System.Reflection;
using Starter.Common.ApiConfiguration;
using Starter.Common.Auth;
using Starter.Common.Clocks;
using Starter.Common.ErrorHandling;
using Starter.Common.Events.Publisher;
using Starter.Common.EventualConsistency.Outbox;
using Starter.Common.Localizations;
using Starter.Common.Telemetry;
using Starter.Contracts;
using Starter.Offers;
using Starter.Passes;
using Starter.Reports;

var builder = WebApplication.CreateBuilder(args);

builder.AddExceptionHandling()
    .AddAuthModule()
    .AddTelemetry();

builder.Services
    .AddApiConfiguration<Program>()
    .AddClock()
    .AddRequestBasedLocalization()
    .AddOutboxModule(Assembly.GetExecutingAssembly())
    .AddPublisher(Assembly.GetExecutingAssembly());

builder.Services
    .AddPasses(builder.Configuration)
    .AddContracts(builder.Configuration)
    .AddOffers(builder.Configuration)
    .AddReports();

var app = builder.Build();

if(app.Environment.IsDevelopment()) {
    app.UseSwagger();
}

app.UseApiConfiguration();
app.UseExceptionHandling();
app.UseAuthModule();
app.UseRequestBasedLocalization();
app.MapControllers();
app.UseHttpLogging();
app.UseTelemetry();

app.UsePasses();
app.UseContracts();
app.UseReports();
app.UseOffers();

app.MapPasses();
app.MapContracts();
app.MapReports();

app.Run();

namespace Starter {
    [UsedImplicitly]
    public sealed class Program;
}
