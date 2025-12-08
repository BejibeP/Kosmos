namespace Kosmos.Common.Configuration
{
    public class CachingOptions
    {
        public const string SectionName = "Caching";

        public string Host { get; set; } = string.Empty;
        public string Port { get; set; } = string.Empty;
        public string User { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
