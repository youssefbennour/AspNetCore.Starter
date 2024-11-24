namespace Starter.Offers.EventBus.Persistent;

internal static class PersistentOffersEventBusModule
{
   public static void AddPersistentOffersEventBus(this IServiceCollection services)
   {
      services.AddScoped<IOffersEventBus, PersistentOffersEventBus>();
      services.AddHostedService<OffersOutboxWorker>();
   }
}