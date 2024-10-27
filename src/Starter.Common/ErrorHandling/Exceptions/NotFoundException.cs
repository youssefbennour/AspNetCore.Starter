using Softylines.Contably.Common.ErrorHandling.Exceptions.Abstractions;

namespace Softylines.Contably.Common.ErrorHandling.Exceptions;

public class NotFoundException(string message) : AppException<NotFoundException>(message);