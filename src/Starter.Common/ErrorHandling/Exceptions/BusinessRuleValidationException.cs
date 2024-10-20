using Starter.Common.ErrorHandling.Exceptions.Abstractions;

namespace Starter.Common.ErrorHandling.Exceptions;

public class BusinessRuleValidationException : AppException
{
    public BusinessRuleValidationException(string message = "One or more errors have occured") : base(message)
    {
    }
}