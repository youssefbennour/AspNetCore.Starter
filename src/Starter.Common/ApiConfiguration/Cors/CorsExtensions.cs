
using Microsoft.Extensions.DependencyInjection;

namespace Softylines.Contably.Common.ApiConfiguration.Cors;

public static class CorsExtensions
{
   public static IServiceCollection AddCorsPolicies(this IServiceCollection services)
   {
      return services.AddCors(options =>
      {
         options.AddDefaultPolicy(
            builder =>
            {
               builder.WithOrigins("https://localhost:3000",
                      "http://localhost:3000",
                     "https://devcomptabilite.softylines.com",
                     "https://devcomptabiliteapi.softylines.com",
                     "http://devcomptabilite.softylines.com",
                     "http://devcomptabiliteapi.softylines.com")
                  .AllowCredentials()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
            });
      });
   }
 
   public static IApplicationBuilder UseCorsPolicies(this IApplicationBuilder app)
   {
      return app.UseCors();
   }
}