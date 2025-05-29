using BeYou.Application.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeYou.Infraestructure.Repositories;
/// <summary>
/// Configuration class for the infrastructure layer
/// </summary>
public static class Configuration
{
    /// <summary>
    /// Extension method to configure the infrastructure layer
    /// </summary>
    /// <param name="services">Collection of services</param>
    public static void ConfigureInfrastructureIoC(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
