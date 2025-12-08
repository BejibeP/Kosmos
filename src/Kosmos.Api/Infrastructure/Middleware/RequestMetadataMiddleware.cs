namespace Bejibe.Kosmos.Api.Infrastructure.Middleware
{
    using global::Kosmos.Common;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// This middleware job is to intercept request headers and metadata obtained with the api call
    /// The middleware will then load the metadate it found in a logging scope provider
    /// This middleware job is to intercept the request headers and other metadata obtained with the api call and loading them into the logging provider
    /// </summary>
    public class RequestMetadataMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestMetadataMiddleware> _logger;

        public RequestMetadataMiddleware(RequestDelegate next, ILogger<RequestMetadataMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var metadata = new Dictionary<string, object?>();

            // if we want another internal requestId https://github.com/dotnet/corefx/blob/master/src/System.Diagnostics.DiagnosticSource/src/HierarchicalRequestId.md

            // Obtain the ExternalRequestId from headers
            if (context.Request.Headers.TryGetValue(ApiConstantes.ExternalRequestId, out var externalRequestId))
                metadata[ApiConstantes.ExternalRequestId] = externalRequestId;

            // Obtain the X-Forwarded-For from headers
            if (context.Request.Headers.TryGetValue(ApiConstantes.XForwardedFor, out var xForwardedFor))
                metadata[ApiConstantes.XForwardedFor] = xForwardedFor;

            // Obtain the CustomFlag from headers
            if (context.Request.Headers.TryGetValue(ApiConstantes.XCustomFlag, out var customFlag))
                metadata[ApiConstantes.XCustomFlag] = customFlag;

            // Obtain the IP Adress from context
            var remoteIpAddress = context.Connection.RemoteIpAddress?.ToString();
            if (!string.IsNullOrWhiteSpace(remoteIpAddress))
                metadata[ApiConstantes.IpAdresss] = remoteIpAddress;

            // Then we load every metadata collected into a Logger Scope to print them to the logs
            using (_logger.BeginScope(metadata))
            {
                await _next(context);
            }

        }
    }
}
