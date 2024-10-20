namespace Starter.Passes.MarkPassAsExpired.Events;

internal record PassExpiredEvent(Guid Id, Guid PassId, Guid CustomerId, DateTimeOffset OccurredDateTime) : IIntegrationEvent
{
    internal static PassExpiredEvent Create(Guid passId, Guid customerId, DateTimeOffset occurredAt) =>
        new(Guid.NewGuid(), passId, customerId, occurredAt);
}
