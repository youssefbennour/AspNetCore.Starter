using Starter.Common.ErrorHandling.Exceptions;

namespace Starter.Common.Auth.JwtBearer;

public sealed class JwtOptions
{
    /// <summary>
    /// Gets the key identifier for <see cref="JwtOptions"/>.
    /// </summary>
    public const string Key = "JwtOptions";

    public string AuthorityPath { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the relative path for obtaining tokens from Keycloak.
    /// </summary>
    public string TokenPath { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the base URL of the Keycloak server.
    /// </summary>
    public string ServerUrl => Environment.GetEnvironmentVariable("KEYCLOAK_SERVER_URL") 
                               ?? throw new InternalServerException();

    /// <summary>
    /// Gets or sets the relative client path for Keycloak operations.
    /// </summary>
    public string ClientPath { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets the absolute URI for Keycloak client operations.
    /// </summary>
    public string ClientUrl => $"{ServerUrl}/{ClientPath}";

    /// <summary>
    /// Gets the aboslute URL for obtaining tokens from Keycloak.
    /// </summary>
    public string TokenUrl => $"{ServerUrl}/{TokenPath}";

    public string Authority => $"{ServerUrl}/{AuthorityPath}";
    public string WellKnown => $"{ServerUrl}/{AuthorityPath}/.well-known/openid-configuration";
    public string Issuer => $"{ServerUrl}/{AuthorityPath}";
}