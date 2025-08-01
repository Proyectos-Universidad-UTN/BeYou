using AutoMapper;
using BeYou.Application.Dtos.Request;
using BeYou.Domain.Core.Models;
using BeYou.Domain.Models;
using BeYou.Infraestructure.Mappings.ValueResolvers;

namespace BeYou.Infraestructure.Mappings.Mapper;

public class DtoToModelApplicationProfile : Profile
{
    public DtoToModelApplicationProfile()
    {
        CreateMap<RequestBaseDto, BaseEntity>()
            .ForMember(m => m.CreatedBy, opts =>
            {
                opts.MapFrom<CurrentUserIdResolverAdd>();
            })
            .ForMember(m => m.UpdatedBy, opts =>
            {
                opts.MapFrom<CurrentUserIdResolverModify>();
            });

        CreateMap<RequestUserDto, User>()
           .IncludeBase<RequestBaseDto, BaseEntity>();

        CreateMap<RequestProductDto, Product>()
            .IncludeBase<RequestBaseDto, BaseEntity>();

        CreateMap<RequestBranchDto, Branch>()
            .IncludeBase<RequestBaseDto, BaseEntity>();

        CreateMap<RequestServiceDto, Service>()
            .IncludeBase<RequestBaseDto, BaseEntity>();

        CreateMap<RequestHolidayDto, Holiday>()
            .IncludeBase<RequestBaseDto, BaseEntity>();

        CreateMap<RequestScheduleDto, Schedule>()
            .IncludeBase<RequestBaseDto, BaseEntity>();

        CreateMap<RequestInventoryDto, Inventory>()
            .IncludeBase<RequestBaseDto, BaseEntity>();

        CreateMap<RequestInventoryProductDto, InventoryProduct>()
            .IncludeBase<RequestBaseDto, BaseEntity>();

        CreateMap<RequestInvoiceDto, Invoice>()
            .IncludeBase<RequestBaseDto, BaseEntity>();

        CreateMap<RequestOrderDto, Order>()
            .IncludeBase<RequestBaseDto, BaseEntity>();

        CreateMap<RequestReservationDto, Reservation>()
            .IncludeBase<RequestBaseDto, BaseEntity>();

        CreateMap<RequestReservationQuestionDto, ReservationQuestion>()
            .IncludeBase<RequestBaseDto, BaseEntity>();

        CreateMap<RequestInventoryProductTransactionDto, InventoryProductTransaction>()
            .IncludeBase<RequestBaseDto, BaseEntity>();

        CreateMap<RequestVendorDto, Vendor>()
            .IncludeBase<RequestBaseDto, BaseEntity>();

        CreateMap<RequestTaxDto, Tax>()
            .IncludeBase<RequestBaseDto, BaseEntity>();

        CreateMap<RequestUnitMeasureDto, UnitMeasure>()
            .IncludeBase<RequestBaseDto, BaseEntity>();

        CreateMap<RequestTypeServiceDto, TypeService>();
        CreateMap<RequestBranchScheduleDto, BranchSchedule>();
        CreateMap<RequestBranchScheduleBlockDto, BranchScheduleBlock>();
        CreateMap<RequestBranchHolidayDto, BranchHoliday>();
        CreateMap<RequestInvoiceDetailDto, InvoiceDetail>();
        CreateMap<RequestOrderDetailDto, OrderDetail>();
        CreateMap<RequestReservationDetailDto, ReservationDetail>();
        CreateMap<RequestUserBranchDto, UserBranch>();
        CreateMap<RequestCustomerDto, Customer>()
    .IncludeBase<RequestBaseDto, BaseEntity>();
    }
}
