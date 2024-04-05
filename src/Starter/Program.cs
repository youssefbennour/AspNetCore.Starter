using JetBrains.Annotations;
using Microsoft.AspNetCore.Localization;
using Starter.Common.Clock;
using Starter.Common.ErrorHandling;
using Starter.Common.Events.EventBus;
using Starter.Common.Validation.Requests;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddExceptionHandling();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddEventBus();
builder.Services.AddRequestsValidations();
builder.Services.AddClock();

builder.Services.AddLocalization();
builder.Services.Configure<RequestLocalizationOptions>(
    opts =>
    {
        var supportedCultures = new List<CultureInfo>
        {
            new CultureInfo("en"),
            new CultureInfo("fr"),
            new CultureInfo("ar")
        };

        opts.DefaultRequestCulture = new RequestCulture("en", "en");
        // Formatting numbers, dates, etc.
        opts.SupportedCultures = supportedCultures;
        // UI strings that we have localized.
        opts.SupportedUICultures = supportedCultures;
    });

var app = builder.Build();

if (app.Environment.IsDevelopment()){
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRequestLocalization();

app.UseAuthorization();

app.UseErrorHandling();

app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.Run();


namespace Starter { 
    [UsedImplicitly]
    public sealed class Program;
}
