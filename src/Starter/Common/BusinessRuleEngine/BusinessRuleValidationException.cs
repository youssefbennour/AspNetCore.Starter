using Starter.Common.Validation.Exceptions.Base;

namespace Starter.Common.BusinessRuleEngine;

internal class BusinessRuleValidationException : AppException
{
    internal BusinessRuleValidationException(string message) : base(message)
    {
    }
}