using Starter.Common.Validation.Exceptions.Base;

namespace Starter.Common.Validation.Requests.Exceptions;


internal sealed class AlreadyExistsException : AppException
{
    internal AlreadyExistsException(string message) : base(message) {}
    internal AlreadyExistsException(IEnumerable<string> messages) : base(messages) {}
}
