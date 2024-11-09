using Starter.Common.ErrorHandling.Exceptions.Abstractions;

namespace Starter.Common.ErrorHandling.Exceptions;

public class NotFoundException() : AppException<NotFoundException>(string.Empty) { };