namespace API.Middlewares
{
    public class RequestLoggingMiddleware : IMiddleware
    {
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(ILogger<RequestLoggingMiddleware> logger)
            => _logger = logger;

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var username = GetMyUsername(context);
            _logger.LogInformation($"Request: {context.Request.Method} {context.Request.Path} - User: {username}");

            await next(context);
        }

        private static string GetMyUsername(HttpContext httpContext)
        {
            if (httpContext?.User?.Identity?.Name != null)
            {
                return httpContext.User.Identity.Name;
            }
            return string.Empty;
        }
    }
}
