namespace Starter.Common.BusinessRuleEngine;

internal static class BusinessRuleValidator {
    internal static void Validate(IBusinessRule rule) {
        if(!rule.IsMet()) {
            throw new BusinessRuleValidationException(rule.Error);
        }
    }
}