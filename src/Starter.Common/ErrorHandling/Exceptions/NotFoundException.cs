using Starter.Common.ErrorHandling.Exceptions.Abstractions;

namespace Starter.Common.ErrorHandling.Exceptions;

internal class NotFoundException : AppException {
    internal NotFoundException() : base(string.Empty) { }
}
