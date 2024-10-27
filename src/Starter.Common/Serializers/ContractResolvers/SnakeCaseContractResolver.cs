using Newtonsoft.Json.Serialization;

namespace Softylines.Contably.Common.Serializers.ContractResolvers;

public class SnakeCaseContractResolver : DefaultContractResolver
{
    public SnakeCaseContractResolver()
        => NamingStrategy = new SnakeCaseNamingStrategy();
}