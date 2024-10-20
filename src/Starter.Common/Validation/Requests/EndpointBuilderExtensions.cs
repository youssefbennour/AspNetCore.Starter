using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Starter.Common.Validation.Requests;

internal static class EndpointBuilderExtensions
{
    internal static RouteHandlerBuilder ValidateRequest<TRequest>(this RouteHandlerBuilder builder) where TRequest : class =>
        builder.AddEndpointFilter<RequestValidationApiFilter<TRequest>>();
}
