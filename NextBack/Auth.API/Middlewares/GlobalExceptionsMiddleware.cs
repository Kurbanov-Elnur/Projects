using System.Net;
using Auth.Shared.Exceptions;
using Newtonsoft.Json;

namespace Auth.API.Middlewares;

public class GlobalExceptionsMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (AuthException ex)
        {
            await HandleExceptionAsync(context, ex);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, AuthException exception)
    {
        var code = HttpStatusCode.InternalServerError;

        switch (exception.AuthErrorType)
        {
            case AuthErrorTypes.InvalidCredentials:
                code = HttpStatusCode.Unauthorized;
                break;
            case AuthErrorTypes.UserNotFound:
                code = HttpStatusCode.NotFound;
                break;
            case AuthErrorTypes.InvalidRequest:
                code = HttpStatusCode.BadRequest;
                break;
            case AuthErrorTypes.InvalidToken:
                code = (HttpStatusCode)498;
                break;
            case AuthErrorTypes.PasswordMismatch:
                code = HttpStatusCode.Unauthorized;
                break;
            case AuthErrorTypes.EmailAlreadyConfirmed:
                code = HttpStatusCode.NoContent;
                break;
            case AuthErrorTypes.EmailNotConfirmed:
                code = HttpStatusCode.Unauthorized;
                break;
        }

        var result = JsonConvert.SerializeObject(new { error = exception.Message });
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        return context.Response.WriteAsync(result);
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;

        var result = JsonConvert.SerializeObject(new { error = exception.Message });
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        return context.Response.WriteAsync(result);
    }
}