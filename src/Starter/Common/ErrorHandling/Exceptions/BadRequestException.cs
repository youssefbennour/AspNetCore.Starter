using Starter.Common.ErrorHandling.Exceptions.Abstractions;

namespace Starter.Common.ErrorHandling.Exceptions;

internal class BadRequestException : AppException {
    internal BadRequestException(string message) : base(message) { }
}
