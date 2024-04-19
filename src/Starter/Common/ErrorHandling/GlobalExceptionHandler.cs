namespace Starter.Common.ErrorHandling;

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Starter.Common.BusinessRuleEngine;
using Starter.Common.ErrorHandling.ErrorModels;
using Starter.Common.Validation.Requests.Exceptions;

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

        ProblemDetails problemDetails = exception switch {
            BusinessRuleValidationException businessRuleValidationException =>
                businessRuleValidationException.ToValidationError(),

            AlreadyExistsException alreadyExistsException => alreadyExistsException.ToUndetailedError(),
            BadRequestException badRequestException => badRequestException.ToUndetailedError(),
            NotFoundException notFoundException => notFoundException.ToUndetailedError(),
            UnauthorizedException unauthorizedException => unauthorizedException.ToUndetailedError(),
            ForbiddenException forbiddenException => forbiddenException.ToUndetailedError(),
            _ => ValidationError.InternalServerError,
        };

        httpContext.Response.StatusCode = problemDetails.Status!.Value;
        await httpContext.Response
            .WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
