using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Starter.Common.Auth.JwtBearer;

public static class JwtAuthenticationModule
{
    public static WebApplicationBuilder AddJwtAuthentication(this WebApplicationBuilder builder)
    {
       
        builder.Services.AddAuthentication()
            .AddJwtBearer(options =>
            {
                JwtOptions jwtOptions =
                    builder.Configuration.GetSection(JwtOptions.Key)
                        .Get<JwtOptions>()
                    ?? throw new ArgumentException("Jwt config is missing!");
                
                options.MetadataAddress =
                    jwtOptions.WellKnown;
                options.RequireHttpsMetadata = false;
                options.Authority = jwtOptions.Authority;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateAudience = false,
                    ValidIssuer = jwtOptions.Issuer,
                };
                options.Validate();
            });
        
        return builder;
    }

    public static IApplicationBuilder UseJwtAuth(this WebApplication app)
    {
        app.UseAuthentication();
        return app;
    }   
}