using Starter.Common.Requests.Models;
using Starter.Contracts.Data.Database;

namespace Starter.Contracts.GetAllContracts;

internal static class GetAllContractsEndpoints
{
    internal static void MapGetAllContracts(this IEndpointRouteBuilder app) =>
        app.MapGet(
            ContractsApiPaths.GetAll,
            async (
                [AsParameters]QueryParameters queryParameters, 
                [FromServices]ContractsPersistence persistence, 
                CancellationToken cancellationToken) =>
            {
                var contractsResponse = await persistence.Contracts
                    .AsNoTracking()
                    .Select(contracts => ContractResponse.From(contracts))
                    .ToPaginatedListAsync(queryParameters, cancellationToken);

                return Results.Ok(contractsResponse);
            }).WithOpenApi(operation => new(operation)
            {
                Summary = "Returns all passes that exist in the system",
                Description =
                    "This endpoint is used to retrieve all existing passes.",
            })
            .Produces<PaginatedList<ContractResponse>>()
            .Produces(StatusCodes.Status500InternalServerError);
}