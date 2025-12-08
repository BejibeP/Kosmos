using Kosmos.Common;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging.Console;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Bejibe.Kosmos.Api.Infrastructure.Logging
{
    public class JsonFormatter : ConsoleFormatter
    {
        public const string formatterName = "ks-json";

        private static readonly JsonSerializerOptions _prettyOptions = new()
        {
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        public JsonFormatter() : base(formatterName) { }

        public override void Write<TState>(in LogEntry<TState> logEntry, IExternalScopeProvider? scopeProvider, TextWriter textWriter)
        {
            // Generic values
            var payload = new Dictionary<string, object?>
            {
                [ApiConstantes.Timestamp] = DateTime.UtcNow, // add custom timestamp format ?
                [ApiConstantes.Level] = logEntry.LogLevel.ToString(),
                [ApiConstantes.Category] = logEntry.Category,
                [ApiConstantes.Message] = logEntry.Formatter(logEntry.State, logEntry.Exception)
            };

            // in standard log : Timestamp, InternalRequestId + ExternalRequestId, Method (RequestPath?), ErrorCode/Exception -> erreur

            // Better RequestId : W3C Trace Context
            // traceparent: 00-4bf92f3577b34da6a3ce929d0e0e4736-00f067aa0ba902b7-01
            // var internalId = Guid.CreateVersion7();

            // Generic Logging Scopes
            string[] requestScopes =
            [
                ApiConstantes.RequestId,
                ApiConstantes.InternalRequestId,
                ApiConstantes.ExternalRequestId
            ]; // Deport as const

            var extraPayload = ExtractKeysFromScopeProvider(scopeProvider, requestScopes, null);

            foreach (var item in extraPayload)
                payload.Add(item.Key, item.Value);

            // Tracing Values
            if (logEntry.LogLevel == LogLevel.Trace)
            {
                payload[ApiConstantes.TraceDetails] = new Dictionary<string, object?>
                {
                    [ApiConstantes.ThreadId] = Environment.CurrentManagedThreadId,
                    [ApiConstantes.MachineName] = Environment.MachineName,
                    [ApiConstantes.ProcessId] = Environment.ProcessId
                };

                extraPayload = ExtractKeysFromScopeProvider(scopeProvider, null, requestScopes);

                foreach (var item in extraPayload)
                    payload.Add(item.Key, item.Value);

            }

            // If Exception
            if (logEntry.Exception is not null)
                payload[ApiConstantes.Exception] = logEntry.Exception.ToString();

            // serialize and output
            var json = JsonSerializer.Serialize(payload, _prettyOptions);
            textWriter.WriteLine(json);

        }

        private static Dictionary<string, object?> ExtractKeysFromScopeProvider(IExternalScopeProvider? scopeProvider, IEnumerable<string>? keysToInclude = null, IEnumerable<string>? keysToIgnore = null)
        {
            if (scopeProvider is null) return [];

            var resultScopes = new Dictionary<string, object?>();

            bool hasIncludeKeys = keysToInclude != null && keysToInclude.Count() != 0;
            bool hasIgnoreKeys = keysToIgnore != null && keysToIgnore.Count() != 0;

            scopeProvider.ForEachScope((scopeObject, state) =>
            {
                if (scopeObject is not IReadOnlyCollection<KeyValuePair<string, object?>> scope) return;

                foreach (var keyValue in scope)
                {
                    if (hasIgnoreKeys && keysToIgnore.Contains(keyValue.Key))
                        continue;

                    if (hasIncludeKeys && !keysToInclude.Contains(keyValue.Key))
                        continue;

                    state[keyValue.Key] = keyValue.Value;
                }

            }, resultScopes);

            return resultScopes;
        }

    }
}
