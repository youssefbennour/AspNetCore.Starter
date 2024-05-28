using Starter.Common.ErrorHandling;
using Starter.Contracts;
using Starter.Offers;
using Starter.Passes;
using Starter.Reports;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
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

var app = builder.Build();

if(app.Environment.IsDevelopment()) {
    app.UseSwagger();
}

app.UseHttpsRedirection();
app.UseAuthModule();
app.UseRequestBasedLocalization();
app.UseAuthorization();
app.UseErrorHandling();
app.MapControllers();
app.UseHttpLogging();
app.UseTelemetry();
app.MapLocalizationSampleEndpoint();
    
app.MapGet("/", async (ILogger<Program> logger) =>
{
    return "Hello world";
}).RequireAuthorization();

app.Run();

namespace Starter {
    [UsedImplicitly]
    public sealed class Program;
}
