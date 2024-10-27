namespace Softylines.Contably.Common.Events;

using MediatR;

public interface IIntegrationEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IIntegrationEvent;
