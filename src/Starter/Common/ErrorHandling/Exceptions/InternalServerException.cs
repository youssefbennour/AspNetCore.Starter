
using Starter.Common.ErrorHandling.Exceptions.Abstractions;

namespace Starter.Common.ErrorHandling.Exceptions;

internal class InternalServerException : AppException {
    internal InternalServerException(string message = "") : base(message) { }
}
