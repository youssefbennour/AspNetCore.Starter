using Starter.Common.BusinessRuleEngine;
using Starter.Common.ErrorHandling.Exceptions;

namespace Starter.Common.BusinessRulesEngine;

public static class BusinessRuleValidator {
    internal static void Validate(IBusinessRule rule, BusinessRuleValidationException? exception = null) {
        if(rule.IsMet())
        {
            return;
        }

        if (exception is null)
        {
            exception = new BusinessRuleValidationException();
            exception.UpsertToException(rule.ErrorKey, rule.Error);
            exception.ThrowIfContainsErrors();
        }
        
        exception.UpsertToException(rule.ErrorKey, rule.Error);
    }
}