
using BeYou.Domain.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BeYou.Infraestructure.Mappings.Mapper;

public class ModelToDtoApplicationProfile : Profile
{
    public ModelToDtoApplicationProfile()
    {
        CreateMap<User, ResponseUserDto>()
            .ForMember(dest => dest.Role, inp => inp.MapFrom(ori => ori.RoleIdNavigation))
            .ForMember(dest => dest.Gender, inp => inp.MapFrom(ori => ori.GenderIdNavigation))
            .ForMember(dest => dest.District, inp => inp.MapFrom(ori => ori.DistrictIdNavigation));
        CreateMap<Product, ResponseProductDto>()
            .ForMember(dest => dest.UnitMeasure, inp => inp.MapFrom(ori => ori.UnitMeasureIdNavigation))
            .ForMember(dest => dest.Category, inp => inp.MapFrom(ori => ori.CategoryIdNavigation));
        CreateMap<UnitMeasure, ResponseUnitMeasureDto>();
        CreateMap<Role, ResponseRoleDto>();
        CreateMap<Category, ResponseCategoryDto>();
        CreateMap<Invoice, ResponseInvoiceDto>()
            .ForMember(dest => dest.Customer, inp => inp.MapFrom(ori => ori.CustomerIdNavigation))
            .ForMember(dest => dest.PaymentType, inp => inp.MapFrom(ori => ori.PaymentTypeIdNavigation))
            .ForMember(dest => dest.Branch, inp => inp.MapFrom(ori => ori.BranchIdNavigation))
            .ForMember(dest => dest.Tax, inp => inp.MapFrom(ori => ori.TaxIdNavigation))
            .ForMember(dest => dest.Order, inp => inp.MapFrom(ori => ori.OrderIdNavigation));
        CreateMap<Order, ResponseOrderDto>()
           .ForMember(dest => dest.Customer, inp => inp.MapFrom(ori => ori.CustomerIdNavigation))
           .ForMember(dest => dest.PaymentType, inp => inp.MapFrom(ori => ori.PaymentTypeIdNavigation))
           .ForMember(dest => dest.Branch, inp => inp.MapFrom(ori => ori.BranchIdNavigation))
           .ForMember(dest => dest.Tax, inp => inp.MapFrom(ori => ori.TaxIdNavigation))
           .ForMember(dest => dest.Reservation, inp => inp.MapFrom(ori => ori.ReservationIdNavigation));
        CreateMap<Branch, ResponseBranchDto>()
            .ForMember(dest => dest.District, inp => inp.MapFrom(ori => ori.DistrictIdNavigation));
        CreateMap<BranchHoliday, ResponseBranchHolidayDto>()
             .ForMember(dest => dest.Holiday, inp => inp.MapFrom(ori => ori.HolidayIdNavigation))
             .ForMember(dest => dest.Branch, inp => inp.MapFrom(ori => ori.BranchIdNavigation));
        CreateMap<ReservationQuestion, ResponseReservationQuestionDto>()
            .ForMember(dest => dest.Reservation, inp => inp.MapFrom(ori => ori.ReservationIdNavigation));
        CreateMap<Reservation, ResponseReservationDto>()
            .ForMember(dest => dest.Branch, inp => inp.MapFrom(ori => ori.BranchIdNavigation))
            .ForMember(dest => dest.Customer, inp => inp.MapFrom(ori => ori.CustomerIdNavigation));
        CreateMap<BranchSchedule, ResponseBranchScheduleDto>()
            .ForMember(dest => dest.Schedule, inp => inp.MapFrom(ori => ori.ScheduleIdNavigation))
            .ForMember(dest => dest.Branch, inp => inp.MapFrom(ori => ori.BranchIdNavigation));
        CreateMap<BranchScheduleBlock, ResponseBranchScheduleBlockDto>()
            .ForMember(dest => dest.BranchSchedule, inp => inp.MapFrom(ori => ori.BranchScheduleIdNavigation));
        CreateMap<Schedule, ResponseScheduleDto>();
        CreateMap<District, ResponseDistrictDto>()
            .ForMember(dest => dest.Canton, inp => inp.MapFrom(ori => ori.CantonIdNavigation));
        CreateMap<ReservationDetail, ResponseReservationDetailDto>()
            .ForMember(dest => dest.Reservation, inp => inp.MapFrom(ori => ori.ReservationIdNavigation))
            .ForMember(dest => dest.Product, inp => inp.MapFrom(ori => ori.ProductIdNavigation))
            .ForMember(dest => dest.Service, inp => inp.MapFrom(ori => ori.ServiceIdNavigation));
        CreateMap<Service, ResponseServiceDto>()
            .ForMember(dest => dest.TypeService, inp => inp.MapFrom(ori => ori.TypeServiceIdNavigation));
        CreateMap<Canton, ResponseCantonDto>()
            .ForMember(dest => dest.Province, inp => inp.MapFrom(ori => ori.ProvinceIdNavigation));
        CreateMap<Contact, ResponseContactDto>()
            .ForMember(dest => dest.Vendor, inp => inp.MapFrom(ori => ori.VendorIdNavigation));
        CreateMap<InvoiceDetail, ResponseInvoiceDetailDto>()
            .ForMember(dest => dest.Invoice, inp => inp.MapFrom(ori => ori.InvoiceIdNavigation))
            .ForMember(dest => dest.Service, inp => inp.MapFrom(ori => ori.ServiceIdNavigation));
        CreateMap<OrderDetail, ResponseOrderDetailDto>()
            .ForMember(dest => dest.Order, inp => inp.MapFrom(ori => ori.OrderIdNavigation))
            .ForMember(dest => dest.Service, inp => inp.MapFrom(ori => ori.ServiceIdNavigation));
        CreateMap<InvoiceDetailProduct, ResponseInvoiceDetailProductDto>()
            .ForMember(dest => dest.InvoiceDetail, inp => inp.MapFrom(ori => ori.InvoiceDetailIdNavigation))
            .ForMember(dest => dest.Product, inp => inp.MapFrom(ori => ori.ProductIdNavigation));
        CreateMap<OrderDetailProduct, ResponseOrderDetailProductDto>()
            .ForMember(dest => dest.OrderDetail, inp => inp.MapFrom(ori => ori.OrderDetailIdNavigation))
            .ForMember(dest => dest.Product, inp => inp.MapFrom(ori => ori.ProductIdNavigation));
        CreateMap<Holiday, ResponseHolidayDto>();
        CreateMap<Gender, ResponseGenderDto>();
        CreateMap<Tax, ResponseTaxDto>();
        CreateMap<Inventory, ResponseInventoryDto>()
            .ForMember(dest => dest.Branch, inp => inp.MapFrom(ori => ori.BranchIdNavigation));
        CreateMap<InventoryProduct, ResponseInventoryProductDto>()
            .ForMember(dest => dest.Inventory, inp => inp.MapFrom(ori => ori.InventoryIdNavigation))
            .ForMember(dest => dest.Product, inp => inp.MapFrom(ori => ori.ProductIdNavigation));
        CreateMap<Vendor, ResponseVendorDto>()
             .ForMember(dest => dest.District, inp => inp.MapFrom(ori => ori.DistrictIdNavigation));
        CreateMap<Province, ResponseProvinceDto>();
        CreateMap<PaymentType, ResponsePaymentTypeDto>();
        CreateMap<TypeService, ResponseTypeServiceDto>();
        CreateMap<UserBranch, ResponseUserBranchDto>()
             .ForMember(dest => dest.Branch, inp => inp.MapFrom(ori => ori.BranchIdNavigation))
             .ForMember(dest => dest.User, inp => inp.MapFrom(ori => ori.UserIdNavigation));
        CreateMap<Customer, ResponseCustomerDto>()
              .ForMember(dest => dest.District, inp => inp.MapFrom(ori => ori.DistrictIdNavigation));
        CreateMap<InventoryProductTransaction, ResponseInventoryProductTransactionDto>()
            .ForMember(dest => dest.InventoryProduct, inp => inp.MapFrom(ori => ori.InventoryProductIdNavigation));
    }
}
