using AutoMapper;
using FluentValidation;
using BeYou.Infrastructure;
using BeYou.Domain.Exceptions;
using BeYou.Application.Dtos.Request;
using BeYou.Application.Dtos.Response;
using BeYou.Domain.Core.Specifications;
using BeYou.Application.Core.Interfaces;
using BeYou.Application.Services.Interfaces;

namespace BeYou.Application.Services.Implementations;

public class ServiceReservationDetail(ICoreService<ReservationDetail> coreService, IMapper mapper,
                                    IValidator<ReservationDetail> detalleReservaValidator) : IServiceReservationDetail
{
    /// <inheritdoc />
    public async Task<bool> CreateReservationDetailAsync(long reservationId, IEnumerable<RequestReservationDetailDto> reservationDetails)
    {
        var validatedReservationDetails = await ValidateReservationDetailAsync(reservationId, reservationDetails);

        var results = await coreService.UnitOfWork.Repository<ReservationDetail>().AddRangeAsync(validatedReservationDetails.ToList());
        await coreService.UnitOfWork.SaveChangesAsync();
        if (results == null) throw new ListNotAddedException("Error al guardar detalles de reserva.");

        return true;
    }

    /// <inheritdoc />
    public async Task<ResponseReservationDetailDto?> FindByIdAsync(long id)
    {
        var spec = new BaseSpecification<ReservationDetail>(x => x.Id == id);
        var reservationDetail = await coreService.UnitOfWork.Repository<ReservationDetail>().FirstOrDefaultAsync(spec);

        if (reservationDetail == null) throw new NotFoundException("Detalle de reserva no encontrado.");

        return mapper.Map<ResponseReservationDetailDto>(reservationDetail);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseReservationDetailDto>> ListAllByReservationAsync(long reservationId)
    {
        var spec = new BaseSpecification<ReservationDetail>(x => x.ReservationId == reservationId);
        var reservationDetails = await coreService.UnitOfWork.Repository<ReservationDetail>().ListAsync(spec);

        return mapper.Map<ICollection<ResponseReservationDetailDto>>(reservationDetails);
    }

    /// <summary>
    /// Validate reservation details to be added
    /// </summary>
    /// <param name="reservationId">Branch id</param>
    /// <param name="reservationDetails">List of reservation details to be validated</param>
    /// <returns>IEnumerable of ReservationDetail</returns>
    private async Task<IEnumerable<ReservationDetail>> ValidateReservationDetailAsync(long reservationId, IEnumerable<RequestReservationDetailDto> reservationDetails)
    {
        var mappedReservationDetails = mapper.Map<List<ReservationDetail>>(reservationDetails);
        foreach (var item in mappedReservationDetails)
        {
            item.ReservationId = reservationId;
            await detalleReservaValidator.ValidateAndThrowAsync(item);
        }
        return mappedReservationDetails;
    }
}