using Starter.Passes.Data.Database;
using Starter.Passes.EventBus;
using Starter.Passes.MarkPassAsExpired.Events;

namespace Starter.Passes.MarkPassAsExpired;

internal static class MarkPassAsExpiredEndpoint
{
    internal static void MapMarkPassAsExpired(this IEndpointRouteBuilder app) => app.MapPatch(
            PassesApiPaths.MarkPassAsExpired,
            async (
                Guid id,
                PassesPersistence persistence,
                TimeProvider timeProvider,
                IPassesEventBus eventBus,
                CancellationToken cancellationToken) =>
            {
                var pass = await persistence.Passes.FindAsync([id], cancellationToken: cancellationToken);
                if (pass is null)
                {
                    throw new NotFoundException();
                }

                var nowDate = timeProvider.GetUtcNow();
                pass.MarkAsExpired(nowDate);
                await eventBus.PublishAsync(
                    PassExpiredEvent.Create(pass.Id, pass.CustomerId, timeProvider.GetUtcNow()),
                    cancellationToken);
                await persistence.SaveChangesAsync(cancellationToken);


                return Results.Ok();
            })
        .WithOpenApi(operation => new(operation)
        {
            Summary = "Marks pass which expired",
            Description =
                "This endpoint is used to mark expired pass. Based on that it is possible to offer new contract to customer.",
        })
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status500InternalServerError);
}
