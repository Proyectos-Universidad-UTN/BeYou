﻿using AutoMapper;
using FluentValidation;
using KeyedSemaphores;
using Microsoft.EntityFrameworkCore;
using BeYou.Domain.Exceptions;
using BeYou.Application.Enums;
using BeYou.Application.Dtos.Request;
using BeYou.Application.Dtos.Response;
using BeYou.Domain.Core.Specifications;
using BeYou.Application.Core.Interfaces;
using BeYou.Application.Services.Interfaces;
using BeYou.Domain.Models;
using BeYou.Domain.Enums;

namespace BeYou.Application.Services.Implementations;

public class ServiceBranchSchedule(ICoreService<BranchSchedule> coreService, IMapper mapper,
                                    IValidator<BranchSchedule> branchScheduleValidator) : IServiceBranchSchedule
{
    private readonly string[] BranchScheduleWithBranch = ["Branch"];
    private readonly string[] BranchScheduleWithBlocks = ["BranchScheduleBlocks"];
    private readonly string[] BranchScheduleWithBranchScheduleAndBlocks = ["Branch", "Schedule", "BranchScheduleBlocks"];
    private readonly string[] BranchScheduleWithScheduleAndBlocks = ["Schedule", "BranchScheduleBlocks"];

    /// <inheritdoc />
    public async Task<bool> CreateBranchScheduleAsync(long branchId, IEnumerable<RequestBranchScheduleDto> branchSchedules)
    {
        var schedules = await ValidateHorarios(branchId, branchSchedules);

        var spec = new BaseSpecification<BranchSchedule>(x => x.BranchId == branchId);

        spec.ApplyNoTracking(true); 

        var existingBranchSchedules = await coreService.UnitOfWork.Repository<BranchSchedule>().ListAsync(spec, BranchScheduleWithBlocks);

        using var keyedSemaphore = await KeyedSemaphore.LockAsync($"AssignSchedules-{branchId}");

        var executionStrategy = coreService.UnitOfWork.CreateExecutionStrategy();
        await executionStrategy.ExecuteAsync(async () =>
        {
            using var transaction = await coreService.UnitOfWork.BeginTransactionAsync();
            try
            {
                coreService.UnitOfWork.Repository<BranchSchedule>().Delete(existingBranchSchedules);

                var existingBySchedule = existingBranchSchedules.ToDictionary(x => x.ScheduleId);

                foreach (var item in schedules)
                {
                    item.Branch = null;

                    if (existingBySchedule.TryGetValue(item.ScheduleId, out var branchSchedule))
                    {
                        if (item.BranchScheduleBlocks == null || !item.BranchScheduleBlocks.Any())
                        {
                            var existingBlocks = branchSchedule.BranchScheduleBlocks.Select(
                                x => new BranchScheduleBlock
                                {
                                    StartHour = x.StartHour,
                                    EndHour = x.EndHour,
                                    Active = x.Active
                                }).ToList();
                            item.BranchScheduleBlocks = existingBlocks;
                        }
                    }

                    if (item.BranchScheduleBlocks != null && item.BranchScheduleBlocks.Any())
                    {
                        foreach (var block in item.BranchScheduleBlocks)
                        {
                            block.BranchSchedule = null; 
                        }
                    }
                }

               

                //schedules.ForEach(m =>
                //{
                //    var existing = existingBranchSchedules.FirstOrDefault(x => x.ScheduleId == m.ScheduleId && x.BranchId == m.BranchId);

                //    if (existing != null)
                //    {
                //        foreach (var block in existing.BranchScheduleBlocks.ToList())
                //        {
                //            coreService.UnitOfWork.Repository<BranchScheduleBlock>().Delete(block);
                //        }

                //        var blocksToAssign = existing.BranchScheduleBlocks
                //            .Select(x => new BranchScheduleBlock
                //            {
                //                StartHour = x.StartHour,
                //                EndHour = x.EndHour,
                //                Active = x.Active
                //            }).ToList();

                //        m.BranchScheduleBlocks = blocksToAssign;
                //    }
                //    //m.Branch= null;

                //    //foreach(var block in m.BranchScheduleBlocks)
                //    //{
                //    //    block.BranchSchedule = null; 
                //    //}
                //});

                //coreService.UnitOfWork.Repository<BranchSchedule>().Delete(existingBranchSchedules);
                await coreService.UnitOfWork.Repository<BranchSchedule>().AddRangeAsync(schedules);
                await coreService.UnitOfWork.SaveChangesAsync();

                await transaction.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();

                var errorDetail = $"Error: {ex.Message}";
                if (ex.InnerException != null)
                {
                    errorDetail += $"\nInner: {ex.InnerException.Message}";
                }

                throw new BeYouException($"Se ha presentado un error al momento de asignar los horarios en la sucursal.\nDetalles: {errorDetail}");
            }
        });

        return true;
    }

    /// <inheritdoc />
    public async Task<ResponseBranchScheduleDto?> FindByIdAsync(long id)
    {
        var spec = new BaseSpecification<BranchSchedule>(x => x.Id == id);
        var branchSchedule = await coreService.UnitOfWork.Repository<BranchSchedule>().FirstOrDefaultAsync(spec, BranchScheduleWithBranchScheduleAndBlocks);
        if (branchSchedule == null) throw new NotFoundException("Horario en sucursal no encontrado.");

        return mapper.Map<ResponseBranchScheduleDto>(branchSchedule);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseBranchScheduleDto>> ListAllByBranchAsync(long branchId)
    {
        var spec = new BaseSpecification<BranchSchedule>(x => x.BranchId == branchId);
        var branchSchedules = await coreService.UnitOfWork.Repository<BranchSchedule>().ListAsync(spec, BranchScheduleWithBlocks);

        return mapper.Map<ICollection<ResponseBranchScheduleDto>>(branchSchedules);
    }

    /// <inheritdoc />
    public async Task<ResponseBranchScheduleDto> FindByWeekDayAsync(long branchId, WeekDayApplication weekDay)
    {
        var spec = new BaseSpecification<BranchSchedule>(x => x.BranchId == branchId && x.Schedule.Day == mapper.Map<WeekDay>(weekDay));
        var branchSchedule = await coreService.UnitOfWork.Repository<BranchSchedule>().FirstOrDefaultAsync(spec, BranchScheduleWithScheduleAndBlocks);

        if (branchSchedule == null) throw new NotFoundException("No se encontró horario en la sucursal.");

        return mapper.Map<ResponseBranchScheduleDto>(branchSchedule);
    }

    /// <summary>
    /// Validate schedules
    /// </summary>
    /// <param name="branchId">Branch id to receiVe schedules that need to be validated</param>
    /// <param name="branchSchedules">List of Branch's schedules request that need validation</param>
    /// <returns>IEnumerable of BranchSchedule</returns>
    private async Task<List<BranchSchedule>> ValidateHorarios(long branchId, IEnumerable<RequestBranchScheduleDto> branchSchedules)
    {
        var existingSchedules = mapper.Map<List<BranchSchedule>>(branchSchedules);
        foreach (var item in existingSchedules)
        {
            item.BranchId = branchId;
            await branchScheduleValidator.ValidateAndThrowAsync(item);
        }
        return existingSchedules;
    }
}
