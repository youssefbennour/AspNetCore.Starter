using Starter.Common.ErrorHandling.Exceptions.Abstractions;

namespace Starter.Common.ErrorHandling.Exceptions;

public sealed class AlreadyExistsException(string message) : AppException<AlreadyExistsException>(message);