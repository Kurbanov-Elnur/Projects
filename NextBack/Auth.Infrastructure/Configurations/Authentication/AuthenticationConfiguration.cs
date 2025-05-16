using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using Auth.Shared.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Auth.Infrastructure.Configurations.Authentication;

public static class AuthenticationConfiguration
{
    public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                RoleClaimType = ClaimTypes.Role,
                ValidateActor = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                RequireExpirationTime = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = configuration.GetSection("Jwt:Issuer").Value,
                ValidAudience = configuration.GetSection("Jwt:Audience").Value,
                IssuerSigningKey =
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("Jwt:Key").Value))
            };

            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    var accessToken = context.HttpContext.Request.Cookies["accessToken"];
                    if (!string.IsNullOrEmpty(accessToken))
                    {
                        context.Token = accessToken;
                    }

                    return Task.CompletedTask;
                },
                OnAuthenticationFailed = async context =>
                {
                    if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                    {
                        var httpContext = context.HttpContext;
                        var accessToken =
                            httpContext.Request.Cookies["accessToken"];

                        var refreshToken =
                            httpContext.Request.Cookies["refreshToken"];

                        if (!string.IsNullOrEmpty(refreshToken))
                        {
                            var refreshEndpoint =
                                $"{httpContext.Request.Scheme}://{httpContext.Request.Host}/api/v1/Auth/Refresh";
                            var client = httpContext.RequestServices.GetRequiredService<IHttpClientFactory>()
                                .CreateClient();

                            var response = await client.PostAsJsonAsync(refreshEndpoint,
                                new TokenDto(accessToken, refreshToken));

                            if (response.IsSuccessStatusCode)
                            {
                                var newTokens = await response.Content.ReadFromJsonAsync<RefreshDto>();
                                if (newTokens != null)
                                {
                                    httpContext.Response.Cookies.Append("accessToken", newTokens.AccessToken,
                                        new CookieOptions { HttpOnly = true });
                                    httpContext.Response.Cookies.Append("refreshToken", newTokens.RefreshToken,
                                        new CookieOptions { HttpOnly = true });

                                    httpContext.Request.Headers["Authorization"] = $"Bearer {newTokens.AccessToken}";

                                    var newToken = new JwtSecurityToken(newTokens.AccessToken);
                                    var principal = new ClaimsPrincipal(new ClaimsIdentity(newToken.Claims, "jwt"));

                                    context.Principal = principal;
                                    context.Success();
                                }
                            }
                        }
                    }
                }
            };
        });
    }
}