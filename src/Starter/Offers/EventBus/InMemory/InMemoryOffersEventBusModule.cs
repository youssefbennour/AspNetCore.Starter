namespace Starter.Offers.EventBus.InMemory;

internal static class InMemoryOffersEventBusModule
{
   public static void AddInMemoryOffersEventBus(this IServiceCollection services)
   {
      services.AddScoped<IOffersEventBus, InMemoryOffersEventBus>();
   }
}