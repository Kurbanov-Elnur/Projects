using Microsoft.Extensions.DependencyInjection;

namespace Auth.Infrastructure.Configurations.ApiVersioning;

public static class ApiVersioningConfiguration
{
    public static void ConfigureApiVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(options => { options.ReportApiVersions = true; }).AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });
    }
}