using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kosmos.Common.Configuration
{
    public class LoggingOptions
    {
        public Dictionary<string, string> LogLevel { get; set; } = new();
    }
}
