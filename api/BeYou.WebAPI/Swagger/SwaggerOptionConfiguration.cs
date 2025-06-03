using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BeYou.WebAPI.Swagger;

/// <summary>
/// Class for swagger option configuration
/// </summary>
public class SwaggerOptionConfiguration(IApiVersionDescriptionProvider apiVersionDescriptionProvider) : IConfigureNamedOptions<SwaggerGenOptions>
{
    /// <summary>
    /// Configure the general documentation for Swagger
    /// </summary>
    /// <param name="name">Name of the API</param>
    /// <param name="options">Swagger generation options</param>
    public void Configure(string? name, SwaggerGenOptions options) => Configure(options);

    /// <summary>
    /// Configure the general documentation for Swagger
    /// </summary>
    /// <param name="options">Swagger generation options</param>
    public void Configure(SwaggerGenOptions options)
    {
        if (options == null) return;

        foreach (var apiDescription in apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(apiDescription.GroupName, CreateVersionInfo(apiDescription));
        }
    }

    /// <summary>
    /// Create the exact version info for API
    /// </summary>
    /// <param name="apiVersionDescription">Api version description information</param>
    /// <returns>OpenApiInfo</returns>
    private static OpenApiInfo CreateVersionInfo(ApiVersionDescription apiVersionDescription)
    {
        if (apiVersionDescription == null) return new OpenApiInfo();

        var info = new OpenApiInfo
        {
            Version = apiVersionDescription.ApiVersion.ToString(),
            Title = "BeYou WebAPI",
            Description = "List of APIs to handle operations for the BeYou Web API application",
            Contact = new OpenApiContact
            {
                Name = "NOT DEFINED YET",
                Email = "PENDING"
            }
        };

        if (apiVersionDescription.IsDeprecated)
        {
            info.Description += "This API version has been deprecated. Please use one of the new APIs available from the explorer";
        }

        return info;
    }
}