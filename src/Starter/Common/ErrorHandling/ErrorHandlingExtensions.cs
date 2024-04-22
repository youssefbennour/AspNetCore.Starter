using Starter.Common.BusinessRuleEngine;
using Starter.Common.ErrorHandling.ErrorModels;
using Starter.Common.ErrorHandling.Exceptions;
using Starter.Common.ErrorHandling.Exceptions.Abstractions;
using Starter.Common.Validation.Requests.Exceptions;
using System.Collections;

namespace Starter.Common.ErrorHandling;

internal static class ErrorHandlingExtensions {

    private const string ServerError = "Server Error";

    internal static IApplicationBuilder UseErrorHandling(this IApplicationBuilder applicationBuilder) {
        applicationBuilder.UseExceptionHandler(o => { });
        return applicationBuilder;
    }

    internal static IServiceCollection AddExceptionHandling(this IServiceCollection services) {
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();
        return services;
    }

    internal static AppProblemDetails ToProblemDetails(this Exception exception) {
        AppProblemDetails problemDetails = new();

        if(exception is BusinessRuleValidationException businessRuleValidationException) {
            problemDetails.Errors = businessRuleValidationException.GetFieldValidationErrors();
        }
        problemDetails.Message = exception.GetErrorMessage();

        return problemDetails;
    }

    private static string GetErrorMessage(this Exception exception) {
        if(exception is InternalServerException
            || exception is not AppException) {
            return ServerError;
        }

        return exception.Message;
    }

    internal static List<FieldValidationError> GetFieldValidationErrors(this AppException appException) {
        List<FieldValidationError> fieldValidationErrors = [];

        foreach(DictionaryEntry error in appException.Data) {
            if(error.Key.ToString() is not string field
               || error.Value?.ToString() is not string errorMessage) {
                continue;
            }
            fieldValidationErrors.Add(new FieldValidationError(field, errorMessage));
        }

        return fieldValidationErrors;
    }
    internal static int GetHttpStatusCode(this Exception exception) {

        if(exception is not AppException appException) {
            return StatusCodes.Status500InternalServerError;
        }

        return appException switch {
            BusinessRuleValidationException => StatusCodes.Status422UnprocessableEntity,
            NotFoundException => StatusCodes.Status404NotFound,
            BadRequestException => StatusCodes.Status400BadRequest,
            AlreadyExistsException => StatusCodes.Status409Conflict,
            ForbiddenException => StatusCodes.Status403Forbidden,
            UnauthorizedException => StatusCodes.Status401Unauthorized,
            _ => StatusCodes.Status500InternalServerError,
        };
    }
}
