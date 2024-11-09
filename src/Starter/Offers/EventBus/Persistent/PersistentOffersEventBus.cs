using Starter.Common.DataAccess.Orms.EfCore.DbContexts;
using Starter.Common.Events.EventBus.Persistent;
using Starter.Passes.EventBus;

namespace Starter.Offers.EventBus.Persistent;

internal sealed class PersistentOffersEventBus(OutboxPersistence persistence) 
    :  PersistentEventBus(persistence), IOffersEventBus
{
    
}