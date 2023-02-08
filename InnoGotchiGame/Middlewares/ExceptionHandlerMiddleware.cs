using System.Net;
using System.Text.Json;

namespace InnoGotchiGame.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionMessageAsync(context.Response, ex);
        }
    }

    private async Task HandleExceptionMessageAsync(HttpResponse response, Exception ex)
    {
        response.ContentType = "application/json";
        response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var message = new
        {
            StatusCode = response.StatusCode,
            ErrorMessage = ex.Message
        };

        await response.WriteAsJsonAsync(message);
    }
}
