namespace Starter.Common.Auth.Keycloak;

public sealed class AuthenticationOptions
{
    public const string Key = "AuthenticationOptions";
    public string CookieName { get; set; } = string.Empty;
    public string ClientId { get; set; } = string.Empty;
    public string ClientSecret { get; set; } = string.Empty;
    public string ResponseType { get; set; } = string.Empty;
    public string ResponseMode { get; set; } = string.Empty;
    public bool GetClaimsFromUserInfoEndpoint { get; set; }
    public bool MapInboundClaims { get; set; }
    public bool SaveTokens { get; set; }
    public string Scope { get; set; } = string.Empty;
}

