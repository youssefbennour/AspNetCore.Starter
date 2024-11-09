using System.Reflection;
using Starter.Common.ApiConfiguration;
using Starter.Common.Auth.JwtBearer;
using Starter.Common.Clocks;
using Starter.Common.ErrorHandling;
using Starter.Common.Events.Publisher;
using Starter.Common.Localizations;
using Starter.Common.Telemetry;
using Starter.Contracts;
using Starter.Offers;
using Starter.Passes;
using Starter.Reports;

var builder = WebApplication.CreateBuilder(args);
builder.AddExceptionHandling()
    .AddJwtAuthentication()
    .AddTelemetry();

builder.Services
    .AddApiConfiguration<Program>()
    .AddClock()
    .AddRequestBasedLocalization()
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
app.UseJwtAuth();
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
app.MapLocalizationSampleEndpoint();
#pragma warning disable S6966
app.Run();

namespace Starter {
    [UsedImplicitly]
    public sealed class Program;
}
