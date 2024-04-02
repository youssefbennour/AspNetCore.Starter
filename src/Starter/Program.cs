using JetBrains.Annotations;
using Starter.Common.Clock;
using Starter.Common.ErrorHandling;
using Starter.Common.Events.EventBus;
using Starter.Common.Validation.Requests;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddExceptionHandling();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddEventBus();
builder.Services.AddRequestsValidations();
builder.Services.AddClock();

var app = builder.Build();

if (app.Environment.IsDevelopment()){
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseErrorHandling();

app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.Run();


namespace Starter { 
    [UsedImplicitly]
    public sealed class Program;
}
