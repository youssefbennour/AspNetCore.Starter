namespace Starter.Common.ErrorHandling;

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

internal sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler {
    private const string ErrorOccurredMessage = "An error occurred.";

    private static readonly Action<ILogger, string, Exception> LogException =
        LoggerMessage.Define<string>(LogLevel.Error, eventId:
            new EventId(0, "ERROR"), formatString: "{Message}");

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken) {
        LogException(logger, ErrorOccurredMessage, exception);

        ProblemDetails problemDetails = exception.ToProblemDetails();

        httpContext.Response.StatusCode = problemDetails.Status!.Value;
        await httpContext.Response
            .WriteAsJsonAsync(problemDetails, typeof(ProblemDetails), ,cancellationToken);

        return true;
    }
}
