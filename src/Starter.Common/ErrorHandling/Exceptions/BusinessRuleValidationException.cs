using Softylines.Contably.Common.ErrorHandling.Exceptions.Abstractions;

namespace Softylines.Contably.Common.ErrorHandling.Exceptions;

public class BusinessRuleValidationException(string message = "One or more errors have occured")
    : AppException<BusinessRuleValidationException>(message);