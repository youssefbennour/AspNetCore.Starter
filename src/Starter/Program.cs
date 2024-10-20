using Starter.Contracts;
using Starter.Offers;
using Starter.Passes;
using Starter.Reports;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddNewtonsoftJson();

builder.Services.AddExceptionHandling();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddEventBus();
builder.Services.AddRequestsValidations();
builder.Services.AddClock();
builder.Services.AddRequestBasedLocalization();
builder.Services.AddCustomApiVersioning();
builder.Services.AddOpenApiConfiguration();
builder.AddAuthModule();
builder.AddTelemetry();

builder.Services.AddPasses(builder.Configuration);
builder.Services.AddContracts(builder.Configuration);
builder.Services.AddOffers(builder.Configuration);
builder.Services.AddReports();

var app = builder.Build();

if(app.Environment.IsDevelopment()) {
    app.UseSwagger();
}
app.UsePasses();
app.UseContracts();
app.UseReports();
app.UseOffers();

app.UseAuthModule();
app.UseRequestBasedLocalization();
app.UseErrorHandling();
app.MapControllers();
app.UseHttpLogging();
app.UseTelemetry();
app.MapLocalizationSampleEndpoint();
    
app.MapPasses();
app.MapContracts();
app.MapReports();

app.Run();

namespace Starter {
    [UsedImplicitly]
    public sealed class Program;
}
