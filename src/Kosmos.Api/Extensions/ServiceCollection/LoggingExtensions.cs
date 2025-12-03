using Bejibe.Kosmos.Api.Infrastructure.Logging;
using Microsoft.Extensions.Logging.Console;

namespace Bejibe.Kosmos.Api.Extensions.ServiceCollection
{
    public static class LoggingExtensions
    {
        public static ILoggingBuilder ConfigureAppLogging1(this ILoggingBuilder logging, IHostEnvironment env)
        {
            // Reset des providers inutiles
            logging.ClearProviders();

            // Logging console optimisé Docker
            logging.AddSimpleConsole(opts =>
            {
                opts.UseUtcTimestamp = true;
                opts.TimestampFormat = "yyyy-MM-ddTHH:mm:ss.fffZ ";
                opts.SingleLine = true;
            });

            // Niveau de log adapté (modifiable selon env)
            logging.SetMinimumLevel(env.IsDevelopment()
                ? LogLevel.Debug
                : LogLevel.Information);

            return logging;
        }

        public static ILoggingBuilder ConfigureLogging(this ILoggingBuilder logging)
        {
            // Reset des providers par défaut
            logging.ClearProviders();

            // Ajout du ConsoleLogger
            logging.AddConsole(options =>
            {
                options.FormatterName = JsonFormatter.formatterName;
            })
            // Ajout du Custom Log Formatter
            .AddConsoleFormatter<JsonFormatter, ConsoleFormatterOptions>();

            return logging;
        }
    }

}
