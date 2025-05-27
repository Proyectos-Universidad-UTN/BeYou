using AutoMapper;
using KeyedSemaphores;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using BeYou.Infrastructure;
using BeYou.Domain.Exceptions;
using BeYou.Application.Dtos.Request;
using BeYou.Application.Dtos.Response;
using BeYou.Domain.Core.Specifications;
using BeYou.Application.Core.Interfaces;
using BeYou.Application.Services.Interfaces;

namespace BeYou.Application.Services.Implementations;

public class ServiceOrder(ICoreService<Order> coreService, IServiceReservation serviceReservation,
                            IMapper mapper, IValidator<Order> orderValidator) : IServiceOrder
{
    /// <inheritdoc />
    public async Task<ResponseOrderDto> CreateOrderAsync(RequestOrderDto orderDto)
    {
        var order = await ValidateOrderAsync(orderDto);
        Order result = null!;

        if (!await serviceReservation.ExistsReservationAsync(orderDto.ReservationId)) throw new NotFoundException("Reserva no existe.");

        var reservation = await serviceReservation.FindByIdAsync(orderDto.ReservationId);
        if (reservation == null) throw new NotFoundException("Reserva no existe.");

        using var keyedSemaphore = await KeyedSemaphore.LockAsync($"CreateOrder-{reservation.Id}");

        var executionStrategy = coreService.UnitOfWork.CreateExecutionStrategy();
        await executionStrategy.ExecuteAsync(async () =>
        {
            using var transaction = await coreService.UnitOfWork.BeginTransactionAsync();
            try
            {
                var reservationMapped = mapper.Map<Reservation>(reservation);

                reservationMapped!.Status = "A";
                order.BranchId = reservationMapped.BranchId;

                result = await coreService.UnitOfWork.Repository<Order>().AddAsync(order);
                if (result == null) throw new NotFoundException("Pedido no creado.");

                coreService.UnitOfWork.Repository<Reservation>().Update(reservationMapped);

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        });

        return mapper.Map<ResponseOrderDto>(result);
    }

    public async Task<bool> ExistsOrderAsync(long id)
    {
        return await coreService.UnitOfWork.Repository<Order>().ExistsAsync(id);
    }

    /// <inheritdoc />
    public async Task<ResponseOrderDto> FindByIdAsync(long id)
    {
        var spec = new BaseSpecification<Order>(x => x.Id == id);

        var order = await coreService.UnitOfWork.Repository<Order>().FirstOrDefaultAsync(spec);
        if (order == null) throw new NotFoundException("Proforma no encontrada.");

        return mapper.Map<ResponseOrderDto>(order);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseOrderDto>> ListAllAsync()
    {
        var orders = await coreService.UnitOfWork.Repository<Order>().ListAllAsync();

        return mapper.Map<ICollection<ResponseOrderDto>>(orders);
    }

    /// <summary>
    /// Validate order
    /// </summary>
    /// <param name="orderDto">Order request model to be added</param>
    /// <returns>Order</returns>
    private async Task<Order> ValidateOrderAsync(RequestOrderDto orderDto)
    {
        var order = mapper.Map<Order>(orderDto);
        await orderValidator.ValidateAndThrowAsync(order);

        return order;
    }
}
