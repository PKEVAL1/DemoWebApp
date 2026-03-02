using System.Diagnostics;

namespace DemoWebApp.Middleware
{
    /// <summary>
    /// Custom middleware that logs HTTP request method, path,
    /// response status code, and elapsed execution time (ms).
    /// </summary>
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next   = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var sw = Stopwatch.StartNew();

            try
            {
                await _next(context);
            }
            finally
            {
                sw.Stop();

                _logger.LogInformation(
                    "[{Method}] {Path} → {StatusCode} ({ElapsedMs}ms)",
                    context.Request.Method,
                    context.Request.Path,
                    context.Response.StatusCode,
                    sw.ElapsedMilliseconds);
            }
        }
    }

    /// <summary>
    /// Extension method to register RequestLoggingMiddleware cleanly.
    /// </summary>
    public static class RequestLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder app)
            => app.UseMiddleware<RequestLoggingMiddleware>();
    }
}
