using System.Collections.Immutable;
using Keycloak.AuthServices.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Microsoft.Extensions.DependencyInjection;

internal static class AuthModule
{
   internal static IServiceCollection AddAuthModule(this WebApplicationBuilder builder)
   {
      return builder.Services;
   }

   internal static IApplicationBuilder UseAuthModule(this IApplicationBuilder app)
   {
      return app;
   }
}