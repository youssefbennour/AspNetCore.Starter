using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Starter.Common.Auth.Keycloak;

public static class KeycloakModule
{
    public static IServiceCollection AddKeycloak(this WebApplicationBuilder builder)
    {
        var authenticationConfig = builder.Configuration.GetSection(AuthenticationOptions.Key) 
                                   ?? throw new ArgumentException("Authentication config is missing!");

        builder.Services.AddOptions<AuthenticationOptions>()
            .Bind(authenticationConfig);
        builder.Services.AddAuthentication()
            .AddJwtBearer(options =>
            {
                KeycloakOptions keycloakOptions =
                    builder.Configuration.GetSection(KeycloakOptions.Key)
                        .Get<KeycloakOptions>()
                    ?? throw new ArgumentException("keycloak config is missing!");
                
                options.MetadataAddress = 
                    keycloakOptions.WellKnown;
                options.RequireHttpsMetadata = false;
                options.Authority = keycloakOptions.Authority;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateAudience = false,
                    ValidIssuers = new []{options.Authority, "http://localhost:7002/realms/contably-realm"},
                };
                options.Validate();
            });
        
        builder.Services.AddAuthorization(options =>
        {
            options.FallbackPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
        });
        return builder.Services;
    }

    public static IApplicationBuilder UseKeycloak(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
        
        return app;
    }   
}