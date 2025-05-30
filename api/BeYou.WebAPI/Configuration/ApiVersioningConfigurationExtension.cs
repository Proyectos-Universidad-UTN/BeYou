using Asp.Versioning;

namespace BeYou.WebAPI.Configuration;

/// <summary>
/// Api versioning configuration extension class
/// </summary>
public static class ApiVersioningConfigurationExtension
{
    /// <summary>
    /// Configure web api versioning
    /// </summary>
    /// <param name="services">Service collection</param>
    public static void ConfigureApiVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(opts =>
        {
            opts.DefaultApiVersion = new ApiVersion(1, 0);
            opts.AssumeDefaultVersionWhenUnspecified = true;
            opts.ReportApiVersions = true;
            opts.ApiVersionReader = new HeaderApiVersionReader("x-api-version");
        })
        .AddMvc()
        .AddApiExplorer(opts =>
        {
            opts.GroupNameFormat = "'v'VVV";
            opts.SubstituteApiVersionInUrl = true;
            opts.AssumeDefaultVersionWhenUnspecified = true;
            opts.DefaultApiVersion = new ApiVersion(1, 0);
        });
    }
}