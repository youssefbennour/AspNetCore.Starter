using Starter.Common.ErrorHandling.Exceptions.Abstractions;

namespace Starter.Common.BusinessRuleEngine;

internal class BusinessRuleValidationException : AppException
{
    internal BusinessRuleValidationException(string message) : base(message)
    {
    }
}