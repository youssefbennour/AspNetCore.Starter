using FluentValidation;

namespace Microsoft.Extensions.DependencyInjection;

internal static class RequestValidationsExtensions
{
    internal static IServiceCollection AddRequestsValidations<T>(this IServiceCollection services) where T : class =>
        services.AddValidatorsFromAssemblyContaining<T>(includeInternalTypes: true);
}
