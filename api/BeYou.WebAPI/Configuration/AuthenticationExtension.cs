using BeYou.Application.Configuration.Authentication;
using BeYou.Application.Services.Implementations;
using BeYou.Application.Services.Interfaces;
using BeYou.Infraestructure.Mappings.ValueResolvers;
using BeYou.WebAPI.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BeYou.WebAPI.Configuration;

/// <summary>
/// Authentication extension class
/// </summary>
public static class AuthenticationExtension
{
    /// <summary>
    /// Configure authentication JWT
    /// </summary>
    /// <param name="services">Service collection</param>
    /// <param name="configuration">Configuration settings</param>
    public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var authenticationConfigurationSection = configuration.GetSection("AuthenticationConfiguration");
        ArgumentNullException.ThrowIfNull(authenticationConfigurationSection);

        services.Configure<AuthenticationConfiguration>(authenticationConfigurationSection);
        var authenticationConfiguration = authenticationConfigurationSection.Get<AuthenticationConfiguration>();

        ArgumentNullException.ThrowIfNull(authenticationConfiguration);

        services.AddSingleton(authenticationConfiguration);
        services.AddTransient<IServiceIdentity, ServiceIdentity>();

        var JwtSecretkey = Encoding.ASCII.GetBytes(authenticationConfiguration.JwtSettings_Secret);
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(JwtSecretkey),
            ValidateIssuer = false,
            ValidateAudience = false,
            RequireExpirationTime = false,
            ValidateLifetime = true
        };

        services.AddSingleton(tokenValidationParameters);

        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = tokenValidationParameters;
        });

        services.AddTransient<CurrentUserIdResolverAdd>();
        services.AddTransient<CurrentUserIdResolverModify>();
        services.AddScoped<IAuthorizationHandler, UserIdentityHandler>();
    }
}