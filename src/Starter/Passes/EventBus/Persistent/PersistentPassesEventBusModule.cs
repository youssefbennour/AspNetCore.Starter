namespace Starter.Passes.EventBus.Persistent;

internal static class PersistentPassesEventBusModule
{
   public static void AddPersistentPassesEventBus(this IServiceCollection services)
   {
      services.AddScoped<IPassesEventBus, PersistentPassesEventBus>();
      services.AddHostedService<PassesOutboxWorker>();
   }
}