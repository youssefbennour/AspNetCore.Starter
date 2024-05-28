using Starter.Common.ErrorHandling.Exceptions.Abstractions;

namespace Starter.Common.Validation.Requests.Exceptions;


internal sealed class AlreadyExistsException : AppException {
    internal AlreadyExistsException() : base(string.Empty) { }
}
