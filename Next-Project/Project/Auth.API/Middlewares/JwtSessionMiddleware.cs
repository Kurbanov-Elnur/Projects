using Auth.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Auth.API.Middlewares;

public class JwtSessionMiddleware : IMiddleware
{
    private readonly ITokenService _tokenService;

    public JwtSessionMiddleware(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var endpoint = context.GetEndpoint();
        var authorizeAttribute = endpoint?.Metadata?.GetMetadata<AuthorizeAttribute>();
        Console.WriteLine(authorizeAttribute);

        if (authorizeAttribute != null)
        {
            string? token = context.Request.Cookies["accessToken"];

            if (token != null)
            {
                var res = await _tokenService.IsTokenAuthorized(token);
                if (!res)
                {
                    context.Response.StatusCode = 403;
                    await context.Response.WriteAsync("Unauthorized");
                    return;
                }
            }
        }

        await next(context);
    }
}