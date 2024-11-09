using Starter.Common.DataAccess.Orms.EfCore.DbContexts;
using Starter.Common.Events.EventBus.Persistent;
using Starter.Contracts.EventBus;

namespace Starter.Passes.EventBus.Persistent;

internal sealed class PersistentPassesEventBus(OutboxPersistence persistence) 
    :  PersistentEventBus(persistence), IPassesEventBus
{
    
}