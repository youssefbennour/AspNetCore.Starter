using Starter.Common.Validation.Exceptions.Base;

namespace Starter.Common.Validation.Requests.Exceptions;

internal class BadRequestException : AppException
{
    internal BadRequestException(string message) : base(message) { }

    internal BadRequestException(IEnumerable<string> messages) : base(messages) { }
}
