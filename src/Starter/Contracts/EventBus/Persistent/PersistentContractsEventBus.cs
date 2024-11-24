using Starter.Common.DataAccess.Orms.EfCore.DbContexts;
using Starter.Common.Events.EventBus.Persistent;
using Starter.Contracts.Data.Database;

namespace Starter.Contracts.EventBus.Persistent;

internal sealed class PersistentContractsEventBus(ContractsPersistence persistence) 
    :  PersistentEventBus(persistence), IContractsEventBus
{
    
}