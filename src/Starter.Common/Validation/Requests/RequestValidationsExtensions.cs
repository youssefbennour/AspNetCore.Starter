using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Softylines.Contably.Common.Validation.Requests;

public static class RequestValidationsExtensions
{
    public static IServiceCollection AddRequestsValidations<T>(this IServiceCollection services) where T : class =>
        services.AddValidatorsFromAssemblyContaining<T>(includeInternalTypes: true);

    public static IRuleBuilderOptions<T, string> MustBeNumeric<T>(this IRuleBuilder<T, string> ruleBuilder, string message)
    {
        return ruleBuilder.Must(number => int.TryParse(number, out _))
                          .WithMessage(message);
    }

    public static IRuleBuilderOptions<T, string> MustBeGreaterThanOrEquals<T>(this IRuleBuilder<T, string> ruleBuilder, int value, string message)
    {
        return ruleBuilder.Must(number => int.TryParse(number, out int result) && result >= value)
                          .WithMessage(message);
    }

    public static IRuleBuilderOptions<T, string> NumberMustBeInRange<T>(this IRuleBuilder<T, string> ruleBuilder, string minValue, string maxValue, string? message = null)
    {
        return ruleBuilder.Must(@string => string.Compare(@string, minValue) >= 0
            && (string.Compare(@string, maxValue) <= 0 
                || @string.StartsWith(maxValue))
            )
                          .WithMessage(message);
    }
}
