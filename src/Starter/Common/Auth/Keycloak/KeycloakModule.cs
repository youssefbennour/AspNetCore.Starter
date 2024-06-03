using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace Starter.Common.Auth.Keycloak;

public static class KeycloakModule
{
    public static IServiceCollection AddKeycloak(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication()
            .AddJwtBearer(options =>
            {
                options.MetadataAddress = "http://localhost:7001/realms/starter-realm/.well-known/openid-configuration";
                options.RequireHttpsMetadata = false;
                options.Authority = "http://localhost:7001/realms/starter-realm";
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidIssuer = "http://localhost:7001/realms/starter-realm",
                };
                options.Validate();
            });
        builder.Services.AddAuthorization();
        return builder.Services;
    }

    internal static IApplicationBuilder UseKeycloak(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
        
        return app;
    }   
}