using System.Net.Http.Headers;
using Duende.Bff.Yarp;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Yarp.ReverseProxy.Transforms;

namespace Starter.BFF.RequestProxying.Yarp;

internal static class YarpModule
{
    internal static IServiceCollection AddYarp(this IServiceCollection services, ConfigurationManager configuration)
    {
        var reverseProxyConfig = 
            configuration.GetSection("ReverseProxy") ?? throw new ArgumentException("ReverseProxy section is missing!");

        services.AddReverseProxy()
            .AddBffExtensions()
            .LoadFromConfig(reverseProxyConfig)
            .AddTransforms(builderContext =>
            {
                
                builderContext.AddRequestTransform(async transformContext =>
                {
                    var accessToken = await transformContext.HttpContext.GetUserAccessTokenAsync();
                    if(accessToken.AccessToken is not null)
                    {
                        transformContext.ProxyRequest.Headers.Authorization = 
                            new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, accessToken.AccessToken);
                    }                            
                });
            });

        return services;
    }  
    
    internal static WebApplication UseYarp(this WebApplication app)
    {
        app.MapReverseProxy()
            .AsBffApiEndpoint();

        return app;
    }   
}