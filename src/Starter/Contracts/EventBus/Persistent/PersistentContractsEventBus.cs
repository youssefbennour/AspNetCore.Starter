using Starter.Common.DataAccess.Orms.EfCore.DbContexts;
using Starter.Common.Events.EventBus.Persistent;

namespace Starter.Contracts.EventBus.Persistent;

internal sealed class PersistentContractsEventBus(OutboxPersistence persistence) 
    :  PersistentEventBus(persistence), IContractsEventBus
{
    
}