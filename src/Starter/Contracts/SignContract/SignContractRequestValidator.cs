using FluentValidation;

namespace Starter.Contracts.SignContract;

internal sealed class SignContractRequestValidator : AbstractValidator<SignContractRequest>
{
    public SignContractRequestValidator() => RuleFor(signContractRequest => signContractRequest.SignedAt)
            .NotEmpty();
}
