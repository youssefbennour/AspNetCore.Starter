using Starter.Contracts.GetAllContracts;
using Starter.Contracts.PrepareContract;
using Starter.Contracts.SignContract;

namespace Starter.Contracts;

internal static class ContractsEndpoints
{
    internal static void MapContracts(this IEndpointRouteBuilder app)
    {
        app.MapPrepareContract();
        app.MapSignContract();
        app.MapGetAllContracts();
    }
}
