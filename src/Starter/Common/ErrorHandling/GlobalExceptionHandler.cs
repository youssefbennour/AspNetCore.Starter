namespace Starter.Common.ErrorHandling;

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Starter.Common.BusinessRuleEngine;
using Starter.Common.ErrorHandling.ErrorModels;
using Starter.Common.ErrorHandling.Exceptions;
using Starter.Common.ErrorHandling.Exceptions.Abstractions;
using Starter.Common.Validation.Requests.Exceptions;

internal sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    private const string ErrorOccurredMessage = "An error occurred.";

    private static readonly Action<ILogger, string, Exception> LogException =
        LoggerMessage.Define<string>(LogLevel.Error, eventId:
            new EventId(0, "ERROR"), formatString: "{Message}");

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        LogException(logger, ErrorOccurredMessage, exception);

        httpContext.Response.StatusCode = 
            exception is AppException appException ? 
            appException.GetHttpStatusCode() :
            StatusCodes.Status500InternalServerError;

        var problemDetails = exception switch
        {
            BusinessRuleValidationException businessRuleValidationException =>
                businessRuleValidationException.ToValidationError(),

            AlreadyExistsException alreadyExistsException => alreadyExistsException.ToUndetailedError(),
            BadRequestException badRequestException => badRequestException.ToUndetailedError(),
            NotFoundException notFoundException => notFoundException.ToUndetailedError(),
            UnauthorizedException unauthorizedException => unauthorizedException.ToUndetailedError(),
            ForbiddenException forbiddenException => forbiddenException.ToUndetailedError(),
            _ => ValidationError.InternalServerError,
        };

        
        await httpContext.Response
            .WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
