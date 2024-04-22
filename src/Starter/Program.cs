using Starter.Common.ApiConfiguration;
using Starter.Common.Clock;
using Starter.Common.ErrorHandling;
using Starter.Common.ErrorHandling.Exceptions;
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

app.MapLocalizationSampleEndpoint();
app.MapGet("/", () => {
    var exception = new InternalServerException("Some title");
    exception.Data["Id"] = "Id cannot be null";
    exception.Data["Id"] = "Id must be exactly 12 characters";
    exception.Data["Email"] = "Email is not correct";

    throw exception;
});

app.Run();

namespace Starter {
    [UsedImplicitly]
    public sealed class Program;
}
