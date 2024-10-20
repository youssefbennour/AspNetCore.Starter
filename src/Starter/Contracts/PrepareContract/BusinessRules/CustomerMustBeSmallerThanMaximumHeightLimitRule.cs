namespace Starter.Contracts.PrepareContract.BusinessRules;

internal sealed class CustomerMustBeSmallerThanMaximumHeightLimitRule : IBusinessRule
{
    private const int MaximumHeight = 210;

    private readonly int height;

    internal CustomerMustBeSmallerThanMaximumHeightLimitRule(int height) => this.height = height;

    public bool IsMet() => height <= MaximumHeight;

    public string ErrorKey => nameof(height);

    public string Error => "Customer height must fit maximum limit for gym instruments";
}
