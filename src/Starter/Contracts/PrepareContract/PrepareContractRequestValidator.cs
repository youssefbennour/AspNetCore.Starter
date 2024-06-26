using FluentValidation;

namespace Starter.Contracts.PrepareContract;

internal sealed class PrepareContractRequestValidator : AbstractValidator<PrepareContractRequest>
{
    public PrepareContractRequestValidator()
    {
        RuleFor(request => request.CustomerId).NotEmpty();
        RuleFor(request => request.CustomerAge).GreaterThan(0);
        RuleFor(request => request.CustomerHeight).GreaterThan(0);
        RuleFor(request => request.PreparedAt).NotEmpty();
    }
}