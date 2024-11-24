using Starter.Common.DataAccess.Orms.EfCore.DbContexts;
using Starter.Common.Events.EventBus.Persistent;
using Starter.Offers.Data.Database;

namespace Starter.Offers.EventBus.Persistent;

internal sealed class PersistentOffersEventBus(OffersPersistence persistence) 
    :  PersistentEventBus(persistence), IOffersEventBus
    {
    
}