using Starter.Common.ErrorHandling;

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
builder.Services.AddTelemetry();
var app = builder.Build();

if(app.Environment.IsDevelopment()) {
    app.UseSwagger();
}

app.UseHttpsRedirection();
app.UseRequestBasedLocalization();
app.UseAuthorization();
app.UseErrorHandling();
app.MapControllers();

app.MapLocalizationSampleEndpoint();
app.MapGet("/", () => {
    for(int i = 0; i < 10_000_000; i++){
        
    }
    return "Hello world";
});

app.Run();

namespace Starter {
    [UsedImplicitly]
    public sealed class Program;
}
