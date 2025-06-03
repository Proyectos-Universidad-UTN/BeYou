using AutoMapper;
using BeYou.Application.Dtos.Response;
using BeYou.Domain.Models;

namespace BeYou.Infraestructure.Mappings.Mapper;

public class ModelToDtoApplicationProfile : Profile
{
    public ModelToDtoApplicationProfile()
    {
        CreateMap<User, ResponseUserDto>()
            .ForMember(dest => dest.Role, inp => inp.MapFrom(ori => ori.Role))
            .ForMember(dest => dest.Gender, inp => inp.MapFrom(ori => ori.Gender))
            .ForMember(dest => dest.District, inp => inp.MapFrom(ori => ori.District));
        CreateMap<Product, ResponseProductDto>()
            .ForMember(dest => dest.UnitMeasure, inp => inp.MapFrom(ori => ori.UnitMeasure))
            .ForMember(dest => dest.Category, inp => inp.MapFrom(ori => ori.Category));
        CreateMap<UnitMeasure, ResponseUnitMeasureDto>();
        CreateMap<Role, ResponseRoleDto>();
        CreateMap<Category, ResponseCategoryDto>();
        CreateMap<Invoice, ResponseInvoiceDto>()
            .ForMember(dest => dest.Customer, inp => inp.MapFrom(ori => ori.Customer))
            .ForMember(dest => dest.PaymentType, inp => inp.MapFrom(ori => ori.PaymentType))
            .ForMember(dest => dest.Branch, inp => inp.MapFrom(ori => ori.Branch))
            .ForMember(dest => dest.Tax, inp => inp.MapFrom(ori => ori.Tax))
            .ForMember(dest => dest.Order, inp => inp.MapFrom(ori => ori.Order));
        CreateMap<Order, ResponseOrderDto>()
           .ForMember(dest => dest.Customer, inp => inp.MapFrom(ori => ori.Customer))
           .ForMember(dest => dest.PaymentType, inp => inp.MapFrom(ori => ori.PaymentType))
           .ForMember(dest => dest.Branch, inp => inp.MapFrom(ori => ori.Branch))
           .ForMember(dest => dest.Tax, inp => inp.MapFrom(ori => ori.Tax))
           .ForMember(dest => dest.Reservation, inp => inp.MapFrom(ori => ori.Reservation));
        CreateMap<Branch, ResponseBranchDto>()
            .ForMember(dest => dest.District, inp => inp.MapFrom(ori => ori.District));
        CreateMap<BranchHoliday, ResponseBranchHolidayDto>()
             .ForMember(dest => dest.Holiday, inp => inp.MapFrom(ori => ori.Holiday))
             .ForMember(dest => dest.Branch, inp => inp.MapFrom(ori => ori.Branch));
        CreateMap<ReservationQuestion, ResponseReservationQuestionDto>()
            .ForMember(dest => dest.Reservation, inp => inp.MapFrom(ori => ori.Reservation));
        CreateMap<Reservation, ResponseReservationDto>()
            .ForMember(dest => dest.Branch, inp => inp.MapFrom(ori => ori.Branch))
            .ForMember(dest => dest.Customer, inp => inp.MapFrom(ori => ori.Customer));
        CreateMap<BranchSchedule, ResponseBranchScheduleDto>()
            .ForMember(dest => dest.Schedule, inp => inp.MapFrom(ori => ori.Schedule))
            .ForMember(dest => dest.Branch, inp => inp.MapFrom(ori => ori.Branch));
        CreateMap<BranchScheduleBlock, ResponseBranchScheduleBlockDto>()
            .ForMember(dest => dest.BranchSchedule, inp => inp.MapFrom(ori => ori.BranchSchedule));
        CreateMap<Schedule, ResponseScheduleDto>();
        CreateMap<District, ResponseDistrictDto>()
            .ForMember(dest => dest.Canton, inp => inp.MapFrom(ori => ori.Canton));
        CreateMap<ReservationDetail, ResponseReservationDetailDto>()
            .ForMember(dest => dest.Reservation, inp => inp.MapFrom(ori => ori.Reservation))
            .ForMember(dest => dest.Product, inp => inp.MapFrom(ori => ori.Product))
            .ForMember(dest => dest.Service, inp => inp.MapFrom(ori => ori.Service));
        CreateMap<Service, ResponseServiceDto>()
            .ForMember(dest => dest.TypeService, inp => inp.MapFrom(ori => ori.TypeService));
        CreateMap<Canton, ResponseCantonDto>()
            .ForMember(dest => dest.Province, inp => inp.MapFrom(ori => ori.Province));
        CreateMap<Contact, ResponseContactDto>()
            .ForMember(dest => dest.Vendor, inp => inp.MapFrom(ori => ori.Vendor));
        CreateMap<InvoiceDetail, ResponseInvoiceDetailDto>()
            .ForMember(dest => dest.Invoice, inp => inp.MapFrom(ori => ori.Invoice))
            .ForMember(dest => dest.Service, inp => inp.MapFrom(ori => ori.Service));
        CreateMap<OrderDetail, ResponseOrderDetailDto>()
            .ForMember(dest => dest.Order, inp => inp.MapFrom(ori => ori.Order))
            .ForMember(dest => dest.Service, inp => inp.MapFrom(ori => ori.Service));
        CreateMap<InvoiceDetailProduct, ResponseInvoiceDetailProductDto>()
            .ForMember(dest => dest.InvoiceDetail, inp => inp.MapFrom(ori => ori.InvoiceDetail))
            .ForMember(dest => dest.Product, inp => inp.MapFrom(ori => ori.Product));
        CreateMap<OrderDetailProduct, ResponseOrderDetailProductDto>()
            .ForMember(dest => dest.OrderDetail, inp => inp.MapFrom(ori => ori.OrderDetail))
            .ForMember(dest => dest.Product, inp => inp.MapFrom(ori => ori.Product));
        CreateMap<Holiday, ResponseHolidayDto>();
        CreateMap<Gender, ResponseGenderDto>();
        CreateMap<Tax, ResponseTaxDto>();
        CreateMap<Inventory, ResponseInventoryDto>()
            .ForMember(dest => dest.Branch, inp => inp.MapFrom(ori => ori.Branch));
        CreateMap<InventoryProduct, ResponseInventoryProductDto>()
            .ForMember(dest => dest.Inventory, inp => inp.MapFrom(ori => ori.Inventory))
            .ForMember(dest => dest.Product, inp => inp.MapFrom(ori => ori.Product));
        CreateMap<Vendor, ResponseVendorDto>()
             .ForMember(dest => dest.District, inp => inp.MapFrom(ori => ori.District));
        CreateMap<Province, ResponseProvinceDto>();
        CreateMap<PaymentType, ResponsePaymentTypeDto>();
        CreateMap<TypeService, ResponseTypeServiceDto>();
        CreateMap<UserBranch, ResponseUserBranchDto>()
             .ForMember(dest => dest.Branch, inp => inp.MapFrom(ori => ori.Branch))
             .ForMember(dest => dest.User, inp => inp.MapFrom(ori => ori.User));
        CreateMap<Customer, ResponseCustomerDto>()
              .ForMember(dest => dest.District, inp => inp.MapFrom(ori => ori.District));
        CreateMap<InventoryProductTransaction, ResponseInventoryProductTransactionDto>()
            .ForMember(dest => dest.InventoryProduct, inp => inp.MapFrom(ori => ori.InventoryProduct));
    }
}
