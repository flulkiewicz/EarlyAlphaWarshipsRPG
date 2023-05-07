using System.Diagnostics;

namespace WarshipsRPGAlpha.Middleware
{
    public class RequestTimeMiddleware : IMiddleware
    {
        private readonly ILogger<RequestTimeMiddleware> _logger;
        private Stopwatch _requestTime;


        public RequestTimeMiddleware(ILogger<RequestTimeMiddleware> logger)
        {
            _logger = logger;
            _requestTime = new Stopwatch();
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            _requestTime.Start();

            await next.Invoke(context);

            _requestTime.Stop();

            if (_requestTime.ElapsedMilliseconds > 4000)
                _logger.LogWarning($"{context.Request.Method}||{context.Request.Path} took {_requestTime.ElapsedMilliseconds}ms");
        }
    }
}
