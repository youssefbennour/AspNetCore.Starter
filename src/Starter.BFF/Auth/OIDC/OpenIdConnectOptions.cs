using Starter.Common.ErrorHandling.Exceptions;

namespace Starter.BFF.Auth.OIDC;

internal sealed class OpenIdConnectOptions
{
    public const string Key = "OpenIdConnectOptions";
    public string ServerUrl => Environment.GetEnvironmentVariable("KEYCLOAK_SERVER_URL") 
                               ?? throw new InternalServerException();
    
    public string CookieName { get; set; } = string.Empty;
    public string ClientId { get; set; } = string.Empty;
    public string ClientSecret { get; set; } = string.Empty;
    public string ResponseType { get; set; } = string.Empty;
    public string ResponseMode { get; set; } = string.Empty;
    public bool GetClaimsFromUserInfoEndpoint { get; set; }
    public bool MapInboundClaims { get; set; }
    public bool SaveTokens { get; set; }
    public string Scope { get; set; } = string.Empty; 
    public string AuthorityPath { get; set; } = string.Empty; 
    public string Authority => $"{ServerUrl}/{AuthorityPath}";
}