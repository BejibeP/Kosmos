using Kosmos.Common.Configuration;

namespace Bejibe.Kosmos.Api.Extensions.ServiceCollection
{
    public static class ConfigurationExtensions
    {

        public static void LoadConfiguration(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            //services.Configure<AppSettings>(configurationManager.GetSection());
            //var apiSettings = configurationManager.GetSection(ConfigSections.ApiSettings);
            //services.Configure<ApiSettings>(apiSettings);

            //var securitySettings = configurationManager.GetSection(ConfigSections.SecuritySettings);
            //services.Configure<SecuritySettings>(securitySettings);

        }

    }
}
