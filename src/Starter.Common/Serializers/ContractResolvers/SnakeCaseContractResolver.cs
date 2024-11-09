using Newtonsoft.Json.Serialization;

namespace Starter.Common.Serializers.ContractResolvers;

public class SnakeCaseContractResolver : DefaultContractResolver
{
    public SnakeCaseContractResolver()
        => NamingStrategy = new SnakeCaseNamingStrategy();
}