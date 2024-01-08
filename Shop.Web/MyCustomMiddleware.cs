namespace Shop.Web
{
    public class MyCustomMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly ILogger _logger;
        public MyCustomMiddleware(RequestDelegate requestDelegate, ILoggerFactory logger)
        {
            _requestDelegate = requestDelegate;
            _logger = logger.CreateLogger("shopLogger");
        }
        public async Task Invoke(HttpContext context)
        {
            _logger.Log(LogLevel.Information, "Start custom middleware");
            await _requestDelegate.Invoke(context);
            _logger.Log(LogLevel.Information, "End custom middleware");
        }
    }
}
