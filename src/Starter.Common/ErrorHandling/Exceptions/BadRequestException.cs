using Starter.Common.ErrorHandling.Exceptions.Abstractions;

namespace Starter.Common.ErrorHandling.Exceptions;

public class BadRequestException(string message) : AppException<BadRequestException>(message);