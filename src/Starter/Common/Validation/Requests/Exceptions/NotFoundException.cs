using Starter.Common.Validation.Exceptions.Base;

namespace Starter.Common.Validation.Requests.Exceptions;

internal sealed class NotFoundException : AppException
{
    internal NotFoundException(string message) : base(message, System.Net.HttpStatusCode.BadRequest)
    {

    }
    internal NotFoundException(IEnumerable<string> messages) : base(messages, System.Net.HttpStatusCode.BadRequest)
    {

    }
}
