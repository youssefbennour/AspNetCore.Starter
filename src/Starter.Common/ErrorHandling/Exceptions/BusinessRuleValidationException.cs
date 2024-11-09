using Starter.Common.ErrorHandling.Exceptions.Abstractions;

namespace Starter.Common.ErrorHandling.Exceptions;

public class BusinessRuleValidationException(string message = "One or more errors have occured")
    : AppException<BusinessRuleValidationException>(message);