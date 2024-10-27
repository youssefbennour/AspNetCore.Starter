using System.Collections;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using NetTopologySuite.Utilities;
using Softylines.Contably.Common.ErrorHandling.ErrorModels;
using Softylines.Contably.Common.ErrorHandling.Exceptions.Abstractions;

namespace Softylines.Contably.Common.ErrorHandling;

public static class ErrorHandlingExtensions 
{
    private const string ServerError = "Server Error";

    public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder applicationBuilder) {
        applicationBuilder.UseExceptionHandler(_ => { });
        return applicationBuilder;
    }

    public static IServiceCollection AddExceptionHandling(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IExceptionHandler>(new GlobalExceptionHandler(builder.Environment));
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddProblemDetails();
        return builder.Services;
    }

    public static AppProblemDetails ToProblemDetails(this Exception exception) {
        AppProblemDetails problemDetails = new(message: exception.GetErrorMessage());

        if(exception is BusinessRuleValidationException businessRuleValidationException) {
            problemDetails.Errors = businessRuleValidationException.GetFieldValidationErrors();
        }

        return problemDetails;
    }
    
    public static AppProblemDetails ToProblemDetails(this IDictionary<string, string[]> errors)
    {
        return new AppProblemDetails(errors.GetFieldValidationErrors());
    }
    private static string GetErrorMessage(this Exception exception)
    {
        return exception is InternalServerException or not IAppException ? ServerError : exception.Message;
    }


    public static BusinessRuleValidationException ToBusinessRuleValidationException(
        this IDictionary<string, string[]> errorsPerField)
    {
        var exception = new BusinessRuleValidationException();
        if (errorsPerField.IsNullOrEmpty())
        {
            return exception;
        }

        foreach (var fieldErrors in errorsPerField)
        {
            foreach (var error in fieldErrors.Value)
            {
                exception.UpsertToException( fieldErrors.Key,  error);
            }
        }

        return exception;
    }
    
    private static List<FieldValidationError> GetFieldValidationErrors(this Exception exception)
    {
        List<FieldValidationError> fieldValidationErrors = [];

        foreach (DictionaryEntry error in exception.Data)
        {
            if (error.Key.ToString() is not { } field
                || error.Value?.ToString() is not { } errorMessage)
            {
                continue;
            }

            fieldValidationErrors.Add(new FieldValidationError(field, errorMessage));
        }

        return fieldValidationErrors;
    }

    private static List<FieldValidationError> GetFieldValidationErrors(this IDictionary<string, string[]> errors) {
        
        return errors.SelectMany(m => 
                m.Value.Select(v => new FieldValidationError(m.Key, v)))
            .ToList();
    }
    
    public static int GetHttpStatusCode(this Exception exception) {

        if(exception is not IAppException appException) {
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
