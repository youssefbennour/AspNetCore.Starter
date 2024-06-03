using Starter.Contracts.Data;

namespace Starter.Contracts.SignContract;

internal sealed record SignContractRequest(DateTimeOffset SignedAt)
{
    internal static SignContractRequest From(Contract contract)
    {
        return new SignContractRequest(contract.SignedAt ?? new DateTimeOffset());
    }
}