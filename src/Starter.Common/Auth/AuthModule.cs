using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Starter.Common.Auth.JwtBearer;

namespace Starter.Common.Auth;

public static class AuthModule
{
   public static IServiceCollection AddAuthModule(this WebApplicationBuilder builder)
   {
      builder.AddJwtAuthentication();
      builder.Services.AddAuthorizationBuilder()
         .SetDefaultPolicy(new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .Build());
      
      return builder.Services;
   }

   public static IApplicationBuilder UseAuthModule(this WebApplication app)
   {
      app.UseJwtAuth();
      app.UseAuthorization();
      return app;
   }
}