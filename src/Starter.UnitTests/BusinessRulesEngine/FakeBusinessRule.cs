using Starter.Common.BusinessRulesEngine;

namespace Starter.UnitTests.BusinessRulesEngine;

internal sealed class FakeBusinessRule : IBusinessRule    
{
    private readonly int someNumber;

    internal FakeBusinessRule(int someNumber) =>
        this.someNumber = someNumber;

    public bool IsMet() => someNumber > 10;
    public string ErrorKey => nameof(someNumber);

    public string Error => "Fake business rule was not met";
}