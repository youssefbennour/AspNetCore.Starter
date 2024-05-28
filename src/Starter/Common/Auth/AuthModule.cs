using System.Collections.Immutable;
using Keycloak.AuthServices.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Microsoft.Extensions.DependencyInjection;

internal static class AuthModule
{
   internal static IServiceCollection AddAuthModule(this WebApplicationBuilder builder)
   {
      builder.Services.AddAuthentication(options =>
         {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

         }).AddJwtBearer(o =>
         {
            o.Authority = builder.Configuration["Keycloak:Authority"];
            o.Audience = builder.Configuration["Keycloak:Audience"];
            o.RequireHttpsMetadata = false;
            o.Events = new JwtBearerEvents()
            {
               OnAuthenticationFailed = c =>
               {
                     c.NoResult();

                     c.Response.StatusCode = 500;
                     c.Response.ContentType = "text/plain";

                     // Debug only for security reasons

                     return c.Response.WriteAsync("An error occured processing your authentication.");
               }
            };
         });

      builder.Services.AddAuthorization();
      return builder.Services;
   }

   internal static IApplicationBuilder UseAuthModule(this IApplicationBuilder app)
   {
      app.UseAuthentication();
      app.UseAuthorization();

      return app;
   }
}