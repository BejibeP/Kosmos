using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kosmos.Common.Configuration
{
    public class DatabasesOptions
    {
        public const string SectionName = "Databases";

        public Dictionary<string, DatabaseOptions> Databases { get; set; } = new();
    }

    public class DatabaseOptions
    {
        public string Provider { get; set; } = string.Empty;
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; }
        public string Database { get; set; } = string.Empty;
        public string User { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DatabaseSpOptions Options { get; set; } = new();
    }

    public class DatabaseSpOptions
    {
        public bool TrustServerCertificate { get; set; }
        public bool SslRequired { get; set; }
        public int RetryCount { get; set; }
        public int CommandTimeoutSeconds { get; set; }
        public bool IsReadOnly { get; set; }
        public bool EnableLogging { get; set; }
    }

}
