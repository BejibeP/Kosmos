using Kosmos.Common.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Bejibe.Kosmos.Api.Extensions.ServiceCollection
{
    public static class SwaggerExtensions
    {

        public static IServiceCollection AddSwaggerServices(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            var securityOptions = configurationManager.Get<SecurityOptions>();

            var oidcOptions = securityOptions?.Bearer?.First() ?? null;

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(options =>
            {
                options
                .AddOIDCSecurityDefinition(oidcOptions)
                .AddBasicSecurityDefinition(oidcOptions)
                .AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            //Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
                        },
                        new[] { "api1" }
                    }
                });

            });
            return services;
        }

        public static SwaggerGenOptions AddOIDCSecurityDefinition(this SwaggerGenOptions genOptions, BearerSecurityOptions? oidcOptions)
        {
            if (oidcOptions is null)
                return genOptions;

            genOptions.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    AuthorizationCode = new OpenApiOAuthFlow
                    {
                        //AuthorizationUrl = new Uri(ssoSettings.Authority),
                        TokenUrl = new Uri(oidcOptions.TokenUrl),
                        Scopes = new Dictionary<string, string>
                            {
                                { "d42442c0-e8ee-5a99-a92e-a543cde57731", "accès en lecture" }
                            }
                    }
                }
            });

            return genOptions;
        }

        public static SwaggerGenOptions AddBasicSecurityDefinition(this SwaggerGenOptions genOptions, BearerSecurityOptions? oidcOptions)
        {
            if (oidcOptions is null)
                return genOptions;

            genOptions.AddSecurityDefinition("basic", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    AuthorizationCode = new OpenApiOAuthFlow
                    {
                        //AuthorizationUrl = new Uri(ssoSettings.Authority),
                        TokenUrl = new Uri(oidcOptions.TokenUrl),
                        Scopes = new Dictionary<string, string>
                            {
                                { "d42442c0-e8ee-5a99-a92e-a543cde57731", "accès en lecture" }
                            }
                    }
                }
            });

            return genOptions;
        }

    }
}
