namespace Starter.Common.DataAccess.Auditing;

public interface ISoftDeletable
{
    public DateTimeOffset? DeletedAt { get; }
}