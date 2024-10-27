using Newtonsoft.Json;
using Softylines.Contably.Common.Events;

namespace Softylines.Contably.Common.EventualConsistency.Outbox;

public sealed class OutboxMessage
{
    public Guid Id { get; private set; }
    public string Message { get; private set; }
    public DateTimeOffset SavedOn { get; private set; }
    public DateTimeOffset? ExecutedOn { get; private set; }
    public string Type { get; private set; }

    
    public void MarkAsExecuted(DateTimeOffset executedOn)
    {
        ExecutedOn = executedOn;
    }
    private OutboxMessage(string message, DateTimeOffset savedOn, string type)
    {
        Id = Guid.NewGuid();
        Message = message;
        Type = type;
        SavedOn = savedOn;
    }

    public static OutboxMessage CreateFrom(IIntegrationEvent @event)
    {
        var serializedMessage = JsonConvert.SerializeObject(@event);
        var type = @event.GetType().ToString();

        return new OutboxMessage
        (
            message: serializedMessage,
            savedOn: @event.OccurredDateTime,
            type: type
        );
    } 
}