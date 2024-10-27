using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Softylines.Contably.Common.ErrorHandling.Exceptions.Abstractions;

namespace Softylines.Contably.Common.ErrorHandling;

public sealed class GlobalExceptionHandler(
    IWebHostEnvironment environment
    ) : IExceptionHandler 
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken) {

        if (exception is not  IAppException && environment.IsDevelopment())
        {
            await httpContext.Response
                .WriteAsJsonAsync(exception, SerializerOptions, cancellationToken);
            return true;
        }
        
        var problemDetails = exception.ToProblemDetails();
        httpContext.Response.StatusCode = exception.GetHttpStatusCode();

        await httpContext.Response
            .WriteAsJsonAsync(problemDetails, SerializerOptions, cancellationToken);

        return true;
    }
    
    private static readonly JsonSerializerOptions SerializerOptions = new() {
        WriteIndented = true,
        IncludeFields = true,
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };
}
