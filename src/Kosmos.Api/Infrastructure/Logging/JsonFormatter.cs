using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging.Console;
using System.IO;
using System.Text.Json;

namespace Bejibe.Kosmos.Api.Infrastructure.Logging
{
    public class JsonFormatter : ConsoleFormatter
    {
        public const string formatterName = "ks-json";

        public JsonFormatter() : base(formatterName) { }

        public override void Write<TState>(in LogEntry<TState> logEntry, IExternalScopeProvider? scopeProvider, TextWriter textWriter)
        {
            var payload = new Dictionary<string, object?>
            {
                ["timestamp"] = DateTime.UtcNow.ToString("o"),
                ["level"] = logEntry.LogLevel.ToString(),
                ["category"] = logEntry.Category,
                ["message"] = logEntry.Formatter(logEntry.State, logEntry.Exception)
            };

            if (logEntry.Exception is not null)
                payload["exception"] = logEntry.Exception.ToString();

            var json = JsonSerializer.Serialize(payload);
            textWriter.WriteLine(json);
        }
    }

}


/*
 
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging.Console;
using System.Runtime.Intrinsics.X86;
using System.Text.Json;

namespace OrangeCarto.Common.Config
{
    /// <summary>
    /// CartoLoggerFormatter is a JSON logs serializer fully based on Microsoft.Extensions.Logging & Microsoft.Extensions.Telemetry Can use the HttpContext(IHttpContextAccessor) when set
    /// </summary>
    public class CartoLoggerFormatter : ConsoleFormatter
    {

        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartoLoggerFormatter(IHttpContextAccessor httpContextAccessor) : base(nameof(CartoLoggerFormatter))
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public override void Write<TState>(in LogEntry<TState> logEntry, IExternalScopeProvider? scopeProvider, TextWriter textWriter)
        {
            var logRecord = new Dictionary<string, object?>
            {
                { "Timestamp", DateTimeOffset.UtcNow },
                { "LogLevel", logEntry.LogLevel.ToString() },
                { "Category", logEntry.Category },
                { "Message", logEntry.Formatter?.Invoke(logEntry.State, logEntry.Exception) }
            };

            if (logEntry.Exception != null)
            {
                logRecord.Add("Exception", logEntry.Exception?.ToString());
            }

            // Error Code & Error Message
            //logRecord.Add("EventId", DateTimeOffset.UtcNow);
            // LoggerName

            // Essaye d'ajouter des infos HttpContext si dispo
            var context = _httpContextAccessor.HttpContext;
            if (context != null)
            {
                string requestId = GetRequestId(context);

                logRecord["RequestId"] = requestId; // marquer la diff Request-Id/X-OAPI-Request-Id ?
                logRecord["Method"] = context.Request?.Method;
            }

            /*
               // 🔹 Ajout du contexte HTTP si dispo
        var context = _httpContextAccessor.HttpContext;
        if (context != null)
        {
            logRecord["TraceId"] = context.TraceIdentifier;
            logRecord["RequestPath"] = context.Request?.Path.Value;
            logRecord["Method"] = context.Request?.Method;
            logRecord["RemoteIp"] = context.Connection.RemoteIpAddress?.ToString();
        }

        // 🔹 Ajout du contexte Telemetry via Activity
        var activity = Activity.Current;
        if (activity != null)
        {
            logRecord["ActivityId"] = activity.Id;
            logRecord["SpanId"] = activity.SpanId.ToString();
            logRecord["TraceIdTelemetry"] = activity.TraceId.ToString();
            logRecord["ParentSpanId"] = activity.ParentSpanId.ToString();
        }
            */
/*
var json = JsonSerializer.Serialize(logRecord);
textWriter.WriteLine(json);
        }

        private string GetRequestId(HttpContext httpContext)
{
    string requestId = string.Empty;

    if (httpContext == null || httpContext.Request == null || httpContext.Request.Headers == null)
        return requestId;

    var oapiRequestHeader = httpContext.Request.Headers.Where(x => x.Key == "X-OAPI-Request-Id");
    if (oapiRequestHeader != null && oapiRequestHeader.Any())
        requestId = oapiRequestHeader.First().Value.ToString();

    var requestHeader = httpContext.Request.Headers.Where(x => x.Key == "Request-Id");
    if (requestHeader != null && requestHeader.Any())
        requestId = requestHeader.First().Value.ToString();

    return requestId;
}

    }
}

*/
/*

{
  "Timestamp":"2025-09-30T10:15:23.456Z",
  "LogLevel":"Information",
  "Category":"Program",
  "EventId":0,
  "Message":"Hello World with custom JSON logger!",
  "Exception":null,
  "TraceId":"0HMSF12ABCDEF",
  "RequestPath":"/",
  "Method":"GET",
  "RemoteIp":"127.0.0.1"
}

log est écrit hors contexte HTTP (par un background service par ex.), alors les propriétés liées au HttpContext seront simplement absentes.

👉 Veux-tu que je t’étende cet exemple pour intégrer directement la partie Microsoft.Extensions.Telemetry (ex : propagation de trace, Activity, correlation ID, etc.), ou tu préfères que je garde ce logger simple en JSON ?

You said:
Je veux bien cette version aussi
ChatGPT said:
Parfait 🚀 !
On va enrichir le Custom JSON Log Formatter avec Microsoft.Extensions.Telemetry (introduit en .NET 8) afin d’ajouter automatiquement les infos de trace / correlation issues d’Activity (via System.Diagnostics).

🔹 1. Formatter JSON enrichi avec Telemetry
using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

public class JsonHttpContextTelemetryLogFormatter : ConsoleFormatter
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public JsonHttpContextTelemetryLogFormatter(IHttpContextAccessor httpContextAccessor)
        : base(nameof(JsonHttpContextTelemetryLogFormatter))
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public override void Write<TState>(
        in LogEntry<TState> logEntry,
        IExternalScopeProvider? scopeProvider,
        TextWriter textWriter)
    {
        var logRecord = new Dictionary<string, object?>
        {
            ["Timestamp"] = DateTimeOffset.UtcNow,
            ["LogLevel"] = logEntry.LogLevel.ToString(),
            ["Category"] = logEntry.Category,
            ["EventId"] = logEntry.EventId.Id,
            ["Message"] = logEntry.Formatter?.Invoke(logEntry.State, logEntry.Exception),
            ["Exception"] = logEntry.Exception?.ToString()
        };

        // 🔹 Ajout du contexte HTTP si dispo
        var context = _httpContextAccessor.HttpContext;
        if (context != null)
        {
            logRecord["TraceId"] = context.TraceIdentifier;
            logRecord["RequestPath"] = context.Request?.Path.Value;
            logRecord["Method"] = context.Request?.Method;
            logRecord["RemoteIp"] = context.Connection.RemoteIpAddress?.ToString();
        }

        // 🔹 Ajout du contexte Telemetry via Activity
        var activity = Activity.Current;
        if (activity != null)
        {
            logRecord["ActivityId"] = activity.Id;
            logRecord["SpanId"] = activity.SpanId.ToString();
            logRecord["TraceIdTelemetry"] = activity.TraceId.ToString();
            logRecord["ParentSpanId"] = activity.ParentSpanId.ToString();
        }

        var json = JsonSerializer.Serialize(logRecord);
        textWriter.WriteLine(json);
    }
}
🔹 2. Configuration dans Program.cs
using Microsoft.Extensions.Logging.Console;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();

// Ajout du custom formatter JSON + telemetry
builder.Logging.ClearProviders();
builder.Logging.AddConsole(options =>
{
    options.FormatterName = nameof(JsonHttpContextTelemetryLogFormatter);
});
builder.Services.AddSingleton<ConsoleFormatter, JsonHttpContextTelemetryLogFormatter>();

// 🔹 Active la Telemetry (OpenTelemetry-like mais light)
builder.Services.AddTelemetry();

var app = builder.Build();

app.MapGet("/", (ILogger<Program> logger) =>
{
    logger.LogInformation("Hello World with telemetry logging!");
    return "Hello World!";
});

app.Run();
🔹 3. Exemple de sortie JSON enrichie
Avec requête HTTP et Activity actif :

{
  "Timestamp":"2025-09-30T11:00:45.789Z",
  "LogLevel":"Information",
  "Category":"Program",
  "EventId":0,
  "Message":"Hello World with telemetry logging!",
  "Exception":null,
  "TraceId":"0HMSF12XYZ123",
  "RequestPath":"/",
  "Method":"GET",
  "RemoteIp":"127.0.0.1",
  "ActivityId":"00-4bf92f3577b34da6a3ce929d0e0e4736-00f067aa0ba902b7-01",
  "SpanId":"00f067aa0ba902b7",
  "TraceIdTelemetry":"4bf92f3577b34da6a3ce929d0e0e4736",
  "ParentSpanId":"0000000000000000"
}
Si le log est déclenché hors requête ou sans Activity, les propriétés correspondantes disparaissent simplement ✅.

👉 Veux-tu que je pousse encore plus loin l’intégration, par exemple en branchant ça sur OpenTelemetry Exporter (OTLP) pour envoyer les logs/metrics/traces à Jaeger ou Grafana Tempo ?

*/


            /*

            namespace Logging;

            using System.Text.Json;
            using System.Text.Json.Serialization;
            using Microsoft.Extensions.Logging.Console;

            public class CustomConsoleJsonFormatter : ConsoleFormatter
            {
                private static readonly JsonSerializerOptions _prettyOptions = new()
                {
                    WriteIndented = true,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                };

                public CustomConsoleJsonFormatter() : base("custom-json") { }

                public override void Write<TState>(
                    in LogEntry<TState> logEntry,
                    IExternalScopeProvider? scopeProvider,
                    TextWriter textWriter)
                {
                    var payload = new Dictionary<string, object?>
                    {
                        ["timestamp"] = DateTime.UtcNow.ToString("o"),
                        ["level"] = logEntry.LogLevel.ToString(),
                        ["category"] = logEntry.Category,
                        ["message"] = logEntry.Formatter(logEntry.State, logEntry.Exception)
                    };

                    if (logEntry.Exception is not null)
                        payload["exception"] = logEntry.Exception.ToString();

                    // Si tu veux inclure les scopes (leur gestion est prête ici, désactivée pour l’instant)
                    /*
                    scopeProvider?.ForEachScope((scope, state) =>
                    {
                        state["scope"] = scope;
                    }, payload);
                    */

            //string json = JsonSerializer.Serialize(payload, _prettyOptions);
            //textWriter.WriteLine(json);
            //    }
            //}

            /*
            using Microsoft.AspNetCore.Http;
        using Microsoft.Extensions.Logging;
        using Microsoft.Extensions.Logging.Abstractions;
        using Microsoft.Extensions.Logging.Console;
        using System.Runtime.Intrinsics.X86;
        using System.Text.Json;

        namespace OrangeCarto.Common.Config
        {
            /// <summary>
            /// CartoLoggerFormatter is a JSON logs serializer fully based on Microsoft.Extensions.Logging & Microsoft.Extensions.Telemetry Can use the HttpContext(IHttpContextAccessor) when set
            /// </summary>
            public class CartoLoggerFormatter : ConsoleFormatter
            {

                private readonly IHttpContextAccessor _httpContextAccessor;

                public CartoLoggerFormatter(IHttpContextAccessor httpContextAccessor) : base(nameof(CartoLoggerFormatter))
                {
                    _httpContextAccessor = httpContextAccessor;
                }

                public override void Write<TState>(in LogEntry<TState> logEntry, IExternalScopeProvider? scopeProvider, TextWriter textWriter)
                {
                    var logRecord = new Dictionary<string, object?>
                    {
                        { "Timestamp", DateTimeOffset.UtcNow },
                        { "LogLevel", logEntry.LogLevel.ToString() },
                        { "Category", logEntry.Category },
                        { "Message", logEntry.Formatter?.Invoke(logEntry.State, logEntry.Exception) }
                    };

                    if (logEntry.Exception != null)
                    {
                        logRecord.Add("Exception", logEntry.Exception?.ToString());
                    }

                    // Error Code & Error Message
                    //logRecord.Add("EventId", DateTimeOffset.UtcNow);
                    // LoggerName

                    // Essaye d'ajouter des infos HttpContext si dispo
                    var context = _httpContextAccessor.HttpContext;
                    if (context != null)
                    {
                        string requestId = GetRequestId(context);

                        logRecord["RequestId"] = requestId; // marquer la diff Request-Id/X-OAPI-Request-Id ?
                        logRecord["Method"] = context.Request?.Method;
                    }

                    /*
                       // 🔹 Ajout du contexte HTTP si dispo
                var context = _httpContextAccessor.HttpContext;
                if (context != null)
                {
                    logRecord["TraceId"] = context.TraceIdentifier;
                    logRecord["RequestPath"] = context.Request?.Path.Value;
                    logRecord["Method"] = context.Request?.Method;
                    logRecord["RemoteIp"] = context.Connection.RemoteIpAddress?.ToString();
                }

                // 🔹 Ajout du contexte Telemetry via Activity
                var activity = Activity.Current;
                if (activity != null)
                {
                    logRecord["ActivityId"] = activity.Id;
                    logRecord["SpanId"] = activity.SpanId.ToString();
                    logRecord["TraceIdTelemetry"] = activity.TraceId.ToString();
                    logRecord["ParentSpanId"] = activity.ParentSpanId.ToString();
                }
                    */
/*
            var json = JsonSerializer.Serialize(logRecord);
textWriter.WriteLine(json);
        }

        private string GetRequestId(HttpContext httpContext)
{
    string requestId = string.Empty;

    if (httpContext == null || httpContext.Request == null || httpContext.Request.Headers == null)
        return requestId;

    var oapiRequestHeader = httpContext.Request.Headers.Where(x => x.Key == "X-OAPI-Request-Id");
    if (oapiRequestHeader != null && oapiRequestHeader.Any())
        requestId = oapiRequestHeader.First().Value.ToString();

    var requestHeader = httpContext.Request.Headers.Where(x => x.Key == "Request-Id");
    if (requestHeader != null && requestHeader.Any())
        requestId = requestHeader.First().Value.ToString();

    return requestId;
}

    }
}
*/

/*

{
  "Timestamp":"2025-09-30T10:15:23.456Z",
  "LogLevel":"Information",
  "Category":"Program",
  "EventId":0,
  "Message":"Hello World with custom JSON logger!",
  "Exception":null,
  "TraceId":"0HMSF12ABCDEF",
  "RequestPath":"/",
  "Method":"GET",
  "RemoteIp":"127.0.0.1"
}

log est écrit hors contexte HTTP (par un background service par ex.), alors les propriétés liées au HttpContext seront simplement absentes.

👉 Veux-tu que je t’étende cet exemple pour intégrer directement la partie Microsoft.Extensions.Telemetry (ex : propagation de trace, Activity, correlation ID, etc.), ou tu préfères que je garde ce logger simple en JSON ?

You said:
Je veux bien cette version aussi
ChatGPT said:
Parfait 🚀 !
On va enrichir le Custom JSON Log Formatter avec Microsoft.Extensions.Telemetry (introduit en .NET 8) afin d’ajouter automatiquement les infos de trace / correlation issues d’Activity (via System.Diagnostics).

🔹 1. Formatter JSON enrichi avec Telemetry
using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

public class JsonHttpContextTelemetryLogFormatter : ConsoleFormatter
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public JsonHttpContextTelemetryLogFormatter(IHttpContextAccessor httpContextAccessor)
        : base(nameof(JsonHttpContextTelemetryLogFormatter))
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public override void Write<TState>(
        in LogEntry<TState> logEntry,
        IExternalScopeProvider? scopeProvider,
        TextWriter textWriter)
    {
        var logRecord = new Dictionary<string, object?>
        {
            ["Timestamp"] = DateTimeOffset.UtcNow,
            ["LogLevel"] = logEntry.LogLevel.ToString(),
            ["Category"] = logEntry.Category,
            ["EventId"] = logEntry.EventId.Id,
            ["Message"] = logEntry.Formatter?.Invoke(logEntry.State, logEntry.Exception),
            ["Exception"] = logEntry.Exception?.ToString()
        };

        // 🔹 Ajout du contexte HTTP si dispo
        var context = _httpContextAccessor.HttpContext;
        if (context != null)
        {
            logRecord["TraceId"] = context.TraceIdentifier;
            logRecord["RequestPath"] = context.Request?.Path.Value;
            logRecord["Method"] = context.Request?.Method;
            logRecord["RemoteIp"] = context.Connection.RemoteIpAddress?.ToString();
        }

        // 🔹 Ajout du contexte Telemetry via Activity
        var activity = Activity.Current;
        if (activity != null)
        {
            logRecord["ActivityId"] = activity.Id;
            logRecord["SpanId"] = activity.SpanId.ToString();
            logRecord["TraceIdTelemetry"] = activity.TraceId.ToString();
            logRecord["ParentSpanId"] = activity.ParentSpanId.ToString();
        }

        var json = JsonSerializer.Serialize(logRecord);
        textWriter.WriteLine(json);
    }
}
🔹 2. Configuration dans Program.cs
using Microsoft.Extensions.Logging.Console;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();

// Ajout du custom formatter JSON + telemetry
builder.Logging.ClearProviders();
builder.Logging.AddConsole(options =>
{
    options.FormatterName = nameof(JsonHttpContextTelemetryLogFormatter);
});
builder.Services.AddSingleton<ConsoleFormatter, JsonHttpContextTelemetryLogFormatter>();

// 🔹 Active la Telemetry (OpenTelemetry-like mais light)
builder.Services.AddTelemetry();

var app = builder.Build();

app.MapGet("/", (ILogger<Program> logger) =>
{
    logger.LogInformation("Hello World with telemetry logging!");
    return "Hello World!";
});

app.Run();
🔹 3. Exemple de sortie JSON enrichie
Avec requête HTTP et Activity actif :

{
  "Timestamp":"2025-09-30T11:00:45.789Z",
  "LogLevel":"Information",
  "Category":"Program",
  "EventId":0,
  "Message":"Hello World with telemetry logging!",
  "Exception":null,
  "TraceId":"0HMSF12XYZ123",
  "RequestPath":"/",
  "Method":"GET",
  "RemoteIp":"127.0.0.1",
  "ActivityId":"00-4bf92f3577b34da6a3ce929d0e0e4736-00f067aa0ba902b7-01",
  "SpanId":"00f067aa0ba902b7",
  "TraceIdTelemetry":"4bf92f3577b34da6a3ce929d0e0e4736",
  "ParentSpanId":"0000000000000000"
}
Si le log est déclenché hors requête ou sans Activity, les propriétés correspondantes disparaissent simplement ✅.

👉 Veux-tu que je pousse encore plus loin l’intégration, par exemple en branchant ça sur OpenTelemetry Exporter (OTLP) pour envoyer les logs/metrics/traces à Jaeger ou Grafana Tempo ?

*/
