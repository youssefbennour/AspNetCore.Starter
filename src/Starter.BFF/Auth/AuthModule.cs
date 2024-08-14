using Starter.BFF.Auth.OIDC;

namespace Starter.BFF.Auth;

internal static class AuthModule
{
    internal static IServiceCollection AddAuth(this WebApplicationBuilder builder)
    {
        builder.AddOidcAuthentication();
        builder.Services.AddAuthorizationPolicies();
        builder.Services.AddBff();
            
        return builder.Services;
    }
    
    internal static IApplicationBuilder UseAuth(this WebApplication app)
    {
        app.UseBff();
        app.UseAuthentication();
        app.UseAuthorization();
        
        app.MapBffManagementEndpoints();
        
        return app;
    }
}