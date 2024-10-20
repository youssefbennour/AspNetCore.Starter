using MediatR;

namespace Starter.Common.Events;

internal interface IIntegrationEvent : INotification
{
    Guid Id { get; }
    DateTimeOffset OccurredDateTime { get; }
}