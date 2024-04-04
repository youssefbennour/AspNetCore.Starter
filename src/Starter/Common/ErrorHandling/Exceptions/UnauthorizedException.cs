using Starter.Common.ErrorHandling.Exceptions.Abstractions;

namespace Starter.Common.Validation.Requests.Exceptions;

internal class UnauthorizedException : AppException
{
    internal UnauthorizedException(string message) : base(message) { }

    internal UnauthorizedException(IEnumerable<string> messages) : base(messages) { }
}
