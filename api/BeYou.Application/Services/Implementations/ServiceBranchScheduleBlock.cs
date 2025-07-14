using AutoMapper;
using FluentValidation;
using BeYou.Domain.Exceptions;
using BeYou.Application.Dtos.Request;
using BeYou.Application.Dtos.Response;
using BeYou.Domain.Core.Specifications;
using BeYou.Application.Core.Interfaces;
using BeYou.Application.Services.Interfaces;
using BeYou.Domain.Models;

namespace BeYou.Application.Services.Implementations;

public class ServiceBranchScheduleBlock(ICoreService<BranchScheduleBlock> coreService,
                                            IValidator<BranchScheduleBlock> blockValidator, IMapper mapper) : IServiceBranchScheduleBlock
{
    private readonly string[] BranchScheduleBlockWithBranchSchedule = ["BranchSchedule"];
    /// <inheritdoc />
    public async Task<ResponseBranchScheduleBlockDto> CreateBranchScheduleBlockAsync(RequestBranchScheduleBlockDto branchScheduleBlock)
    {
        var block = await ValidateBranchScheduleBlock(branchScheduleBlock);

        var result = await coreService.UnitOfWork.Repository<BranchScheduleBlock>().AddAsync(block);
        await coreService.UnitOfWork.SaveChangesAsync();

        if (result == null) throw new NotFoundException("Horario bloqueo no se ha creado.");

        return mapper.Map<ResponseBranchScheduleBlockDto>(result);
    }

    /// <inheritdoc />
    public async Task<bool> CreateBranchScheduleBlockAsync(long branchScheduleId, IEnumerable<RequestBranchScheduleBlockDto> branchScheduleBlocks)
    {
        var blocksGuardar = await ValidateBranchScheduleBlock(branchScheduleId, branchScheduleBlocks);

        await coreService.UnitOfWork.Repository<BranchScheduleBlock>().AddRangeAsync(blocksGuardar.ToList());
        await coreService.UnitOfWork.SaveChangesAsync();

        return true;
    }

    /// <inheritdoc />
    public async Task<ResponseBranchScheduleBlockDto> FindByIdAsync(long id)
    {
        var spec = new BaseSpecification<BranchScheduleBlock>(x => x.Id == id);
        var block = await coreService.UnitOfWork.Repository<BranchScheduleBlock>().FirstOrDefaultAsync(spec);
        if (block == null) throw new NotFoundException("Horario bloqueo no encontrado.");

        return mapper.Map<ResponseBranchScheduleBlockDto>(block);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseBranchScheduleBlockDto>> ListAllByBranchScheduleAsync(long branchScheduleId)
    {
        var spec = new BaseSpecification<BranchScheduleBlock>(x => x.BranchScheduleId == branchScheduleId);
        var blocks = await coreService.UnitOfWork.Repository<BranchScheduleBlock>().ListAsync(spec);

        return mapper.Map<ICollection<ResponseBranchScheduleBlockDto>>(blocks);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseBranchScheduleBlockDto>> ListAllByBranchAsync(long branchId)
    {
        var spec = new BaseSpecification<BranchScheduleBlock>(x => x.BranchSchedule.BranchId == branchId);
        var blocks = await coreService.UnitOfWork.Repository<BranchScheduleBlock>().ListAsync(spec, BranchScheduleBlockWithBranchSchedule);

        return mapper.Map<ICollection<ResponseBranchScheduleBlockDto>>(blocks);
    }

    /// <inheritdoc />
    public async Task<ResponseBranchScheduleBlockDto> UpdateBranchScheduleBlockAsync(long id, RequestBranchScheduleBlockDto branchScheduleBlock)
    {
        if (!await coreService.UnitOfWork.Repository<BranchScheduleBlock>().ExistsAsync(id)) throw new NotFoundException("Horario bloqueo no encontrado.");

        var block = await ValidateBranchScheduleBlock(branchScheduleBlock);
        block.Id = id;
        coreService.UnitOfWork.Repository<BranchScheduleBlock>().Update(block);

        await coreService.UnitOfWork.SaveChangesAsync();

        return await FindByIdAsync(id);
    }

    /// <inheritdoc />
    public async Task<bool> DeleteBranchScheduleBlockAsync(long id)
    {
        if (!await coreService.UnitOfWork.Repository<BranchScheduleBlock>().ExistsAsync(id)) throw new NotFoundException("Horario bloqueo no encontrado.");

        var spec = new BaseSpecification<BranchScheduleBlock>(x => x.Id == id);
        var block = await coreService.UnitOfWork.Repository<BranchScheduleBlock>().FirstOrDefaultAsync(spec);
        block!.Active = false;

        coreService.UnitOfWork.Repository<BranchScheduleBlock>().Update(block);

        await coreService.UnitOfWork.SaveChangesAsync();

        return true;
    }

    /// <inheritdoc />
    private async Task<BranchScheduleBlock> ValidateBranchScheduleBlock(RequestBranchScheduleBlockDto blockDTO)
    {
        var block = mapper.Map<BranchScheduleBlock>(blockDTO);
        await blockValidator.ValidateAndThrowAsync(block);
        return block;
    }

    /// <summary>
    /// Validate branch schedule's blocks
    /// </summary>
    /// <param name="branchScheduleId">Branch schedule id that receive blocks</param>
    /// <param name="blocksDto">List of branch schedule's blocks request model will be validated</param>
    /// <returns>IEnumerable of BranchScheduleBlock</returns>
    private async Task<IEnumerable<BranchScheduleBlock>> ValidateBranchScheduleBlock(long branchScheduleId, IEnumerable<RequestBranchScheduleBlockDto> branchScheduleBlocks)
    {
        var blocks = mapper.Map<List<BranchScheduleBlock>>(branchScheduleBlocks);
        foreach (var item in blocks)
        {
            item.Id = 0;
            item.BranchScheduleId = branchScheduleId;
            await blockValidator.ValidateAndThrowAsync(item);
        }
        return blocks;
    }
}
