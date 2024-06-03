using Starter.Common.Auth.Keycloak;
namespace Microsoft.Extensions.DependencyInjection;

internal static class AuthModule
{
   internal static IServiceCollection AddAuthModule(this WebApplicationBuilder builder)
   {
      builder.AddKeycloak();
      return builder.Services;
   }

   internal static IApplicationBuilder UseAuthModule(this WebApplication app)
   {
      app.UseKeycloak();
      return app;
   }
}