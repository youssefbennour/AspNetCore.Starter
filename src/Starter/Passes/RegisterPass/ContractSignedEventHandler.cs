using Starter.Common.Events;
using Starter.Common.Events.EventBus;
using Starter.Contracts.SignContract.Events;
using Starter.Passes.Data;
using Starter.Passes.Data.Database;
using Starter.Passes.EventBus;
using Starter.Passes.RegisterPass.Events;

namespace Starter.Passes.RegisterPass;

internal sealed class ContractSignedEventHandler(
    PassesPersistence persistence,
    IPassesEventBus eventBus) : IIntegrationEventHandler<ContractSignedEvent>
{
    public async Task Handle(ContractSignedEvent @event, CancellationToken cancellationToken)
    {
        var pass = Pass.Register(@event.ContractCustomerId, @event.SignedAt, @event.ExpireAt);
        await persistence.Passes.AddAsync(pass, cancellationToken);
        await persistence.SaveChangesAsync(cancellationToken);

        var passRegisteredEvent = PassRegisteredEvent.Create(pass.Id);
        await eventBus.PublishAsync(passRegisteredEvent, cancellationToken);
    }
}
