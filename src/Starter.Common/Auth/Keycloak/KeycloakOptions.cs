namespace Softylines.Contably.Common.Auth.Keycloak;

public sealed class KeycloakOptions
{
    /// <summary>
    /// Gets the key identifier for <see cref="KeycloakOptions"/>.
    /// </summary>
    public const string Key = "KeycloakOptions";

    public string AuthorityPath { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the client ID for the Keycloak admin client.
    /// </summary>
    public string AdminClientId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the client secret for the Keycloak admin client.
    /// </summary>
    public string AdminClientSecret { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the requested scope(s) for the client.<br></br>
    /// Space-delimited string
    /// </summary>
    public string Scope { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the relative path for obtaining tokens from Keycloak.
    /// </summary>
    public string TokenPath { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the base URL of the Keycloak server.
    /// </summary>
    public string? ServerUrl { get; set; } = Environment.GetEnvironmentVariable("KEYCLOAK_SERVER_URL");
    /// <summary>
    /// Gets or sets the relative client path for Keycloak operations.
    /// </summary>
    public string ClientPath { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the relative admin path for Keycloak operations.
    /// </summary>
    public string AdminPath { get; set; } = string.Empty;

    /// <summary>
    /// Gets the absolute URL for Keycloak client operations.
    /// </summary>
    public string ClientUrl => $"{ServerUrl}/{ClientPath}";

    /// <summary>
    /// Gets the absolute URL for Keycloak admin operations.
    /// </summary>
    public string AdminUrl => $"{ServerUrl}/{AdminPath}";

    /// <summary>
    /// Gets the aboslute URL for obtaining tokens from Keycloak.
    /// </summary>
    public string TokenUrl => $"{ServerUrl}/{TokenPath}";

    /// <summary>
    /// Gets or sets the redirect URI of Invitations.
    /// </summary>
    public string InvitationRedirectUri => $"{ServerUrl}/{AuthorityPath}/account";

    public string Authority => $"{ServerUrl}/{AuthorityPath}";
    public string WellKnown => $"{ServerUrl}/{AuthorityPath}/.well-known/openid-configuration";
}