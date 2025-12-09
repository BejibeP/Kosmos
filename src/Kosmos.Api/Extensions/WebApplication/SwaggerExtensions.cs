using Kosmos.Common.Configuration;
using System;

namespace Bejibe.Kosmos.Api.Extensions.Application
{
    public static class SwaggerExtensions
    {
        public static void UseSwaggerWebUI(this WebApplication application, ConfigurationManager configurationManager)
        {
            var apiOptions = configurationManager.Get<ApiOptions>();

            if (apiOptions is null || !apiOptions.SwaggerEnabled)
                return;

            var securityOptions = configurationManager.Get<SecurityOptions>();
            var oidcOptions = securityOptions?.Bearer?.First() ?? null;

            application.UseSwagger();
            application.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "OC v1");

                if(oidcOptions != null)
                {
                    string absoluteUrl = "swagger/oauth2-redirect.html\"";

                    options.OAuthAppName("Swagger UI");
                    options.OAuthClientId(oidcOptions.Audience);
                    options.OAuthUsePkce();
                    options.OAuth2RedirectUrl(absoluteUrl);
                }

            });


        }
    }
}
