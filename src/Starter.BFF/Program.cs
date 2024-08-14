using Starter.BFF.Auth;
using Starter.BFF.RequestProxying;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProxy(builder.Configuration);
builder.AddAuth();
builder.Services.AddClock();
builder.Services.AddControllers();


var app = builder.Build();

app.UseRouting();
app.UseAuth();
app.UseProxy();
app.MapControllers();

app.MapGet("/", () => "Hello World!").RequireAuthorization();

app.Run();