using Starter.Common.ErrorHandling.Exceptions;

namespace Starter.Common.BusinessRulesEngine;

public static class BusinessRuleValidator {
    public static void Validate(params IBusinessRule[] rules)
    {
        BusinessRuleValidationException exception = new();
        foreach (var rule in rules)
        {
            Validate(rule, exception);
        }  
        
        exception.ThrowIfContainsErrors();
    }
    
    private static void Validate(IBusinessRule rule, BusinessRuleValidationException exception) {
        if(rule.IsMet())
        {
            return;
        }

        exception.UpsertToException(rule.ErrorKey, rule.Error);
    }
}