using Starter.Passes.Data;

namespace Starter.Passes.GetAllPasses;

internal record PassDto(Guid Id, Guid CustomerId)
{
    internal static PassDto From(Pass contract) => new(contract.Id, contract.CustomerId);
}