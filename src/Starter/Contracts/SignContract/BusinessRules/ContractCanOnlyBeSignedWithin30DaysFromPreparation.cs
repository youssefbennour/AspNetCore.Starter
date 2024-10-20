namespace Starter.Contracts.SignContract.BusinessRules;

internal sealed class ContractCanOnlyBeSignedWithin30DaysFromPreparation : IBusinessRule
{
    private readonly DateTimeOffset preparedAt;
    private readonly DateTimeOffset signedAt;

    internal ContractCanOnlyBeSignedWithin30DaysFromPreparation(DateTimeOffset preparedAt,
        DateTimeOffset signedAt)
    {
        this.preparedAt = preparedAt;
        this.signedAt = signedAt;
    }

    public bool IsMet()
    {
        var timeDifference = signedAt.Date - preparedAt.Date;

        return timeDifference <= TimeSpan.FromDays(30);
    }

    public string ErrorKey => nameof(signedAt);

    public string Error =>
        "Contract can not be signed because more than 30 days have passed from the contract preparation";
}
