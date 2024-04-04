using Starter.Common.ErrorHandling.Exceptions.Abstractions;

namespace Starter.Common.Validation.Requests.Exceptions;

internal class BadRequestException : AppException
{
    internal BadRequestException(string message) : base(message) { }

    internal BadRequestException(IEnumerable<string> messages) : base(messages) { }
}
