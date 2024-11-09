
using Starter.Common.ErrorHandling.Exceptions.Abstractions;

namespace Starter.Common.ErrorHandling.Exceptions;

public class InternalServerException() : AppException<InternalServerException>(string.Empty);
