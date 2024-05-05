    
namespace Microsoft.Extensions.DependencyInjection
{
    internal static class TelemetryModule
    {
        internal static IServiceCollection AddTelemetry(this IServiceCollection services){
            return services.AddOpenTelemetryModule();
        }
    }
}
