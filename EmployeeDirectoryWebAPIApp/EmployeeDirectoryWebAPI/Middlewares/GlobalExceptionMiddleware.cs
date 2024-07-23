using EmployeeDirectory.Models;
using System.Net;
using System.Text.Json;

namespace EmployeeDirectory.UI.Middlewares;

public class GlobalExceptionMiddleware : IMiddleware
{
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(ILogger<GlobalExceptionMiddleware> logger)
    {
        _logger = logger;
    }


    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            ApiResponse<string> response = new ApiResponse<string>(
                isSuccess: false,
                statusCode: (int)(HttpStatusCode.InternalServerError),
                data: null,
                errorMessage: ex.Message
                ) ;

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
