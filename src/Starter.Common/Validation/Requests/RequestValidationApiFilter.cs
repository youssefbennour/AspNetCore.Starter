using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Softylines.Contably.Common.ErrorHandling;

namespace Softylines.Contably.Common.Validation.Requests;

using FluentValidation;

public sealed class RequestValidationApiFilter<TRequestToValidate> : IEndpointFilter where TRequestToValidate : class
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var requestToValidate = context.Arguments.FirstOrDefault(argument => argument?.GetType() == typeof(TRequestToValidate)) as TRequestToValidate;
        var validator = context.HttpContext.RequestServices.GetService<IValidator<TRequestToValidate>>();

        if (validator is null)
        {
            return await next.Invoke(context);
        }

        var validationResult = await validator.ValidateAsync(requestToValidate!);
        if (validationResult.IsValid)
        {
            return await next.Invoke(context);
        }
        
        var problemDetails = validationResult.ToDictionary()
            .ToProblemDetails();

        var serializerOptions = new JsonSerializerOptions {
            WriteIndented = true,
            IncludeFields = true,
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };
        
        return Results.Json(
            data: problemDetails,
            options: serializerOptions,
            statusCode: StatusCodes.Status422UnprocessableEntity);
    }
}