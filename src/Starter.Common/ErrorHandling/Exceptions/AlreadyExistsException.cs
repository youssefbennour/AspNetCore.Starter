using Softylines.Contably.Common.ErrorHandling.Exceptions.Abstractions;

namespace Softylines.Contably.Common.ErrorHandling.Exceptions;

public sealed class AlreadyExistsException(string message) : AppException<AlreadyExistsException>(message);