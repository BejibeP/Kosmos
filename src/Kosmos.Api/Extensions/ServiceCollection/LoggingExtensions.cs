using Bejibe.Kosmos.Api.Infrastructure.Logging;
using Microsoft.Extensions.Logging.Console;

namespace Bejibe.Kosmos.Api.Extensions.ServiceCollection
{
    public static class LoggingExtensions
    {
        public static ILoggingBuilder ConfigureLogging(this ILoggingBuilder logging)
        {
            // Remove all the other Logger and Logging Providers
            logging.ClearProviders();

            // Declare a Console Logger named after our custom Log Formatter
            logging.AddConsole(options =>
            {
                options.FormatterName = JsonFormatter.formatterName;
            })
            // Add our custom Log Formatter to the Logger and the DI
            .AddConsoleFormatter<JsonFormatter, ConsoleFormatterOptions>();

            return logging;
        }
    }
}
