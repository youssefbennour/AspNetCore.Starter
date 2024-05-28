using Starter.Common.Events;

namespace Starter.Passes.RegisterPass.Events;

internal record PassRegisteredEvent(Guid Id, Guid PassId, DateTimeOffset OccurredDateTime) : IIntegrationEvent
{
    internal static PassRegisteredEvent Create(Guid passId) =>
        new(Guid.NewGuid(), passId, DateTimeOffset.UtcNow);
}
