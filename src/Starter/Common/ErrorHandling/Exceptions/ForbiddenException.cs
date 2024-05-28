using Starter.Common.ErrorHandling.Exceptions.Abstractions;

namespace Starter.Common.Validation.Requests.Exceptions;

internal class ForbiddenException : AppException {
    internal ForbiddenException() : base(string.Empty) { }
}
