namespace Kosmos.Common.Configuration
{
    public class ApiOptions
    {
        public const string SectionName = "Api";

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ContactName { get; set; } = string.Empty;
        public string ContactEmail { get; set; } = string.Empty;
        public string ContactUrl { get; set; } = string.Empty;
        public string RoutePrefix { get; set; } = string.Empty;
        public bool SwaggerEnabled { get; set; }

        public HealthOptions Health { get; set; } = new();
    }

    public class HealthOptions
    {
        public bool Enabled { get; set; }
        public string Route { get; set; } = string.Empty;
    }

}
