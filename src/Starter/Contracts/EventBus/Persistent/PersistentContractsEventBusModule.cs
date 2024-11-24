namespace Starter.Contracts.EventBus.Persistent;

internal static class PersistentContractsEventBusModule
{
   public static void AddPersistentContractsEventBus(this IServiceCollection services)
   {
      services.AddScoped<IContractsEventBus, PersistentContractsEventBus>();
      services.AddHostedService<ContractsOutboxWorker>();
   }
}