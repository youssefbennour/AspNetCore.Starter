using Microsoft.AspNetCore.Mvc;
using Starter.Common.BusinessRuleEngine;
using Starter.Common.ErrorHandling.ErrorModels;
using Starter.Common.ErrorHandling.Exceptions.Abstractions;
using Starter.Common.Validation.Requests.Exceptions;
using System.Collections;

namespace Starter.Common.ErrorHandling;

internal static class ErrorHandlingExtensions {

    internal static ProblemDetails ToUndetailedError(this AppException appException) =>
        new() {
            Status = appException.GetHttpStatusCode(),
            Title = appException.Message,
        };

    internal static ProblemDetails ToValidationError(this AppException appException) {
        ProblemDetails problemDetails = new() {
            Status = appException.GetHttpStatusCode(),
            Title = appException.Message,
        };

        problemDetails.Extensions["Errors"] = appException.GetFieldValidationErrors();
        return problemDetails;
    }

    internal static IEnumerable<FieldValidationError> GetFieldValidationErrors(this AppException appException) {
        foreach(DictionaryEntry error in appException.Data) {
            if(error.Key.ToString() is not string field
               || error.Key.ToString() is not string errorMessage) {
                continue;
            }
            yield return new FieldValidationError(field, errorMessage);
        }
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

    internal static IApplicationBuilder UseErrorHandling(this IApplicationBuilder applicationBuilder) {
        applicationBuilder.UseExceptionHandler(o => { });

        return applicationBuilder;
    }

    internal static IServiceCollection AddExceptionHandling(this IServiceCollection services) {
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();
        return services;
    }
}
