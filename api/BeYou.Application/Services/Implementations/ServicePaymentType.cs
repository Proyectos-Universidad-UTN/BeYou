using AutoMapper;
using BeYou.Infrastructure;
using BeYou.Application.Dtos.Response;
using BeYou.Application.Core.Interfaces;
using BeYou.Application.Services.Interfaces;

namespace BeYou.Application.Services.Implementations;

public class ServicePaymentType(ICoreService<PaymentType> coreService, IMapper mapper) : IServicePaymentType
{
    /// <inheritdoc />
    public async Task<ICollection<ResponsePaymentTypeDto>> ListAllAsync()
    {
        var paymentTypes = await coreService.UnitOfWork.Repository<PaymentType>().ListAllAsync();
        return mapper.Map<ICollection<ResponsePaymentTypeDto>>(paymentTypes);
    }
}
