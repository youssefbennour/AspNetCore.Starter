using Starter.Common.DataAccess.Orms.EfCore.DbContexts;
using Starter.Common.Events.EventBus.Persistent;
using Starter.Passes.Data.Database;

namespace Starter.Passes.EventBus.Persistent;

internal sealed class PersistentPassesEventBus(PassesPersistence persistence) 
    :  PersistentEventBus(persistence), IPassesEventBus
{
    
}