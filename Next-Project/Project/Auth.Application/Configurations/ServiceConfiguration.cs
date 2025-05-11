using Microsoft.Extensions.DependencyInjection;
using Auth.Application.Services;
using Auth.Application.Validators;
using Auth.Domain.Interfaces;
using Auth.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Auth.Application.Configurations;

public static class ServiceConfiguration
{
    public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IProductService, ProductService>();

        services.AddScoped<SignInValidator>();
        services.AddScoped<SignUpValidator>();

        var connectionString = configuration.GetConnectionString("Local");
        services.AddDbContext<AuthContext>(options => { options.UseSqlServer(connectionString); });
    }
}