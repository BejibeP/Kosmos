using Bejibe.Kosmos.Api.Infrastructure.Middleware;

namespace Bejibe.Kosmos.Api.Extensions.Application
{
    public static class RequestMetadataMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestMetadataMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<RequestMetadataMiddleware>();
        }
    }
}
