namespace Starter.Contracts.PrepareContract.BusinessRules;

internal sealed class PreviousContractHasToBeSignedRule : IBusinessRule
{
    private readonly bool? signed;

    internal PreviousContractHasToBeSignedRule(bool? signed) => this.signed = signed;
    public bool IsMet() => signed is true or null;
    public string ErrorKey => nameof(signed);
    public string Error => "Previous contract must be signed by the customer";
}
