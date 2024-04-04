using Starter.Common.ErrorHandling.Exceptions.Abstractions;

namespace Starter.Common.Validation.Requests.Exceptions;

internal class NotFoundException : AppException
{
    internal NotFoundException(string message) : base(message){}

    internal NotFoundException(IEnumerable<string> messages) : base(messages){}
}
