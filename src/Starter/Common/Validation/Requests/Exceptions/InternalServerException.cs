using Starter.Common.Validation.Exceptions.Base;

namespace Starter.Common.Validation.Requests.Exceptions;

internal class InternalServerException : AppException
{
    internal InternalServerException(string message) : base(message) { }

    internal InternalServerException(IEnumerable<string> messages) : base(messages) { }
}
