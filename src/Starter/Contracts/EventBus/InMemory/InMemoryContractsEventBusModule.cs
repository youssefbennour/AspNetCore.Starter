namespace Starter.Contracts.EventBus.InMemory;

internal static class InMemoryContractsEventBusModule
{
   public static void AddInMemoryContractsEventBus(this IServiceCollection services)
   {
      services.AddScoped<IContractsEventBus, InMemoryContractsEventBus>();
   }
}