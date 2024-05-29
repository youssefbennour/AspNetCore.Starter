using Starter.Contracts.Data;

namespace Starter.Contracts.GetAllContracts;

internal record ContractResponse(Guid id, Guid customerId, DateTimeOffset PreparedAt, TimeSpan Duration)
{
   internal static ContractResponse From(Contract contract) =>
      new(contract.Id, contract.CustomerId, contract.PreparedAt, contract.Duration);
}