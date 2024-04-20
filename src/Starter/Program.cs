using Microsoft.Extensions.Localization;
using Starter.Common.ApiConfiguration;
using Starter.Common.BusinessRuleEngine;
using Starter.Common.Clock;
using Starter.Common.ErrorHandling;
using Starter.Common.Events.EventBus;
using Starter.Common.Localizations;
using Starter.Common.Validation.Requests;

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

var app = builder.Build();

if(app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRequestBasedLocalization();
app.UseAuthorization();
app.UseErrorHandling();
app.MapControllers();


app.MapGet("/", (IStringLocalizer<TestLocalization> loc) => {
    return loc["Hello"].Value;
});

app.Run();


namespace Starter {
    [UsedImplicitly]
    public sealed class Program;
}
