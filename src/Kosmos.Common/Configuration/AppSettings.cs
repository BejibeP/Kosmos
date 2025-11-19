using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kosmos.Common.Configuration
{
    public class AppSettings
    {
        public ApiOptions Api { get; set; } = new();
        public SecurityOptions Security { get; set; } = new();
        public Dictionary<string, DatabaseOptions> Databases { get; set; } = new();
        public Dictionary<string, MessagingOptions> Messaging { get; set; } = new();
        public CachingOptions Caching { get; set; } = new();
        public MailingOptions Mailing { get; set; } = new();
        public Dictionary<string, ExternalAPIOptions> ExternalAPIs { get; set; } = new();
        public LoggingOptions Logging { get; set; } = new();
    }
}
