using Starter.Common.BusinessRuleEngine;

namespace Starter.Contracts.PrepareContract.BusinessRules;

internal sealed class ContractCanBePreparedOnlyForAdultRule : IBusinessRule
{
    private readonly int age;

    internal ContractCanBePreparedOnlyForAdultRule(int age) => this.age = age;

    public string ErrorKey => nameof(age);
    public bool IsMet() => age >= 18;

    public string Error => "Contract can not be prepared for a person who is not adult";
}
