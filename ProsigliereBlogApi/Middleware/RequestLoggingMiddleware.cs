namespace ProsigliereBlogApi.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(
            RequestDelegate next,
            ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            _logger.LogInformation(
                "HTTP {Method} {Path} started",
                context.Request.Method,
                context.Request.Path);

            var start = DateTime.UtcNow;

            await _next(context);

            var elapsed = DateTime.UtcNow - start;

            _logger.LogInformation(
                "HTTP {Method} {Path} finished in {Elapsed}ms with status {StatusCode}",
                context.Request.Method,
                context.Request.Path,
                elapsed.TotalMilliseconds,
                context.Response.StatusCode);
        }
    }
}
