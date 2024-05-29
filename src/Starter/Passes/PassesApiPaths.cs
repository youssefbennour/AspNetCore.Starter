namespace Starter.Passes;

internal static class PassesApiPaths
{
    private const string PassesRootPath = $"{ApiPaths.Root}/passes";
    internal const string GetAll = PassesRootPath;
    internal const string MarkPassAsExpired = $"{PassesRootPath}/{{id}}";
}