using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kosmos.Common.Configuration
{
    public class MailingsOptions
    {
        public const string SectionName = "Mailing";

        public Dictionary<string, MailingOptions> Mailing { get; set; } = new();
    }

    public class MailingOptions
    {
        public string Host { get; set; } = string.Empty;
        public int ApiPort { get; set; }
        public int SMTPPort { get; set; }
        public string User { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
