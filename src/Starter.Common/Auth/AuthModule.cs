using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Softylines.Contably.Common.Auth.Keycloak;

namespace Softylines.Contably.Common.Auth;

public static class AuthModule
{
   public static IServiceCollection AddAuthModule(this WebApplicationBuilder builder)
   {
      builder.AddKeycloak();
      builder.WebHost.UseKestrel(options => 
         options.AddServerHeader = false);
      return builder.Services;
   }

   public static IApplicationBuilder UseAuthModule(this WebApplication app)
   {
      app.UseKeycloak();
      return app;
   }
}