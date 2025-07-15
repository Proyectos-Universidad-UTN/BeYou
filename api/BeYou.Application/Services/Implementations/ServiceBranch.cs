using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using BeYou.Domain.Exceptions;
using BeYou.Application.Dtos.Request;
using BeYou.Application.Dtos.Response;
using BeYou.Domain.Core.Specifications;
using BeYou.Application.Core.Interfaces;
using BeYou.Application.Services.Interfaces;
using BeYou.Application.Services.Interfaces.Authorization;
using BeYou.Domain.Models;

namespace BeYou.Application.Services.Implementations;

public class ServiceBranch(ICoreService<Branch> coreService, IMapper mapper,
                            IValidator<Branch> branchValidator,
                            IServiceUserAuthorization serviceUserAuthorization) : IServiceBranch
{
    private readonly string[] BranchWithAddress = ["District", "Canton", "Province"];
    private readonly string[] BranchDefaults = [
        "District",
        "District.Canton",
        "District.Canton.Province",
        "BranchSchedules",
        "BranchSchedules.Schedule",
        "BranchSchedules.BranchScheduleBlocks",
    ];

    /// <inheritdoc />
    public async Task<ResponseBranchDto> CreateBranchAsync(RequestBranchDto branchDTO)
    {
        var branch = await ValidateBranch(branchDTO);

        var result = await coreService.UnitOfWork.Repository<Branch>().AddAsync(branch);
        await coreService.UnitOfWork.SaveChangesAsync();

        if (result == null) throw new NotFoundException("Sucursal no se ha creado.");

        return mapper.Map<ResponseBranchDto>(result);
    }

    /// <inheritdoc />
    public async Task<ResponseBranchDto> UpdateBranchAsync(long id, RequestBranchDto branchDTO)
    {
        if (!await coreService.UnitOfWork.Repository<Branch>().ExistsAsync(id)) throw new NotFoundException("Sucursal no encontrada.");

        var branch = await ValidateBranch(branchDTO);
        branch.Id = id;
        branch.Active = true;

        coreService.UnitOfWork.Repository<Branch>().Update(branch);
        await coreService.UnitOfWork.SaveChangesAsync();

        return await FindByIdAsync(id);
    }

    /// <inheritdoc />
    public async Task<ResponseBranchDto> FindByIdAsync(long id)
    {
        var spec = new BaseSpecification<Branch>(x => x.Id == id);
        var branch = await coreService.UnitOfWork.Repository<Branch>().FirstOrDefaultAsync(spec, BranchDefaults);
        if (branch == null) throw new NotFoundException("Sucursal no encontrada.");

        return mapper.Map<ResponseBranchDto>(branch);
    }

    /// <inheritdoc />
    public async Task<bool> ExistsBranchAsync(long id)
    {
        return await coreService.UnitOfWork.Repository<Branch>().ExistsAsync(id);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseBranchDto>> ListAllAsync()
    {
        var list = await coreService.UnitOfWork.Repository<Branch>().ListAllAsync();

        return mapper.Map<ICollection<ResponseBranchDto>>(list);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseBranchDto>> ListAllByRoleAsync()
    {
        var user = await serviceUserAuthorization.GetLoggedUser();

        var existingBranches = await (from a in coreService.UnitOfWork.Repository<UserBranch>().AsQueryable()
                                      join b in coreService.UnitOfWork.Repository<User>().AsQueryable() on a.BranchId equals b.Id
                                      join c in coreService.UnitOfWork.Repository<Role>().AsQueryable() on b.RoleId equals c.Id
                                      where c.Description == user.Role.Description
                                      select a.BranchId).Distinct().ToListAsync();

        existingBranches = existingBranches ?? new List<long>();

        var query = from a in coreService.UnitOfWork.Repository<Branch>().AsQueryable()
                    where existingBranches.Contains(a.Id)
                    select a;

        var branches = await coreService.UnitOfWork.Repository<Branch>().ListAsync(query, BranchWithAddress);

        return mapper.Map<ICollection<ResponseBranchDto>>(branches);
    }

    /// <inheritdoc />
    public async Task<bool> DeleteBranchAsync(long id)
    {
        if (!await coreService.UnitOfWork.Repository<Branch>().ExistsAsync(id)) throw new NotFoundException("Sucursal no encontrada.");

        var spec = new BaseSpecification<Branch>(x => x.Id == id);
        var branch = await coreService.UnitOfWork.Repository<Branch>().FirstOrDefaultAsync(spec);
        branch!.Active = false;

        coreService.UnitOfWork.Repository<Branch>().Update(branch);
        return await coreService.UnitOfWork.SaveChangesAsync() != 0;
    }

    private async Task<Branch> ValidateBranch(RequestBranchDto branchDTO)
    {
        var branch = mapper.Map<Branch>(branchDTO);
        await branchValidator.ValidateAndThrowAsync(branch);

        return branch;
    }
}
