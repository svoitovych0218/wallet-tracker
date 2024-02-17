namespace Wallet.Tracker.API.Middlewares;

public class TraceIdentifierLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public TraceIdentifierLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ILogger<TraceIdentifierLoggingMiddleware> logger)
    {
        using (logger.BeginScope($"TraceIdentifier: {context.TraceIdentifier}"))
        {
            await _next(context);
        }
    }
}
