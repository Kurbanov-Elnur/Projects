using Auth.API.Middlewares;
using Auth.Application.Configurations;
using Auth.Infrastructure.Configurations.Authentication;
using Auth.Infrastructure.Configurations.Security;
using Auth.Infrastructure.Configurations.ApiVersioning;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddHttpClient("Client")
    .ConfigurePrimaryHttpMessageHandler(() =>
    {
        return new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
        };
    });

builder.Services.AddEndpointsApiExplorer();

builder.Services.ConfigureCors();
builder.Services.ConfigureCookiePolicy();
builder.Services.ConfigureAuthentication(builder.Configuration);
builder.Services.ConfigureServices(builder.Configuration);
builder.Services.ConfigureApiVersioning();

builder.Services.AddScoped<JwtSessionMiddleware>();
builder.Services.AddScoped<GlobalExceptionsMiddleware>();

builder.Services.AddHttpClient("MyClient");

builder.Services.AddAuthorization();

builder.Services.AddControllers();

var app = builder.Build();

app.UseCors();

app.UseCookiePolicy();

app.MapControllers();

app.MapOpenApi();
app.MapScalarApiReference();
app.UseHttpsRedirection();
app.UseMiddleware<GlobalExceptionsMiddleware>();
app.UseMiddleware<JwtSessionMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", context =>
{
    context.Response.Redirect("/scalar");
    return Task.CompletedTask;
});

app.Run();