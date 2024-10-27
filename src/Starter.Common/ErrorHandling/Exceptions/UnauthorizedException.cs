using Softylines.Contably.Common.ErrorHandling.Exceptions.Abstractions;

namespace Softylines.Contably.Common.ErrorHandling.Exceptions;

public class UnauthorizedException(string message) : AppException<UnauthorizedException>(message);