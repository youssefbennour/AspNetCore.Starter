
using Starter.Common.ErrorHandling.Exceptions.Abstractions;

namespace Starter.Common.ErrorHandling.Exceptions;

public sealed class InternalServerException : AppException {
    public InternalServerException() : base(string.Empty) { }
}
