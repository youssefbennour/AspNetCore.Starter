namespace Starter.IntegrationTests.Common.TestEngine.IntegrationEvents.Handlers;

using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Starter.Common.Events;

internal sealed class IntegrationEventHandlerScope<TIntegrationEvent> : IDisposable
where TIntegrationEvent : IIntegrationEvent
{
    private readonly IServiceScope _serviceScope;
    internal readonly IIntegrationEventHandler<TIntegrationEvent> IntegrationEventHandler;

    public IntegrationEventHandlerScope(WebApplicationFactory<Program> applicationInMemoryFactory)
    {
        _serviceScope = applicationInMemoryFactory.Services.CreateScope();
        IntegrationEventHandler = (IIntegrationEventHandler<TIntegrationEvent>)_serviceScope
            .ServiceProvider
            .GetRequiredService<INotificationHandler<TIntegrationEvent>>();
    }

    public async Task Consume(TIntegrationEvent @event, CancellationToken cancellationToken = default) =>
        await IntegrationEventHandler.Handle(@event, cancellationToken);

    public void Dispose() =>
        _serviceScope.Dispose();
}
