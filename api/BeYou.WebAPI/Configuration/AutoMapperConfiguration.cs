using BeYou.Infraestructure.Mappings.Mapper;

namespace BeYou.WebAPI.Configuration;

/// <summary>
/// Configuration extension for auto mapper
/// </summary>
public static class AutoMapperConfiguration
{
    /// <summary>
    /// Extension method to configure auto mapper
    /// </summary>
    /// <param name="services">Collection of services to use as extension</param>
    public static void ConfigureAutoMapper(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddAutoMapper(typeof(ModelToDtoApplicationProfile));
        services.AddAutoMapper(typeof(DtoToModelApplicationProfile));
    }
}