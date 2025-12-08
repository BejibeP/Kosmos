using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kosmos.Common.Configuration
{
    public class SecurityOptions
    {
        public const string SectionName = "Security";

        public List<string> ActiveSchemes { get; set; } = new();
        public List<BasicSecurityOptions> Basic { get; set; } = new();
        public List<ApiTokenSecurityOptions> ApiToken { get; set; } = new();
        public List<BearerSecurityOptions> Bearer { get; set; } = new();
    }

    public class BasicSecurityOptions
    {
        public string Name { get; set; } = string.Empty;
        public List<ClientCredentialsOptions> ClientCredentials { get; set; } = new();
    }

    public class ApiTokenSecurityOptions
    {
        public string Issuer {  get; set; } = string.Empty;
        public string ApiKey {  get; set; } = string.Empty;
        public List<string> Scopes { get; set; } = new();
    }

    public class BearerSecurityOptions
    {
        public string Name { get; set; } = string.Empty;
        public string Authority { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public bool IsSSLRequired { get; set; }
        public List<string> ActiveFlows { get; set; } = new();
    }

    public class ClientCredentialsOptions
    {
        public string ClientId { get; set; } = string.Empty;
        public string ClientSecret { get; set; } = string.Empty;
        public List<string> Scopes { get; set; } = new();
    }

}
