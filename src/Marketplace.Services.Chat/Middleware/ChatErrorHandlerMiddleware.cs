namespace Marketplace.Services.Chat.Middleware;

// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
public class ChatErrorHandlerMiddleware
{
    private readonly ILogger<ChatErrorHandlerMiddleware> _logger;
    private readonly RequestDelegate _next;

    public ChatErrorHandlerMiddleware(ILogger<ChatErrorHandlerMiddleware> logger, RequestDelegate next)
    {
        _logger = logger;
        _next = next;
    }

    public async Task Invoke( HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);

        }
        catch (Exception e)
        {
            _logger.LogError(e,  "Internal PRODUCT server error!");
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await httpContext.Response.WriteAsJsonAsync(new
            {
                Error = e.Message
            });
        }
    }
}

// Extension method used to add the middleware to the HTTP request pipeline.
public static class ErrorMiddlewareExtensions
{
    public static IApplicationBuilder UseChatErrorHandlerMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ChatErrorHandlerMiddleware>();
    }
}