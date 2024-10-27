using Starter.Common.BusinessRulesEngine;
using Starter.Contracts.PrepareContract.BusinessRules;
using Starter.Contracts.SignContract.BusinessRules;

namespace Starter.Contracts.Data;

internal sealed class Contract
{
    private static TimeSpan StandardDuration => TimeSpan.FromDays(365);

    public Guid Id { get; init; }

    public Guid CustomerId { get; init; }

    public DateTimeOffset PreparedAt { get; init; }
    public TimeSpan Duration { get; init; }

    public DateTimeOffset? SignedAt { get; private set; }
    public DateTimeOffset? ExpiringAt { get; private set; }

    public bool Signed => SignedAt.HasValue;

    private Contract(Guid id,
        Guid customerId,
        DateTimeOffset preparedAt,
        TimeSpan duration)
    {
        Id = id;
        CustomerId = customerId;
        PreparedAt = preparedAt;
        Duration = duration;
    }

    internal static Contract Prepare(Guid customerId, int customerAge, int customerHeight, DateTimeOffset preparedAt, bool? isPreviousContractSigned = null)
    {
        BusinessRuleValidator.Validate(new ContractCanBePreparedOnlyForAdultRule(customerAge), 
            new CustomerMustBeSmallerThanMaximumHeightLimitRule(customerHeight), 
            new PreviousContractHasToBeSignedRule(isPreviousContractSigned));

        return new(Guid.NewGuid(),
            customerId,
            preparedAt,
            StandardDuration);
    }

    internal void Sign(DateTimeOffset signedAt, DateTimeOffset dateNow)
    {
        BusinessRuleValidator.Validate(
            new ContractCanOnlyBeSignedWithin30DaysFromPreparation(PreparedAt, signedAt));

        SignedAt = signedAt;
        ExpiringAt = dateNow.Add(Duration);
    }
}
