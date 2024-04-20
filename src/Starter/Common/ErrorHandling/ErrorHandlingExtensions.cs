using Microsoft.AspNetCore.Mvc;
using Starter.Common.BusinessRuleEngine;
using Starter.Common.ErrorHandling.ErrorModels;
using Starter.Common.ErrorHandling.Exceptions.Abstractions;
using Starter.Common.Validation.Requests.Exceptions;
using System.Collections;

namespace Starter.Common.ErrorHandling;

internal static class ErrorHandlingExtensions {

    internal static ProblemDetails ToProblemDetails(this Exception exception) {
        ProblemDetails problemDetails = new() {
            Status = exception.GetHttpStatusCode(),
            Title = exception.Message,
        };

        if(exception is BusinessRuleValidationException businessRuleValidationException) {
            problemDetails.Extensions.Add("Errors", businessRuleValidationException.GetFieldValidationErrors());
        }

        return problemDetails;
    }

    internal static List<FieldValidationError> GetFieldValidationErrors(this AppException appException) {
        List<FieldValidationError> fieldValidationErrors = new();

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
