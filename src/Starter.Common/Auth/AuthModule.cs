using Microsoft.Extensions.DependencyInjection;
using Starter.Common.Auth.JwtBearer;

namespace Starter.Common.Auth;

internal static class AuthModule
{
   internal static IServiceCollection AddAuthModule(this WebApplicationBuilder builder)
   {
      builder.AddJwtAuthentication();
      builder.Services.AddAuthorization();
      return builder.Services;
   }

   internal static IApplicationBuilder UseAuthModule(this WebApplication app)
   {
      app.UseJwtAuth();
      app.UseAuthorization();
      return app;
   }
}