using Starter.Common.Events.EventBus.InMemory;
using Starter.Common.Events.Publisher;

namespace Starter.Offers.EventBus.InMemory;

internal sealed class InMemoryOffersEventBus(IPublisher eventPublisher) : 
    InMemoryEventBus(eventPublisher), IOffersEventBus
{
    
}