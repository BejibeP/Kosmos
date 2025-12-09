namespace Kosmos.Common.Configuration
{
    public class SecurityOptions
    {
        public const string SectionName = "Security";

        public List<string> ActiveSchemes { get; set; } = new();
        public List<BasicSecurityOptions> Basic { get; set; } = new();
        public List<ApiTokenSecurityOptions> ApiToken { get; set; } = new();
        public List<BearerSecurityOptions> Bearer { get; set; } = new();
        public CorsOptions Cors { get; set; } = new();
    }

    public class BasicSecurityOptions
    {
        public string Name { get; set; } = string.Empty;
        public List<ClientCredentialsOptions> ClientCredentials { get; set; } = new();
    }

    public class ApiTokenSecurityOptions
    {
        public string Issuer { get; set; } = string.Empty;
        public string ApiKey { get; set; } = string.Empty;
        public List<string> Scopes { get; set; } = new();
    }

    public class BearerSecurityOptions
    {
        public string Name { get; set; } = string.Empty;
        public string Authority { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public string TokenUrl { get; set; } = string.Empty;
        public bool IsSSLRequired { get; set; }
        public List<string> ActiveFlows { get; set; } = new();
    }

    public class ClientCredentialsOptions
    {
        public string ClientId { get; set; } = string.Empty;
        public string ClientSecret { get; set; } = string.Empty;
        public List<string> Scopes { get; set; } = new();
    }

    public class CorsOptions
    {
        public List<string> AllowedOrigins { get; set; } = new();
        public List<string> AllowedHeaders { get; set; } = new();
        public List<string> AllowedMethods { get; set; } = new();
        public List<string> AllowedCredentials { get; set; } = new();
    }

}
