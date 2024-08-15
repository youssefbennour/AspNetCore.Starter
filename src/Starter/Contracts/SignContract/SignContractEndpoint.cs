using Starter.Common.Events.EventBus;
using Starter.Common.Validation.Requests;
using Starter.Contracts.Data.Database;
using Starter.Contracts.SignContract.Events;
using NotFoundException = Starter.Common.ErrorHandling.Exceptions.NotFoundException;

namespace Starter.Contracts.SignContract;

internal static class SignContractEndpoint
{
    internal static void MapSignContract(this IEndpointRouteBuilder app) => app.MapPatch(ContractsApiPaths.Sign,
            async (Guid id, SignContractRequest request,
                ContractsPersistence persistence,
                IEventBus bus,
                TimeProvider timeProvider,
                CancellationToken cancellationToken) =>
            {
                var contract =
                    await persistence.Contracts.FindAsync([id], cancellationToken: cancellationToken);

                if (contract is null)
                {
                    throw new NotFoundException();
                }

                var dateNow = timeProvider.GetUtcNow();
                contract.Sign(request.SignedAt, dateNow);
                await persistence.SaveChangesAsync(cancellationToken);

                var @event = ContractSignedEvent.Create(
                    contract.Id,
                    contract.CustomerId,
                    contract.SignedAt!.Value,
                    contract.ExpiringAt!.Value,
                    timeProvider.GetUtcNow());
                await bus.PublishAsync(@event, cancellationToken);

                return Results.Ok();
            })
        .ValidateRequest<SignContractRequest>()
        .WithOpenApi(operation => new(operation)
        {
            Summary = "Signs prepared contract",
            Description = "This endpoint is used to sign prepared contract by customer."
        })
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status422UnprocessableEntity)
        .Produces(StatusCodes.Status500InternalServerError);
}
