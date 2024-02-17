namespace Wallet.Tracker.API.Middlewares;

using System.Net;
using System.Text.Json;
using Wallet.Tracker.Domain.Services;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            var logger = context.RequestServices.GetRequiredService<ILogger<ExceptionHandlingMiddleware>>();
            logger.LogError(exception: error, message: error.Message + error.StackTrace);

            var response = context.Response;
            response.ContentType = "application/json";

            switch (error)
            {
                case CustomException e:
                    // custom application error
                    response.StatusCode = e.StatusCode;
                    var errorResult = new
                    {
                        TraceId = context.TraceIdentifier,
                        message = e.Message
                    };
                    await response.WriteAsJsonAsync(errorResult);
                    break;
                case KeyNotFoundException e:
                    // not found error
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                default:
                    // unhandled error
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            var result = JsonSerializer.Serialize(new { message = error?.Message });
            await response.WriteAsync(result);
        }
    }
}
