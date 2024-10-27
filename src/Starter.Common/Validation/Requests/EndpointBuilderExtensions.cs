using Microsoft.AspNetCore.Http;

namespace Softylines.Contably.Common.Validation.Requests;

public static class EndpointBuilderExtensions
{
    public static RouteHandlerBuilder ValidateRequest<TRequest>(this RouteHandlerBuilder builder) where TRequest : class =>
        builder.AddEndpointFilter<RequestValidationApiFilter<TRequest>>();
}
