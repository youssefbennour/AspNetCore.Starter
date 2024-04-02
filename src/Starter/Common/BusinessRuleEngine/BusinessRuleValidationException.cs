namespace Starter.Common.BusinessRuleEngine;

internal class BusinessRuleValidationException : InvalidOperationException
{
    internal BusinessRuleValidationException(string message) : base(message)
    {
    }
}