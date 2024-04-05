namespace Starter.Common.BusinessRuleEngine;

internal interface IBusinessRule
{
    bool IsMet();
    string Error { get; }
}