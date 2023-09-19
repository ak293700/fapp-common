using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace FappCommon.Middlewares;

public class LogTraceMiddleware : IMiddleware
{
    private readonly ILogger<LogTraceMiddleware> _logger;

    public LogTraceMiddleware(ILogger<LogTraceMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        await next(context);

        LogLevel logLevel;
        HttpResponse response = context.Response;
        context.Response.OnCompleted(() =>
        {
            switch (response.StatusCode)
            {
                case >= 200 and < 300:
                    return Task.CompletedTask;
                case < 200 or < 400: // [0-199] + [300-399]
                    logLevel = LogLevel.Warning;
                    break;
                case < 500: // [400-499]
                    logLevel = LogLevel.Error;
                    break;
                default: // > 500
                    logLevel = LogLevel.Critical;
                    break;
            }

            HttpRequest request = context.Request;
            _logger.Log(logLevel,
                """Request at "{Path}" failed with status code {StatusCode}""",
                request.Path, response.StatusCode);

            return Task.CompletedTask;
        });
    }
}

public static class LogTraceMiddlewareExtensions
{
    public static IApplicationBuilder UseLogTraceMiddleware(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<LogTraceMiddleware>();
    }
}