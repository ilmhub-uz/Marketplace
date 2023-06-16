namespace Marketplace.Services.Chat.Middleware;

// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
public class OrganizationErrorHandlerMiddleware
{
    private readonly ILogger<OrganizationErrorHandlerMiddleware> _logger;
    private readonly RequestDelegate _next;

    public OrganizationErrorHandlerMiddleware(ILogger<OrganizationErrorHandlerMiddleware> logger, RequestDelegate next)
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
            _logger.LogError(e,  "Internal ORGANIZATION server error!");
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
    public static IApplicationBuilder UseChatErrorMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<OrganizationErrorHandlerMiddleware>();
    }
}