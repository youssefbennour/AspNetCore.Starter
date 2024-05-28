namespace Starter.Passes.RegisterPass;

public record RegisterPassRequest(Guid CustomerId, DateTimeOffset From, DateTimeOffset To);