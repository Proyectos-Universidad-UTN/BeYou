using AutoMapper;
using Microsoft.Extensions.Logging;
using BeYou.Application.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BeYou.Application.Configuration;

public static class Configuration
{
    public static void ConfigureGeneralServicesIoc(this IServiceCollection services)
    {
        services.AddScoped(typeof(ICoreService<>), typeof(CoreService<>));
    }

    /// <summary>
    /// Configure all elements of Application layer
    /// </summary>
    /// <param name="services">Service collection configuration</param>
    public static void ConfigureApplication(this IServiceCollection services)
    {
        services.AddTransient<IServiceBranch, ServiceBranch>();
        services.AddTransient<IServiceBranchHoliday, ServiceBranchHoliday>();
        services.AddTransient<IServiceBranchSchedule, ServiceBranchSchedule>();
        services.AddTransient<IServiceBranchScheduleBlock, ServiceBranchScheduleBlock>();
        services.AddTransient<IServiceCanton, ServiceCanton>();
        services.AddTransient<IServiceCategory, ServiceCategory>();
        services.AddTransient<IServiceCustomer, ServiceCustomer>();
        services.AddTransient<IServiceDistrict, ServiceDistrict>();
        services.AddTransient<IServiceHoliday, ServiceHoliday>();
        services.AddTransient<IServiceIdentity, ServiceIdentity>();
        services.AddTransient<IServiceInventory, ServiceInventory>();
        services.AddTransient<IServiceInventoryProduct, ServiceInventoryProduct>();
        services.AddTransient<IServiceInventoryProductTransaction, ServiceInventoryProductTransaction>();
        services.AddTransient<IServiceInvoice, ServiceInvoice>();
        services.AddTransient<IServiceInvoiceDetail, ServiceInvoiceDetail>();
        services.AddTransient<IServiceOrder, ServiceOrder>();
        services.AddTransient<IServicePaymentType, ServicePaymentType>();
        services.AddTransient<IServiceProduct, ServiceProduct>();
        services.AddTransient<IServiceProvince, ServiceProvince>();
        services.AddTransient<IServiceReservation, ServiceReservation>();
        services.AddTransient<IServiceReservationDetail, ServiceReservationDetail>();
        services.AddTransient<IServiceReservationQuestion, ServiceReservationQuestion>();
        services.AddTransient<IServiceRole, ServiceRole>();
        services.AddTransient<IServiceSchedule, ServiceSchedule>();
        services.AddTransient<IServiceService, ServiceService>();
        services.AddTransient<IServiceTax, ServiceTax>();
        services.AddTransient<IServiceTypeService, ServiceTypeService>();
        services.AddTransient<IServiceUnitMeasure, ServiceUnitMeasure>();
        services.AddTransient<IServiceUser, ServiceUser>();
        services.AddTransient<IServiceUserBranch, ServiceUserBranch>();
        services.AddTransient<IServiceVendor, ServiceVendor>();

        services.AddScoped<IServiceUserContext, ServiceUserContext>();
        services.AddScoped<IServiceUserAuthorization, ServiceUserAuthorization>();
    }
}
