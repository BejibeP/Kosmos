using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kosmos.Common.Configuration
{

    public class ExternalAPIsOptions
    {
        public const string SectionName = "ExternalAPIs";

        public Dictionary<string, ExternalAPIOptions> ExternalAPIs { get; set; } = new();
    }

    public class ExternalAPIOptions
    {
        public string Protocol { get; set; } = string.Empty;
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; }
        public string RoutePrefix { get; set; } = string.Empty;
        public SecurityOptions Security { get; set; } = new();
    }
}
