using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kosmos.Common.Configuration
{

    public class MessagingsOptions
    {
        public Dictionary<string, MessagingOptions> Messaging { get; set; } = new();
    }

    public class MessagingOptions
    {
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; }
        public string User { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string VirtualHost { get; set; } = string.Empty;
        public bool UseSsl { get; set; }
        public int RetryCount { get; set; }
        public int HeartbeatSeconds { get; set; }
        public string BootstrapServers { get; set; } = string.Empty;
        public string SaslUser { get; set; } = string.Empty;
        public string SaslPassword { get; set; } = string.Empty;
        public string SecurityProtocol { get; set; } = string.Empty;
    }
}
