using AutoMapper;
using BeYou.Domain.Exceptions;
using BeYou.Application.Dtos.Response;
using BeYou.Domain.Core.Specifications;
using BeYou.Application.Core.Interfaces;
using BeYou.Application.Services.Interfaces;
using BeYou.Domain.Models;

namespace BeYou.Application.Services.Implementations;

public class ServiceReservationQuestion(ICoreService<ReservationQuestion> coreService, IMapper mapper) : IServiceReservationQuestion
{
    /// <inheritdoc />
    public async Task<ResponseReservationQuestionDto> FindByIdAsync(long id)
    {
        if (!await coreService.UnitOfWork.Repository<ReservationQuestion>().ExistsAsync(id)) throw new NotFoundException("Pregunta no encontrada.");

        var spec = new BaseSpecification<ReservationQuestion>(x => x.Id == id);
        var reservationQuestion = await coreService.UnitOfWork.Repository<ReservationQuestion>().FirstOrDefaultAsync(spec);

        return mapper.Map<ResponseReservationQuestionDto>(reservationQuestion);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseReservationQuestionDto>> ListAllAsync()
    {
        var reservationQuestions = await coreService.UnitOfWork.Repository<ReservationQuestion>().ListAllAsync();
        return mapper.Map<ICollection<ResponseReservationQuestionDto>>(reservationQuestions);
    }
}
