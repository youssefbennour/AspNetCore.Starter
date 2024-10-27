
using Softylines.Contably.Common.ErrorHandling.Exceptions.Abstractions;

namespace Softylines.Contably.Common.ErrorHandling.Exceptions;

public class InternalServerException() : AppException<InternalServerException>(string.Empty);
