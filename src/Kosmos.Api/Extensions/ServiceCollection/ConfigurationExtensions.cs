using Kosmos.Common.Configuration;

namespace Bejibe.Kosmos.Api.Extensions.ServiceCollection
{
    public static class ConfigurationExtensions
    {

        public static void AddConfigurationSources(this ConfigurationManager configuration)
        {
            configuration.AddJsonFile("appsettings.passwords.json", optional: true, reloadOnChange: true);
        }

        public static void LoadConfiguration(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            var apiOptions = configurationManager.GetSection(ApiOptions.SectionName);
            services.Configure<ApiOptions>(apiOptions);

            var securityOptions = configurationManager.GetSection(SecurityOptions.SectionName);
            services.Configure<SecurityOptions>(securityOptions);

            var databasesOptions = configurationManager.GetSection(DatabasesOptions.SectionName);
            services.Configure<DatabasesOptions>(databasesOptions);

            var messagingOptions = configurationManager.GetSection(MessagingsOptions.SectionName);
            services.Configure<MessagingsOptions>(messagingOptions);

            var cachingOptions = configurationManager.GetSection(CachingOptions.SectionName);
            services.Configure<CachingOptions>(cachingOptions);

            var mailingOptions = configurationManager.GetSection(MailingsOptions.SectionName);
            services.Configure<MailingsOptions>(mailingOptions);

            var externalApisOptions = configurationManager.GetSection(ExternalAPIsOptions.SectionName);
            services.Configure<ExternalAPIsOptions>(externalApisOptions);

        }

    }
}
