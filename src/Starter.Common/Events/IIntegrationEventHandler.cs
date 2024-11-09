using MediatR;

namespace Starter.Common.Events;

public interface IIntegrationEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IIntegrationEvent;
