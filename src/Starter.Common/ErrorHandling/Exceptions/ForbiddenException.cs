using Starter.Common.ErrorHandling.Exceptions.Abstractions;

namespace Starter.Common.ErrorHandling.Exceptions;

public class ForbiddenException(string message) : AppException<ForbiddenException>(message);
