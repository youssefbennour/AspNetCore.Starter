using FluentValidation;

namespace Starter.Contracts.PrepareContract;

internal sealed class PrepareContractRequestValidator : AbstractValidator<PrepareContractRequest>
{
    public PrepareContractRequestValidator()
    {
        RuleFor(request => request.CustomerId).NotEmpty();
        RuleFor(request => request.CustomerAge).GreaterThanOrEqualTo(0);
        RuleFor(request => request.CustomerHeight).GreaterThanOrEqualTo(0);
        RuleFor(request => request.PreparedAt).NotEmpty();
    }
}