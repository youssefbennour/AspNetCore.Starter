using Softylines.Contably.Common.ErrorHandling.Exceptions.Abstractions;

namespace Softylines.Contably.Common.ErrorHandling.Exceptions;

public class BadRequestException(string message) : AppException<BadRequestException>(message);