using Starter.Common.BusinessRulesEngine;
using Starter.Common.ErrorHandling.Exceptions;
using Starter.Contracts.SignContract.BusinessRules;

namespace Starter.UnitTests.Contracts.SignContract.BusinessRules;

public sealed class ContractCanOnlyBeSignedWithin30DaysFromPreparationTests
{
    [Fact]
    internal void Given_signed_at_date_which_is_more_than_30_days_from_prepared_at_date_Then_validation_should_throw()
    {
        // Arrange

        // Act
        var act = () =>
            BusinessRuleValidator.Validate(
                new ContractCanOnlyBeSignedWithin30DaysFromPreparation(DateTimeOffset.Now,
                    DateTimeOffset.Now.AddDays(31)));

        // Assert
        act.Should().Throw<BusinessRuleValidationException>();
    }

    [Fact]
    internal void Given_signed_at_date_which_is_30_days_from_prepared_at_date_Then_validation_should_pass()
    {
        // Arrange

        // Act
        var act = () =>
            BusinessRuleValidator.Validate(
                new ContractCanOnlyBeSignedWithin30DaysFromPreparation(DateTimeOffset.Now,
                    DateTimeOffset.Now.AddDays(30)));

        // Assert
        act.Should().NotThrow<BusinessRuleValidationException>();
    }

    [Fact]
    internal void Given_signed_at_date_which_is_less_than_30_days_from_prepared_at_date_Then_validation_should_pass()
    {
        // Arrange

        // Act
        var act = () =>
            BusinessRuleValidator.Validate(
                new ContractCanOnlyBeSignedWithin30DaysFromPreparation(DateTimeOffset.Now,
                    DateTimeOffset.Now.AddDays(29)));

        // Assert
        act.Should().NotThrow<BusinessRuleValidationException>();
    }
}
