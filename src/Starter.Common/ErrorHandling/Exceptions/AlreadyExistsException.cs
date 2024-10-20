using Starter.Common.ErrorHandling.Exceptions.Abstractions;

namespace Starter.Common.ErrorHandling.Exceptions;


public sealed class AlreadyExistsException : AppException {
    internal AlreadyExistsException() : base(string.Empty) { }
}
