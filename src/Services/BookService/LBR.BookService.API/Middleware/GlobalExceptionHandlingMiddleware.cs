using LBR.Shared.Common.Models;
using System.Net;
using System.Text.Json;

namespace LBR.BookService.API.Middleware;

public class GlobalExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

    public GlobalExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<GlobalExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        
        var response = exception switch
        {
            ArgumentException => new
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest,
                Message = exception.Message,
                Success = false
            },
            KeyNotFoundException => new
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound,
                Message = exception.Message,
                Success = false
            },
            UnauthorizedAccessException => new
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized,
                Message = "Unauthorized access.",
                Success = false
            },
            _ => new
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError,
                Message = "An internal server error occurred. Please try again later.",
                Success = false
            }
        };

        var jsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        await context.Response.WriteAsync(jsonResponse);
    }
}
