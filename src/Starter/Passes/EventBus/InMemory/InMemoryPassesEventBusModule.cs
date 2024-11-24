namespace Starter.Passes.EventBus.InMemory;

internal static class InMemoryPassesEventBusModule
{
   public static void AddInMemoryPassesEventBus(this IServiceCollection services)
   {
      services.AddScoped<IPassesEventBus, InMemoryPassesEventBus>();
   }
}