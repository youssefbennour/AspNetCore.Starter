using Starter.Common.BusinessRuleEngine;
using Starter.Common.ErrorHandling.ErrorModels;
using Starter.Common.ErrorHandling.Exceptions;
using Starter.Common.ErrorHandling.Exceptions.Abstractions;
using Starter.Common.Validation.Requests.Exceptions;
using System.Collections;

namespace Starter.Common.ErrorHandling;

internal static class ErrorHandlingExtensions
{

    internal static ValidationError ToUndetailedError(this AppException appException) =>
        new ValidationError(appException.Message);

    internal static ValidationError ToValidationError(this AppException appException) =>
        new ValidationError(appException.Message)
        {
            Errors = appException.GetFieldValidationErrors()
            .ToList()
        };

    internal static IEnumerable<FieldValidationError> GetFieldValidationErrors(this AppException appException)
    {
        foreach (DictionaryEntry error in appException.Data)
        {
            if (error.Key.ToString() is not string field
               || error.Key.ToString() is not string errorMessage)
            {
                continue;
            }
            yield return new FieldValidationError(field, errorMessage);
        }
    }
    internal static int GetHttpStatusCode(this AppException appException) =>
        appException switch
        {
            BusinessRuleValidationException => StatusCodes.Status422UnprocessableEntity,
            NotFoundException => StatusCodes.Status404NotFound,
            BadRequestException => StatusCodes.Status400BadRequest,
            AlreadyExistsException => StatusCodes.Status409Conflict,
            ForbiddenException => StatusCodes.Status403Forbidden,
            UnauthorizedException => StatusCodes.Status401Unauthorized,
            _ => StatusCodes.Status500InternalServerError,
        };
        

    internal static IApplicationBuilder UseErrorHandling(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseExceptionHandler();

        return applicationBuilder;
    }

    internal static IServiceCollection AddExceptionHandling(this IServiceCollection services)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();

        return services;
    }
}
