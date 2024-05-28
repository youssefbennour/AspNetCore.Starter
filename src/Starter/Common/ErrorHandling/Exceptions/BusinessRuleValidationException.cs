using Starter.Common.ErrorHandling.Exceptions.Abstractions;

namespace Starter.Common.ErrorHandling.Exceptions;

internal class BusinessRuleValidationException : AppException
{
    internal BusinessRuleValidationException(string message = "One or more errors have occured") : base(message)
    {
    }
}