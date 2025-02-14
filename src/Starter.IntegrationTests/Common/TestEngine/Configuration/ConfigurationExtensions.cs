using Starter.Common.Events.EventBus;
using Starter.Common.Events.EventBus.InMemory;
using Starter.Contracts.EventBus;
using Starter.IntegrationTests.Common.Events.EventBus.InMemory;
using Starter.Offers.EventBus;
using Starter.Passes.EventBus;

namespace Starter.IntegrationTests.Common.TestEngine.Configuration;

using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Time.Testing;

internal static class ConfigurationExtensions
{
    internal static WebApplicationFactory<T> WithContainerDatabaseConfigured<T>(
        this WebApplicationFactory<T> webApplicationFactory, 
        string connectionString)
        where T : class
    {
        var connectionStringsConfiguration = new Dictionary<string, string?>
        {
            {ConfigurationKeys.PassesConnectionString, connectionString},
            {ConfigurationKeys.ContractsConnectionString, connectionString},
            {ConfigurationKeys.OffersConnectionString, connectionString},
            {ConfigurationKeys.ReportsConnectionString, connectionString},
        };

        return webApplicationFactory.UseSettings(connectionStringsConfiguration);
    }

    private static WebApplicationFactory<T> UseSettings<T>(
        this WebApplicationFactory<T> webApplicationFactory,
        Dictionary<string, string?> settings)
        where T : class =>
        webApplicationFactory.WithWebHostBuilder(webHostBuilder =>
        {
            foreach (var setting in settings)
            {
                webHostBuilder.UseSetting(setting.Key, setting.Value);
            }
        });

    internal static WebApplicationFactory<T> SetFakeSystemClock<T>(
        this WebApplicationFactory<T> webApplicationFactory, 
        DateTimeOffset fakeDateTimeOffset)
        where T : class =>
        webApplicationFactory.WithWebHostBuilder(webHostBuilder => webHostBuilder.ConfigureTestServices(services =>
            services.AddSingleton<TimeProvider>(new FakeTimeProvider(fakeDateTimeOffset))));

    internal static WebApplicationFactory<T> WithFakePassesEventBus<T>(
        this WebApplicationFactory<T> webApplicationFactory,
        IPassesEventBus eventBusMock)
        where T : class=>
        webApplicationFactory.WithWebHostBuilder(webHostBuilder => webHostBuilder.ConfigureTestServices(services =>
            services.AddSingleton<IPassesEventBus>(eventBusMock)));
    internal static WebApplicationFactory<T> WithFakeOffersEventBus<T>(
        this WebApplicationFactory<T> webApplicationFactory,
        IOffersEventBus eventBusMock)
        where T : class=>
        webApplicationFactory.WithWebHostBuilder(webHostBuilder => webHostBuilder.ConfigureTestServices(services =>
            services.AddSingleton<IOffersEventBus>(eventBusMock)));
    internal static WebApplicationFactory<T> WithFakeContractsEventBus<T>(
        this WebApplicationFactory<T> webApplicationFactory,
        IContractsEventBus eventBusMock)
        where T : class=>
        webApplicationFactory.WithWebHostBuilder(webHostBuilder => webHostBuilder.ConfigureTestServices(services =>
            services.AddSingleton<IContractsEventBus>(eventBusMock)));

    internal static WebApplicationFactory<T> WithFakeConsumers<T>(
        this WebApplicationFactory<T> webApplicationFactory)
        where T : class =>
        webApplicationFactory.WithWebHostBuilder(webHostBuilder => webHostBuilder.ConfigureTestServices(services =>
        {
            services.AddSingleton<IEventBus, InMemoryEventBus>();
            services.AddMediatR(configuration =>
                configuration.RegisterServicesFromAssembly(typeof(FakeEvent).Assembly));
        }));
}
