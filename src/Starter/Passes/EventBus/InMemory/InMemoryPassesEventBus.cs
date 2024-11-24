using Starter.Common.Events.EventBus.InMemory;
using Starter.Common.Events.Publisher;

namespace Starter.Passes.EventBus.InMemory;

internal sealed class InMemoryPassesEventBus(IPublisher eventPublisher) : 
    InMemoryEventBus(eventPublisher), IPassesEventBus
{
    
}