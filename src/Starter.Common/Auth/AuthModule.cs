using Microsoft.AspNetCore.Hosting;
using Starter.Common.Auth.Keycloak;

namespace Starter.Common.Auth;

public static class AuthModule
{
   public static WebApplicationBuilder AddAuthModule(this WebApplicationBuilder builder)
   {
      builder.AddKeycloak();
      builder.WebHost.UseKestrel(options => 
         options.AddServerHeader = false);
      return builder;
   }

   public static IApplicationBuilder UseAuthModule(this WebApplication app)
   {
      app.UseKeycloak();
      return app;
   }
}