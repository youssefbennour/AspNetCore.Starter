using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace Starter.BFF.Auth.OIDC;

internal static class OpenIdConnectModule
{
   public static IServiceCollection AddOidcAuthentication(this WebApplicationBuilder builder)
   {
      var openIdConnectOptions = builder.Configuration.GetSection(OpenIdConnectOptions.Key)
                                    .Get<OpenIdConnectOptions>() 
                                 ?? throw new ArgumentException("OpenId Connect config is missing!");
      
      builder.Services.AddAuthentication(options =>
      {
         options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
         options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
         options.DefaultSignOutScheme = OpenIdConnectDefaults.AuthenticationScheme;
      })
      .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
      {
         options.Cookie.Name = openIdConnectOptions.CookieName;
         options.ExpireTimeSpan = TimeSpan.FromHours(8);
         options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
         options.Cookie.SameSite = SameSiteMode.Lax; //Consider switching to SameSiteMode.Strict in production
         options.Cookie.HttpOnly = true;
      })
      .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
      {
         options.Authority = openIdConnectOptions.Authority;
         options.ClientId = openIdConnectOptions.ClientId;
         options.ClientSecret = openIdConnectOptions.ClientSecret;
         options.ResponseType = openIdConnectOptions.ResponseType;
         options.ResponseMode = openIdConnectOptions.ResponseMode;
         options.GetClaimsFromUserInfoEndpoint = openIdConnectOptions.GetClaimsFromUserInfoEndpoint;
         options.MapInboundClaims = openIdConnectOptions.MapInboundClaims;
         options.SaveTokens = openIdConnectOptions.SaveTokens;
         options.RequireHttpsMetadata = false;

         var scopes = openIdConnectOptions.Scope
            .Split(" ")
            .ToList(); 
         
         options.Scope.Clear();
         scopes.ForEach(scope => options.Scope.Add(scope));
      });
      
      return builder.Services;
   }
}