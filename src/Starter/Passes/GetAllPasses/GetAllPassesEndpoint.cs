using Starter.Common.Requests.Models;
using Starter.Passes.Data.Database;

namespace Starter.Passes.GetAllPasses;

internal static class GetAllPassesEndpoint
{
    internal static void MapGetAllPasses(this IEndpointRouteBuilder app) =>
        app.MapGet(
                PassesApiPaths.GetAll, 
                async (
                    [FromServices]PassesPersistence persistence, 
                    CancellationToken cancellationToken,
                    [AsParameters]QueryParameters queryParameters) =>
            {
                var passes= await persistence.Passes
                    .AsNoTracking()
                    .Select(passes => PassDto.From(passes))
                    .ToPaginatedListAsync(queryParameters, cancellationToken);


                return Results.Ok(passes);
            })
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Returns all passes that exist in the system",
                Description =
                    "This endpoint is used to retrieve all existing passes.",
            })
            .Produces<PaginatedList<PassDto>>()
            .Produces(StatusCodes.Status500InternalServerError);
}