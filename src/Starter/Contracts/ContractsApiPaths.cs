namespace Starter.Contracts;

internal static class ContractsApiPaths
{
    private const string ContractsRootApi = $"{ApiPaths.Root}/contracts";

    internal const string Prepare = ContractsRootApi;
    internal const string Sign = $"{ContractsRootApi}/{{id}}";
    internal const string GetAll = ContractsRootApi;
}
