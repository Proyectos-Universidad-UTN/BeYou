using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BeYou.WebAPI.Swagger;

/// <summary>
/// Operation filter to clean and include only required parameters per endpoint
/// </summary>
public class CleanOperationFilter : IOperationFilter
{
    /// <summary>
    /// Apply filtering
    /// </summary>
    /// <param name="operation">Operation definition</param>
    /// <param name="context">Context for the operation filter</param>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Parameters = operation.Parameters
            .Where(p => p.In == ParameterLocation.Header || p.In == ParameterLocation.Query || p.In == ParameterLocation.Path || p.In == ParameterLocation.Cookie)
            .ToList();
    }
}