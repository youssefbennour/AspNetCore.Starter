namespace Starter.Common.BusinessRuleEngine;

internal interface IBusinessRule
{
    bool IsMet();
    string ErrorKey { get; }
    string Error { get; }
}