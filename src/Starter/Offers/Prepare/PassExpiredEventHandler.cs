using Starter.Common.Events;
using Starter.Offers.Data;
using Starter.Offers.Data.Database;
using Starter.Offers.EventBus;
using Starter.Passes.MarkPassAsExpired.Events;

namespace Starter.Offers.Prepare;

internal sealed class PassExpiredEventHandler(
    IOffersEventBus eventBus,
    OffersPersistence persistence,
    TimeProvider timeProvider) : IIntegrationEventHandler<PassExpiredEvent>
{
    public async Task Handle(PassExpiredEvent @event, CancellationToken cancellationToken)
    {
        var nowDate = timeProvider.GetUtcNow();
        var offer = Offer.PrepareStandardPassExtension(@event.CustomerId, nowDate);
        persistence.Offers.Add(offer);
        
        var offerPreparedEvent = OfferPrepareEvent.Create(offer.Id, offer.CustomerId, timeProvider.GetUtcNow());
        await eventBus.PublishAsync(offerPreparedEvent, cancellationToken);
        
        await persistence.SaveChangesAsync(cancellationToken);
    }
}
