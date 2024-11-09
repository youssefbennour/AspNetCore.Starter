using Starter.Common.Events.EventBus.InMemory;
using Starter.Common.Events.Publisher;

namespace Starter.Contracts.EventBus.InMemory;

internal sealed class InMemoryContractsEventBus(IPublisher eventPublisher) : 
    InMemoryEventBus(eventPublisher), IContractsEventBus
{
    
}