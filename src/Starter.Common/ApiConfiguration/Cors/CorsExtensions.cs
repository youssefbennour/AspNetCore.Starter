
using Microsoft.Extensions.DependencyInjection;

namespace Starter.Common.ApiConfiguration.Cors;

public static class CorsExtensions
{
   public static IServiceCollection AddCorsPolicies(this IServiceCollection services)
   {
      return services.AddCors(options =>
      {
         options.AddDefaultPolicy(
            builder =>
            {
               builder.WithOrigins("https://localhost:3000")
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