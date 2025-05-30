using Asp.Versioning.ApiExplorer;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace BeYou.WebAPI.Swagger;

/// <summary>
/// Swagger configuration class extension
/// </summary>
public static class SwaggerConfiguration
{
    /// <summary>
    /// Configure swagger operations
    /// </summary>
    /// <param name="services">Service collection</param>
    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(opts =>
        {
            opts.CustomSchemaIds(type => type.Name);
            opts.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using Bearer schema",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer"
            });
            opts.AddSecurityRequirement(new OpenApiSecurityRequirement{
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });

            opts.OperationFilter<AuthorizeOperationFilter>();
            opts.OperationFilter<CleanOperationFilter>();
            opts.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
        });

        services.AddSwaggerGenNewtonsoftSupport();

        services.ConfigureOptions<SwaggerOptionConfiguration>();
    }

    /// <summary>
    /// Load swagger options
    /// </summary>
    /// <param name="app">WebApplication builder</param>
    public static void LoadSwagger(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(opts =>
        {
            var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
            var groupNames = from description in apiVersionDescriptionProvider.ApiVersionDescriptions
                             select description.GroupName;

            foreach (var groupName in groupNames)
            {
                opts.SwaggerEndpoint($"/swagger/{groupName}/swagger.json", groupName.ToUpperInvariant());
            }
        });
    }
}