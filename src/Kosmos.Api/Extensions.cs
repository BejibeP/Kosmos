//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.IdentityModel.JsonWebTokens;
//using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
//using OC.Configuration;
//using OC.Repository;
//using OC.Services;
using System.Runtime;
using System.Security.AccessControl;

namespace Kosmos.Api
{
    public static class Extensions
    {
        public static void LoadConfiguration(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            //var apiSettings = configurationManager.GetSection(ConfigSections.ApiSettings);
            //services.Configure<ApiSettings>(apiSettings);

            //var securitySettings = configurationManager.GetSection(ConfigSections.SecuritySettings);
            //services.Configure<SecuritySettings>(securitySettings);

        }

        public static void ConfigureAuthentication(this IServiceCollection services, ConfigurationManager configurationManager)
        {

            //var securitySettings = configurationManager.GetSection(ConfigSections.SecuritySettings).Get<SecuritySettings>();

            //var ssoSettings = securitySettings?.External?.First() ?? new ExternalAuthSettings();

            //string Authority = "https://authz.apps.orange/realms/multipass/protocol/openid-connect/auth/"; // URL du realm GWARD
            //bool RequireHttpsMetadata = true; // mettre true en prod
            //string Issuer = "https://authz.apps.orange/realms/multipass";
            //string Audience = "efad7d42-7320-5596-bbb3-1f50abf11549"; // le client id configuré dans Keycloak pour cette API

            // Ajout de l'authentification JWT avec Keycloak/GWARD
            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                //.AddJwtBearer(options =>
                //{
                //    //options.SaveToken = true;

                //    options.Authority = ssoSettings.Authority;
                //    options.RequireHttpsMetadata = ssoSettings.IsSSLRequired;

                //    // Ajoute des logs pour déboguer
                //    options.Events = new JwtBearerEvents
                //    {
                //        OnAuthenticationFailed = context =>
                //        {
                //            Console.WriteLine($"Authentication failed: {context.Exception}");
                //            return Task.CompletedTask;
                //        },
                //        OnTokenValidated = context =>
                //        {
                //            Console.WriteLine("Token validated successfully");
                //            return Task.CompletedTask;
                //        }
                //    };

                //    options.TokenValidationParameters = new TokenValidationParameters
                //    {
                //        ValidIssuer = ssoSettings.Issuer,
                //        ValidAudience = ssoSettings.Audience,

                //        ValidateIssuer = true,
                //        ValidateAudience = false,
                //        ValidateLifetime = true,
                //        ValidateIssuerSigningKey = false,
                //        SignatureValidator = delegate (string token, TokenValidationParameters parameters)
                //        {
                //            var jwt = new JsonWebToken(token);
                //            return jwt;
                //        }
                //    };

                //});

            services.AddAuthorization();
        }

        public static void ConfigureDatabase(this IServiceCollection services, ConfigurationManager configurationManager)
        {

            // configure DatabaseContext with serviceProvider and optionBuilder
            //services.AddDbContext<MyDbContext>((provider, options) =>
            //{
            //    options.UseSqlServer(configurationManager.GetConnectionString("SoccerConnectionString"));
            //});
        }

        public static void ConfigureDependencyInjection(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            //Dependency Injection

            //services.AddScoped<IDemoService, DemoService>();
            //services.AddScoped<IWeatherForecastService, WeatherForecastService>();

            //services.AddScoped<ITechnologyRepository, TechnologyRepository>();
            //services.AddScoped<ITechnologyService, TechnologyService>();

            services.AddControllers(options =>
            {
                //options.Conventions.Add(new RoutePrefixConvention("api/v1"));
            });
            services.AddHttpContextAccessor();
        }

        public static void ConfigureSwaggerServices(this IServiceCollection services, ConfigurationManager configurationManager)
        {

            //var securitySettings = configurationManager.GetSection(ConfigSections.SecuritySettings).Get<SecuritySettings>();
            //var ssoSettings = securitySettings?.External?.First() ?? new ExternalAuthSettings();

            //string Authority = "https://authz.apps.orange/realms/multipass/protocol/openid-connect/auth/"; // URL du realm Keycloak/GWARD

            string tokenUrl = "http://localhost:8180/realms/ocr/protocol/openid-connect/token";
            //string TokenUrl = "https://authz.apps.orange/realms/multipass/protocol/openid-connect/token";

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            //AuthorizationUrl = new Uri(ssoSettings.Authority),
                            TokenUrl = new Uri(tokenUrl),
                            Scopes = new Dictionary<string, string>
                            {
                                { "d42442c0-e8ee-5a99-a92e-a543cde57731", "accès en lecture" }
                            }
                        },
                        ClientCredentials = new OpenApiOAuthFlow
                        {
                            //AuthorizationUrl = new Uri(ssoSettings.Authority),
                            TokenUrl = new Uri(tokenUrl),
                            Scopes = new Dictionary<string, string>
                            {
                                { "d42442c0-e8ee-5a99-a92e-a543cde57731", "accès en lecture" }
                            }
                        }
                    }
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
        }

        public static void ConfigureSwaggerClient(this WebApplication application, ConfigurationManager configurationManager)
        {
            // Configure the HTTP request pipeline.

            // if you want swagger only in Developpment
            //if (!application.Environment.IsDevelopment()) return;

            //var securitySettings = configurationManager.GetSection(ConfigSections.SecuritySettings).Get<SecuritySettings>();
            //var ssoSettings = securitySettings?.External?.First() ?? new ExternalAuthSettings();

            string Audience = "efad7d42-7320-5596-bbb3-1f50abf11549"; // le client id configuré dans Keycloak pour cette API

            string clientId = Audience;
            string url = "https://localhost:9843/swagger/oauth2-redirect.html";

            application.UseSwagger();
            application.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "OC v1");
                options.OAuthAppName("Swagger UI");
                //options.OAuthClientId(ssoSettings.Audience); // Client ID Keycloak pour Swagger UI
                options.OAuthUsePkce(); // Recommandé d’utiliser PKCE avec Authorization Code Flow
                options.OAuth2RedirectUrl(url);
            });

            /*
            builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = apiSettings.Title, Version = "v1" });
});

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint($"/{apiSettings.RoutePrefix}/swagger/v1/swagger.json", apiSettings.Title);
});

            */

        }

        public static void ConfigureRouting(this WebApplication application, ConfigurationManager configurationManager)
        {
            // Configure the HTTP request pipeline.
            // Route prefix
            //var prefix = string.IsNullOrEmpty(apiSettings.Routing.Prefix) ? "" : $"/{apiSettings.Routing.Prefix.Trim('/')}";
            //application.MapGet($"{prefix}/hello", () => "Hello World");

        }

    }
}
