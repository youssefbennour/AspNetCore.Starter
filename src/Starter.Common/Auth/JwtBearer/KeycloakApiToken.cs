using System.Text.Json.Serialization;

namespace Starter.Common.Auth.Keycloak;

[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
public sealed class KeycloakApiToken
{
    private readonly DateTime _creationTime;

    /// <summary>
    /// Indicates whether the token is expired.
    /// </summary>
    public bool IsExpired => DateTime.UtcNow - _creationTime > ExpiresIn;

    /// <summary>
    /// Initializes a new instance of the <see cref="KeycloakApiToken"/> class.
    /// </summary>
    /// <param name="accessToken"></param>
    /// <param name="expiresIn"></param>
    /// <param name="refreshExpiresIn"></param>
    /// <param name="refreshToken"></param>
    /// <param name="tokenType"></param>
    /// <param name="notBeforePolicy"></param>
    /// <param name="sessionState"></param>
    /// <param name="scope"></param>
    public KeycloakApiToken(string accessToken, int expiresIn, int refreshExpiresIn, string refreshToken, string tokenType, int notBeforePolicy, string sessionState, string scope)
    {
        _creationTime = DateTime.UtcNow;
        AccessToken = accessToken;
        ExpiresIn = TimeSpan.FromSeconds(expiresIn - 10);
        RefreshExpiresIn = refreshExpiresIn;
        RefreshToken = refreshToken;
        TokenType = tokenType;
        NotBeforePolicy = notBeforePolicy;
        SessionState = sessionState;
        Scope = scope;
    }

    /// <summary>
    /// Access token.
    /// </summary>
    public string AccessToken { get; }

    /// <summary>
    /// Time until the token expires.
    /// </summary>
    public TimeSpan ExpiresIn { get; }

    /// <summary>
    /// Time until the refresh token expires.
    /// </summary>
    public int RefreshExpiresIn { get; }

    /// <summary>
    /// Refresh token.
    /// </summary>
    public string RefreshToken { get; }

    /// <summary>
    /// Token type.
    /// </summary>
    public string TokenType { get; }

    /// <summary>
    /// Not before policy.
    /// </summary>
    [JsonPropertyName("not-before-policy")]
    public int NotBeforePolicy { get; }

    /// <summary>
    /// Session state.
    /// </summary>
    public string SessionState { get; }

    /// <summary>
    /// Scope.
    /// </summary>
    public string Scope { get; } 
}