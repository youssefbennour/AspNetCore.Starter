using Microsoft.Extensions.DependencyInjection;

namespace Softylines.Contably.Common.Clocks;

public static class ClockModule
{
    public static IServiceCollection AddClock(this IServiceCollection services) =>
        services.AddSingleton(TimeProvider.System);
}
