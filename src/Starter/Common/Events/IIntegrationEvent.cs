namespace Starter.Common.Events;

using MediatR;

internal interface IIntegrationEvent : INotification
{
    Guid Id { get; }
    DateTimeOffset OccurredDateTime { get; }
}