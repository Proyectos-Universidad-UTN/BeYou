using BeYou.Application.Validations;
using FluentValidation.AspNetCore;
using FluentValidation;

namespace BeYou.WebAPI.Configuration;

/// <summary>
/// Fluent validation configuration extension class
/// </summary>
public static class FluentValidationConfiguration
{
    /// <summary>
    /// Add configuration for fluent validations
    /// </summary>
    /// <param name="services">Service collection</param>
    public static void ConfigureFluentValidation(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddValidatorsFromAssemblyContaining<BranchValidator>();

        services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
    }
}
