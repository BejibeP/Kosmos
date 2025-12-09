using Bejibe.Kosmos.Api.Infrastructure.Authentication;
using Kosmos.Common.Configuration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace Bejibe.Kosmos.Api.Extensions.ServiceCollection
{
    public static class SecurityExtensions
    {

        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var securityOptions = configuration.Get<SecurityOptions>();

            // we configure the OIDC provider first
            var oidcOptions = securityOptions?.Bearer?.First() ?? null;
            var basicOptions = securityOptions?.Basic?.First() ?? null;

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddCustomOIDCScheme(oidcOptions)
                .AddCustomBasicScheme(oidcOptions);

            services.AddAuthorization();

            return services;
        }

        public static AuthenticationBuilder AddCustomOIDCScheme(this AuthenticationBuilder authenticationBuilder, BearerSecurityOptions? oidcOptions)
        {
            if (oidcOptions is null)
                return authenticationBuilder;

            authenticationBuilder.AddJwtBearer(options =>
            {
                //options.SaveToken = true;

                options.Authority = oidcOptions.Authority;
                options.RequireHttpsMetadata = oidcOptions.IsSSLRequired;

                // Ajoute des logs pour déboguer
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine($"Authentication failed: {context.Exception}");
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        Console.WriteLine("Token validated successfully");
                        return Task.CompletedTask;
                    }
                };

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = oidcOptions.Issuer,
                    ValidAudience = oidcOptions.Audience,

                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = false,
                    SignatureValidator = delegate (string token, TokenValidationParameters parameters)
                    {
                        var jwt = new JsonWebToken(token);
                        return jwt;
                    }
                };

            });
            return authenticationBuilder;
        }

        public static AuthenticationBuilder AddCustomBasicScheme(this AuthenticationBuilder authenticationBuilder, BearerSecurityOptions? oidcOptions)
        {
            if (oidcOptions is null)
                return authenticationBuilder;

            authenticationBuilder.AddJwtBearer(options =>
            {
                //options.SaveToken = true;

                options.Authority = oidcOptions.Authority;
                options.RequireHttpsMetadata = oidcOptions.IsSSLRequired;

                // Ajoute des logs pour déboguer
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine($"Authentication failed: {context.Exception}");
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        Console.WriteLine("Token validated successfully");
                        return Task.CompletedTask;
                    }
                };

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = oidcOptions.Issuer,
                    ValidAudience = oidcOptions.Audience,

                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = false,
                    SignatureValidator = delegate (string token, TokenValidationParameters parameters)
                    {
                        var jwt = new JsonWebToken(token);
                        return jwt;
                    }
                };

            });

            authenticationBuilder.AddScheme<AuthenticationSchemeOptions, BasicAuthHandler>("basic", null);
            return authenticationBuilder;
        }

        public static IServiceCollection AddCorsPolicies(this IServiceCollection services, IConfiguration configuration)
        {
            var securityOptions = configuration.Get<SecurityOptions>();

            var corsOptions = securityOptions?.Cors ?? new CorsOptions();

            services.AddCors(options =>
            {
                options.AddPolicy("DefaultPolicy", policy =>
                {
                    if (corsOptions.AllowedOrigins.Any())
                        policy.WithOrigins(corsOptions.AllowedOrigins.ToArray());
                    else
                        policy.AllowAnyOrigin();

                    if (corsOptions.AllowedHeaders.Any())
                        policy.WithHeaders(corsOptions.AllowedHeaders.ToArray());
                    else
                        policy.AllowAnyHeader();

                    if (corsOptions.AllowedMethods.Any())
                        policy.WithMethods(corsOptions.AllowedMethods.ToArray());
                    else
                        policy.AllowAnyMethod();

                    policy.AllowCredentials();
                });
            });

            return services;
        }

    }
}
