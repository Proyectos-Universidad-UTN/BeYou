using AutoMapper;
using BeYou.Application.Dtos.Request;
using BeYou.Application.Services.Interfaces.Authorization;
using BeYou.Domain.Core.Models;

namespace BeYou.Infraestructure.Mappings.ValueResolvers;

public class CurrentUserIdResolverModify(IServiceUserContext serviceUserContext) : IValueResolver<RequestBaseDto, BaseEntity, string?>
{
    public string? Resolve(RequestBaseDto source, BaseEntity destination, string? destMember, ResolutionContext context) =>
        serviceUserContext.UserId!;
}