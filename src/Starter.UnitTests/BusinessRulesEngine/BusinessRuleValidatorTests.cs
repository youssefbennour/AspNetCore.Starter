using FluentAssertions;
using Starter.Common.BusinessRuleEngine;
using Starter.Common.BusinessRulesEngine;
using Starter.Common.ErrorHandling.Exceptions;

namespace Starter.UnitTests.BusinessRulesEngine;

public sealed class BusinessRuleValidatorTests
{
    [Fact]
    internal void Given_concrete_business_rule_which_is_met_Then_validation_should_pass()
    {
        // Arrange

        // Act
        var act = () => BusinessRuleValidator.Validate(new FakeBusinessRule(20));

        // Assert
        act.Should().NotThrow<BusinessRuleValidationException>();
    }

    [Fact]
    internal void Given_concrete_business_rule_which_is_not_met_Then_validation_should_throw()
    {
        // Arrange

        // Act
        var act = () => BusinessRuleValidator.Validate(new FakeBusinessRule(1));

        // Assert
        act.Should().Throw<BusinessRuleValidationException>();
    }
}
